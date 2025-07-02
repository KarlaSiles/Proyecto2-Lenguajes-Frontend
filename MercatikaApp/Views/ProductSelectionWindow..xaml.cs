using MercatikaApp.Models;
using MercatikaApp.ViewModel;
using System.Windows;

namespace MercatikaApp.Views
{
    public partial class ProductSelectionWindow : Window
    {
        public Product SelectedProduct => ((ProductViewModel)DataContext).SelectedProduct;

        public ProductSelectionWindow()
        {
            InitializeComponent();
            DataContext = new ProductViewModel();
            _ = ((ProductViewModel)DataContext).LoadProductsAsync();
        }
        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProduct != null)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un producto primero.");
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }


        private void SearchBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            ((ProductViewModel)DataContext).SearchCommand.Execute(null);
        }

        private void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (SelectedProduct != null)
                DialogResult = true;
        }
    }
}
