using EmployeeDirectory.Models;

namespace EmployeeDirectory.Interfaces
{
    internal interface IEmployeeService
    {
        //return employee
        void AddEmployee(Employee employee);
        List<Employee> GetEmployees();
        void UpdateEmployee(Employee employee);
        int DeleteEmployee(string empId);
        Employee GetEmployeeById(string id);
    }
}
