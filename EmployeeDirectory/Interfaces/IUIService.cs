using EmployeeDirectory.Models;

namespace EmployeeDirectory.Interfaces
{
    public interface IUIService
    {
        void AddEmployee();
        void AddRole();
        void DeleteEmployee();
        bool DoesRoleIdExist(string roleId);
        void EditEmployee();
        void GetEmployeeDetailsFromConsole(Employee employee, string type, string empId = "");
        string GetEmployeeRoleDetails(string parameter, string department = "", string roleName = "");
        void ShowEmployeesDataInTabularFormat(Employee employee);
        void ShowEmployeesDataInTabularFormat(List<Employee> employees);
        void ShowRolesDataInTabularFormat(List<Role> roles);
        void ViewAllRoles();
        void ViewEmployee();
        void ViewEmployees();
    }
}