using EmployeeDirectory.Models;
using Newtonsoft.Json;

namespace EmployeeDirectory.DAL
{
    public class JsonDataHandler
    {
        //TODO: generic methods
        public static List<Employee> GetEmployeesDataFromJson()
        {
            string data = File.ReadAllText(Directory.GetCurrentDirectory()+"\\DAL\\employees.json");
            List<Employee> employees = System.Text.Json.JsonSerializer.Deserialize<List<Employee>>(data);
            return employees;
        }

        public static void UpdateEmployeesDataToJson(List<Employee> employees){

            string UpdatedEmployeesList = JsonConvert.SerializeObject(employees);
            string path = Directory.GetCurrentDirectory() + "\\DAL\\employees.json";
            File.WriteAllText(path, UpdatedEmployeesList);
        }

        public static List<Role> GetRolesDataFromJson()
        {
            string data = File.ReadAllText(Directory.GetCurrentDirectory() + "\\DAL\\roles.json");
            List<Role> roles = System.Text.Json.JsonSerializer.Deserialize<List<Role>>(data);
            return roles;
        }

        public static void UpdateRolesDataToJson(List<Role> roles)
        {
            string UpdatedRolesList = JsonConvert.SerializeObject(roles);
            string path = Directory.GetCurrentDirectory() + "\\DAL\\roles.json";
            File.WriteAllText(path, UpdatedRolesList);
        }
    }
}
