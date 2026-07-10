using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace myshop.Entities.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        [Required]
        public string FullName { get; set; }
    }
}
