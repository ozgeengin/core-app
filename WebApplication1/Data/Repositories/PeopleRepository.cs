using WebApplication1.Data.Base;
using WebApplication1.Data.Interfaces;
using WebApplication1.Entities;

namespace WebApplication1.Data.Repositories
{
    public class PeopleRepository : BaseRepository<People>,IPeopleRepository
    {
        public PeopleRepository(CustomContext context) : base(context)
        {
        }
    }
}
