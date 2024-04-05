﻿using EmployeeDirectory.Core;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Models;
using EmployeeDirectory.Models.ViewModel;
using System.Globalization;


namespace EmployeeDirectory.UI.UIServices
{
    internal class UIService : IUIService
    {

        private readonly IRoleController roleController;
        private readonly IEmployeeController employeeController;
        private readonly IValidator validator;

        public UIService(IEmployeeController employeeController, IRoleController roleController, IValidator validator)
        {
            this.roleController = roleController;
            this.validator = validator;
            this.employeeController = employeeController;
        }

        #region "Employee Service"

        //Add Employee
        public void AddEmployee()
        {
            Console.WriteLine("\n----Welcome to Add Employee Form----\n");
            Employee employee = GetEmployeeDetailsFromConsole(new Employee(), "Add");
            employeeController.AddEmployee(employee);
            Console.WriteLine("The employee is added successfully");
        }

        //Edit Employee
        public void EditEmployee()
        {
            Employee employee;
            Console.WriteLine("\n----Welcome To Edit Employee Form----\n");

            string? empId;
            do
            {
                Console.Write("Input Id of the Employee which you want to Edit or -1 to exit: ");
                empId = Console.ReadLine();
                if (empId!.Equals("-1"))
                {
                    break;
                }
                else if (empId.Equals(""))
                {
                    Console.WriteLine("Don't leave it blank");
                }
                else if (employeeController.GetEmployeeById(empId)==null)
                {
                    Console.WriteLine($"{empId} does not exist");
                }
                else
                {   
                    employee = employeeController.GetEmployeeById(empId);

                    employee = GetEmployeeDetailsFromConsole(employee, "Edit", empId);
                    employeeController.EditEmployee(employee);
                    Console.WriteLine($"Employee with id {empId} is updated successfully");
                    break;
                }

            }
            while (true);
        }

        //Get Employee Details From Console
        public Employee GetEmployeeDetailsFromConsole(Employee employee, string type, string? empId = "")
        {

            Console.WriteLine("----Input Employee Details----");
            ValidationResult result;

            if (type.Equals("Add"))
            {
                do
                {
                    Console.Write("Enter Employee Id(TZ0000):");

                    empId = Console.ReadLine();
                    result = validator.IsValidInput(empId, "empId");
                    if (result.IsValid != true)
                        Console.WriteLine(result.ErrorMessage);
                }
                while (result.IsValid == false);

            }

            string? firstName;
            do
            {
                Console.Write("Enter First Name:");

                firstName = Console.ReadLine();
                result = validator.IsValidInput(firstName, "firstName");
                if (result.IsValid != true)
                    Console.WriteLine(result.ErrorMessage);

            }
            while (result.IsValid == false);

            string? lastName;
            do
            {
                Console.Write("Enter Last Name:");

                lastName = Console.ReadLine();
                result = validator.IsValidInput(lastName, "lastName");
                if (result.IsValid != true)
                    Console.WriteLine(result.ErrorMessage);

            }
            while (result.IsValid == false);

            string? email;
            do
            {
                Console.Write("Enter Email:");

                email = Console.ReadLine();
                result = validator.IsValidInput(email, "email");
                if (result.IsValid != true)
                    Console.WriteLine(result.ErrorMessage);

            }
            while (result.IsValid == false);

            DateTime dob;
            do
            {
                Console.Write("Enter Dob (mm/dd/yyyy):");

                if (DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Date Format");
                }
            }
            while (true);

            string? mobileNumber;
            do
            {
                Console.Write("Enter Mobile No.:");

                mobileNumber = Console.ReadLine();
                result = validator.IsValidInput(mobileNumber, "mobileNumber");
                if (result.IsValid != true)
                    Console.WriteLine(result.ErrorMessage);

            }
            while (result.IsValid == false);

            string? managerName;
            do
            {
                Console.Write("Enter Manager Name:");

                managerName = Console.ReadLine();
                result = validator.IsValidInput(managerName, "managerName");
                if (result.IsValid != true)
                    Console.WriteLine(result.ErrorMessage);

            }
            while (result.IsValid == false);

            string? projectName;
            do
            {
                Console.Write("Enter Project Name:");

                projectName = Console.ReadLine();
                result = validator.IsValidInput(projectName, "projectName");
                if (result.IsValid != true)
                    Console.WriteLine(result.ErrorMessage);

            }
            while (result.IsValid == false);

            DateTime joinDate;
            do
            {
                Console.Write("Enter Join Date (mm/dd/yyyy):");

                if (DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out joinDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Date Format");
                }
            }
            while (true);


            string? department = GetEmployeeRoleDetails("department");
            string? roleName = GetEmployeeRoleDetails("roleName", department);
            string? location = GetEmployeeRoleDetails("location", department, roleName);

            employee.Id = empId!;
            employee.FirstName = firstName!;
            employee.LastName = lastName!;
            employee.Email = email!;
            employee.JoinDate = joinDate;
            employee.ManagerName = managerName!;
            employee.ProjectName = projectName!;
            employee.IsDeleted = false;

            string roleId = roleController.GetRoleId(roleName, location);

            employee.RoleId = roleId;

            return employee;
        }

