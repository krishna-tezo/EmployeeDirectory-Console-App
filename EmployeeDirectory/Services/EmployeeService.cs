using EmployeeDirectory.DATA;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Models;
using EmployeeDirectory.Models.ViewModel;
namespace EmployeeDirectory.Services
{

    public class EmployeeService : IEmployeeService
    {
        private List<Employee> Employees { get; set; }
        IRoleService roleService;
        public EmployeeService(IRoleService roleService)
        {
            this.Employees = this.GetEmployees();
            this.roleService = roleService;
        }

        public List<EmployeeView> ViewEmployees()
        {
            List<Role> roles = roleService.GetAllRoles();
            List<EmployeeView> employeesToView = Employees.Join(roles, emp => emp.RoleId, role => role.Id, (employee, role) =>
            new EmployeeView
            {
                Id = employee.Id,
                Name = $"{employee.FirstName} {employee.LastName}",
                Role = role.Name,
                Department = role.Department,
                Location = role.Location,
                JoinDate = employee.JoinDate,
                ManagerName = employee.ManagerName,
                ProjectName = employee.ProjectName
            }).ToList();
            return employeesToView;
        }
        public EmployeeView GetEmployeeViewById(string id)
        {
            List<EmployeeView> employees = this.ViewEmployees();
            return employees.Find( emp => emp.Id == id)!;
        }
        public Employee AddEmployee(Employee employee)
        {
            Employees.Add(employee);
            JsonDataHandler.UpdateEmployeesDataToJson<Employee>(Employees, "employee");
            return employee;
        }

        public Employee DeleteEmployee(string empId)
        {
            Employee employee = Employees.Find(emp => emp.Id == empId)!;
            if(employee != null)
            {
                employee.IsDeleted = true;
                JsonDataHandler.UpdateEmployeesDataToJson<Employee>(Employees, "employee");
                Employees.Remove(employee);
            }
            return employee!;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            int indexOfEmployee = Employees.FindIndex(emp => employee.Id == emp.Id);
            if (indexOfEmployee != -1)
            {
                Employees[indexOfEmployee] = employee;
            }
            JsonDataHandler.UpdateEmployeesDataToJson<Employee>(Employees, "employee");
            return employee;
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = JsonDataHandler.GetDataFromJson<Employee>("employee");
            employees = employees.Where(emp => emp.IsDeleted == false).ToList();
            return employees;
        }

        public Employee GetEmployeeById(string id)
        {
            return Employees.Find((emp) => emp.Id == id)!;
        }
    }
}