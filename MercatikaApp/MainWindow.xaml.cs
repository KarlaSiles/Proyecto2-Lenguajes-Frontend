using MercatikaApp.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MercatikaApp.ViewModel;

namespace MercatikaApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void AbrirProducto_Click(object sender, RoutedEventArgs e)
        {
            var ventanaProductos = new ProductView();
            ventanaProductos.ShowDialog();
        }
        private void BtnInsertDetail_Click(object sender, RoutedEventArgs e)
        {
            var detailWindow = new InsertProductDetailView();
            detailWindow.Show();
        }

        private void NuevoPedido_Click(object sender, RoutedEventArgs e)
        {
            var nuevaVista = new OrdersCreateView();
            if (nuevaVista.DataContext is OrderViewModel vm)
            {
                vm.NewOrderCommand.Execute(null); // activa el modo creación
            }

            MainContentFrame.Content = nuevaVista;
        }

        private void Historial_Click(object sender, RoutedEventArgs e)
        {
            var historialVista = new OrdersListView();
            if (historialVista.DataContext is OrderViewModel vm)
            {
                vm.BackToListCommand.Execute(null); // activa el modo lista
            }

            MainContentFrame.Content = historialVista;
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();

            this.Close();// Cerrar este MainWindow
        }
    }
}