using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Site.Models.ViewModels
{
    public class FileViewModel
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
