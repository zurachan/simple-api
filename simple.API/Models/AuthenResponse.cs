namespace simple.API.Models
{
    public class AuthenResponse
    {
        public object? User { get; set; }
        public string? Token { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}
