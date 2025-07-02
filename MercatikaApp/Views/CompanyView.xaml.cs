using MercatikaApp.ViewModel;
using System.Windows.Controls;

namespace MercatikaApp.Views
{
    public partial class CompanyView : UserControl
    {
        public CompanyView()
        {
            InitializeComponent();
            DataContext = new CompanyViewModel();
        }
    }
}