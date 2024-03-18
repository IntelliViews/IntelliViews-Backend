using AutoMapper;
using IntelliViews.API.DTOs.User;
using IntelliViews.API.Helpers;
using IntelliViews.API.Services;
using IntelliViews.Data.DataModels;
using IntelliViews.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IntelliViews.API.Endpoints
{
    public static class ThreadsEndpoint
    {
        public static void ThreadsConfiguration(this WebApplication app) 
        {
            var threadsGroup = app.MapGroup("threads");
            threadsGroup.MapGet("/", GetAll);
            threadsGroup.MapGet("/{user_id}", GetThreads);
            threadsGroup.MapGet("/{user_id}/{thread_id}", GetThread);
            threadsGroup.MapGet("/{user_id}/feedback/{thread_id}", GetFeedback);
        }


        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public static async Task<IResult> GetThread(
           [FromQuery] string id,
           [FromServices] IRepository<ApplicationUser> repository,
           [FromServices] IMapper mapper,
           [FromServices] ClaimsPrincipal user
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
                return TypedResults.Ok(new { DateTime = DateTime.Now, User = user.Email(), response });
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return TypedResults.NotFound(ex.Message);
            }

        }

        public static IResult GetThreads() { throw new NotImplementedException(); }

        public static IResult GetFeedback() { throw new NotImplementedException(); }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public static async Task<IResult> GetAll(
            [FromServices] IRepository<ApplicationUser> repository,
            [FromServices] IMapper mapper,
            [FromServices] ClaimsPrincipal user
            )
        {
            ServiceResponse<List<OutUserDTO>> response = new();

            // Source: 
            List<ApplicationUser> source = await repository.GetAll();
            // Transferring:
            List<OutUserDTO> results = source.Select(mapper.Map<OutUserDTO>).ToList();
            response.Data = results;
            response.Status = true;
            return TypedResults.Ok(new { DateTime = DateTime.Now, User = user.Email(), response });

        }


    }
}
