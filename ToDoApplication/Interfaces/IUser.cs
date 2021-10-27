namespace ToDoApplication.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IUser
    {
        string Username { get; set; }

        string Password { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        
        
    }
}
