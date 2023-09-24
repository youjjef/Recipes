using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class User
    {
        [Key]
        public int userId { get; set; }
        [Required]
        public string username { get; set; } = string.Empty;
        [Required]
        public string password { get; set; } = string.Empty;
        [Required] 
        public string email { get; set; } = string.Empty;
    }
}
