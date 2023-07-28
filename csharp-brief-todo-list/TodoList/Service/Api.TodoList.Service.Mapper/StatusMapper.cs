
﻿using Api.TodoList.Data.Entity.Model;
using Api.TodoList.Service.DTO;
using AutoMapper;

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