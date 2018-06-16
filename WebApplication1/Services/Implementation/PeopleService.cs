using AutoMapper;
using WebApplication1.Data.Interfaces;
using WebApplication1.Dtos;
using WebApplication1.Entities;
using WebApplication1.Services.Base;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services.Implementation
{
    public class PeopleService : BaseService<People, PeopleDto, IPeopleRepository>, IPeopleService
    {
        public PeopleService(IPeopleRepository serviceRepository,IMapper mapper) : base(serviceRepository,mapper)
        {
        }
        
    }
}