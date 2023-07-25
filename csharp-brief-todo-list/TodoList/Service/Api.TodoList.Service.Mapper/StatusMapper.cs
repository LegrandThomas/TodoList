using Api.TodoList.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
