﻿using EmployeeDirectory.Interfaces;
using EmployeeDirectory.UI.Menus;
using EmployeeDirectory.UI.UIServices;
using EmployeeDirectory.UI;
using Microsoft.Extensions.DependencyInjection;
using EmployeeDirectory.Services;

namespace EmployeeDirectory.Core
{
    public class StartupService
    {

        private IServiceCollection services;
        public StartupService(IServiceCollection services)
        {
            this.services = services;
        }

        public ServiceProvider Configure()
        {
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<IRoleService, RoleService>();
            services.AddSingleton<IUIService, UIService>();
            services.AddSingleton<IEmployeeMenu, EmployeeMenu>();
            services.AddSingleton<IRoleMenu, RoleMenu>();
            services.AddSingleton<IValidator, Validator>();
            services.AddSingleton<MainMenu>();

            return services.BuildServiceProvider();
        }


    }
}
