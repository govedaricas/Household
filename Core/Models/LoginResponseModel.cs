namespace Application.Models
{
    public class LoginResponseModel
    {
        public bool Flag { get; set; }
        public required string Message { get; set; }
        public required string Token { get; set; }
    }
}
