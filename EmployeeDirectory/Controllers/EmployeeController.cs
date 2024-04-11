using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Models;
using EmployeeDirectory.Models.ViewModel;
namespace EmployeeDirectory.Controllers
{
    public class EmployeeController : IEmployeeController
    {
        IEmployeeService employeeService;
        IRoleService roleService;

        public EmployeeController(IEmployeeService employeeService, IRoleService roleService)
        {
            this.employeeService = employeeService;
            this.roleService = roleService;
        }

        public string GetNewEmployeeId()
        {
            return employeeService.GenerateNewId();
        }

        public List<EmployeeView> ViewEmployees()
        {   
            List <Employee> employees = employeeService.GetEmployees();
            List <Role> roles = roleService.GetAllRoles();
            List<EmployeeView> employeesToView = employees.Join(roles, emp => emp.RoleId, role => role.Id, (employee, role) =>
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
        
        public EmployeeView? ViewEmployee(string empId)
        {
            List<EmployeeView> employees = this.ViewEmployees();
            EmployeeView? employee = employees.Find(emp => emp.Id == empId);
            if(employee == null)
            {
                return null;
            }
            return employee;
            
        }

        public Employee? GetEmployeeById(string id)
        {
            return employeeService.GetEmployeeById(id);
        }

        public Employee? AddEmployee(Employee employee)
        {
            return employeeService.AddEmployee(employee);
        }

        public Employee? EditEmployee(Employee employee)
        {
            return employeeService.UpdateEmployee(employee);
        }

        public Employee? DeleteEmployee(string empId)
        {
            return employeeService.DeleteEmployee(empId);
        }

    }
}
