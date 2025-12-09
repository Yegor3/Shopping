namespace Shopping.API.Models.Response
{
    public class ResponseWrapper<T>
    {
        public T Data { get; set; }
        public string? Message { get; set; }

        public ResponseWrapper(T data, string? message = null)
        {
            Data = data;
            Message = message;
        }
    }
}