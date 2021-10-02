using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaSoft.Models
{
    public class TaskTable
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string TaskName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Discription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeliverDate { get; set; }

        [DefaultValue(false)]
        public bool TaskState { get; set; }

        [ForeignKey("ApplicationUsers")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUsers { get; set; }
    }
}
