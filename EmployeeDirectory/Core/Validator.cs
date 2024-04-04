using EmployeeDirectory.Interfaces;
using System.Text.RegularExpressions;
using EmployeeDirectory.Core;
namespace EmployeeDirectory.Core
{
    internal class Validator : IValidator
    {
        private IEmployeeService employeeService;

        public Validator(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public ValidationResult IsValidInput(string value, string parameter = "")
        {
            if (value == null || value.Equals(""))
            {
                return ValidationResult.Fail("Blank Value");
                //return 0;   //Blank Value
            }
            else
            {
                if (parameter.Equals("empId"))
                {
                    string pattern = @"TZ\d{4}";
                    Match m = Regex.Match(value, pattern);

                    if (employeeService.DoesEmployeeIdExist(value))
                    {
                        return ValidationResult.Fail("Id already exists");

                        
                    }
                    else if (!m.Success)
                    {
                        return ValidationResult.Fail("Invalid Id format");
                    }
                }
            }
            return ValidationResult.Success();
        }

    }
}
