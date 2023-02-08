using Microsoft.Build.Framework;


namespace OurProject.ViewModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users =new List<String>();
        }
     public string Id { get; set; }

    [Required]
    public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
