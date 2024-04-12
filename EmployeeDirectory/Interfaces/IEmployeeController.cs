﻿using EmployeeDirectory.Models;
using EmployeeDirectory.ViewModel;
namespace EmployeeDirectory.UI.Interfaces
{
    public interface IEmployeeController
    {
        List<EmployeeView> ViewEmployees();
        EmployeeView? ViewEmployee(string empId);
        string GetNewEmployeeId(string firstName, string lastName);
        Employee? AddEmployee(Employee employee);
        Employee? EditEmployee(Employee employee);
        Employee? DeleteEmployee(string empId);
        
        Employee? GetEmployeeById(string empId);
    }
}