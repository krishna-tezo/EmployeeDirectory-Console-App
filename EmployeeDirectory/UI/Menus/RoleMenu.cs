using EmployeeDirectory.Models;
using EmployeeDirectory.UI.UIServices;

namespace EmployeeDirectory.UI.Menus
{
    internal class RoleMenu
    {
        public void ShowRoleMenu()
        {
            UIService uiService = new UIService();
            List<Role> roles = new List<Role>();
            string roleId = string.Empty;
            Console.WriteLine("\nWelcome to Role Management\n");
            string choice;
            bool loopMenu = true;
            Console.Clear();
            while (loopMenu)
            {
                Console.WriteLine("\nRole Menu\n");
                Console.WriteLine("1. Add Role");
                Console.WriteLine("2. Display All Roles");
                Console.WriteLine("3. Go Back");
                Console.Write("\nChoose Any option:");

                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        uiService.AddRole();

                        break;
                    case "2":
                        uiService.ViewAllRoles();

                        break;
                    case "3":
                        loopMenu = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Input! Please Re-Enter");
                        break;
                }
            }
        }
    }
}
