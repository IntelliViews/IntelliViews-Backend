using IntelliViews.API.Services;
using IntelliViews.Data.DataModels;
using IntelliViews.Data;
using IntelliViews.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IntelliViews.API.DTOs;
using AutoMapper;
using Microsoft.Win32;

namespace IntelliViews.API.Endpoints
{
    public static class AuthenticationEnpoint
    {

        public static void AuthenticationConfiguration(this WebApplication app)
        {
            var userGroup = app.MapGroup("users");
            userGroup.MapPost("/register", CreateUser);
            userGroup.MapPost("/login", LoginUser);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreateUser(
            [FromBody] InRegisterDTO newUser,
            [FromServices] AuthenticationRepository<ApplicationUser> repository,
            [FromServices] IMapper mapper,
            [FromServices] UserManager<ApplicationUser> userManager
            )
        {

            ServiceResponse<string> response = new();
            try
            {
                ApplicationUser? dbUser = await userManager.FindByEmailAsync(newUser.Email);
                if (dbUser != null)
                {
                    response.Status = false;
                    response.Message = "Email already exists!";
                    return TypedResults.BadRequest(response);
                }

                else
                {
                    ApplicationUser user = mapper.Map<ApplicationUser>(newUser);
                    // Source:
                    var source = await repository.CreateUser(user, userManager);
                    // Transferring:
                    response.Data = mapper.Map<OutRegisterDTO>(source);
                    response.Message = "User created successfully!";
                    return TypedResults.Created(nameof(user), response);    //THIS WORK!


                }
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex.Message);
            }

 

        }
           

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        private static async Task<IResult> LoginUser(LoginUserDTO lDTO, IRepository repository)
        {
           
        }
    }
}
