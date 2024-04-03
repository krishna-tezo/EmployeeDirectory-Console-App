using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Models;
using System.Text.RegularExpressions;

namespace EmployeeDirectory.Services
{
    internal class Validator : IValidator
    {
        private IEmployeeService employeeService;

        public Validator(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public int IsValidInput(string value, string parameter = "")
        {
            if (value == null || value.Equals(""))
            {
                
                return 0;   //Blank Value
            }
            else
            {
                if (parameter.Equals("empId"))
                {
                    string pattern = @"TZ\d{4}";
                    Match m = Regex.Match(value, pattern);

                    if (employeeService.DoesEmployeeIdExist(value))
                    {   
                        return -1;  //Id Already Exists
                    }
                    else if (!m.Success)
                    {
                        return -2;   //Invalid Format
                    }
                }
            }
            return 1;
        }

        public string ShowErrorMessage(int errorCode)
        {

            switch (errorCode)
            {
                case -2:
                    return "Invalid Format";
                case -1:
                    return "Id already exists";
                case 0:
                    return "Please provide some value.";
                default:
                    return "Unexpected Error";
            }

        }



    }
}
