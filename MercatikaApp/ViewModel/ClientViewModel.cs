using MercatikaApp.Helpers;
using MercatikaApp.Models;
using MercatikaApp.Services;
using MercatikaApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MercatikaApp.ViewModel
{
    public class ClientViewModel : BaseViewModel
    {
        private readonly ClientApiService _clientService = new ClientApiService();
        private Client _selectedClient;
        private ObservableCollection<Client> _allClients = new ObservableCollection<Client>();
        private bool _isSearching = false;
        private string _searchTerm;

        public ObservableCollection<Client> FilteredClients { get; } = new ObservableCollection<Client>();

        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsClientSelected));
            }
        }

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
                OnPropertyChanged();
            }
        }

        public bool IsClientSelected => SelectedClient != null;

        public bool IsSearching
        {
            get => _isSearching;
            set { _isSearching = value; OnPropertyChanged(); }
        }


        public ICommand LoadClientsCommand => new AsyncRelayCommand(LoadClientsAsync);
        public ICommand SearchCommand => new AsyncRelayCommand(SearchClientsAsync);
        public ICommand DeleteClientCommand => new AsyncRelayCommand(DeleteClientAsync);
        public ICommand OpenAddClientWindowCommand => new RelayCommand(OpenAddClientWindow);
        public ICommand OpenEditClientWindowCommand => new RelayCommand(OpenEditClientWindow);
        public ICommand ClearSearchCommand => new RelayCommand(ClearSearch);

        public ClientViewModel()
        {
            _ = LoadClientsAsync();
        }

        public async Task LoadClientsAsync()
        {
            try
            {
                IsSearching = true;
                var clients = await _clientService.GetAllClients();
                _allClients.Clear();
                FilteredClients.Clear();

                foreach (var client in clients.OrderBy(c => c.CompanyName))
                {
                    _allClients.Add(client);
                    FilteredClients.Add(client);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading clients: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsSearching = false;
            }
        }

        private async Task SearchClientsAsync()
        {
            try
            {
                IsSearching = true;
                FilteredClients.Clear();

                if (string.IsNullOrWhiteSpace(SearchTerm))
                {
                    await LoadClientsAsync();
                    return;
                }

                List<Client> results = new List<Client>();

              
                if (int.TryParse(SearchTerm, out int id))
                {
                    var client = await _clientService.GetClientById(id);
                    if (client != null) results.Add(client);
                }

          
                var companyTask = _clientService.GetClientsByCompanyName(SearchTerm);
                var nameTask = _clientService.GetClientsByName(SearchTerm);
                var lastnameTask = _clientService.GetClientsByLastname(SearchTerm);

                await Task.WhenAll(companyTask, nameTask, lastnameTask);

                results.AddRange(companyTask.Result);
                results.AddRange(nameTask.Result);
                results.AddRange(lastnameTask.Result);

           
                var distinctResults = results
                    .DistinctBy(c => c.ClientId)
                    .OrderBy(c => c.CompanyName);

                foreach (var client in distinctResults)
                {
                    FilteredClients.Add(client);
                }

                if (FilteredClients.Count == 0)
                {
                    MessageBox.Show("No results found", "Búsqueda",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Search error: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsSearching = false;
            }
        }

        private void ClearSearch()
        {
            SearchTerm = string.Empty;
            _ = LoadClientsAsync();
        }

        private async Task DeleteClientAsync()
        {
            if (SelectedClient == null) return;

            try
            {
                var confirm = MessageBox.Show($"¿Delete the client {SelectedClient.CompanyName}?",
                                            "Confirm",
                                            MessageBoxButton.YesNo,
                                            MessageBoxImage.Question);

                if (confirm != MessageBoxResult.Yes) return;

                bool success = await _clientService.DeleteClient(SelectedClient.ClientId);

                if (success)
                {
                    MessageBox.Show("Successfully deleted client", "Éxito",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                    _allClients.Remove(SelectedClient);
                    FilteredClients.Remove(SelectedClient);
                    SelectedClient = null;
                }
                else
                {
                    MessageBox.Show("The client could not be deleted", "Error",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Delete error: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenAddClientWindow()
        {
            try
            {
                var dialog = new ClientEditWindow(new Client())
                {
                    Owner = Application.Current.MainWindow
                };
                dialog.ShowDialog();
                _ = LoadClientsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening window: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenEditClientWindow()
        {
            if (SelectedClient == null) return;

            var clientCopy = new Client
            {
                ClientId = SelectedClient.ClientId,
                CompanyName = SelectedClient.CompanyName,
                ContractName = SelectedClient.ContractName,
                ContractLastname = SelectedClient.ContractLastname,
                ContractPosition = SelectedClient.ContractPosition,
                Address = SelectedClient.Address,
                City = SelectedClient.City,
                Province = SelectedClient.Province,
                ZipCode = SelectedClient.ZipCode,
                Country = SelectedClient.Country,
                Phone = SelectedClient.Phone,
                FaxNumber = SelectedClient.FaxNumber
            };

            var dialog = new ClientEditWindow(clientCopy)
            {
                Owner = Application.Current.MainWindow
            };

            if (dialog.ShowDialog() == true)
            {
                _ = LoadClientsAsync();
            }
        }
    }
}