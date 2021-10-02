using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaSoft.DTO
{
    public class EditTaskState
    {
        [Required]
        public bool TaskState { get; set; }
    }
}
