using MercatikaApp.Helpers;
using MercatikaApp.Models;
using MercatikaApp.Services;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MercatikaApp.ViewModel
{
    public class CompanyEditViewModel : BaseViewModel
    {
        private readonly CompanyApiService _companyService = new CompanyApiService();
        private Company _currentCompany;

        public Company CurrentCompany
        {
            get => _currentCompany;
            set
            {
                _currentCompany = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanSave));
            }
        }


        public string Name_company
        {
            get => CurrentCompany?.Name_company;
            set
            {
                if (CurrentCompany != null)
                {
                    CurrentCompany.Name_company = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CanSave));
                }
            }
        }

        public string Address_company
        {
            get => CurrentCompany?.Address_company;
            set
            {
                if (CurrentCompany != null)
                {
                    CurrentCompany.Address_company = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CanSave));
                }
            }
        }


        public bool CanSave => CurrentCompany != null &&
                             !string.IsNullOrWhiteSpace(CurrentCompany.Name_company) &&
                             !string.IsNullOrWhiteSpace(CurrentCompany.Address_company) &&
                             CurrentCompany.Phone_company > 0 &&
                             CurrentCompany.Sale_tax >= 0;

        public ICommand SaveCommand => new RelayCommand(async () => await SaveCompanyAsync(), () => CanSave);

        public CompanyEditViewModel(Company company)
        {
            CurrentCompany = company ?? new Company();
        }

        private async Task SaveCompanyAsync()
        {
            try
            {
                bool success = await _companyService.UpdateCompanyAsync(CurrentCompany);
                if (success)
                {
                    MessageBox.Show("Information saved correctly", "Éxito",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                    CloseWindow(true);
                }
                else
                {
                    MessageBox.Show("The information could not be saved", "Error",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseWindow(bool result)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.DialogResult = result;
                    window.Close();
                    break;
                }
            }
        }
    }
}