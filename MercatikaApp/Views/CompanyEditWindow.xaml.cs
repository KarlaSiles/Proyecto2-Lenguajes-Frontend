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
using MercatikaApp.Models;
using MercatikaApp.ViewModel;

namespace MercatikaApp.Views
{
    /// <summary>
    /// Lógica de interacción para CompanyEditWindow.xaml
    /// </summary>
    public partial class CompanyEditWindow : Window
    {
        public CompanyEditWindow(Company company)
        {
            InitializeComponent();
            DataContext = new CompanyEditViewModel(company);
        }
    }
}