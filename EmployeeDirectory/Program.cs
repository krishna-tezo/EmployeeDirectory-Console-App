using Microsoft.Extensions.DependencyInjection;
using EmployeeDirectory.UI;
using EmployeeDirectory.Core;

namespace EmployeeDirectory
{
    public static class Program
    {
        public static void Main(string[] args)
        { 
            IServiceCollection services = new ServiceCollection();
            StartupService startupService = new StartupService(services);

            ServiceProvider serviceProvider = startupService.Configure();
            StartApplication(serviceProvider);
        }
        public static void StartApplication(ServiceProvider serviceProvider)
        {
            MainMenu? menu = serviceProvider.GetService<MainMenu>();
            if (menu != null)
            {
                menu.ShowMainMenu();
            }
            else
            {
                Console.WriteLine("Some Error Occurred");
            }
        }
    }
}