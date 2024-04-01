namespace EmployeeDirectory.Models
{
    public class Employee
    {
        //Id
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Dob { get; set; }  //datetime
        public string MobNumber { get; set; }  //fullname
        public string ManagerName { get; set; }
        public string ProjectName { get; set; }
        public string JoinDate { get; set; }
        public string RoleId { get; set; }


        public override string ToString()
        {
            return "EmpId: " + Id + ", Name: " + FirstName + " " + LastName + ", RoleId: " + RoleId;
        }
    }
}