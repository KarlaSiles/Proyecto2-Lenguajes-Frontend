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
                MessageBox.Show("Seleccione un pago primero.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show("¿Desea proceder a pagar esta cuenta ahora?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                MessageBox.Show("❌ El pago no se ha completado.");
                return;
            }

            // Mostrar ventana custom para seleccionar método
            var dialog = new PaymentMethodDialog();
            if (dialog.ShowDialog() == true)
            {
                string method = dialog.SelectedMethod;
                int methodId = (method == "Efectivo") ? 2 : 1;

                string cardInput = null;

                if (method == "Tarjeta")
                {
                    cardInput = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el número de tarjeta:", "Pago con Tarjeta", "");
                    if (string.IsNullOrWhiteSpace(cardInput))
                    {
                        MessageBox.Show("❌ No se ingresó número de tarjeta. El pago se ha cancelado.");
                        return;
                    }

                    if (MessageBox.Show($"¿Confirma que este es su número: {cardInput}?", "Confirmar Tarjeta", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                    {
                        MessageBox.Show("❌ Número de tarjeta no confirmado. El pago se ha cancelado.");
                        return;
                    }
                }

                if (MessageBox.Show("¿Está seguro de confirmar y completar el pago?", "Confirmar Pago", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    await _paymentService.ConfirmPaymentAsync(SelectedPayment.PaymentId, "pagado", cardInput, methodId);
                    MessageBox.Show("✅ Pago realizado con éxito. ¡Gracias por su compra!");
                }
                else
                {
                    MessageBox.Show("❌ El pago no se ha completado.");
                }
            }
            else
            {
                MessageBox.Show("❌ No se seleccionó método. El pago se ha cancelado.");
            }

            await LoadPaymentsAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}

