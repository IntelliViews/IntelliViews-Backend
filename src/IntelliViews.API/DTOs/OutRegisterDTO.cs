using IntelliViews.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace IntelliViews.API.DTOs
{
    public class OutRegisterDTO
    {
       public string? Id { get; set; }  
        public string? Email { get; set; }
        
        public string? UserName { get { return this.Email; } set { } }
       
        public Roles Role { get; set; } = Roles.User;
    }
}
