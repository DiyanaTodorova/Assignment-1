namespace ToDoApplication.Interfaces
{

    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ITask
    {
        int ListId { get;}

        string Description { get;}

        bool IsCompleted { get;}
    }
}
