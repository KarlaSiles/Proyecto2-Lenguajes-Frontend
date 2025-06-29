using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MercatikaApp.Views
{
    /// <summary>
    /// ProductViewModel.xaml
    /// </summary>
    public partial class ProductView : Window
    {
        public ProductView()
        {
            InitializeComponent();
        }
        private void AbrirInsertarProducto_Click(object sender, RoutedEventArgs e)
        {
            var insertWindow = new ProductInsertView();
            insertWindow.ShowDialog(); 
        }

    }
}
