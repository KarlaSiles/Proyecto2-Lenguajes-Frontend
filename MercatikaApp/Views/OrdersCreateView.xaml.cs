using MercatikaApp.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MercatikaApp.Views
{
    /// <summary>
    /// Lógica de interacción para OrdersUcView.xaml
    /// </summary>
    public partial class OrdersCreateView : UserControl
    {
        public OrdersCreateView()
        {
            InitializeComponent();
        }

        private async void GuardarOrden_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is OrderViewModel vm)
            {
                bool success = await vm.CreateOrderAsync();
                if (success)
                {
                    MessageBox.Show("Orden guardada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo guardar la orden.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
