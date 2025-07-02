using MercatikaApp.Models;
using MercatikaApp.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MercatikaApp.ViewModel
{
    public class OrderViewModel : INotifyPropertyChanged
    {
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

        public OrderViewModel()
        {
            LoadOrdersCommand = new RelayCommand(async () => await LoadOrdersAsync());
            BackToListCommand = new RelayCommand(() =>
            {
                IsDetailMode = false;
                IsCreateMode = false;
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
            });
            CreateOrderCommand = new RelayCommand(async () => await CreateOrderAsync());


            _ = LoadOrdersAsync();
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
                Console.WriteLine("Error loading orders: " + ex.Message);
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
                OrderDetails.Clear();
                var details = await _orderApiService.GetOrderDetailsByOrderIdAsync(orderId);
                OrderDetails = new ObservableCollection<OrderDetail>(details);
                OnPropertyChanged(nameof(OrderDetails));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading details: " + ex.Message);
            }
        }

        public async Task<bool> CreateOrderAsync()
        {

            if (NewOrder.ClientId <= 0 || NewOrder.EmployeeId <= 0 ||
                string.IsNullOrWhiteSpace(NewOrder.AddressTrip) ||
                NewOrder.Details == null || NewOrder.Details.Count == 0)
            {
                MessageBox.Show("Complete all required fields and add at least one detail.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            foreach (var detail in NewOrder.Details)
            {
                if (detail.ProductDetailId <= 0 || detail.Amount <= 0)
                {
                    MessageBox.Show("All details must have valid product and quantity.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }

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
                MessageBox.Show("Error creating order: " + ex.Message);
            }

            return false;
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
