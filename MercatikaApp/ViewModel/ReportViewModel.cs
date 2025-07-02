using MercatikaApp.Models;
using MercatikaApp.Services;
using MercatikaApp.Views;
using Microsoft.Win32;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using IContainer = QuestPDF.Infrastructure.IContainer;
using System.Diagnostics;
using System.IO;

namespace MercatikaApp.ViewModel
{
    public class ReportViewModel : INotifyPropertyChanged
    {

        private List<int> _originalDetailIds = new();  // IDs ya existentes al cargar

        private readonly ReportApiService _orderApiService = new();
        private readonly ProductService _productService = new ProductService();

        private ObservableCollection<Order> _orders = new();
        public ICollectionView OrdersView { get; }
        public ObservableCollection<OrderDetail> OrderDetails { get; set; } = new();

        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                if (_selectedOrder != value)
                {
                    _selectedOrder = value;
                    OnPropertyChanged();
                    IsDetailMode = _selectedOrder != null;
                    _ = LoadOrderDetailsAsync(_selectedOrder?.OrderId ?? 0);
                }
            }
        }
        private string _filterText = string.Empty;
        public string FilterText
        {
            get => _filterText;
            set
            {
                if (_filterText != value)
                {
                    _filterText = value;
                    OnPropertyChanged();
                    OrdersView.Refresh();    // refresca el filtro al cambiar el texto
                }
            }
        }

        // --- Filtros ---
        private string _filterClientId = "";
        public string FilterClientId
        {
            get => _filterClientId;
            set { _filterClientId = value; OnPropertyChanged(); OrdersView.Refresh(); }
        }

        private string _filterProvince = "";
        public string FilterProvince
        {
            get => _filterProvince;
            set { _filterProvince = value; OnPropertyChanged(); OrdersView.Refresh(); }
        }

        private DateTime? _filterDateFrom;
        public DateTime? FilterDateFrom
        {
            get => _filterDateFrom;
            set { _filterDateFrom = value; OnPropertyChanged(); OrdersView.Refresh(); }
        }

        private DateTime? _filterDateTo;
        public DateTime? FilterDateTo
        {
            get => _filterDateTo;
            set { _filterDateTo = value; OnPropertyChanged(); OrdersView.Refresh(); }
        }

        private bool FilterOrders(object obj)
        {
            if (obj is not Order o) return false;

            // Filtro global
            if (!string.IsNullOrWhiteSpace(FilterText))
            {
                var txt = FilterText.Trim().ToLower();
                if (!(o.OrderId.ToString().Contains(txt)
                      || o.ClientId.ToString().Contains(txt)
                      || (o.ProvinceTrip?.ToLower().Contains(txt) == true)))
                    return false;
            }

            // Filtro por ClientId (si hay texto)
            if (!string.IsNullOrWhiteSpace(FilterClientId))
            {
                if (!int.TryParse(FilterClientId, out var cid) || o.ClientId != cid)
                    return false;
            }

            // Filtro por Provincia (contains, insensible a mayúsculas)
            if (!string.IsNullOrWhiteSpace(FilterProvince))
            {
                if (o.ProvinceTrip == null ||
                    !o.ProvinceTrip.ToLower().Contains(FilterProvince.Trim().ToLower()))
                    return false;
            }

            // Filtro por rango de fecha de orden
            if (FilterDateFrom.HasValue && o.OrderDate < FilterDateFrom.Value.Date) return false;
            if (FilterDateTo.HasValue && o.OrderDate > FilterDateTo.Value.Date) return false;

            return true;
        }

        private Order _newOrder = new();
        public Order NewOrder
        {
            get => _newOrder;
            set { _newOrder = value; OnPropertyChanged(); }
        }

        private bool _isDetailMode;
        public bool IsDetailMode
        {
            get => _isDetailMode;
            set { _isDetailMode = value; OnPropertyChanged(); }
        }

        private bool _isCreateMode;
        public bool IsCreateMode
        {
            get => _isCreateMode;
            set { _isCreateMode = value; OnPropertyChanged(); }
        }

        private bool _isEditMode;
        public bool IsEditMode
        {
            get => _isEditMode;
            set { _isEditMode = value; OnPropertyChanged(); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public ICommand LoadOrdersCommand { get; }
        public ICommand BackToListCommand { get; }
        public ICommand NewOrderCommand { get; }
        public ICommand CreateOrderCommand { get; }
        public ICommand AddProductToOrderCommand { get; }
        public ICommand EditOrderCommand { get; }
        public ICommand UpdateOrderCommand { get; }
        public ICommand SelectClientCommand { get; }
        public ICommand AddDetailToExistingOrderCommand { get; }
        public ICommand RemoveDetailFromExistingOrderCommand { get; }
        public ICommand SavePDFCommand => new RelayCommand(async () => await SavePdfAsync());


        public ReportViewModel()
        {
            OrdersView = CollectionViewSource.GetDefaultView(_orders);
            OrdersView.Filter = FilterOrders;
            AddDetailToExistingOrderCommand = new RelayCommand(() => AddOrderDetailToSelectedOrder());
            RemoveDetailFromExistingOrderCommand = new RelayCommand(() => RemoveSelectedOrderDetail());

            SelectClientCommand = new RelayCommand(OpenClientSelectionWindow);
            LoadOrdersCommand = new RelayCommand(async () => await LoadOrdersAsync());
            BackToListCommand = new RelayCommand(() =>
            {
                IsDetailMode = false;
                IsCreateMode = false;
                IsEditMode = false;
                NewOrder = new();
            });

            NewOrderCommand = new RelayCommand(() =>
            {
                NewOrder = new Order
                {
                    OrderDate = DateTime.Now,
                    DateTrip = DateTime.Now,
                    Details = new ObservableCollection<OrderDetail>()
                };
                IsCreateMode = true;
                IsDetailMode = false;
                IsEditMode = false;
            });

            EditOrderCommand = new RelayCommand(() => EnterEditMode());
            UpdateOrderCommand = new RelayCommand(async () => await UpdateOrderAsync());
            CreateOrderCommand = new RelayCommand(async () => await CreateOrderAsync());
            AddProductToOrderCommand = new RelayCommand(() => OpenProductSelectionWindow());

            _ = LoadOrdersAsync();
        }


        private async Task SavePdfAsync()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            if (SelectedOrder == null)
            {
                MessageBox.Show("Primero selecciona una orden.", "Atención",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dlg = new SaveFileDialog
            {
                Filter = "PDF document (*.pdf)|*.pdf",
                FileName = $"Orden_{SelectedOrder.OrderId}.pdf"
            };
            if (dlg.ShowDialog() != true)
                return;

            var productService = new ProductService();
            foreach (var det in OrderDetails)
            {
                var fullDetail = await productService.GetProductDetailAsync(det.ProductDetailId);
                if (fullDetail?.Product != null)
                    det.ProductName = fullDetail.Product.ProductName;
                else
                    det.ProductName = "[Nombre no disponible]";
            }

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);

                    page.Header()
                        .Text($"Factura de la Orden #{SelectedOrder.OrderId}")
                        .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                    page.Content().Column(col =>
                    {
                        // Datos generales
                        col.Item().Text($"Fecha Orden: {SelectedOrder.OrderDate:yyyy-MM-dd}");
                        col.Item().Text($"Cliente ID: {SelectedOrder.ClientId}");
                        col.Item().Text($"Empleado ID: {SelectedOrder.EmployeeId}");
                        col.Item().Text($"Dirección: {SelectedOrder.AddressTrip}, {SelectedOrder.ProvinceTrip}, {SelectedOrder.CountryTrip}");
                        col.Item().Text($"Teléfono: {SelectedOrder.PhoneTrip}");
                        col.Item().Text($"Fecha Viaje: {SelectedOrder.DateTrip:yyyy-MM-dd}");
                        col.Item().Text($"Estado: {SelectedOrder.Estado}");

                        col.Item().PaddingVertical(10).LineHorizontal(1);

                        // Tabla de detalles
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.RelativeColumn();
                                cols.ConstantColumn(80);
                                cols.ConstantColumn(80);
                                cols.ConstantColumn(80);
                            });

                            // Cabecera
                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Producto");
                                header.Cell().Element(CellStyle).Text("Detalle ID");
                                header.Cell().Element(CellStyle).Text("Cantidad");
                                header.Cell().Element(CellStyle).Text("Precio Unidad");
                            });

                            // Filas
                            foreach (var det in OrderDetails)
                            {
                                table.Cell().Element(CellStyle).Text(det.ProductName);
                                table.Cell().Element(CellStyle).Text(det.ProductDetailId.ToString());
                                table.Cell().Element(CellStyle).Text(det.Amount.ToString());
                                table.Cell().Element(CellStyle).Text($"{det.LinePrice:C}");
                            }

                            static IContainer CellStyle(IContainer c) =>
                                c.Border(1)
                                 .BorderColor(Colors.Grey.Lighten2)
                                 .Padding(4);
                        });

                        // Total
                        var total = OrderDetails.Sum(d => d.LinePrice * d.Amount);
                        col.Item().AlignRight().PaddingTop(10)
                           .Text($"TOTAL: {total:C}")
                           .SemiBold();
                    });

                    // Footer
                    page.Footer()
                        .AlignCenter()
                        .Text(t =>
                        {
                            t.Span("Generado el ").FontSize(10);
                            t.Span($"{DateTime.Now:yyyy-MM-dd HH:mm}").SemiBold().FontSize(10);
                        });
                });
            });

            document.GeneratePdf(dlg.FileName);

            MessageBox.Show($"PDF guardado en:\n{dlg.FileName}", "Éxito",
                            MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async void RemoveSelectedOrderDetail()
        {
            if (SelectedOrder == null || SelectedOrderDetail == null)
                return;

            var confirm = MessageBox.Show("¿Deseas eliminar este producto de la orden?", "Confirmar eliminación",
                                          MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm != MessageBoxResult.Yes)
                return;

            try
            {
                var success = await _orderApiService.DeleteOrderDetailAsync(
                    SelectedOrder.OrderId,
                    SelectedOrderDetail.ProductDetailId);

                if (success)
                {
                    OrderDetails.Remove(SelectedOrderDetail);
                    OnPropertyChanged(nameof(OrderDetails));
                    MessageBox.Show("Detalle eliminado exitosamente.", "Éxito",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el detalle del pedido.", "Error",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error eliminando detalle: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AddOrderDetailToSelectedOrder()
        {
            if (SelectedOrder == null) return;

            var window = new ProductSelectionWindow();
            if (window.ShowDialog() == true)
            {
                var selected = window.SelectedProduct;
                int quantity = ((ProductViewModel)window.DataContext).SelectedQuantity;

                if (selected == null || selected.ProductId <= 0) return;

                var productService = new ProductService();
                var fullProduct = await productService.GetByIdAsync(selected.ProductId);

                if (fullProduct?.ProductDetails?.Any() == true)
                {
                    var detail = fullProduct.ProductDetails.First();
                    var newDetail = new OrderDetail
                    {
                        ProductDetailId = detail.ProductDetailId,
                        Amount = quantity,
                        ProductName = fullProduct.ProductName
                    };

                    OrderDetails.Add(newDetail);
                    OnPropertyChanged(nameof(OrderDetails));
                }
            }
        }

        private void OpenClientSelectionWindow()
        {
            var window = new ClientSelectionWindow();
            if (window.ShowDialog() == true)
            {
                var selected = window.SelectedClient;
                if (selected != null)
                {
                    NewOrder.ClientId = selected.ClientId;
                    OnPropertyChanged(nameof(NewOrder));
                }
            }
        }

        private void EnterEditMode()
        {
            if (SelectedOrder == null) return;

            NewOrder = new Order
            {
                OrderId = SelectedOrder.OrderId,
                ClientId = SelectedOrder.ClientId,
                EmployeeId = SelectedOrder.EmployeeId,
                AddressTrip = SelectedOrder.AddressTrip,
                ProvinceTrip = SelectedOrder.ProvinceTrip,
                CountryTrip = SelectedOrder.CountryTrip,
                PhoneTrip = SelectedOrder.PhoneTrip,
                OrderDate = SelectedOrder.OrderDate,
                DateTrip = SelectedOrder.DateTrip,
                Details = new ObservableCollection<OrderDetail>(OrderDetails)
            };

            IsCreateMode = true;
            IsDetailMode = false;
            IsEditMode = true;
        }

        private OrderDetail _selectedOrderDetail;
        public OrderDetail SelectedOrderDetail
        {
            get => _selectedOrderDetail;
            set { _selectedOrderDetail = value; OnPropertyChanged(); }
        }

        private async Task LoadOrdersAsync()
        {
            IsLoading = true;
            try
            {
                var list = await _orderApiService.GetAllOrdersAsync();
                _orders.Clear();
                foreach (var o in list)
                    _orders.Add(o);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task LoadOrderDetailsAsync(int orderId)
        {
            try
            {
                var details = await _orderApiService.GetOrderDetailsByOrderIdAsync(orderId);

                foreach (var d in details)
                {
                    var fullDetail = await _productService.GetProductDetailAsync(d.ProductDetailId);
                    if (fullDetail?.Product != null)
                    {
                        d.Product = fullDetail.Product;
                        d.ProductName = fullDetail.Product.ProductName;
                    }
                }

                OrderDetails = new ObservableCollection<OrderDetail>(details);
                OnPropertyChanged(nameof(OrderDetails));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar detalles con producto: {ex.Message}");
                MessageBox.Show("No se pudieron cargar los detalles de la orden.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void OpenProductSelectionWindow()
        {
            var window = new ProductSelectionWindow();

            if (window.ShowDialog() == true)
            {
                var selected = window.SelectedProduct;
                int quantity = ((ProductViewModel)window.DataContext).SelectedQuantity;

                if (selected == null || selected.ProductId <= 0)
                {
                    MessageBox.Show("Selecciona un producto válido.");
                    return;
                }

                var productService = new ProductService();
                var fullProduct = await productService.GetByIdAsync(selected.ProductId);

                if (fullProduct?.ProductDetails?.Any() == true)
                {
                    var detail = fullProduct.ProductDetails.First();

                    var newDetail = new OrderDetail
                    {
                        ProductDetailId = detail.ProductDetailId,
                        Amount = quantity,
                        ProductName = fullProduct.ProductName ?? "Producto sin nombre",
                    };

                    if (IsEditMode)
                    {
                        NewOrder.Details.Add(newDetail);
                        OnPropertyChanged(nameof(NewOrder));
                    }
                    else
                    {
                        NewOrder.Details.Add(newDetail);
                        OnPropertyChanged(nameof(NewOrder));
                    }
                }
                else
                {
                    MessageBox.Show("El producto no tiene detalles disponibles.");
                }
            }
        }

        public async Task<bool> CreateOrderAsync()
        {
            if (!ValidarOrden()) return false;

            try
            {
                var success = await _orderApiService.CreateOrderAsync(NewOrder);
                if (success)
                {
                    await LoadOrdersAsync();
                    BackToListCommand.Execute(null);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creando orden: " + ex.Message);
            }

            return false;
        }

        public async Task<bool> UpdateOrderAsync()
        {

            try
            {
                // Detectar detalles NUEVOS agregados: están en NewOrder.Details pero no en OrderDetails.
                var nuevosDetalles = NewOrder.Details
                    .Where(nd => !OrderDetails.Any(od => od.ProductDetailId == nd.ProductDetailId))
                    .ToList();

                foreach (var detalleNuevo in nuevosDetalles)
                {
                    // Para cada detalle nuevo, asignar el OrderId actual
                    detalleNuevo.OrderId = NewOrder.OrderId;

                    var success = await _orderApiService.CreateOrderDetailAsync(detalleNuevo);
                    if (!success)
                    {
                        MessageBox.Show($"Error agregando detalle del producto {detalleNuevo.ProductName}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }

                // Recargar datos para actualizar UI
                await LoadOrdersAsync();
                await LoadOrderDetailsAsync(NewOrder.OrderId);

                BackToListCommand.Execute(null);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error actualizando detalles de la orden: " + ex.Message);
            }

            return false;
        }


        private bool ValidarOrden()
        {
            if (NewOrder.ClientId <= 0 || NewOrder.EmployeeId <= 0 ||
                string.IsNullOrWhiteSpace(NewOrder.AddressTrip) ||
                NewOrder.Details == null || NewOrder.Details.Count == 0)
            {
                MessageBox.Show("Completa todos los campos obligatorios y añade al menos un detalle.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            foreach (var detail in NewOrder.Details)
            {
                if (detail.ProductDetailId <= 0 || detail.Amount <= 0)
                {
                    MessageBox.Show("Todos los detalles deben tener producto y cantidad válida.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }

            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private class RelayCommand : ICommand
        {
            private readonly Func<Task>? _asyncExecute;
            private readonly Action? _syncExecute;
            private readonly Func<bool>? _canExecute;

            public RelayCommand(Func<Task> asyncExecute, Func<bool>? canExecute = null)
            {
                _asyncExecute = asyncExecute;
                _canExecute = canExecute;
            }

            public RelayCommand(Action syncExecute, Func<bool>? canExecute = null)
            {
                _syncExecute = syncExecute;
                _canExecute = canExecute;
            }

            public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;

            public async void Execute(object? parameter)
            {
                if (_asyncExecute != null)
                    await _asyncExecute();
                else
                    _syncExecute?.Invoke();
            }

            public event EventHandler? CanExecuteChanged
            {
                add => CommandManager.RequerySuggested += value!;
                remove => CommandManager.RequerySuggested -= value!;
            }
        }
    }
}
