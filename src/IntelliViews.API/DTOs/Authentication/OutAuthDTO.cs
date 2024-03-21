using IntelliViews.Data.Enums;

namespace IntelliViews.API.DTOs.Authentication
{
    public class OutAuthDTO
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public Roles Role { get; set; }
    }
}
