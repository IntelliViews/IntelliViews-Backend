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
            [FromServices] AuthenticationRepository repository,
            [FromServices] IMapper mapper,
            [FromServices] UserManager<ApplicationUser> userManager
            )
        {

            ServiceResponse<OutRegisterDTO> response = new();
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
                    ApplicationUser source = await repository.CreateUser(user, userManager);
                    /*var source = await userManager.CreateAsync(
                    new ApplicationUser { UserName = newUser.UserName, Email = newUser.Email, Role = newUser.Role },
                    newUser.Password!    
                );*/
                    // Transferring:
                    response.Data = mapper.Map<OutRegisterDTO>(source);
                    response.Message = "User created successfully!";
                    return TypedResults.Created(nameof(user), response);    //THIS WORK

                }
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex.Message);
            }
        }
           

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        private static async Task<IResult> LoginUser(
            [FromBody] InAuthDTO inUser,
            [FromServices] AuthenticationRepository repository,
            [FromServices] IMapper mapper,
            [FromServices] UserManager<ApplicationUser> userManager,
            [FromServices] TokenService tokenService
            )
        {
            ServiceResponse<OutAuthDTO> response = new();
            try
            {
                ApplicationUser? dbUser = await userManager.FindByEmailAsync(inUser.Email);
                if (dbUser == null)
                {
                    response.Status = false;
                    response.Message = "User with email not found";
                    return TypedResults.BadRequest(response);
                }

                bool isPasswordValid = await userManager.CheckPasswordAsync(dbUser, inUser.Password!);

                if (!isPasswordValid)
                {
                    response.Status = false;
                    response.Message = "Wrong password!";
                    return TypedResults.BadRequest(response);
                }

                else
                {
                    //ApplicationUser user = mapper.Map<ApplicationUser>(inUser);
                    string token = tokenService.CreateToken(dbUser);
                   
                    await repository.LoginUser();
                    // Transferring:
                    response.Data = new OutAuthDTO
                    {
                        Username = dbUser.UserName,
                        Email = dbUser.Email,
                        Token = token
                    };
                    response.Message = "User created successfully!";
                    return TypedResults.Ok(response);   

                }
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex.Message);
            }

        }
    }
}
