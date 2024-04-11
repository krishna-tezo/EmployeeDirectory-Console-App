using EmployeeDirectory.Models;
using EmployeeDirectory.Models.ViewModel;

namespace EmployeeDirectory.Interfaces
{
    public interface IEmployeeService
    {
        string GenerateNewId();
        Employee AddEmployee(Employee employee);
        List<Employee> GetEmployees();
        Employee? DeleteEmployee(string empId);
        Employee? GetEmployeeById(string id);
        Employee UpdateEmployee(Employee employee);

    }
}
