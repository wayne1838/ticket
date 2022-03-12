using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Models.Ticket;
using Ticket.ViewModels.Ticket;

namespace Ticket.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            //CreateMap<User, UserDto>();
            //CreateMap<UserDto, User>();
            
            CreateMap<TicketModel, TicketDto>().ReverseMap();
            CreateMap<TicketCreateModel, TicketDto>().ReverseMap();
        }
    }
}
