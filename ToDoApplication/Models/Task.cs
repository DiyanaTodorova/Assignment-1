namespace ToDoApplication.Models
{
    using System;
    using ToDoApplication.Models;
    using ToDoApplication.Interfaces;
    using System.Collections.Generic;
    using System.Text;

    public class Task : Entity, ITask
    {
        
        public int ListId { get ;  set; }
        public string Title { get; set; }
        public string Description { get; set ; }
        public bool IsCompleted { get;  set; }

        public List<int> AssignedUsersIds { get; private set; }

        

        public Task(int id,int listId, string title, string description, bool isComplited, int creatorId)
            :base(creatorId)
        {
            this.Id = id;
            this.ListId = listId;
            this.Title = title;
            this.Description = description;
            this.IsCompleted = isComplited;
            this.AssignedUsersIds = new List<int>();
            
        }

        
    }
}
