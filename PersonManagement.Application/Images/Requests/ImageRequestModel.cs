namespace PizzApp.Application.Images.Requests
{
    public class ImageRequestModel
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public string OriginalName { get; set; }
        public string Path { get; set; }
    }
}
