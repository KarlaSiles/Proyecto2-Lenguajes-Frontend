using System.Configuration;
using System.Data;
using System.Windows;

namespace MercatikaApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // 1) Mostrar la ventana de login de forma modal
            var login = new LoginWindow();
            bool? ok = login.ShowDialog();

            if (ok == true)
            {
                // 2) Si DialogResult == true → abrir MainWindow
                var main = new MainWindow();
                main.Show();
            }
            else
            {
                // Si canceló o falló, cerrar la app
                Shutdown();
            }
        }
    }
}
