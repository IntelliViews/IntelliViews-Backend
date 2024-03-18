using IntelliViews.Data.Enums;

namespace IntelliViews.API.DTOs.User
{
    public class InUserDTO
    {
        public string? Id { get; set; }
        public string? userName { get; set; }
        public string? Email { get; set; }
        public Roles Roles { get; set; }
    }
}
