using EmployeeDirectory.Models;

namespace EmployeeDirectory.Interfaces
{
    public interface IEmployeeService
    {
        //return employee
        void AddEmployee(Employee employee);
        int DeleteEmployee(string empId);
        bool DoesEmployeeIdExist(string id);
        Employee GetEmployeeById(string id);
        List<Employee> GetEmployees();
        void UpdateEmployee(Employee employee);

    }
}
