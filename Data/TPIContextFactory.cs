using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Data
{
    public class TPIContextFactory : IDesignTimeDbContextFactory<TPIContext>
    {
        public TPIContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TPIContext>();

            // Obtener el directorio de ejecución actual (normalmente el directorio del proyecto Data)
            var currentDirectory = Directory.GetCurrentDirectory();

            // Construir la ruta al archivo appsettings.json del proyecto WebAPI
            // Desde 'Data/', para llegar a 'WebAPI/appsettings.json', la ruta relativa es '../WebAPI/appsettings.json'
            var webApiAppsettingsPath = Path.Combine(currentDirectory, "..", "WebAPI", "appsettings.json");

            // Para depuración, puedes imprimir esta ruta para verificarla:
            Console.WriteLine($"Current Directory: {currentDirectory}");
            Console.WriteLine($"Looking for appsettings.json at: {webApiAppsettingsPath}");


            // Construir la configuración
            var configuration = new ConfigurationBuilder()
                // Opcional: Cargar appsettings.json si existe en el proyecto Data (no suele existir)
                .SetBasePath(currentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                // Cargar el appsettings.json del proyecto WebAPI (este es el crucial)
                .AddJsonFile(webApiAppsettingsPath, optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException($"DefaultConnection not found in appsettings.json. Checked: {webApiAppsettingsPath}");
            }

            optionsBuilder.UseSqlServer(connectionString);

            return new TPIContext(optionsBuilder.Options);
        }
    }
}