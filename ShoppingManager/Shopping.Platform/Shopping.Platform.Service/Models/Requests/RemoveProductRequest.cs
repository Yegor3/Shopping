namespace Shopping.Platform.Service.Models.Requests
{
    public class RemoveProductRequest
    {
        public long OrderId { get; set; }
        public List<long>? ProductIds { get; set; } 
    }
}