        //Get Employee Role Details From Roles Data
        public string GetEmployeeRoleDetails(string parameter, string department = "", string roleName = "")
        {
            string? inputKey;
            int number = 1;
            List<string> options = new List<string>();
            if (parameter.Equals("department"))
            {
                Console.WriteLine("\n\n----Available Departments----\n");
                options = roleController.GetAllDepartments();
                options.ForEach((option) =>
                {
                    Console.WriteLine(option);
                });
            }
            else if (parameter.Equals("roleName"))
            {
                Console.WriteLine($"\n\n----Available Roles Under {department}----\n");
                options = roleController.GetAllRoleNamesByDepartment(department);
            }
            else
            {
                Console.WriteLine($"\n\n----Available Locations Under {roleName}----\n");
                options = roleController.GetAllLocationByDepartmentAndRoleNames(roleName);
            }

            Dictionary<string, string> optionsMap = [];

            options.ForEach(option =>
            {
                optionsMap.Add(number.ToString(), option);
                Console.WriteLine(number + ". " + option);
                number++;
            });

            Console.Write("\nChoose Option:");
            inputKey = Console.ReadLine();
            if (!optionsMap.ContainsKey(inputKey!))
            {
                Console.WriteLine("Please Enter a valid option");
                if (parameter.Equals("department"))
                {
                    return GetEmployeeRoleDetails("department");
                }
                else if (parameter.Equals("roleName"))
                {
                    return GetEmployeeRoleDetails("roleName", department);
                }
                else
                {
                    return GetEmployeeRoleDetails("location", department, roleName);
                }
            }
            return optionsMap[inputKey!];
        }

        //View Employees in Console
        public void ViewEmployees()
        {
            List<EmployeeView> employeesToView = employeeController.ViewEmployees();
            
            if (employeesToView is null || employeesToView.Count == 0)
                Console.WriteLine("No Employees To Show");
            else
                this.ShowEmployeesDataInTabularFormat(employeesToView);
        }

        //View Single Employee
        public void ViewEmployee()
        {

            List<EmployeeView> employeesToView = [];

            while (true)
            {
                Console.WriteLine("Enter the emp Id to fetch the employee or -1 to exit:");
                string? empId = Console.ReadLine();
                
                EmployeeView employee = employeeController.ViewEmployee(empId!);
                if (empId!.Equals("-1"))
                {
                    break;
                }
                if (employee == null)
                {
                    Console.WriteLine("The Employee Is Not Found");
                }
                else
                {
                    employeesToView.Add(employee);
                    ShowEmployeesDataInTabularFormat(employeesToView);
                    break;
                }
            }

        }

        //Delete Employee
        public void DeleteEmployee()
        {
            while (true)
            {
                Console.WriteLine("Enter Employee Id You want to delete OR -1 to exit");
                string empId = Console.ReadLine() ?? string.Empty;
                Employee employee;
                if (empId.Equals(""))
                {
                    Console.WriteLine("Don't leave blank");
                }
                else if (empId != "-1")
                {
                    employee = employeeController.DeleteEmployee(empId);
                    if (employee == null)
                        Console.WriteLine("Employee was not found");
                    else
                        Console.WriteLine("The Employee is Removed");
                }
                else
                {
                    break;
                }
            }
        }

        //Representation of Data in Tabular Format
        public void ShowEmployeesDataInTabularFormat(List<EmployeeView> employees)
        {
            Console.WriteLine("\nEmployee List");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            string headers = String.Format("|{0,10}|{1,20}|{2,30}|{3,20}|{4,20}|{5,20}|{6,20}|{7,20}|", "EmpId", "Name", "Role", "Department", "Location", "Join Date", "Manager Name", "Project Name");
            Console.WriteLine(headers);
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            employees.ForEach((emp) =>
            {
                string empData = String.Format("|{0,10}|{1,20}|{2,30}|{3,20}|{4,20}|{5,20}|{6,20}|{7,20}|",
                        emp.Id, emp.Name, emp.Role, emp.Department, emp.Location, emp.JoinDate.ToString("MM/dd/yyyy"), emp.ManagerName, emp.ProjectName);
                Console.WriteLine(empData);
            });

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
        }
        #endregion



        #region "Roles Service"

        //Get New Role Details From Console
        public void AddRole()
        {

            Console.WriteLine("\n----Welcome to Add Role Menu----\n");
            Console.WriteLine("\n----Input Role Details----\n");
            string? department = GetEmployeeRoleDetails("department");

            string? roleName;
            string? location;
            string? description;
            string roleId;
            do
            {
                Console.Write("Enter Role Name: ");
                roleName = Console.ReadLine();

                Console.Write("Enter Location: ");
                location = Console.ReadLine();

                Console.Write("Enter Description:");
                description = Console.ReadLine();

                roleId = roleController.GetRoleId(roleName!, location!);

                if (DoesRoleIdExist(roleId))
                    Console.WriteLine("This role already exists");
                else
                    break;
            } while (true);

            Role role = new();

            role.Id = roleId;
            role.Name = roleName!;
            role.Location = location!;
            role.Department = department;
            role.Description = description!;


            roleController.Add(role);
            Console.WriteLine("New Role has been added");
        }

        public bool DoesRoleIdExist(string roleId)
        {
            List<Role> roles = roleController.ViewRoles();
            return roles.Any(role => role.Id == roleId);
        }

        public void ViewAllRoles()
        {
            List<Role> roles = roleController.ViewRoles();

            ShowRolesDataInTabularFormat(roles);
        }
        public void ShowRolesDataInTabularFormat(List<Role> roles)
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
            string headers = String.Format("|{0,30}|{1,30}|{2,20}|{3,50}|", "RoleName", "Department", "Location", "Description");
            Console.WriteLine(headers);
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");

            roles.ForEach(role =>
            {
                string roleData = String.Format("|{0,30}|{1,30}|{2,20}|{3,50}|", role.Name, role.Department, role.Location, role.Description);
                Console.WriteLine(roleData);
            });
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
        }
        #endregion
    }
}