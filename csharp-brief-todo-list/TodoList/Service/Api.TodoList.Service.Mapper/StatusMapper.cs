using Api.TodoList.Service.DTO;

namespace Api.TodoList.Service.Mapper
{
    public class StatusMapper
    {

        /// <summary>
        /// Transform CreateDTO en Entity Object 
        /// </summary>
        /// <param name="statusDTO"></param>
        /// <returns></returns>
        public static Data.Entity.Model.Status TransformCreateDTOToEntity(CreateStatusDTO statusDTO)
        {
            return new Data.Entity.Model.Status
            {
                Value = statusDTO.Value
            };
        }

        public static ReadStatusDTO TransformEntityToReadDTO(Data.Entity.Model.Status status)
        {
            return new ReadStatusDTO
            {
                IdStatus = status.IdStatus,
                Value = status.Value
            };
        }
    }
}
