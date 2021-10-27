namespace ToDoApplication.Interfaces
{
    using System;
    using System.Collections.Generic;
    using ToDoApplication.Models;
    using System.Text;

    public interface IToDoList
    {
        string Title { get; }

        List<Task> Tasks { get; set; }

    }
}
