using AutoMapper;
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
            threadsGroup.MapGet("/{user_id}", GetThreads);
            threadsGroup.MapGet("/{user_id}/{thread_id}", GetThread);
            threadsGroup.MapGet("/{user_id}/feedback/{thread_id}", GetFeedback);
        }


        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public static async Task<IResult> GetThread(
           [FromQuery] string userId,
           [FromQuery] string threadId,
           [FromServices] ThreadRepository repository,
           [FromServices] IMapper mapper
       )
        {
            ServiceResponse<OutThreadsDTO> response = new();
            try
            {
                // Source: 
                ThreadUser source = await repository.FindByUserIdAndThreadId(userId, threadId);
                // Transferring:
                response.Data = mapper.Map<OutThreadsDTO>(source);
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

        public async static Task<IResult> GetThreads
            (
                [FromRoute] string userId,
                [FromServices] ThreadRepository repository,
                [FromServices] IMapper mapper
            ) {

            ServiceResponse<List<OutThreadsDTO>> response = new();
            try
            {
                // Source: 
                List<ThreadUser> source = await repository.GetById(userId);
                // Transferring:
                List<OutThreadsDTO> results = source.Select(mapper.Map<OutThreadsDTO>).ToList();
                response.Data = results;
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



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async static Task<IResult> GetFeedback
            (
                [FromQuery] string threadId,
                [FromQuery] string userId,
                [FromServices] ThreadRepository repository,
                [FromServices] IMapper mapper
            )
        {
            ServiceResponse<List<OutFeedbackDTO>> response = new();
            try
            {
                // Source: 
                List<Feedback> source = await repository.FindFeedbacksByThreadIdAndUserId(threadId, userId);
                // Transferring:
                List<OutFeedbackDTO> results = source.Select(mapper.Map<OutFeedbackDTO>).ToList();
                response.Data = results;
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


    }
}
