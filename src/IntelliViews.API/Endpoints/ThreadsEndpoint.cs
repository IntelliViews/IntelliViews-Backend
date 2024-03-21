using AutoMapper;
using IntelliViews.API.DTOs.Feedback;
using IntelliViews.API.DTOs.Threads;
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
            threadsGroup.MapGet("/", GetAllThreads);
            threadsGroup.MapPost("/", AddThread);
            threadsGroup.MapPut("/{id}", UpdateThread);
            threadsGroup.MapDelete("/{id}", DeleteThread);

            threadsGroup.MapGet("/feedback", GetAllFeedbacks);
            threadsGroup.MapPost("/feedback/{id}", AddFeedback);
            threadsGroup.MapPut("/feedback/{id}", UpdateFeedback);
            threadsGroup.MapDelete("/feedback/{id}", DeleteFeedback);


        }

        private static string GetUserId(ClaimsPrincipal principal) => principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)!.Value;

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public static async Task<IResult> GetAllThreads(
            [FromServices] ThreadRepository repository,
            [FromServices] IMapper mapper
            )
        {
            ServiceResponse<List<OutThreadsDTO>> response = new();
            try
            {
                List<ThreadUser> source = await repository.GetAll();
                List<OutThreadsDTO> results = source.Select(mapper.Map<OutThreadsDTO>).ToList();
                response.Data = results;
                return TypedResults.Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Data = null;
                response.Status = false;
                return TypedResults.BadRequest(response);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async static Task<IResult> AddThread
            (
                [FromBody] InThreadDTO newThread,
                ClaimsPrincipal claimsPrincipal,
                [FromServices] IRepository<ThreadUser> repository,
                [FromServices] IMapper mapper
            )
        {

            ServiceResponse<OutThreadsDTO> response = new();
            try
            {
                ThreadUser thread = new()
                {
                    UserId = GetUserId(claimsPrincipal),
                    Id = newThread.Id
                };
                ThreadUser source = await repository.Create(thread);
                OutThreadsDTO result = mapper.Map<OutThreadsDTO>(source);
                response.Data = result;
                response.Status = true;
                return TypedResults.Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return TypedResults.NotFound(response);
            }
        }

        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateThread(
            [FromServices] IRepository<ThreadUser> repository,
            [FromServices] IMapper mapper,
            [FromBody] InThreadDTO newThread,
            [FromRoute] string id
            )
        {
            ServiceResponse<OutThreadsDTO> response = new();
            try
            {
                ThreadUser thread = mapper.Map<ThreadUser>(newThread);
                thread.Id = id;
                // source: 
                var source = await repository.Update(thread, id);
                //Transferring:
                response.Data = mapper.Map<OutThreadsDTO>(source);
                response.Status = true;

                return response != null ? TypedResults.Ok(response) :
                    TypedResults.BadRequest($"Couldn't save to the database");
            }

            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return TypedResults.NotFound(response);
            }

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public static async Task<IResult> DeleteThread(
            [FromServices] IRepository<ThreadUser> repository,
            [FromServices] IMapper mapper,
            [FromRoute] string id)
        {

            ServiceResponse<OutThreadsDTO> response = new();

            try
            {
                // source: 
                var source = await repository.DeleteById(id);
                //Transferring:
                response.Data = mapper.Map<OutThreadsDTO>(source);
                response.Status = true;

                return response != null ? TypedResults.Ok(response) :
                    TypedResults.BadRequest($"Couldn't delete from the database");
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return TypedResults.NotFound(response);
            }
        }


        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public static async Task<IResult> GetAllFeedbacks(
            [FromServices] IRepository<Feedback> repository,
            [FromServices] IMapper mapper
            )
        {
            ServiceResponse<List<OutFeedbackDTO>> response = new();

            // Source: 
            List<Feedback> source = await repository.GetAll();
            // Transferring:
            List<OutFeedbackDTO> results = source.Select(mapper.Map<OutFeedbackDTO>).ToList();
            response.Data = results;
            response.Status = true;
            return TypedResults.Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async static Task<IResult> AddFeedback
           (
               [FromBody] InFeedbackDTO newFeedback,
               [FromServices] IRepository<Feedback> repository,
               [FromServices] IMapper mapper
           )
        {

            ServiceResponse<OutFeedbackDTO> response = new();
            try
            {
                Feedback feedback = mapper.Map<Feedback>(newFeedback);
                // Source: 
                Feedback source = await repository.Create(feedback);
                // Transferring:
                OutFeedbackDTO result = mapper.Map<OutFeedbackDTO>(source);
                response.Data = result;
                response.Status = true;
                return TypedResults.Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return TypedResults.NotFound(response);
            }


        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateFeedback(
            [FromServices] IRepository<Feedback> repository,
            [FromServices] IMapper mapper,
            [FromBody] InFeedbackDTO newFeedback,
            [FromRoute] string id
            )
        {
            ServiceResponse<OutFeedbackDTO> response = new();
            try
            {
                Feedback feedback = mapper.Map<Feedback>(newFeedback);
                feedback.Id = id;
                // source: 
                var source = await repository.Update(feedback, id);
                //Transferring:
                response.Data = mapper.Map<OutFeedbackDTO>(source);
                response.Status = true;

                return response != null ? TypedResults.Ok(response) :
                    TypedResults.BadRequest($"Couldn't save to the database");
            }

            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return TypedResults.NotFound(response);
            }

        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public static async Task<IResult> DeleteFeedback(
            [FromServices] IRepository<Feedback> repository,
            [FromServices] IMapper mapper,
            [FromRoute] string id)
        {

            ServiceResponse<OutFeedbackDTO> response = new();

            try
            {
                // source: 
                var source = await repository.DeleteById(id);
                //Transferring:
                response.Data = mapper.Map<OutFeedbackDTO>(source);
                response.Status = true;

                return response != null ? TypedResults.Ok(response) :
                    TypedResults.BadRequest($"Couldn't delete from the database");
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return TypedResults.NotFound(response);
            }
        }

    }
}
