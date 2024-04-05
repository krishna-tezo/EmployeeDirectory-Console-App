using EmployeeDirectory.Models;
using EmployeeDirectory.Models.ViewModel;

namespace EmployeeDirectory.Interfaces
{
    public interface IEmployeeController
    {
        List<EmployeeView> ViewEmployees();
        EmployeeView ViewEmployee(string empId);
        Employee AddEmployee(Employee employee);
        Employee EditEmployee(Employee employee);
        Employee DeleteEmployee(string empId);
        
        Employee GetEmployeeById(string empId);
    }
}