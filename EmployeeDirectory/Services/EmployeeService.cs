using EmployeeDirectory.DAL;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Models;
namespace EmployeeDirectory.Services
{
    
    public class EmployeeService : IEmployeeService
    {
        List<Employee> employees = [];
        private IRoleService roleService;
        public EmployeeService(IRoleService roleService)
        {
            this.roleService = roleService;
            this.employees = this.GetEmployees();
        }
        public bool DoesEmployeeIdExist(string id)
        {
            return employees.Any(employee => employee.Id == id);
        }
        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
            JsonDataHandler.UpdateEmployeesDataToJson<Employee>(employees, "employee");
        }

        public int DeleteEmployee(string empId)
        {
            //TODO: Soft delete
            if (employees.RemoveAll(emp => emp.Id == empId) > 0)
            {
                JsonDataHandler.UpdateEmployeesDataToJson<Employee>(employees, "employee");

                return 1;
            }
            else
            {
                return -1;
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            int indexOfEmployee = employees.FindIndex(emp => employee.Id == emp.Id);
            employees[indexOfEmployee] = employee;
            JsonDataHandler.UpdateEmployeesDataToJson<Employee>(employees, "employee");
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = JsonDataHandler.GetDataFromJson<Employee>("employee");
            return employees;
        }

        public Employee GetEmployeeById(string id)
        {
            return employees.Find(emp => emp.Id == id);
        }
    }
}