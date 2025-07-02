using MercatikaApp.Helpers;
using MercatikaApp.Models;
using MercatikaApp.Services;
using MercatikaApp.Views;
using System.Windows;
using System.Windows.Input;

namespace MercatikaApp.ViewModel
{
    public class CompanyViewModel : BaseViewModel
    {
        private readonly CompanyApiService _companyService = new CompanyApiService();
        private Company _company;

        public Company Company
        {
            get => _company;
            set
            {
                _company = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadCompanyCommand => new AsyncRelayCommand(LoadCompanyAsync);
        public ICommand OpenEditWindowCommand => new RelayCommand(OpenEditWindow);

        public CompanyViewModel()
        {
            LoadCompanyCommand.Execute(null);
        }

        private async Task LoadCompanyAsync()
        {
            try
            {
                Company = await _companyService.GetCompanyAsync();
                if (Company == null)
                {
                    MessageBox.Show("Company information could not be loaded", "Error",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading company: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenEditWindow()
        {
            if (Company == null) return;

            var editWindow = new CompanyEditWindow(new Company
            {
                Idsetup = Company.Idsetup,
                Name_company = Company.Name_company,
                Address_company = Company.Address_company,
                City_company = Company.City_company,
                State_or_province = Company.State_or_province,
                Zip_code_company = Company.Zip_code_company,
                Country_company = Company.Country_company,
                Phone_company = Company.Phone_company,
                Fax_number_company = Company.Fax_number_company,
                Sale_tax = Company.Sale_tax,
                Payments_terms = Company.Payments_terms,
                Message = Company.Message
            });

            if (editWindow.ShowDialog() == true)
            {
                LoadCompanyCommand.Execute(null);
            }
        }
    }
}