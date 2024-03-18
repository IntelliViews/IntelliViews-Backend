using AutoMapper;
using IntelliViews.API.Services;
using IntelliViews.Data.DataModels;
using IntelliViews.Repository;

namespace IntelliViews.Infrastructure.Services
{

    /*public class UserService
    {
        private readonly IRepository<ApplicationUser> _repository;
        private readonly IMapper _mapper;
        public UserService(IRepository<ApplicationUser> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<OutUserDTO>> Get(string id)
        {
            ServiceResponse<GetUserDTO> response = new();
            try
            {
                User user = await _repository.Get(id);
                response.Data = _mapper.Map<GetUserDTO>(user);
            }
            catch (ArgumentException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetUserDTO>>> GetAll()
        {
            ServiceResponse<List<GetUserDTO>> response = new();
            try
            {
                List<User> users = await _repository.GetAll();
                response.Data = users.Select(_mapper.Map<GetUserDTO>).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
*/
}
