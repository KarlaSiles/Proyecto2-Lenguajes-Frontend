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
using MercatikaApp.ViewModel;

namespace MercatikaApp.Views
{
    /// <summary>
    /// Lógica de interacción para ClientsUcView.xaml
    /// </summary>
    public partial class ClientsUcView : UserControl
    {
        public ClientsUcView()
        {
            InitializeComponent();
            DataContext = new ClientViewModel(); 
            Loaded += async (s, e) => await LoadData();
        }
        private async Task LoadData()
        {
            if (DataContext is ClientViewModel viewModel)
            {
                await viewModel.LoadClientsAsync();
            }
        }
    }
}
