using EmployeeDirectory.Models;
using EmployeeDirectory.Models.ViewModel;

namespace EmployeeDirectory.Interfaces
{
    public interface IEmployeeService
    {
        Employee AddEmployee(Employee employee);
        List<EmployeeView> ViewEmployees();
        EmployeeView GetEmployeeViewById(string id);
        List<Employee> GetEmployees();
        Employee DeleteEmployee(string empId);
        Employee GetEmployeeById(string id);
        Employee UpdateEmployee(Employee employee);

    }
}
