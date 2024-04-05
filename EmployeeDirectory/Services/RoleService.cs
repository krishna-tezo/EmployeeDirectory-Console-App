using EmployeeDirectory.DATA;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Models;

namespace EmployeeDirectory.Services
{
    internal class RoleService : IRoleService
    {
        private List<Role> Roles { get; set; }
        public RoleService()
        {
            this.Roles = GetAllRoles();
        }
        public Role AddRole(Role role)
        {
            Roles.Add(role);
            JsonDataHandler.UpdateEmployeesDataToJson(Roles, "roles");
            return role;
        }

        public List<Role> GetAllRoles()
        {
            List<Role> roles = JsonDataHandler.GetDataFromJson<Role>("roles");
            return roles;
        }

        public Role GetRoleDetailsById(string id)
        {
            return Roles.Find(role => role.Id == id)!;
        }

        public string GenerateRoleId(string roleName, string location)
        {
            return roleName[..3] + location[..3];
        }

        public List<string> GetAllDepartments()
        {
            return Roles.Select(role => role.Department).Distinct().ToList();
        }

        public List<string> GetAllRoleNamesByDepartment(string department)
        {

            return Roles.Where(role => role.Department == department).Select(role => role.Name).Distinct().ToList();
        }

        public List<string> GetAllLocationByDepartmentAndRoleNames(string roleName)
        {
            return Roles.Where(role => role.Name == roleName).Select(role => role.Location).Distinct().ToList();
        }
    }
}