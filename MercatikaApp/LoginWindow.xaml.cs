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

namespace MercatikaApp
{
    /// <summary>
    /// Lógica de interacción para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password;

            if (Authenticate(username, password))
            {
                Application.Current.MainWindow = new MainWindow();
                Application.Current.MainWindow.Show();

                this.Close(); //cierra la ventana de login
            }
            else
            {
                lblError.Text = "Incorrect username or password.";
            }
        }

        private bool Authenticate(string username, string password)
        {
            //validación
            return username == "admin" && password == "1234";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
