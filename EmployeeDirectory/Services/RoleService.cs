using EmployeeDirectory.DAL;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Models;

namespace EmployeeDirectory.Services
{
    internal class RoleService : IRoleService
    {   
        public bool RoleValidator(string roleId)
        {
            List<Role> roles = new List<Role>();
            return roles.Any(role => role.Id == roleId);
        }
        public void AddRole(Role role)
        {
            List<Role> roles = JsonDataHandler.GetRolesDataFromJson();
            roles.Add(role);
            JsonDataHandler.UpdateRolesDataToJson(roles);
        }

        public List<Role> GetAllRoles()
        {
            List<Role> roles = JsonDataHandler.GetRolesDataFromJson();
            return roles;
        }

        public Role GetRoleDetailsById(string id)
        {
            List<Role> roles = GetAllRoles();
            return roles.Find(role => role.Id == id);
        }

        public string GenerateRoleId(string roleName, string location)
        {
            return roleName[..3] + location[..3];
        }

        public List<string> GetAllDepartments()
        {
            List<Role> roles = JsonDataHandler.GetRolesDataFromJson();

            return roles.Select(role => role.Department).Distinct().ToList();
        }

        public List<string> GetAllRoleNamesByDepartment(string department)
        {
            List<Role> roles = JsonDataHandler.GetRolesDataFromJson();
            
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
            List<Role> roles = JsonDataHandler.GetRolesDataFromJson();

            return roles.Where(role=>role.Name==roleName).Select(role=> role.Location).Distinct().ToList();
        }
    }
}