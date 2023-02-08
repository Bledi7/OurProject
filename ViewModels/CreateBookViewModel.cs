namespace OurProject.ViewModels
{
    public class CreateBookViewModel
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public IFormFile Image { get; set; }
    }
}
