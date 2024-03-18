using IntelliViews.Data.DataModels;
using IntelliViews.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace IntelliViews.API.DTOs
{
    public class InRegisterDTO
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Username { get { return this.Email; } set { } }
        [Required]
        public string? Password { get; set; }
        [Required]
        public Roles Role { get; set; } = Roles.User;

    }
}
