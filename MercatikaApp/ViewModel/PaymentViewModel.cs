using MercatikaApp.Models;
using MercatikaApp.Services;
using MercatikaApp.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MercatikaApp.Helpers;

namespace MercatikaApp.ViewModel
{
    public class PaymentViewModel : INotifyPropertyChanged
    {
        private readonly PaymentApiService _paymentService = new();

        public ObservableCollection<Payment> Payments { get; set; } = new();
        private Payment _selectedPayment;

        public Payment SelectedPayment
        {
            get => _selectedPayment;
            set { _selectedPayment = value; OnPropertyChanged(); }
        }

        private string _searchId;
        public string SearchId
        {
            get => _searchId;
            set { _searchId = value; OnPropertyChanged(); }
        }

        private string _searchStatus;
        public string SearchStatus
        {
            get => _searchStatus;
            set { _searchStatus = value; OnPropertyChanged(); }
        }

        public ICommand LoadPaymentsCommand { get; }
        public ICommand PaySelectedCommand { get; }
        public ICommand FilterCommand { get; }

        public PaymentViewModel()
        {
            LoadPaymentsCommand = new RelayCommand(async () => await LoadPaymentsAsync());
            PaySelectedCommand = new RelayCommand(async () => await StartPaymentFlowAsync(), () => SelectedPayment != null && SelectedPayment.Estado == "pendiente");
            FilterCommand = new RelayCommand(async () => await LoadPaymentsAsync());

            _ = LoadPaymentsAsync();
        }

        private async Task LoadPaymentsAsync()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            var filtered = payments;

            if (!string.IsNullOrWhiteSpace(SearchId) && int.TryParse(SearchId, out int id))
                filtered = filtered.FindAll(p => p.OrderId == id);

            if (!string.IsNullOrWhiteSpace(SearchStatus))
                filtered = filtered.FindAll(p => p.Estado.Equals(SearchStatus, System.StringComparison.OrdinalIgnoreCase));

            Payments = new ObservableCollection<Payment>(filtered);
            OnPropertyChanged(nameof(Payments));
        }

        private async Task StartPaymentFlowAsync()
        {
            if (SelectedPayment == null)
            {
                MessageBox.Show("Select a payment first.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show("Would you like to proceed with paying this bill now?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                MessageBox.Show("❌Payment has not been completed.");
                return;
            }


            var dialog = new PaymentMethodDialog();
            if (dialog.ShowDialog() == true)
            {
                string method = dialog.SelectedMethod;
                int methodId = (method == "Efectivo") ? 2 : 1;

                string cardInput = null;

                if (method == "Tarjeta")
                {
                    cardInput = Microsoft.VisualBasic.Interaction.InputBox("Enter the card number:", "Pago con Tarjeta", "");
                    if (string.IsNullOrWhiteSpace(cardInput))
                    {
                        MessageBox.Show("❌ No card number entered. Payment has been cancelled.");
                        return;
                    }

                    if (MessageBox.Show($"¿Confirm that this is your number: {cardInput}?", "Confirm Card", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                    {
                        MessageBox.Show("❌ Card number not confirmed. Payment has been cancelled..");
                        return;
                    }
                }

                if (MessageBox.Show("¿Are you sure you want to confirm and complete the payment?", "Confirmar Pago", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    await _paymentService.ConfirmPaymentAsync(SelectedPayment.PaymentId, "pagado", cardInput, methodId);
                    MessageBox.Show("✅ Payment made successfully. Thank you for your purchase.!");
                }
                else
                {
                    MessageBox.Show("❌ Payment has not been completed.");
                }
            }
            else
            {
                MessageBox.Show("❌ No method selected. Payment has been cancelled.");
            }

            await LoadPaymentsAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}

