using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.TodoList.Service.DTO
{
    public class ReadStatusDTO : CreateStatusDTO
    {
        public int IdStatus { get; set; }

    }
}
