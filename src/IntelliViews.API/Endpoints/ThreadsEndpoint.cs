using AutoMapper;
using IntelliViews.API.DTOs.Feedback;
using IntelliViews.API.DTOs.Threads;
using IntelliViews.API.DTOs.User;
using IntelliViews.API.Helpers;
using IntelliViews.API.Services;
using IntelliViews.Data.DataModels;
using IntelliViews.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;

namespace IntelliViews.API.Endpoints
{
    public static class ThreadsEndpoint
    {
        public static void ThreadsConfiguration(this WebApplication app) 
        {
            var threadsGroup = app.MapGroup("threads");
            threadsGroup.MapGet("/", GetAll);
            //threadsGroup.MapPost("/{user_id}", AddThread);
            //threadsGroup.MapPut("/{thread_id}", UpdateThread);
            //threadsGroup.MapDelete("/{thread_id}", DeleteThread);

            //threadsGroup.MapPost("/feedback/{thread_id}", AddFeedback);
            //threadsGroup.MapPut("/feedback/{thread_id}", UpdateFeedback);
            //threadsGroup.MapDelete("/feedback/{thread_id}", DeleteFeedback);


        }

         

        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public static async Task<IResult> GetAll(
            [FromServices] ThreadRepository repository,
            [FromServices] IMapper mapper
            )
        {
            ServiceResponse<List<OutThreadsDTO>> response = new();

            // Source: 
            List<ThreadUser> source = await repository.GetAll();
            // Transferring:
            List<OutThreadsDTO> results = source.Select(mapper.Map<OutThreadsDTO>).ToList();
            response.Data = results;
            response.Status = true;
            return TypedResults.Ok(new { DateTime = DateTime.Now, response });

        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async static Task<IResult> AddThread
            (
                [FromBody] InThreadDTO newThread,
                [FromServices] IRepository<ThreadUser> repository,
                [FromServices] IMapper mapper
            )
        {
            ServiceResponse<OutThreadsDTO> response = new();
            try
            {
                ThreadUser thread = mapper.Map<ThreadUser>(newThread);
                // Source: 
                ThreadUser source = await repository.Create(thread);
                // Transferring:
                OutThreadsDTO result = mapper.Map<OutThreadsDTO>(source);
                response.Data = result;
                response.Status = true;
                return TypedResults.Ok(new { DateTime = DateTime.Now, response });
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
