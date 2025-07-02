using System.Windows;

namespace MercatikaApp.Views
{
    public partial class PaymentMethodDialog : Window
    {
        public string SelectedMethod { get; private set; }

        public PaymentMethodDialog()
        {
            InitializeComponent();
        }

        private void Efectivo_Click(object sender, RoutedEventArgs e)
        {
            SelectedMethod = "Efectivo";
            DialogResult = true;
            Close();
        }

        private void Tarjeta_Click(object sender, RoutedEventArgs e)
        {
            SelectedMethod = "Tarjeta";
            DialogResult = true;
            Close();
        }
    }
}
