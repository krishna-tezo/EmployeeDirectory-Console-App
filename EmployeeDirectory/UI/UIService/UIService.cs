﻿using EmployeeDirectory.Core;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Models;
using System.Globalization;


namespace EmployeeDirectory.UI.UIServices
{
    internal class UIService : IUIService
    {

        private IEmployeeService employeeService;
        private IRoleService roleService;
        private IValidator validator;

        public UIService(IEmployeeService employeeService, IRoleService roleService, IValidator validator)
        {
            this.employeeService = employeeService;
            this.roleService = roleService;
            this.validator = validator;
        }

        #region "Employee Service"

        //Add Employee
        public void AddEmployee()
        {
            Console.WriteLine("\n----Welcome to Add Employee Form----\n");
            Employee employee = new Employee();
            GetEmployeeDetailsFromConsole(employee, "Add");

        }

        //Edit Employee
        public void EditEmployee()
        {
            Console.WriteLine("\n----Welcome To Edit Employee Form----\n");

            string? empId;
            do
            {
                Console.Write("Input Id of the Employee which you want to Edit: ");
                empId = Console.ReadLine();
                if (empId.Equals(""))
                {
                    Console.WriteLine("Don't leave it blank");

                }
                else if (!employeeService.DoesEmployeeIdExist(empId))
                {
                    Console.WriteLine($"{empId} does not exist");
                }
                else
                {
                    GetEmployeeDetailsFromConsole(employeeService.GetEmployeeById(empId), "Edit", empId);
                    Console.WriteLine($"Employee with {empId} is updated successfully");
                    break;
                }

            }
            while (true);
        }

        //Get Employee Details From Console
        public void GetEmployeeDetailsFromConsole(Employee employee, string type, string empId = "")
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

            string firstName;
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

            string email;
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

            string mobileNumber;
            do
            {
                Console.Write("Enter Mobile No.:");

                mobileNumber = Console.ReadLine();
                result = validator.IsValidInput(mobileNumber, "mobileNumber");
                if (result.IsValid != true)
                    Console.WriteLine(result.ErrorMessage);

            }
            while (result.IsValid == false);

            string managerName;
            do
            {
                Console.Write("Enter Manager Name:");

                managerName = Console.ReadLine();
                result = validator.IsValidInput(managerName, "managerName");
                if (result.IsValid != true)
                    Console.WriteLine(result.ErrorMessage);

            }
            while (result.IsValid == false);

            string projectName;
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

            employee.Id = empId;
            employee.FirstName = firstName;
            employee.LastName = lastName;
            employee.Email = email;
            employee.JoinDate = joinDate;
            employee.ManagerName = managerName;
            employee.ProjectName = projectName;
            employee.IsDeleted = false;

            string roleId = roleService.GenerateRoleId(roleName, location);

            employee.RoleId = roleId;

            if (type.Equals("Add"))
            {
                employeeService.AddEmployee(employee);
                Console.WriteLine("The employee is added successfully");
            }

            else
            {
                employeeService.UpdateEmployee(employee);
                Console.WriteLine("The employee is updated successfully");
            }
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
                options = roleService.GetAllDepartments();
                options.ForEach((option) =>
                {
                    Console.WriteLine(option);
                });
            }
            else if (parameter.Equals("roleName"))
            {
                Console.WriteLine($"\n\n----Available Roles Under {department}----\n");
                options = roleService.GetAllRoleNamesByDepartment(department);
            }
            else
            {
                Console.WriteLine($"\n\n----Available Locations Under {roleName}----\n");
                options = roleService.GetAllLocationByDepartmentAndRoleNames(roleName);
            }

            Dictionary<string, string> optionsMap = new Dictionary<string, string>();

            options.ForEach(option =>
            {
                optionsMap.Add(number.ToString(), option);
                Console.WriteLine(number + ". " + option);
                number++;
            });

