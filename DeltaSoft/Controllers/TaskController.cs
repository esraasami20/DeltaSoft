using DeltaSoft.DTO;
using DeltaSoft.Models;
using DeltaSoft.Services;
using DeltaSoft.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;

        private IAuthentication _authentication;
        private TaskService _taskService;

        public TaskController(UserManager<ApplicationUser> userManager, IAuthentication authentication, TaskService taskService)
        {
            this.userManager = userManager;
            _authentication = authentication;
            _taskService = taskService;
        }



        //get all tasks
        [HttpGet]
        public ActionResult<TaskTable> GetAllTasks()
        {
            var result = _taskService.GetAllTasks();
            if (result != null)
                return Ok(result);
            return NotFound();
        }

        //get all tasks
        [HttpGet("emps")]
        public ActionResult<ApplicationUser> GetAllEmps()
        {
            var result = _taskService.GetAllEmployees();
            if (result != null)
                return Ok(result);
            return NotFound();
        }



        // get task by id
        [HttpGet("{id}")]
        public ActionResult<TaskTable> GetTaskById(int id)
        {
            var result = _taskService.GetTaskById(id);
            if (result != null)
                return Ok(result);
            return NotFound();
        }



        //add task
        [HttpPost]
        //[Authorize(Roles = ("Admin"))] // it working on post man I disable it so you can work with front so easly
        public async Task<ActionResult<TaskTable>> AddTask(TaskTable taskTable)
        {
            if (ModelState.IsValid)
            {

                var result = await _taskService.AddTask(taskTable);
                if (result != null)
                    return Ok(taskTable);
                return StatusCode(StatusCodes.Status500InternalServerError, "Try again");
            }
            return BadRequest(ModelState);
        }

        //edit Task state
        [HttpPut("{id}")]
        public ActionResult EditCustomerAddress([FromBody] EditTaskState state,int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                _taskService.EditTaskState(state.TaskState,id);
                return NoContent();
            }
        }



        //delete task
        [HttpDelete("{id}")]
        public ActionResult<TaskTable> DeleteTask(int id)
        {
            var result = _taskService.DeleteTask(id);
            if (result.Status == "Success")
                return NoContent();
            else if (result.Status == "Error")
                return new StatusCodeResult(StatusCodes.Status405MethodNotAllowed);
            else
                return NotFound();

        }

    }
}
