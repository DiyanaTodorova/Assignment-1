namespace ToDoApplication.Models
{
    using System;
    using ToDoApplication.Interfaces;
    using System.Collections.Generic;
    using System.Text;

    public class ToDoList : Entity, IToDoList
    {
        public List<int> SharedWith { get; private set; }
        public string Title { get;  set; }
        public List<Task> Tasks { get; set; }        
        public ToDoList( int id, string title, int creatorID)
            :base(creatorID)
        {
            this.Id =id;
            this.Title = title;
            this.Tasks = new List<Task>();
            this.SharedWith = new List<int>();
        }

       
    }
}
