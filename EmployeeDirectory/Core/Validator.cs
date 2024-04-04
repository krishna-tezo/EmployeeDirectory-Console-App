using EmployeeDirectory.Interfaces;
using System.Text.RegularExpressions;
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
            }
            else
            {
                string pattern;
                switch (parameter)
                {
                    case "empId":
                        pattern = @"TZ\d{4}";
                        Match m = Regex.Match(value, pattern);

                        if (employeeService.GetEmployeeById(value) != null)
                        {
                            return ValidationResult.Fail("Id already exists");


                        }
                        else if (!m.Success)
                        {
                            return ValidationResult.Fail("Invalid Id format");
                        }
                        break;
                    case "email":
                        pattern = @"^[\w-\.]+@([\w]+\.)+[\w]{2,4}$";
                       
                        if (!Regex.Match(value, pattern).Success)
                        {
                            return ValidationResult.Fail("Invalid email format");
                        }
                        break;
                    case "mobileNumber":
                        pattern = @"\d{10}";

                        if (!Regex.Match(value, pattern).Success)
                        {
                            return ValidationResult.Fail("Invalid Number format");
                        }
                        break;
                    default:
                        return ValidationResult.Success();
                }
            }
            return ValidationResult.Success();
        }
    }
}
