

namespace Beer2beer.Infrastructure.Repositories;
using Beer2beer.Core.Entities;
using Beer2beer.Core.Interfaces;
using Beer2beer.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {


    }
}
