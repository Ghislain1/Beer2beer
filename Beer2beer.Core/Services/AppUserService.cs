using Beer2beer.Core.Entities;
using Beer2beer.Core.Interfaces;

namespace Beer2beer.Core.Services;

/// <summary>
/// Wrap repository and Mapper bidirectional
/// </summary>
public class AppUserService : IAppUserService
{
    private readonly IBaseMapper<AppUser, AppUserViewModel> appUserViewModelMapper;
    private readonly IBaseMapper<AppUserViewModel, AppUser> appUserMapper;
    private readonly IAppUserRepository appUserRepository;

    public AppUserService(IBaseMapper<AppUser, AppUserViewModel> appUserViewModelMapper,
        IBaseMapper<AppUserViewModel, AppUser> appUserMapper,
        IAppUserRepository appUserRepository)
    {
        this.appUserViewModelMapper = appUserViewModelMapper;
        this.appUserMapper = appUserMapper;
        this.appUserRepository = appUserRepository;
    }
    public Task<AppUserViewModel> CreateAsync(AppUserViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AppUserViewModel>> GetAppUsersAsync() => this.appUserViewModelMapper.MapList(await this.appUserRepository.GetAll());
    public Task<AppUserViewModel> GetAsync(int id)
    {
        throw new NotImplementedException();
    }


    public Task<bool> IsExistsAsync(string key, string value)
    {
        throw new NotImplementedException();
    }



    public Task<bool> IsExistsForUpdateAsync(int id, string key, string value)
    {
        throw new NotImplementedException();
    }



    public Task UpdateAsync(AppUserViewModel model)
    {
        throw new NotImplementedException();
    }
}