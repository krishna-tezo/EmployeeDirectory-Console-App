using EmployeeDirectory.DATA;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Models;
namespace EmployeeDirectory.Services
{

    public class EmployeeService : IEmployeeService
    {

        private List<Employee> Employees { get; set; }
        private IJsonDataHandler jsonDataHandler;
        public EmployeeService(IJsonDataHandler jsonDataHandler)
        {
            this.jsonDataHandler = jsonDataHandler;
            this.Employees = this.GetEmployees();
        }

        public string GenerateNewId()
        {
            string? lastId = Employees.Last().Id;
            if (lastId == null)
            {
                return "TZ0001";
            }
            return "TZ" + (int.Parse(lastId.Substring(2)) + 1).ToString();
        }
        


        public Employee AddEmployee(Employee employee)
        {
            Employees.Add(employee);
            jsonDataHandler.UpdateDataToJson<Employee>(Employees);
            return employee;
        }

        public Employee? DeleteEmployee(string empId)
        {
            Employee? employee = Employees.Find(emp => emp.Id == empId);
            if (employee != null)
            {
                employee.IsDeleted = true;
                jsonDataHandler.UpdateDataToJson<Employee>(Employees);
                Employees.Remove(employee);
                return employee;
            }
            else
            {
                return null;
            }
        }

        public Employee UpdateEmployee(Employee newEmployee)
        {

            Employee? existingEmployee = Employees.Find((emp) => emp.Id == newEmployee.Id);

            if (existingEmployee != null)
            {
                existingEmployee = newEmployee;
            }
            jsonDataHandler.UpdateDataToJson<Employee>(Employees);
            return newEmployee;
           
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = [];
            try
            {
                employees = jsonDataHandler.GetDataFromJson<Employee>();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            employees = employees.Where(emp => emp.IsDeleted == false).ToList();
            return employees;
        }

        public Employee GetEmployeeById(string id)
        {
            return Employees.Find((emp) => emp.Id == id)!;
        }
    }
}
