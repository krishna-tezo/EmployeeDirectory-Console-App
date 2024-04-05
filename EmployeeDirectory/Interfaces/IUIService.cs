using EmployeeDirectory.Models;
using EmployeeDirectory.Models.ViewModel;

namespace EmployeeDirectory.Interfaces
{
    public interface IUIService
    {
        void AddEmployee();
        void AddRole();
        void DeleteEmployee();
        bool DoesRoleIdExist(string roleId);
        void EditEmployee();
        Employee GetEmployeeDetailsFromConsole(Employee employee, string type, string empId = "");
        string GetEmployeeRoleDetails(string parameter, string department = "", string roleName = "");
        void ShowEmployeesDataInTabularFormat(List<EmployeeView> employees);
        void ShowRolesDataInTabularFormat(List<Role> roles);
        void ViewAllRoles();
        void ViewEmployee();
        void ViewEmployees();
    }
}