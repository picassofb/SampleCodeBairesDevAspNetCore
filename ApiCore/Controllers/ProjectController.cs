using System;
using System.Collections.Generic;
using System.Linq;
using ApiCore.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        // Dependency injection to bring ApplicationDbContext and use EF

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // Return all the projects
        // Route -> api/project/
        public IEnumerable<Project> GetProject()
        {
            return _context.Projects.ToList();
        }


        [HttpGet("{projectId}", Name = "projectCreated")]
        // This Obtains the project information including all the tasks that has been assigned to it. 
        // Route -->  api/project/{ProjectId}
        public IActionResult GetById(Guid projectId)
        {
            var project = _context.Projects.Include(x => x.ProjectTasks).FirstOrDefault(p => p.ProjectId == projectId);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        // Creates a new Project
        // Route -> api/project/
        public IActionResult PostProject([FromBody] Project project)
        {
            if (ModelState.IsValid)
            {
                project.ProjectId = Guid.NewGuid();
                _context.Projects.Add(project);
                _context.SaveChanges();

                return  new CreatedAtRouteResult("projectCreated", new{ projectId = project.ProjectId }, project);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{projectId}")]
        // Updates an existing Project
        // Route -> api/project/{projectId}
        public IActionResult PutProject([FromBody] Project project, Guid projectId)
        {
            if (project.ProjectId != projectId)
                return BadRequest();

            var objectToModified = _context.Attach(project);
            objectToModified.Property(x => x.ProjectName).IsModified = true;
            objectToModified.Property(x => x.IsDone).IsModified = true;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{projectId}")]
        // Soft Delete a project changing the IsDeleted property to true if exists.
        // Route -> api/project/{projectId}
        public IActionResult DeleteProject(Guid projectId)
        {
            var project = _context.Projects.FirstOrDefault(x => x.ProjectId == projectId);

            if (project == null)
                return NotFound();

            project.IsDeleted = true;

            var objectToModified = _context.Attach(project);
            objectToModified.Property(x => x.IsDeleted).IsModified = true;

            _context.SaveChanges();
            return Ok(project);
        }
    }
}