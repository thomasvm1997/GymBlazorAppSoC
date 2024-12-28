namespace Pri.Ee.Api.Dtos.Users
{
    public class RegisterUserResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
