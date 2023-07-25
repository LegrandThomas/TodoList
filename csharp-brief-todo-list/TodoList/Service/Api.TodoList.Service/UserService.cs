using Api.TodoList.Data.Entity.Model;
using Api.TodoList.Data.Repository.Contract;
using Api.TodoList.Service.Contract;
using Api.TodoList.Service.DTO;
using Api.TodoList.Service.Mapper;

namespace Api.TodoList.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<ReadUserDTO>> GetUsersAsync()
        {
            var users = await _userRepository.GetAll(u => u.Tasks).ConfigureAwait(false);
            return users.Select(UserMapper.TransformEntityToReadDTO);
        }

        public async Task<ReadUserDTO> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetById(id, u => u.Tasks).ConfigureAwait(false);
            if (user == null)
            {
                throw new Exception($"User {id} not found.");
            }

            return UserMapper.TransformEntityToReadDTO(user);
        }

        public async Task<ReadUserDTO> AddUserAsync(CreateUserDTO userDTO)
        {
            if (userDTO == null)
            {
                throw new ArgumentNullException(nameof(userDTO));
            }

            var userToAdd = UserMapper.TransformCreateDTOToEntity(userDTO);

            var userAdded = await _userRepository.Add(userToAdd).ConfigureAwait(false);
            return UserMapper.TransformEntityToReadDTO(userAdded);
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

            return UserMapper.TransformEntityToReadDTO(userDeleted);
        }
    }
}