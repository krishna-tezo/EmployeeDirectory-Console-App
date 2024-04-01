using EmployeeDirectory.Models;
using EmployeeDirectory.Services;

namespace EmployeeDirectory.UI.UIServices
{
    internal class UIService
    {
        readonly EmployeeService employeeService = new EmployeeService();
        readonly RoleService roleService = new RoleService();
        readonly Validator validator = new Validator();

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
            Console.Write("Input Id of the Employee which you want to Edit: ");
        InputEmpId:
            string? empId = Console.ReadLine();
            if (empId == "")
            {
                Console.WriteLine("The input is empty.Pls Re_enter the id:");
                goto InputEmpId;
            }
            else if (!employeeService.DoesEmployeeIdExist(empId))
            {
                Console.WriteLine($"{empId} does not exist");
            }
            else
            {
                GetEmployeeDetailsFromConsole(employeeService.GetEmployeeById(empId), "Edit", empId);
                Console.WriteLine($"Employee with {empId} is updated successfully");
            }

        }


        //Get Employee Details From Console
        public void GetEmployeeDetailsFromConsole(Employee employee, string type, string empId = "")
        {

            Console.WriteLine("----Input Employee Details----");
            int result;

            if (type == "Add")
            {
                do
                {
                    Console.Write("Enter Employee Id(TZ0000):");

                    empId = Console.ReadLine();
                    result = validator.IsValidInput(empId, "empId");
                    if (result != 1)
                        Console.WriteLine(validator.ShowErrorMessage(result));
                }
                while (result != 1);

            }

            string firstName;
            do
            {
                Console.Write("Enter First Name: ");
                firstName = Console.ReadLine();
                result = validator.IsValidInput(firstName);
                if (result != 1)
                    Console.WriteLine(validator.ShowErrorMessage(result));
            }
            while (result != 1);

            string? lastName;
            do
            {
                Console.Write("Enter Last Name: ");
                lastName = Console.ReadLine();
                result = validator.IsValidInput(lastName);
                if (result != 1)
                {
                    Console.WriteLine(validator.ShowErrorMessage(result));
                }
            } while (result != 1);

            string email;
            do
            {
                Console.Write("Enter Email: ");
                email = Console.ReadLine();
                result = validator.IsValidInput(email);
                if (result != 1)
                    Console.WriteLine(validator.ShowErrorMessage(result));
            }
            while (result != 1);

            string dob;
            do
            {
                Console.Write("Enter DOB: ");
                dob = Console.ReadLine();
                result = validator.IsValidInput(dob);
                if (result != 1)
                    Console.WriteLine(validator.ShowErrorMessage(result));
            }
            while (result != 1);

            string mobileNumber;
            do
            {
                Console.Write("Enter Mobile Number: ");
                mobileNumber = Console.ReadLine();
                result = validator.IsValidInput(mobileNumber);
                if (result != 1)
                    Console.WriteLine(validator.ShowErrorMessage(result));
            }
            while (result != 1);

            string managerName;
            do
            {
                Console.Write("Enter Manager Name: ");
                managerName = Console.ReadLine();
                result = validator.IsValidInput(managerName);
                if (result != 1)
                    Console.WriteLine(validator.ShowErrorMessage(result));
            }
            while (result != 1);

            string projectName;
            do
            {
                Console.Write("Enter Project Name: ");
                projectName = Console.ReadLine();
                result = validator.IsValidInput(projectName);
                if (result != 1)
                    Console.WriteLine(validator.ShowErrorMessage(result));
            }
            while (result != 1);

            string joinDate;
            do
            {
                Console.Write("Enter Join Date: ");
                joinDate = Console.ReadLine();
                result = validator.IsValidInput(joinDate);
                if (result != 1)
                    Console.WriteLine(validator.ShowErrorMessage(result));
            }
            while (result != 1);

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

            string roleId = roleService.GenerateRoleId(roleName, location);

            employee.RoleId = roleId;

            if (type == "Add")
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
            if (parameter == "department")
            {
                Console.WriteLine("\n\n----Available Departments----\n");
                options = roleService.GetAllDepartments();
            }
            else if (parameter == "roleName")
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
                if (parameter == "department")
                {
                    return GetEmployeeRoleDetails("department");
                }
                else if (parameter == "roleName")
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
        ViewSingleEmployee:
            Console.WriteLine("Enter the emp Id to fetch the employee or -1 to exit:");
            string? empId = Console.ReadLine();
            Employee employee = employeeService.GetEmployeeById(empId);
            if (empId == "-1")
            {
                return;
            }
            if (employee == null)
            {
                Console.WriteLine("The Employee Is Not Found");
                goto ViewSingleEmployee;
            }
            else
            {
                this.ShowEmployeesDataInTabularFormat(employee);
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
                int status;
                if (empId != "-1")
                {
                    status = employeeService.DeleteEmployee(empId);
                    if (status == -1)
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
                        emp.Id, emp.EmpName, emp.Name, emp.Department, emp.Location, emp.JoinDate, emp.ManagerName, emp.ProjectName);

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
            string empData = String.Format("|{0,10}|{1,20}|{2,30}|{3,20}|{4,20}|{5,20}|{6,20}|{7,20}|", employee.Id, employee.FirstName + " " + employee.LastName, role.Name, role.Department, role.Location, employee.JoinDate, employee.ManagerName, employee.ProjectName);
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

        RoleDepartment:
            string? department = GetEmployeeRoleDetails("department");

        RoleName:
            Console.Write("Enter Role Name: ");
            string? roleName = Console.ReadLine();

        Location:
            Console.Write("Enter Location: ");
            string? location = Console.ReadLine();

        Description:
            Console.Write("Enter Description:");
            string? description = Console.ReadLine();

            Role role = new Role();
            string roleId = roleService.GenerateRoleId(roleName, location);
            if (roleService.RoleValidator(roleId))
            {
                Console.WriteLine("This role already exists");
                goto RoleDepartment;
            }
            else
            {
                role.Id = roleId;
                role.Name = roleName;
                role.Location = location;
                role.Department = department;
                role.Description = description;
            }

            roleService.AddRole(role);
            Console.WriteLine("New Role has been added");
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