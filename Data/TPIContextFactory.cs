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

            var currentDirectory = Directory.GetCurrentDirectory();

            var webApiAppsettingsPath = Path.Combine(currentDirectory, "..", "WebAPI", "appsettings.json");
            Console.WriteLine($"Current Directory: {currentDirectory}");
            Console.WriteLine($"Looking for appsettings.json at: {webApiAppsettingsPath}");


            var configuration = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
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