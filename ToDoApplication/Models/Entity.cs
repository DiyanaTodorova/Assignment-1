namespace ToDoApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ToDoApplication.Interfaces;

    public abstract class Entity : IEntity
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatorId { get; set; }

        public DateTime LastChangedOn { get; set; }

        public int IdUserLastChange { get; set; }

        public Entity(int creatorID)
        {            
            this.CreatedAt = DateTime.Now;
            this.CreatorId = creatorID;
            this.LastChangedOn = DateTime.Now;
            this.IdUserLastChange = creatorID;
        }
    }
}
