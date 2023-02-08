using OurProject.Models;

namespace OurProject.ViewModels
{
    public class CreateAuthorViewModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Biography { get; set; } 

        public  IFormFile Photo { get; set; }

        
    }
}
