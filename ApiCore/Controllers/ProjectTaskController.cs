using System;
using System.Collections.Generic;
using System.Linq;
using ApiCore.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Controllers
{
    [Route("api/Project/{projectId}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectTaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // Returns all the Tasks assigned to ProjectId
        // Route -> api/Project/{projectId}/ProjectTask
        public IEnumerable<ProjectTask> GetAllProjectTask(Guid projectId)
        {
            return _context.ProjectTasks.Where(x=>x.ProjectId == projectId).ToList();
        }


        [HttpGet("{projectTaskId}", Name = "projectTaskCreated")]
        // Returns the information of an specific Task.
        // Route -> api/Project/{projectId}/ProjectTask/{ProjectTaskId}
        public IActionResult GetByIdProjectTask(Guid projectTaskId)
        {
            var projectTask = _context.ProjectTasks.FirstOrDefault(p => p.ProjectTaskId == projectTaskId);

            if (projectTask == null)
                return NotFound();

            return Ok(projectTask);
        }

        [HttpPost]
        // Create a Task assigned to ProjectId
        // Route -> api/Project/{projectId}/ProjectTask/
        public IActionResult PostProjectTask([FromBody] ProjectTask projectTask, Guid projectId)
        {
            projectTask.ProjectId = projectId;
            if (ModelState.IsValid)
            {
                projectTask.ProjectTaskId = Guid.NewGuid();
                _context.ProjectTasks.Add(projectTask);
                _context.SaveChanges();

                return new CreatedAtRouteResult("projectTaskCreated", new { projectTaskId = projectTask.ProjectId }, projectTask);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{projectTaskId}")]
        // Updates the information of an specific Task.
        // Route -> api/Project/{projectId}/ProjectTask/{ProjectTaskId}
        public IActionResult PutProjectTask([FromBody] ProjectTask projectTask, Guid projectTaskId)
        {
            if (projectTask.ProjectTaskId != projectTaskId)
                return BadRequest();

            var objectToModified = _context.Attach(projectTask);
            objectToModified.Property(x => x.Task).IsModified = true;
            objectToModified.Property(x => x.StartDate).IsModified = true;
            objectToModified.Property(x => x.EndDate).IsModified = true;
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{projectTaskId}")]
        // Soft Delete a Task changing the IsDeleted property to true if exists.
        // Route -> api/Project/{projectId}/ProjectTask/{ProjectTaskId}
        public IActionResult DeleteProjectTask(Guid projectTaskId)
        {
            var projectTask = _context.ProjectTasks.FirstOrDefault(x => x.ProjectTaskId == projectTaskId);

            if (projectTask == null)
                return NotFound();

            projectTask.IsDeleted = true;
            var objectToModified = _context.Attach(projectTask);
            objectToModified.Property(x => x.IsDeleted).IsModified = true;
            _context.SaveChanges();

            return Ok(projectTask);
        }
    }
}