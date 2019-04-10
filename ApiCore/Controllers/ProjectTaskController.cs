﻿using System;
using System.Collections.Generic;
using System.Linq;
using ApiCore.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            this._context = context;
        }

        [HttpGet]
        public IEnumerable<ProjectTask> GetAllProjectTask(Guid projectId)
        {
            return _context.ProjectTasks.Where(x=>x.ProjectId == projectId).ToList();
        }


        [HttpGet("{projectTaskId}", Name = "projectTaskCreated")]
        public IActionResult GetByIdProjectTask(Guid projectTaskId)
        {
            var projectTask = _context.ProjectTasks.FirstOrDefault(p => p.ProjectTaskId == projectTaskId);

            if (projectTask == null)
                return NotFound();

            return Ok(projectTask);
        }

        [HttpPost]
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