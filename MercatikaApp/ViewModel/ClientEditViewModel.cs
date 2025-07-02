using MercatikaApp.Helpers;
using MercatikaApp.Models;
using MercatikaApp.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MercatikaApp.ViewModel
{
    public class ClientEditViewModel : BaseViewModel
    {
        private readonly ClientApiService _clientService = new ClientApiService();
        private Client _currentClient;

        public string Title => CurrentClient.ClientId == 0 ? "Add New Client" : $"Edit Client: {CurrentClient.CompanyName}";

        public Client CurrentClient
        {
            get => _currentClient;
            set
            {
                _currentClient = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand => new AsyncRelayCommand(SaveClientAsync, CanSave);

        public ClientEditViewModel(Client client)
        {
            CurrentClient = client ?? new Client { ClientId = 0 };
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(CurrentClient.CompanyName) &&
                   !string.IsNullOrWhiteSpace(CurrentClient.ContractName) &&
                   !string.IsNullOrWhiteSpace(CurrentClient.ContractLastname) &&
                   !string.IsNullOrWhiteSpace(CurrentClient.Address) &&
                   !string.IsNullOrWhiteSpace(CurrentClient.City) &&
                   !string.IsNullOrWhiteSpace(CurrentClient.Province) &&
                   CurrentClient.ZipCode > 0 &&
                   !string.IsNullOrWhiteSpace(CurrentClient.Country) &&
                   CurrentClient.Phone > 0;
        }

        private async Task SaveClientAsync()
        {
            try
            {
                if (CurrentClient.ClientId == 0)
                {
                    int newId = await _clientService.CreateClient(CurrentClient);
                    MessageBox.Show($"Client successfully created with ID: {newId}", "Éxito",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                    CloseWindow(true);
                }
                else
                {
                    bool success = await _clientService.UpdateClient(CurrentClient);
                    if (success)
                    {
                        MessageBox.Show("Client successfully updated", "Éxito",
                                      MessageBoxButton.OK, MessageBoxImage.Information);
                        CloseWindow(true);
                    }
                    else
                    {
                        MessageBox.Show("The client could not be updated.", "Error",
                                      MessageBoxButton.OK, MessageBoxImage.Error);
                    }
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