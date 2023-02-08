using Microsoft.Build.Framework;

namespace OurProject.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
