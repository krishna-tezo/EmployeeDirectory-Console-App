using EmployeeDirectory.Models;

namespace EmployeeDirectory.Interfaces
{
    public interface IEmployeeService
    {
        Employee AddEmployee(Employee employee);
        Employee DeleteEmployee(string empId);
        bool DoesEmployeeIdExist(string id);
        Employee GetEmployeeById(string id);
        List<Employee> GetEmployees();
        Employee UpdateEmployee(Employee employee);

    }
}
