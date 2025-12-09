namespace Shopping.Platform.Service.Models.Requests
{
    public class CreateOrderRequest
    {
        public bool IsGift { get; set; }
        public string? Description { get; set; }
    }
}