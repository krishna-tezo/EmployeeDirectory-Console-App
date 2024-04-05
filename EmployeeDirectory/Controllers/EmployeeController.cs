using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Models;
using EmployeeDirectory.Models.ViewModel;

namespace EmployeeDirectory.Controllers
{
    public class EmployeeController : IEmployeeController
    {
        IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        public List<EmployeeView> ViewEmployees()
        {
            return employeeService.ViewEmployees();
        }
        public EmployeeView ViewEmployee(string empId)
        {
            return employeeService.GetEmployeeViewById(empId);
        }

        public Employee GetEmployeeById(string id)
        {
            return employeeService.GetEmployeeById(id);
        }

        public Employee AddEmployee(Employee employee)
        {
            return employeeService.AddEmployee(employee);
        }

        public Employee EditEmployee(Employee employee)
        {
            return employeeService.UpdateEmployee(employee);
        }

        public Employee DeleteEmployee(string empId)
        {
            return employeeService.DeleteEmployee(empId);
        }

    }
}
