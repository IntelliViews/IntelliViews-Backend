using AutoMapper;
using IntelliViews.API.DTOs.Authentication;
using IntelliViews.API.DTOs.Feedback;
using IntelliViews.API.DTOs.Threads;
using IntelliViews.API.DTOs.User;
using IntelliViews.API.Services;
using IntelliViews.Data.DataModels;
using IntelliViews.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IntelliViews.API.Endpoints
{
    public static class UserEndpoint
    {

        public static void AuthenticationConfiguration(this WebApplication app)
        {
            var userGroup = app.MapGroup("users");
            userGroup.MapPost("/register", CreateUser);
            userGroup.MapPost("/login", LoginUser);

            // For Admin:
            userGroup.MapGet("/{id}", Get);
            userGroup.MapGet("/", GetAll);
            userGroup.MapPut("/{id}", UpdateUser);
            userGroup.MapDelete("/{id}", DeleteUser);

            userGroup.MapGet("/{id}/feedback/{feedback_id}", GetFeedback);
            userGroup.MapGet("/{id}/threads", GetThreads);
            userGroup.MapGet("/{id}/threads/{thread_id}", GetThread);
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

                    // Transferring:
                    response.Data = mapper.Map<OutRegisterDTO>(source);
                    response.Message = "User created successfully!";
                    return TypedResults.Created(nameof(user), response);

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
                    response.Message = "Successfully logged in!";
                    return TypedResults.Ok(response);

                }
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public static async Task<IResult> Get(
            [FromRoute] string id,
            [FromServices] IRepository<ApplicationUser> repository,
            [FromServices] IMapper mapper
        )
        {
            ServiceResponse<OutUserDTO> response = new();
            try
            {
                // Source: 
                ApplicationUser source = await repository.GetById(id);
                // Transferring:
                response.Data = mapper.Map<OutUserDTO>(source);
                response.Status = true;
                response.Message = "Sucessful!";
                return TypedResults.Ok(response); // return TypedResults.Ok(new { DateTime = DateTime.Now, User = user.Email(), response });
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return TypedResults.NotFound(ex.Message);
            }

        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public static async Task<IResult> GetAll(
            [FromServices] IRepository<ApplicationUser> repository,
            [FromServices] IMapper mapper
            //[FromServices] ClaimsPrincipal user
            )
        {
            ServiceResponse<List<OutUserDTO>> response = new();

            // Source: 
            List<ApplicationUser> source = await repository.GetAll();
            // Transferring:
            List<OutUserDTO> results = source.Select(mapper.Map<OutUserDTO>).ToList();
            response.Data = results;
            response.Status = true;
            response.Message = "Ok";
            return TypedResults.Ok(response);  //return TypedResults.Ok(new { DateTime = DateTime.Now, response });

        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateUser(
            [FromServices] IRepository<ApplicationUser> repository,
            [FromServices] IMapper mapper,
            [FromBody] InUserDTO newUser,
            [FromRoute] string id
            )
        {
            ServiceResponse<OutUserDTO> response = new();
            try
            {
                ApplicationUser _user = mapper.Map<ApplicationUser>(newUser);
                _user.Id = id;
                // source: 
                var source = await repository.Update(_user, id);
                //Transferring:
                response.Data = mapper.Map<OutUserDTO>(source);
                response.Status = true;
                response.Message = "Sucessful!";

                return response != null ? TypedResults.Ok(response) :
                    TypedResults.BadRequest($"Couldn't save to the database");
            }

            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return TypedResults.NotFound(ex.Message);
            }

        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public static async Task<IResult> DeleteUser(
            [FromServices] IRepository<ApplicationUser> repository,
            [FromServices] IMapper mapper,
            [FromRoute] string id)
        {

            ServiceResponse<OutUserDTO> response = new();

            try
            {
                // source: 
                var source = await repository.DeleteById(id);
                //Transferring:
                response.Data = mapper.Map<OutUserDTO>(source);
                response.Status = true;
                response.Message = "Sucessful!";

                return response != null ? TypedResults.Ok(response) :
                    TypedResults.BadRequest($"Couldn't delete from the database");
            }

            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return TypedResults.NotFound(ex.Message);
            }
        }

        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public static async Task<IResult> GetThread(
        [FromRoute] string id,
        [FromRoute] string thread_id,
          [FromServices] ThreadRepository repository,
          [FromServices] IMapper mapper
      )
        {
            ServiceResponse<OutThreadsDTO> response = new();
            try
            {
                // Source: 
                ThreadUser source = await repository.FindByUserIdAndThreadId(id, thread_id);
                // Transferring:
                response.Data = mapper.Map<OutThreadsDTO>(source);
                response.Status = true;
                response.Message = "Sucessful!";
                return TypedResults.Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return TypedResults.NotFound(ex.Message);
            }

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async static Task<IResult> GetThreads
            (
                [FromRoute] string id,
                [FromServices] ThreadRepository repository,
                [FromServices] IMapper mapper
            )
        {

            ServiceResponse<List<OutThreadsDTO>> response = new();
            try
            {
                // Source: 
                List<ThreadUser> source = await repository.GetById(id);
                // Transferring:
                List<OutThreadsDTO> results = source.Select(mapper.Map<OutThreadsDTO>).ToList();
                response.Data = results;
                response.Status = true;
                response.Message = "Sucessful!";
                return TypedResults.Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return TypedResults.NotFound(ex.Message);
            }

        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async static Task<IResult> GetFeedback
            (
                [FromRoute] string id,
                [FromRoute] string feedback_id,
                [FromServices] ThreadRepository repository,
                [FromServices] IMapper mapper
            )
        {
            ServiceResponse<List<OutFeedbackDTO>> response = new();
            try
            {
                // Source: 
                List<Feedback> source = await repository.FindFeedbacksByIdAndUserId(feedback_id, id);
                // Transferring:
                List<OutFeedbackDTO> results = source.Select(mapper.Map<OutFeedbackDTO>).ToList();
                response.Data = results;
                response.Status = true;
                response.Message = "Sucessful!";
                return TypedResults.Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return TypedResults.NotFound(ex.Message);
            }


        }





    }
}
