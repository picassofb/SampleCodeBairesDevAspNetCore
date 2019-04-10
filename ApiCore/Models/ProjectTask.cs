using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ApiCore.Models
{
    public class ProjectTask
    {
        [Key]
        public Guid ProjectTaskId { get; set; }

        [StringLength(150), Required]
        public string Task { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }

        [JsonIgnore]
        public Project Project { get; set; }
    }
}
