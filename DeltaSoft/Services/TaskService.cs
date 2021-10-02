using DeltaSoft.Helper;
using DeltaSoft.Models;
using DeltaSoft.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace DeltaSoft.Services
{
    public class TaskService
    {
        DeltaSoftContext _db;
        private readonly IUriService _uriService;
        public TaskService(DeltaSoftContext db, IUriService uriService)
        {
            _db = db;
            _uriService = uriService;

        }

        //get all tasks
        public List<TaskTable> GetAllTasks()
        {
            var tasks = _db.TaskTables.Include(a=>a.ApplicationUsers).OrderByDescending(a=>a.TaskId).ToList();

           return tasks;

        }
        //get all employee
        public List<ApplicationUser> GetAllEmployees()
        {
            var emps = _db.ApplicationUsers.ToList();

            return emps;

        }


        // edit Task state 


        public TaskTable EditTaskState(bool state, int id)
        {
            TaskTable task = _db.TaskTables.Include(r => r.ApplicationUsers).FirstOrDefault(s => s.TaskId == id);
            task.TaskState = state;
            _db.SaveChangesAsync();
            return task;
        }


        //add new task
        public async Task<Response> AddTask(TaskTable taskTable)
        {
           
            try
            {
                _db.TaskTables.Add(taskTable);
                _db.SaveChanges();
            }
            catch (Exception e)
            {

            }
            //var result=_db.ApplicationUsers.FirstOrDefault(a => a.Id == empId);
            //if (result != null)
            //{
            //    //_db.UserTasks.Add(new UserTask { TaskId = taskTable.TaskId, UserId = spicificEmp });
            //    _db.SaveChanges();
            //    return new Response { Status = "Success", Message = "Task added successfully", data = taskTable };
            //}
            return new Response { Status = "Error2", Message = "Task Not Found" };
        }

        

        //add new task
        //public async Task<Response> AddNewTask(TaskTable taskTable, IIdentity admin,string empId)
        //{
        //    var emp = HelperMethods.GetAuthnticatedUserId(admin);
        //    try
        //    {
        //        _db.TaskTables.Add(taskTable);
        //        _db.SaveChanges();
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //    _db.UserTasks.Add(new UserTask { TaskId = taskTable.TaskId, UserId = empId });
        //    _db.SaveChanges();
        //    return new Response { Status = "Success", Message = "Task added successfully", data = taskTable };
        //}





        //get task by id
        public TaskTable GetTaskById(int id)
        {
            return _db.TaskTables.Where(b => b.TaskId == id ).FirstOrDefault();
        }



        //delete Task
        public Response DeleteTask(int id)
        {
            TaskTable taskDetails = GetTaskById(id);
            if (taskDetails != null)
            {
                _db.Remove(taskDetails);
                _db.SaveChanges();
                return new Response { Status = "Success", Message = "Task Deleted successully" };
            }
            return new Response { Status = "Error2", Message = "Task Not Found" };

        }

       

    }
}
