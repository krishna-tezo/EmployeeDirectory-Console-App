using EmployeeDirectory.DAL;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Models;
using System.Text.RegularExpressions;
namespace EmployeeDirectory.Services
{
    public class EmployeeService : IEmployeeService
    {

        public bool DoesEmployeeIdExist(string id)
        {
            List<Employee> employees = this.GetEmployees();
            return employees.Any(employee => employee.Id == id);
        }
        public void AddEmployee(Employee employee)
        {
            List<Employee> employees = JsonDataHandler.GetEmployeesDataFromJson();
            employees.Add(employee);
            JsonDataHandler.UpdateEmployeesDataToJson(employees);
        }

        public int DeleteEmployee(string empId)
        {
            //TODO: Soft delete
            List<Employee> employees = JsonDataHandler.GetEmployeesDataFromJson();
            if(employees.RemoveAll(emp => emp.Id == empId) > 0)
            {
                JsonDataHandler.UpdateEmployeesDataToJson(employees);
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            List<Employee> employees = JsonDataHandler.GetEmployeesDataFromJson();

            int indexOfEmployee = employees.FindIndex(emp => employee.Id == emp.Id);
            employees[indexOfEmployee] = employee;

            JsonDataHandler.UpdateEmployeesDataToJson(employees);
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = JsonDataHandler.GetEmployeesDataFromJson();
            return employees;
        }

        public Employee GetEmployeeById(string id)
        {
            List<Employee> employees = JsonDataHandler.GetEmployeesDataFromJson();
            return employees.Find(emp => emp.Id == id);
        }
    }
}