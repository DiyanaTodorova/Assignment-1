namespace ToDoApplication.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IEntity
    {
        int Id { get; }

        DateTime CreatedAt { get; } 

        int CreatorId { get; }

        DateTime LastChangedOn { get; }

        int IdUserLastChange { get; }
    }
}
