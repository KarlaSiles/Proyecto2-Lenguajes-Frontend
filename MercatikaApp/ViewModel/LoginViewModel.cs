using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MercatikaApp.Services;
using MercatikaApp.Helpers;

namespace MercatikaApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isBusy;

        private readonly LoginApiService _service;

        public event PropertyChangedEventHandler? PropertyChanged;

        public LoginViewModel()
        {
            _service = new LoginApiService();
            LoginCommand = new RelayCommand(async () => await LoginAsync(), () => !IsBusy);
            CancelCommand = new RelayCommand(Cancel);
        }

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand CancelCommand { get; }

        private async Task LoginAsync()
        {
            ErrorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Ingrese usuario y contraseña.";
                return;
            }

            IsBusy = true;
            var ok = await _service.AuthenticateAsync(Username, Password);
            IsBusy = false;

            if (ok)
            {
                var main = new MainWindow();
                Application.Current.MainWindow = main;
                main.Show();
                CloseLoginWindow();
            }
            else
            {
                ErrorMessage = "Credenciales inválidas.";
            }
        }

        private void Cancel() => Application.Current.Shutdown();

        private void CloseLoginWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is Views.LoginWindow)
                {
                    window.Close();
                    break;
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}