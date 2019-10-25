using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        public string Type { get; set; }

        public List<string> TypeList { get; set; } = new List<string> { "Model", "Make" };

        public List<string> FormatList { get; set; } = new List<string> { "MMM yyyy", "y" };
    }
}
