using EmployeeDirectory.Core;
namespace EmployeeDirectory.Interfaces
{
    internal interface IValidator
    {
        ValidationResult IsValidInput(string? value, string parameter = "");
    }
}