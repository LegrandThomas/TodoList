using Api.TodoList.Data.Entity.Model;
using Api.TodoList.Data.Repository.Contract;
using Api.TodoList.Service.Contract;
using Api.TodoList.Service.DTO;
using Api.TodoList.Service.Mapper;

namespace Api.TodoList.Service
{
    public class StatusService : IStatusService
    {
        private readonly IRepository<Status> _statusRepository;

        public StatusService(IRepository<Status> statusRepository)
        {
            _statusRepository = statusRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All the statuses entities from our table</returns>
        public async Task<IEnumerable<ReadStatusDTO>> GetStatusesAsync()
        {
            var status = await _statusRepository.GetAll().ConfigureAwait(false);
            return status.Select(StatusMapper.TransformEntityToReadDTO);
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

            return StatusMapper.TransformEntityToReadDTO(status);
        }
    }
}
