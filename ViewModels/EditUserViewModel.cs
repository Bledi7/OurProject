using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Build.Framework;


using System.Security.Claims;

namespace OurProject.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel(){
            Claims = new List<string>();
            Roles = new List<string>();
        }
        public string   Id { get; set; }

        [Required]
        public string   UserName { get; set; }
        [Required]
        [System.ComponentModel.DataAnnotations.EmailAddress]
        public string Email { get; set; }

        public List<string> Roles { get; set;}
        public List<string> Claims { get; set;}

    }
}
