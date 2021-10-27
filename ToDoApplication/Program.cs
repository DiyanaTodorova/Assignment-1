namespace ToDoApplication
{
    using System;
    using ToDoApplication.Interfaces;
    using ToDoApplication.Services;
    using ToDoApplication.Models;
    using System.Collections.Generic;
    class Program
    {

        private static UserManagementService userService =  new UserManagementService();
        private static ToDoListsManagementService listaAndTasksService = new ToDoListsManagementService();
        static void Main(string[] args)
        {

            if (args.Length > 0)
            {
                userService.LogIn(args[0]);
            }
            bool shouldExit = false;
            while (!shouldExit)
            {
                shouldExit = MainMenu();
            }
        }

        private static void RenderMenu()
        {
            Console.WriteLine("----------ToDo Lists Managent----------");

            if(userService.CurrentUser == null)
            {
                Console.WriteLine("1.Log In");
                LogIn();
                
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You are logged in as: {userService.CurrentUser.Username}");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("1. LogOut");
                Console.WriteLine("2. Create Task");
                Console.WriteLine("3. Edit Task");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("5. Assign Task");
                Console.WriteLine("6. List Tasks In List ");
                Console.WriteLine("7. Complete Task");
                Console.WriteLine("8. List Lists");
                Console.WriteLine("9. Create List");
                Console.WriteLine("10. Edit List");
                Console.WriteLine("11. Delete List");
                Console.WriteLine("12. Share List");

                if(userService.CurrentUser.Role==(Role) 1)
                {
                    Console.WriteLine("13. Create user");
                    Console.WriteLine("14. Edit user");
                    Console.WriteLine("15. Delete user");
                    Console.WriteLine("16. List users");
                }
                Console.WriteLine("0.Exit");
                
            }
        }

        private static bool MainMenu()
        {
            RenderMenu();

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    if (userService.CurrentUser == null)
                    {
                        LogIn();
                        MainMenu();
                    }
                    else
                    {
                        LogOut();
                        RenderMenu();
                    }
                    return false;
                case "2":
                    CreateTask();
                    return false;
                case "3":
                    EditTask();
                    return false;
                case "4":
                   DeleteTask();
                    return false;
                case "5":
                    AssignTask();
                    return false;
                case "6":
                    ListTasks();
                    return false;
                case "7":
                    CompleteTask();
                    return false;
                case "8":
                    ListLists();
                    return false;
                case "9":
                    CreateList();
                    return false;
                case "10":
                    EditLists();
                    return false;
                case "11":
                    DeleteLists();
                    return false;
                case "12":
                    ShareLists();
                    return false;
                case "13":
                    CreateUser();
                    return false;
                case "14":
                    EditUser();
                    return false;
                case "15":
                    DeleteUser();
                    return false;
                case "16":
                    ListUsers();
                    return false;
                case "0":
                    
                    return true;
                default:
                    Console.WriteLine("Unknown Command");
                    return false;
            }

        }

        private static void Exit()
        {
            throw new NotImplementedException();
        }

        private static void ListUsers()
        {
            Console.WriteLine(userService.ListUsers());
        }
        private static void DeleteUser()
        {
            Console.WriteLine("Enter id:");
            int id = int.Parse(Console.ReadLine());
            userService.DeleteUser(id);
        }

        private static void EditUser()
        {
            Console.WriteLine("Enter id:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();
            Console.WriteLine("Enter first name:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter last name:");
            string lastName = Console.ReadLine();
            userService.EditUser(id,username, password, firstName, lastName);
        }

        private static void CreateUser()
        {
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();
            Console.WriteLine("Enter first name:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter last name:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter role:");
            Role role =(Role)Enum.Parse(typeof (Role),Console.ReadLine());

            userService.CreateUser(username, password, firstName, lastName, role);
        }

        private static void ShareLists()
        {
            Console.WriteLine("Enter list id:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter number of users to share with:");
            int num = int.Parse(Console.ReadLine());
            List<int> usersIds = new List<int>();

            for(int i=0;i<num;i++)
            {
                Console.WriteLine("Enter user id:");
                int user = int.Parse(Console.ReadLine());
                usersIds.Add(user);
            }

            listaAndTasksService.ShareList(id, userService.CurrentUser.Id, usersIds);
        }

        private static void DeleteLists()
        {
            Console.WriteLine("Enter id:");
            int id = int.Parse(Console.ReadLine());
            listaAndTasksService.DeleteToDoList(id, userService.CurrentUser.Id);
        }

        private static void EditLists()
        {
            Console.WriteLine("Enter list id:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter new title:");
            string title = Console.ReadLine();    
            listaAndTasksService.EditToDoList(id,title,userService.CurrentUser.Id );
        }

        private static void CreateList()
        {
            Console.WriteLine("Enter list title:");
            string title = Console.ReadLine();

            listaAndTasksService.CreateToDoList(title, userService.CurrentUser.Id);
        }

        private static void ListLists()
        {
            Console.WriteLine(listaAndTasksService.ListAllLists());
        }

        private static void CompleteTask()
        {
            Console.WriteLine("Enter task id:");
            int id = int.Parse(Console.ReadLine());
            listaAndTasksService.CompleteTask(id, userService.CurrentUser.Id);
        }

        private static void ListTasks()
        {
            Console.WriteLine("Enter list id:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine(listaAndTasksService.ListTasks(id, userService.CurrentUser.Id));
        }

        private static void AssignTask()
        {
           Console.WriteLine("Enter task id:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter number of user to be assigned:");
            int num = int.Parse(Console.ReadLine());
            
            for(int i=0; i<num;i++)
            {
                Console.WriteLine( "Enter user id:" );
                int idUser = int.Parse(Console.ReadLine());
                listaAndTasksService.AssignTask(id, idUser, userService.CurrentUser.Id);

            }

            
        }

        private static void DeleteTask()
        {
            Console.WriteLine("Enter task id:");
            int id = int.Parse(Console.ReadLine());
            listaAndTasksService.DeleteTask(id, userService.CurrentUser.Id);
        }

        private static void EditTask()
        {
            Console.WriteLine("Enter task id:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter task title: ");
            string title = Console.ReadLine();
            Console.WriteLine("Enter task description: ");
            string description = Console.ReadLine();
            listaAndTasksService.EditTask(id, title, description, userService.CurrentUser.Id);
        }

        private static void CreateTask()
        {
            Console.WriteLine("Enter list Id:");
            int listId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter task title: ");
            string title = Console.ReadLine();
            Console.WriteLine("Enter task description: ");
            string description = Console.ReadLine();
            listaAndTasksService.CreateTask(listId,title, description, userService.CurrentUser.Id);
        }

        private static void LogOut()
        {
            userService.LogOut();
        }

        private static void LogIn()
        {
            Console.WriteLine("Enter your user name:");
            string userName = Console.ReadLine();
            userService.LogIn(userName);
            if (userService.CurrentUser == null)
            {
                Console.WriteLine("Login failed.");
            }
            else
            {
                Console.WriteLine("Login successful.");
            }
        }
    }
}
