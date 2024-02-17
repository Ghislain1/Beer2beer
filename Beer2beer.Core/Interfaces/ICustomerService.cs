

namespace Beer2beer.Core.Interfaces;
using Beer2beer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public interface ICustomerService
{
    Task<IEnumerable<CustomerViewModel>> GetCustomers();
    //   Task<PaginatedDataViewModel<CustomerViewModel>> GetPaginatedCustomers(int pageNumber, int pageSize);
    Task<CustomerViewModel> GetCustomer(int id);
    Task<bool> IsExists(string key, string value);
    Task<bool> IsExistsForUpdate(int id, string key, string value);
    Task<CustomerViewModel> Create(CustomerViewModel model);
    Task Update(CustomerViewModel model);
    Task Delete(int id);

}