            Console.Write("\nChoose Option:");
            inputKey = Console.ReadLine();
            if (!optionsMap.ContainsKey(inputKey))
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
            return optionsMap[inputKey];
        }

        //View Employees in Console
        public void ViewEmployees()
        {
            List<Employee> employees = employeeService.GetEmployees();

            if (employees is null || employees.Count == 0)
                Console.WriteLine("No Employees To Show");
            else
                this.ShowEmployeesDataInTabularFormat(employees);
        }

        //View Single Employee
        public void ViewEmployee()
        {

            while (true)
            {
                Console.WriteLine("Enter the emp Id to fetch the employee or -1 to exit:");
                string? empId = Console.ReadLine();
                Employee employee = employeeService.GetEmployeeById(empId);
                if (empId.Equals("-1"))
                {
                    break;
                }
                if (employee == null)
                {
                    Console.WriteLine("The Employee Is Not Found");
                }
                else
                {
                    ShowEmployeesDataInTabularFormat(employee);
                    break;
                }
            }

        }

        //Delete Employee
        public void DeleteEmployee()
        {
            bool takeInput = true;
            while (takeInput)
            {
                Console.WriteLine("Enter Employee Id You want to delete OR -1 to exit");
                string empId = Console.ReadLine() ?? string.Empty;
                Employee employee;
                if (empId != "-1")
                {
                    employee = employeeService.DeleteEmployee(empId);
                    if (employee == null)
                        Console.WriteLine("Employee was not found");
                    else
                        Console.WriteLine("The Employee is Removed");
                }
                else
                {
                    takeInput = false;
                }
            }
        }

        //Representation of Data in Tabular Format
        public void ShowEmployeesDataInTabularFormat(List<Employee> employees)
        {
            Console.WriteLine("\nEmployee List");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            string headers = String.Format("|{0,10}|{1,20}|{2,30}|{3,20}|{4,20}|{5,20}|{6,20}|{7,20}|", "EmpId", "Name", "Role", "Department", "Location", "Join Date", "Manager Name", "Project Name");
            Console.WriteLine(headers);
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            List<Role> roles = roleService.GetAllRoles();

            employees.Join(roles, emp => emp.RoleId, role => role.Id, (employee, role) =>
            new
            {
                employee.Id,
                EmpName = $"{employee.FirstName} {employee.LastName}",
                role.Name,
                role.Department,
                role.Location,
                employee.JoinDate,
                employee.ManagerName,
                employee.ProjectName
            }).ToList().ForEach(emp =>
                {
                    string empData = String.Format("|{0,10}|{1,20}|{2,30}|{3,20}|{4,20}|{5,20}|{6,20}|{7,20}|",
                        emp.Id, emp.EmpName, emp.Name, emp.Department, emp.Location, emp.JoinDate.ToString("MM/dd/yyyy"), emp.ManagerName, emp.ProjectName);

                    Console.WriteLine(empData);
                });

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
        }

        public void ShowEmployeesDataInTabularFormat(Employee employee)
        {
            Console.WriteLine("\nEmployee List");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            string headers = String.Format("|{0,10}|{1,20}|{2,30}|{3,20}|{4,20}|{5,20}|{6,20}|{7,20}|", "EmpId", "Name", "Role", "Department", "Location", "Join Date", "Manager Name", "Project Name");
            Console.WriteLine(headers);
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            Role role = roleService.GetRoleDetailsById(employee.RoleId);
            string empData = String.Format("|{0,10}|{1,20}|{2,30}|{3,20}|{4,20}|{5,20}|{6,20}|{7,20}|", employee.Id, employee.FirstName + " " + employee.LastName, role.Name, role.Department, role.Location, employee.JoinDate.ToString("MM/dd/yyyy"), employee.ManagerName, employee.ProjectName);
            Console.WriteLine(empData);
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

                roleId = roleService.GenerateRoleId(roleName, location);

                if (DoesRoleIdExist(roleId))
                    Console.WriteLine("This role already exists");
                else
                    break;
            } while (true);

            Role role = new Role();

            role.Id = roleId;
            role.Name = roleName;
            role.Location = location;
            role.Department = department;
            role.Description = description;


            roleService.AddRole(role);
            Console.WriteLine("New Role has been added");
        }

        public bool DoesRoleIdExist(string roleId)
        {
            List<Role> roles = roleService.GetAllRoles();
            return roles.Any(role => role.Id == roleId);
        }

        public void ViewAllRoles()
        {
            List<Role> roles = roleService.GetAllRoles();

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