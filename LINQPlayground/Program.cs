using EmployeeDirectory.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    
    public static void Main(string[] args)
    {
        string employeesData = File.ReadAllText("C:\\Users\\Krishna.a\\Desktop\\Daily Tasks\\EmployeeDirectory\\EmployeeDirectory\\DAL\\employees.json");
        string rolesData = File.ReadAllText("C:\\Users\\Krishna.a\\Desktop\\Daily Tasks\\EmployeeDirectory\\EmployeeDirectory\\DAL\\roles.json");

        List<Employee> employees = System.Text.Json.JsonSerializer.Deserialize<List<Employee>>(employeesData);
        List<Role> roles = System.Text.Json.JsonSerializer.Deserialize<List<Role>>(rolesData);

        //Console.Clear();

        var queriedEmployees = from employee in employees
                    join role in roles on employee.RoleId equals role.Id
                    where employee.FirstName is not "Krishnaa"
                    select new { Name = $"{employee.FirstName} {employee.LastName}", DepartmentName = role.Department };

        foreach (var emp in queriedEmployees)
        {
            Console.WriteLine($"{emp.Name} - {emp.DepartmentName}");
        }
    }
    
}