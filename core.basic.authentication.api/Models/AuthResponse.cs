namespace core.basic.authentication.api.Models
{
    public class AuthResponse
    {
        public Guid? UserId { get; set; }
        public string Message { get; set; }
        public bool Success{ get; set; }
    }
}
