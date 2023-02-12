namespace SelfCare.Api.Requests.User
{
    public class SignupClientRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? DisplayName { get; set; }
    }
}
