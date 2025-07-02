using MercatikaApp.Models;
using MercatikaApp.Services;
using MercatikaApp.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MercatikaApp.ViewModel
{
    public class OrderViewModel : INotifyPropertyChanged
    {

        private List<int> _originalDetailIds = new();  // IDs ya existentes al cargar

        private readonly OrderApiService _orderApiService = new();

        public ObservableCollection<Order> Orders { get; set; } = new();
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

        public OrderViewModel()
        {
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
        public ICommand AddDetailCommand => new RelayCommand(async () =>
        {
            if (SelectedOrder == null) return;

            var window = new ProductSelectionWindow();
            if (window.ShowDialog() != true) return;

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
                    ProductName = fullProduct.ProductName,
                    OrderId = SelectedOrder.OrderId
                };

                OrderDetails.Add(newDetail);
                OnPropertyChanged(nameof(OrderDetails));
            }
        });


        public ICommand SaveNewDetailsCommand => new RelayCommand(async () =>
        {
            if (SelectedOrder == null) return;

            int orderId = SelectedOrder.OrderId;
            var nuevos = OrderDetails
                .Where(d => !_originalDetailIds.Contains(d.ProductDetailId))
                .ToList();

            foreach (var d in nuevos)
            {
                d.OrderId = orderId;
                bool ok = await _orderApiService.CreateOrderDetailAsync(d);
                if (!ok)
                {
                    MessageBox.Show($"Fallo al agregar producto {d.ProductDetailId}");
                    return;
                }
            }

            await LoadOrderDetailsAsync(orderId); // actualiza vista
            MessageBox.Show("Detalles agregados correctamente.");
        });




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
            try
            {
                IsLoading = true;
                var orderList = await _orderApiService.GetAllOrdersAsync();
                Orders = new ObservableCollection<Order>(orderList);
                OnPropertyChanged(nameof(Orders));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar órdenes: " + ex.Message);
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
                OrderDetails = new ObservableCollection<OrderDetail>(details);
                _originalDetailIds = details.Select(d => d.ProductDetailId).ToList();
                OnPropertyChanged(nameof(OrderDetails));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar detalles: " + ex.Message);
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
