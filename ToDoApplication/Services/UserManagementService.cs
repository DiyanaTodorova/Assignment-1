using System;
using System.Collections.Generic;
using System.Text;
using ToDoApplication.Models;
using System.Linq;
using ToDoApplication.Data;

namespace ToDoApplication.Services
{
    public  class UserManagementService
    {
        private readonly List<User> users = new List<User>();

        private const string StoreFileName = "Users.json";

        public User CurrentUser { get; private set; }

        private readonly FileDatabase storage;       


        public UserManagementService()
        {
            storage = new FileDatabase();
            List<User> usersFromFile = storage.Read<List<User>>(StoreFileName);

            if (usersFromFile == null)
            {
                CreateFirstUser("user", "admin","Ivan", "Ivanov");
            }
            else
            {
                this.users = usersFromFile;
            }

        }

        private void SaveToFile()
        {
            storage.Write(StoreFileName, this.users);
        }

        // should it be a string or list?
        public string ListUsers()
        {
            StringBuilder sb = new StringBuilder();

            foreach( var user in this.users)
            {
                sb.Append(UserToString(user));
            }

            return sb.ToString();
        }

        public bool CreateUser(string username, string password,string firstName, string lastName, Role role)
        {
            if(this.users.Any(x=>x.Username == username))
            {
                return false;
            }
            int id = this.users.Count + 1; 
            User user = new User(id, username, password, firstName, lastName, role, CurrentUser.Id);

            this.users.Add(user);
            SaveToFile();

            return true;
        }

        public void CreateFirstUser( string username,string password, string firstName, string lastName)
        {
            int id = this.users.Count + 1;
            User user = new User(id, username, password, firstName, lastName,Role.Admin, 1);

            this.users.Add(user);
            SaveToFile();
        }

        public void DeleteUser(int Id)
        {
            this.users.Remove(this.users.FirstOrDefault(x => x.Id == Id));
            SaveToFile();
        }

        // is it ok? should id be the only parameter 
        // maybe other way to edit
        public void EditUser(int Id, string username, string password, string firstName, string lastName)
        {
            var user = this.users.FirstOrDefault(x => x.Id == Id);
            this.users.Remove(user);

            user.Username = username;
            user.Password = password;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.IdUserLastChange = CurrentUser.Id;
            user.LastChangedOn = DateTime.Now;

            this.users.Add(user);
            SaveToFile();

        }

        public void LogIn(string username)
        {
            CurrentUser = this.users.FirstOrDefault(x => x.Username == username);
        }

        public void LogOut()
        {
            CurrentUser = null;
        }

        private string UserToString(User user)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"Username: {user.Username}\n");
            sb.Append($"First name: {user.FirstName}\n");
            sb.Append($"Last name: {user.LastName}\n");
            sb.Append($"Role: {user.Role}\n");
            sb.Append($"Created by user with id: {user.CreatorId}\n");
            sb.Append($"Created on: {user.CreatedAt}\n");
            sb.Append($"Last changed by user with id: {user.IdUserLastChange}\n");
            sb.Append($"Last changed on {user.LastChangedOn}\n");
            sb.Append($"          **********\n");

            return sb.ToString();
        }
    }
}
