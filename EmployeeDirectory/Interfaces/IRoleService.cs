using EmployeeDirectory.Models;

namespace EmployeeDirectory.Interfaces
{
    internal interface IRoleService
    {
        void AddRole(Role role);
        List<Role> GetAllRoles();
        Role GetRoleDetailsById(string id);

    }
}