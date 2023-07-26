using Api.TodoList.Data.Entity.Model;
using Api.TodoList.Data.Repository.Contract;
using Api.TodoList.Service.Contract;
using Api.TodoList.Service.DTO;
using Api.TodoList.Service.Mapper;
using AutoMapper;

namespace Api.TodoList.Service
{
    public class StatusService : IStatusService
    {
        private readonly IRepository<Status> _statusRepository;
        private readonly IMapper _mapper;

        public StatusService(IRepository<Status> statusRepository, IMapper mapper)
        {
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All the statuses entities from our table</returns>
        public async Task<IEnumerable<ReadStatusDTO>> GetStatusesAsync()
        {
            var status = await _statusRepository.GetAll().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<ReadStatusDTO>>(status);
        }

        /// <summary>
        ///   Get one Status by id 
        /// </summary>
        /// <returns>All the statuses entities from our table</returns>
        public async Task<ReadStatusDTO> GetStatusByIdAsync(int idStatus)
        {
            var status = await _statusRepository.GetById(idStatus).ConfigureAwait(false);
            if (status == null)
            {
                throw new Exception($"Status {idStatus} not found.");
            }

            return _mapper.Map<ReadStatusDTO>(status);  
        }
    }
}
