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

        public ProjectController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IEnumerable<Project> GetProject()
        {
            return _context.Projects.ToList();
        }

        [HttpGet("{projectId}", Name = "projectCreated")]
        public IActionResult GetById(Guid projectId)
        {
            var project = _context.Projects.Include(x => x.ProjectTasks).FirstOrDefault(p => p.ProjectId == projectId);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
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
        public IActionResult PutProject([FromBody] Project project, Guid projectId)
        {
            if (project.ProjectId != projectId)
                return BadRequest();

            var objectToModified = _context.Attach(project);
            objectToModified.Property(x => x.ProjectName).IsModified = true;
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{projectId}")]
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