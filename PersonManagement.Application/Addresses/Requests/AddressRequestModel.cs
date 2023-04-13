namespace PizzApp.Application.Addresses.Requests
{
    public class AddressRequestModel
    {
        public int UserId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string Description { get; set; }
    }
}
