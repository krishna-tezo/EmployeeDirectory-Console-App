using EmployeeDirectory.DAL;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Models;

namespace EmployeeDirectory.Services
{
    internal class RoleService : IRoleService
    {
        List<Role> roles = new List<Role>();
        public RoleService()
        {
            this.roles = GetAllRoles();
        }
        public void AddRole(Role role)
        {
            roles.Add(role);
            JsonDataHandler.UpdateEmployeesDataToJson(roles, "roles");

        }

        public List<Role> GetAllRoles()
        {
            List<Role> roles = JsonDataHandler.GetDataFromJson<Role>("roles");
            return roles;
        }

        public Role GetRoleDetailsById(string id)
        {
            return roles.Find(role => role.Id == id);
        }

        public string GenerateRoleId(string roleName, string location)
        {
            return roleName[..3] + location[..3];
        }

        public List<string> GetAllDepartments()
        {
            return roles.Select(role => role.Department).Distinct().ToList();
        }

        public List<string> GetAllRoleNamesByDepartment(string department)
        {

            return roles.Where(role => role.Department == department).Select(role => role.Name).Distinct().ToList();

            //List<string> roleNames = new List<string>();
            //foreach (Role role in roles)
            //{
            //    if (role.Department == department)
            //    {
            //        if (!roleNames.Contains(role.Name))
            //        {
            //            roleNames.Add(role.Name);
            //        }
            //    }
            //}
            //return roleNames;
        }

        public List<string> GetAllLocationByDepartmentAndRoleNames(string roleName)
        {

            return roles.Where(role => role.Name == roleName).Select(role => role.Location).Distinct().ToList();
        }
    }
}