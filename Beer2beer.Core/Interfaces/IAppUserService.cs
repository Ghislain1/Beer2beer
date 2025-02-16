

namespace Beer2beer.Core.Interfaces;
using Beer2beer.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAppUserService
{
    Task<IEnumerable<AppUserViewModel>> GetAppUsersAsync();

    Task<AppUserViewModel> GetAsync(int id);
    Task<bool> IsExistsAsync(string key, string value);
    Task<bool> IsExistsForUpdateAsync(int id, string key, string value);
    Task<AppUserViewModel> CreateAsync(AppUserViewModel model);
    Task UpdateAsync(AppUserViewModel model);
    Task DeleteAsync(int id);
}
