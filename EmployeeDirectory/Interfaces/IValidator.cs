namespace EmployeeDirectory.Interfaces
{
    internal interface IValidator
    {
        int IsValidInput(string value, string parameter = "");
        string ShowErrorMessage(int errorCode);
    }
}