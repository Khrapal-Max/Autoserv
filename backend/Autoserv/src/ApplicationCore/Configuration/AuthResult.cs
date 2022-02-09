namespace ApplicationCore.Configuration
{
    public class AuthResult
    {
        public string Token { get; set; } = null!;
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = null!;
    }
}
