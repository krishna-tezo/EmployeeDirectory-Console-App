using EmployeeDirectory.UI.Menus;

namespace EmployeeDirectory.UI
{
    public class MainMenu
    {
        public static void ShowMainMenu()
        {
            Console.Clear();
            EmployeeMenu employeeMenu = new EmployeeMenu();
            RoleMenu roleMenu = new RoleMenu();
            string choice;
            //update the name
            bool loopMenu = true;
            while (loopMenu)
            {
                Console.WriteLine("\nMain Menu\n");
                Console.WriteLine("1. Employee Management\n2. Role Management\n3. Exit\n");
                Console.Write("\nChoose Any option:");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        employeeMenu.ShowEmployeeMenu();
                        break;
                    case "2":
                        roleMenu.ShowRoleMenu();
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