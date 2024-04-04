using EmployeeDirectory.DATA;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Models;
namespace EmployeeDirectory.Services
{
    
    public class EmployeeService : IEmployeeService
    {
        private List<Employee> Employees { get; set; }
        public EmployeeService()
        {
            this.Employees = this.GetEmployees();
        }
        public bool DoesEmployeeIdExist(string id)
        {
            return Employees.Any(employee => employee.Id == id);
        }
        public Employee AddEmployee(Employee employee)
        {
            Employees.Add(employee);
            JsonDataHandler.UpdateEmployeesDataToJson<Employee>(Employees, "employee");
            return employee;
        }

        public Employee DeleteEmployee(string empId)
        {

            Employee employee = Employees.Find(emp => emp.Id == empId);
            if(employee != null)
            {
                employee.IsDeleted = true;
                JsonDataHandler.UpdateEmployeesDataToJson<Employee>(Employees, "employee");
                Employees.Remove(employee);
            }
            return employee;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            int indexOfEmployee = Employees.FindIndex(emp => employee.Id == emp.Id);
            if (indexOfEmployee != -1)
            {
                Employees[indexOfEmployee] = employee;
            }
            JsonDataHandler.UpdateEmployeesDataToJson<Employee>(Employees, "employee");
            return employee;
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = JsonDataHandler.GetDataFromJson<Employee>("employee");
            employees = employees.Where(emp => emp.IsDeleted == false).ToList();
            return employees;
        }

        public Employee GetEmployeeById(string id)
        {
            return Employees.Find((emp) => emp.Id == id);
        }
    }
}