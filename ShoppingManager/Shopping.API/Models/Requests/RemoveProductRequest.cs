namespace Shopping.API.Models.Requests
{
    public class RemoveProductRequest
    {
        public long OrderId { get; set; }
        public List<long>? ProductIds { get; set; } 
    }
}