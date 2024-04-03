using EmployeeDirectory.Interfaces;
using EmployeeDirectory.UI.Menus;
using EmployeeDirectory.UI.UIServices;
using EmployeeDirectory.UI;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeDirectory.Services
{
    public class StartupService
    {

        private IServiceCollection services;
        public StartupService(IServiceCollection services)
        {
            this.services = services; 
        }

        public void Configure()
        {
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<IRoleService, RoleService>();
            services.AddSingleton<IUIService, UIService>();
            services.AddSingleton<IEmployeeMenu, EmployeeMenu>();
            services.AddSingleton<IRoleMenu, RoleMenu>();
            services.AddSingleton<IValidator, Validator>();
            services.AddSingleton<MainMenu>();

            services.BuildServiceProvider();
        }

        public void ShowMainMenu()
        {
            ServiceProvider serviceProvider = services.BuildServiceProvider();

            MainMenu? menu = serviceProvider.GetService<MainMenu>();
            if(menu != null)
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
