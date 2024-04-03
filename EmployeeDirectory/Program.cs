using Microsoft.Extensions.DependencyInjection;
using EmployeeDirectory.Services;

namespace EmployeeDirectory
{
    public static class Program
    {
        public static void Main(string[] args)
        { 
            IServiceCollection services = new ServiceCollection();
            StartupService startupService = new StartupService(services);

            startupService.Configure();
            startupService.ShowMainMenu();
        }
    }
}