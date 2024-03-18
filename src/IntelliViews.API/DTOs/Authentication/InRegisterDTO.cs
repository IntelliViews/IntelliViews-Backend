using IntelliViews.Data.DataModels;
using IntelliViews.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace IntelliViews.API.DTOs.Authentication
{
    public class InRegisterDTO
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? UserName { get { return Email; } set { } }
        [Required]
        public string? Password { get; set; }
        [Required]
        public Roles Role { get; set; }

    }
}
