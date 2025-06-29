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

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();

            this.Close();// Cerrar este MainWindow
        }
    }
}