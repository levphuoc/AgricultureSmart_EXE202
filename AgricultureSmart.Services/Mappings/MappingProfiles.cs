using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Services.Models.TicketModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Ticket, TicketViewModel>().ReverseMap();
            CreateMap<CreateTicketModel, Ticket>().ReverseMap();
            CreateMap<UpdateTicketModel, Ticket>().ReverseMap();
        }
    }
}
