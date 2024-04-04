using EmployeeDirectory.Models;

namespace EmployeeDirectory.Interfaces
{
    public interface IEmployeeController
    {
        List<Employee> ViewEmployees();
        Employee ViewEmployee(string empId);
        Employee AddEmployee(Employee employee);
        Employee EditEmployee(Employee employee);
        Employee DeleteEmployee(string empId);
        
        Employee GetEmployeeById(string empId);
    }
}