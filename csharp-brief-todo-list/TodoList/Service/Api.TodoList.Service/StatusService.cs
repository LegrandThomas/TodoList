using Api.TodoList.Data.Entity.Model;
using Api.TodoList.Data.Repository.Contract;
using Api.TodoList.Service.Contract;

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
        public async Task<IEnumerable<Status>> GetStatusesAsync()
        {
            return await _statusRepository.GetAll().ConfigureAwait(false);
        }
    }
}