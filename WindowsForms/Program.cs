using API.Clients;
using Domain.Model;

namespace WindowsForms
{
    internal static class Program
    {

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Handler para excepciones de UI no manejadas
            Application.ThreadException += Application_ThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Ejecutar async main
            Task.Run(async () => await MainAsync()).GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var authService = new WindowsFormsAuthService();
            AuthServiceProvider.Register(authService);

            while (true)
            {

                if (!await authService.IsAuthenticatedAsync())
                {
                    var loginForm = new Login();
                    if (loginForm.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                }

                var currentUser = await authService.GetCurrentUserAsync();
                if (currentUser == null)
                {
                    await authService.LogoutAsync();
                    continue;
                }

                try
                {
                    Form menuForm = currentUser.TypeUser switch
                    {
                        UserType.Student => new StudentMenu(),
                        UserType.Teacher => new TeacherMenu(),
                        UserType.Admin => new AdminMenu(),
                        _ => new AdminMenu()
                    };

                    Application.Run(menuForm);
                    break;
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show(ex.Message, "Sesión Expirada",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (e.Exception is UnauthorizedAccessException)
            {
                MessageBox.Show("Su sesión ha expirado. Debe volver a autenticarse.", "Sesión Expirada",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                Application.Restart();
            }
            else
            {
                MessageBox.Show($"Error inesperado: {e.Exception.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}