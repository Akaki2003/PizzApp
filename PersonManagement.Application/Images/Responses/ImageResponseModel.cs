namespace PizzApp.Application.Images.Responses
{
    public class ImageResponseModel
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public string OriginalName { get; set; }
        public string Path { get; set; }
    }
}
