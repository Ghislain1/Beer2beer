

namespace Beer2beer.Infrastructure.Repositories;
using Beer2beer.Core.Entities;
using Beer2beer.Core.Interfaces;
using Beer2beer.Infrastructure.Data;

public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
{
    public AppUserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {


    }
}
