using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace ApiCore.Models
{
    public class Project
    {
        public Project()
        {
            // Initialized ProjectTasks to avoid return null if the projects has no tasks
            ProjectTasks = new List<ProjectTask>();
        }

        [Key]
        public Guid ProjectId { get; set; }

        [StringLength(50)]
        public string ProjectName { get; set; }

        [Required]
        public bool IsDone { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public List<ProjectTask> ProjectTasks { get; set; }
    }
}
