using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoApplication.Interfaces;
using ToDoApplication.Models;

namespace ToDoApplication.Services
{
    public class ToDoListsManagementService
    {
        public List<ToDoList> lists = new List<ToDoList>();       

              
        public string ListAllLists()
        {
            StringBuilder sb = new StringBuilder();

            foreach(var list in this.lists)
            {
                sb.Append(ListToString(list));
            }

            return sb.ToString();
        }

        public void CreateToDoList(string title, int creatorId)
        {
            ToDoList list = new ToDoList(this.lists.Count+1, title, creatorId);
            this.lists.Add(list); 
        }

        public void DeleteToDoList(int id, int userId)
        {
            bool excist = CheckIfListExcist(id);
            bool owned = CheckIfListIsOwnedOrShared(id, userId);
            

            if(excist & owned)
            {
                var list = this.lists.FirstOrDefault(x => x.Id == id);
                this.lists.Remove(list);
            }

        }

        public void EditToDoList(int id, string title, int userId)
        {

            bool excist = CheckIfListExcist(id);
            bool owned = CheckIfListIsOwnedOrShared(id, userId);


            if (excist & owned)
            {
                var list = this.lists.FirstOrDefault(x => x.Id == id);
                this.lists.Remove(list);

                list.Title = title;
                list.LastChangedOn = DateTime.Now;
                list.IdUserLastChange = userId;

                this.lists.Add(list);
            }
        }

        //are users list? 
        public void ShareList(int id, int currentUserId, List<int> userIds)
        {
            
            bool excist = CheckIfListExcist(id);
            bool owned = CheckIfListIsOwnedOrShared(id, currentUserId);


            if (excist & owned)
            {
                var list = this.lists.FirstOrDefault(x => x.Id == id);
                //this.lists.Remove(list);

                list.SharedWith.AddRange(userIds);
            }
        }
        private bool CheckIfListIsOwnedOrShared(int listId,int userId)
        {
           
            var list = this.lists.FirstOrDefault(x => x.Id == listId);

            if(list.CreatorId == userId)
            {
                return true;
            }

            if(list.SharedWith.Contains(userId))
            {
                return true;
            }

            return false;
        }
        private bool CheckIfListExcist(int id)
        {
            bool result = false;

            var list = this.lists.FirstOrDefault(x => x.Id == id);

            if(list != null)
            {
                result = true;
            }

            return result;
        }

        public string ListTasks(int listId, int userId)
        {
            bool listExcist = CheckIfListExcist(listId);
            bool listOwned = CheckIfListIsOwnedOrShared(listId, userId);

            if (listExcist & listOwned)
            {
                List<Task> tasks=  this.lists.FirstOrDefault(x => x.Id == listId).Tasks;
                StringBuilder sb = new StringBuilder();

                foreach(var task in tasks)
                {
                    sb.Append(TaskToString(task));
                }

                return sb.ToString();
            }


            return null;
            
        }

        public void CreateTask(int listId, string title, string description, int creatorId)
        {
            bool listExcist = CheckIfListExcist(listId);
            bool listOwned = CheckIfListIsOwnedOrShared(listId, creatorId);
            

            if (listExcist & listOwned)
            {
                int id = this.lists.FirstOrDefault(x => x.Id == listId).Tasks.Count +1;
                var task = new Task(id, listId, title, description, false, creatorId);
                this.lists.FirstOrDefault(x => x.Id == listId).Tasks.Add(task);
            }
        }

