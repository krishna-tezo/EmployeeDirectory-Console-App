using EmployeeDirectory.DATA;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Models;

namespace EmployeeDirectory.Services
{
    public class RoleService : IRoleService
    {
        private List<Role> Roles { get; set; }
        private IJsonDataHandler jsonDataHandler;
        public RoleService(IJsonDataHandler jsonDataHandler)
        {
            this.jsonDataHandler = jsonDataHandler;
            this.Roles = GetAllRoles();
        }
        public Role AddRole(Role role)
        {
            Roles.Add(role);
            jsonDataHandler.UpdateDataToJson(Roles);
            return role;
        }

        public List<Role> GetAllRoles()
        {
            List<Role> roles = jsonDataHandler.GetDataFromJson<Role>();
            return roles;
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