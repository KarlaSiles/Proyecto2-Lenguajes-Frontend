using MercatikaApp.ViewModel;
using MercatikaApp.Models;
using System.Windows;
using System.Windows.Input;

namespace MercatikaApp.Views
{
    public partial class ClientSelectionWindow : Window
    {
        public Client SelectedClient => ((ClientViewModel)DataContext).SelectedClient;

        public ClientSelectionWindow()
        {
            InitializeComponent();
            DataContext = new ClientViewModel();
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            ((ClientViewModel)DataContext).SearchCommand.Execute(null);
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedClient != null)
            {
                DialogResult = true;
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedClient != null)
            {
                DialogResult = true;
            }
        }
    }
}
