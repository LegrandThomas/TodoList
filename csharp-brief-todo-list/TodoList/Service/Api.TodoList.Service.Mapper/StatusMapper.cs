
﻿using Api.TodoList.Data.Entity.Model;
using Api.TodoList.Service.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Api.TodoList.Service.Mapper
{
    public class StatusMapper : Profile
    {

        public StatusMapper()
        {


            CreateMap<Status, CreateStatusDTO>()
                  .ReverseMap();

            CreateMap<Status, ReadStatusDTO>()
               .IncludeBase<Status, CreateStatusDTO>()
                 .ReverseMap();
        }
        
    }
}
