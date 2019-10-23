using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Models.ViewModels
{
    public class PeriodViewModel
    {
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime FinishDate { get; set; }

        [Required]
        public string Format { get; set; }
    }
}
