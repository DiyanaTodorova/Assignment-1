namespace ToDoApplication.Models
{
    using System;
    using ToDoApplication.Interfaces;
    using System.Collections.Generic;
    using System.Text;

    public class User : Entity, IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Role Role { get; set; }

        public List<IToDoList> lists { get; set; }

        public List<Task> AssignedTasks { get; set; }



        public User(int id, string username, string password, string firstName, string lastName, Role role, int idCreator)
            : base(idCreator)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Role = role;
            this.lists = new List<IToDoList>();

        }

        public User():base(1)
        {
            this.Id = 1;
            this.Username = "user";
            this.Password = "adminpassword";
            this.FirstName = "Ivan";
            this.LastName = "Ivanov";
            this.Role = Role.Admin;
            this.lists = new List<IToDoList>();
            

        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            // todo

            return sb.ToString();
                
        }


    }
}
