using Api.TodoList.Data.Entity.Model;
using Api.TodoList.Data.Repository.Contract;
using Api.TodoList.Service.Contract;
using Api.TodoList.Service.DTO;
using AutoMapper;

namespace Api.TodoList.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadUserDTO>> GetUsersAsync()
        {
            var users = await _userRepository.GetAll(u => u.Tasks).ConfigureAwait(false);
            return _mapper.Map<IEnumerable<ReadUserDTO>>(users);
        }

        public async Task<ReadUserDTO> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetById(id, u => u.Tasks).ConfigureAwait(false);
            if (user == null)
            {
                throw new Exception($"User {id} not found.");
            }

            return _mapper.Map<ReadUserDTO>(user);
        }

        public async Task<ReadUserDTO> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetBy("Email", email, u => u.Tasks).ConfigureAwait(false);
            if (user == null)
            {
                throw new Exception($"User with {email} not found.");
            }

            return _mapper.Map<ReadUserDTO>(user);
        }

        public async Task<ReadUserDTO> AddUserAsync(CreateUserDTO userDTO)
        {
            if (userDTO == null)
            {
                throw new ArgumentNullException(nameof(userDTO));
            }

            var userToAdd = _mapper.Map<User>(userDTO);
            var userAdded = await _userRepository.Add(userToAdd).ConfigureAwait(false);

            return _mapper.Map<ReadUserDTO>(userAdded);
        }

        public async Task<ReadUserDTO> RemoveUserAsync(int id)
        {
            var user = await _userRepository.GetById(id).ConfigureAwait(false);
            if (user == null)
            {
                throw new Exception($"User {id} not found.");
            }

            var userDeleted = await _userRepository.Remove(user).ConfigureAwait(false);
            
            //todo remove tasks ?

            return _mapper.Map<ReadUserDTO>(userDeleted);
        }
    }
}