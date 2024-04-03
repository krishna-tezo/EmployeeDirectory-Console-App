using EmployeeDirectory.Models;

namespace EmployeeDirectory.Interfaces
{
    public interface IRoleService
    {
        void AddRole(Role role);
        string GenerateRoleId(string roleName, string location);
        List<string> GetAllDepartments();
        List<string> GetAllLocationByDepartmentAndRoleNames(string roleName);
        List<string> GetAllRoleNamesByDepartment(string department);
        List<Role> GetAllRoles();
        Role GetRoleDetailsById(string id);

    }
}