        public void DeleteTask(int id, int userId)
        {
            int listId = 0;
            Task taskToBeDeleted = null;
                
             foreach(var list in this.lists)
            {
                foreach(Task task in list.Tasks)
                {
                    if(task.Id == id)
                    {
                        listId = list.Id;
                        taskToBeDeleted = task; 
                    }
                }
            }

            bool listExcist = CheckIfListExcist(listId);
            bool listOwned = CheckIfListIsOwnedOrShared(listId,userId);

            if(listExcist & listOwned)
            {
                this.lists.FirstOrDefault(x => x.Id == listId).Tasks.Remove(taskToBeDeleted);
            }
        }
        //not ok, better way?
        public void EditTask(int id, string title, string description, int userId)
        {
            int listId = 0;
            Task taskToBeEdited = null;

            foreach (var list in this.lists)
            {
                foreach (Task task in list.Tasks)
                {
                    if (task.Id == id)
                    {
                        listId = list.Id;
                        taskToBeEdited = task;
                    }
                }
            }

            bool listExcist = CheckIfListExcist(listId);
            bool listOwned = CheckIfListIsOwnedOrShared(listId, userId);

            if(listExcist & listOwned )
            {
                this.lists.FirstOrDefault(x => x.Id == listId).Tasks.Remove(taskToBeEdited);

                taskToBeEdited.Title = title;
                taskToBeEdited.Description = description;                
                taskToBeEdited.LastChangedOn = DateTime.Now;
                taskToBeEdited.IdUserLastChange = userId;

                this.lists.FirstOrDefault(x => x.Id == listId).Tasks.Add(taskToBeEdited);

            }
        }

        public void AssignTask(int idTask, int userTorecieveTask, int  currentUserId)
        {
            foreach (var list in this.lists)
            {
                foreach (Task task in list.Tasks)
                {
                    if (task.Id == idTask)
                    {
                        if (!task.AssignedUsersIds.Contains(userTorecieveTask))
                        {
                            task.AssignedUsersIds.Add(userTorecieveTask);
                        }

                    }
                }
            } 

             
        }
        //assigned only or any task in owned list?
        public void CompleteTask(int id, int userId)
        {
            int listId = 0;


            foreach (var list in this.lists)
            {
                foreach(var task in list.Tasks)
                {
                    if(task.Id == id)
                    {
                        listId = task.ListId;
                    }
                }
            }

            bool listOwned = CheckIfListIsOwnedOrShared(listId, userId);
            
            if(listOwned)
            {
                this.lists.FirstOrDefault(x => x.Id == listId)
                    .Tasks.FirstOrDefault(x => x.Id == id).IsCompleted = true;
            }
        }

        private string TaskToString(Task task)
        {
            StringBuilder sb = new StringBuilder();
            string users = string.Join(",", task.AssignedUsersIds.Select(n => n.ToString()).ToArray());

            sb.AppendFormat($"Id: {task.Id}\n");
            sb.AppendFormat($"Title: {task.Title}\n");
            sb.AppendFormat($"Description: {task.Description}\n");
            sb.AppendFormat($"Is complited: {task.IsCompleted}\n");
            sb.AppendFormat($"Assigned users ids: {users}\n");
            sb.AppendFormat($"Created at:  {task.CreatedAt}\n");
            sb.AppendFormat($"Created by user with id: {task.CreatorId}\n");
            sb.AppendFormat($"Last changed at: {task.LastChangedOn}\n");
            sb.AppendFormat($"Last changed by user with id: {task.IdUserLastChange}\n");
            sb.AppendFormat($"     **********\n");

            return sb.ToString();

        }
        private string ListToString(ToDoList list)
        {
            StringBuilder sb = new StringBuilder();
            string shared = string.Join(",", list.SharedWith.Select(n => n.ToString()).ToArray());

            sb.AppendFormat($"Id: {list.Id}\n");
            sb.AppendFormat($"Title: {list.Title}\n");
            sb.AppendFormat($"Shared with: {shared}\n");
            sb.AppendFormat($"Created at:  {list.CreatedAt}\n");
            sb.AppendFormat($"Created by user with id: {list.CreatorId}\n");
            sb.AppendFormat($"Last changed at: {list.LastChangedOn}\n");
            sb.AppendFormat($"Last changed by user with id: {list.IdUserLastChange}\n");
            sb.AppendFormat($"     **********\n");

            return sb.ToString();

        }



    }
}
