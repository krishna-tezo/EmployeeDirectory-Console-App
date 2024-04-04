using EmployeeDirectory.Models;

namespace EmployeeDirectory.Interfaces
{
    public interface IEmployeeService
    {
        Employee AddEmployee(Employee employee);
        Employee DeleteEmployee(string empId);
        Employee GetEmployeeById(string id);
        List<Employee> GetEmployees();
        Employee UpdateEmployee(Employee employee);

    }
}
