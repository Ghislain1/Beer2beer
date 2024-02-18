

namespace Beer2beer.Core.Interfaces;
using Beer2beer.Core.Entities;

public interface ISupplierService
{
    Task<IEnumerable<ArticleViewModel>> GetSuppliers();

    //   Task<PaginatedDataViewModel<CustomerViewModel>> GetPaginatedCustomers(int pageNumber, int pageSize);
    Task<SupplierViewModel> GetSupplierAsync(int id);
    Task<bool> IsExists(string key, string value);
    Task<bool> IsExistsForUpdate(int id, string key, string value);
    Task<SupplierViewModel> Create(SupplierViewModel model);
    Task Update(SupplierViewModel model);
    Task Delete(int id);
}

public interface IArticleService
{
    Task<IEnumerable<ArticleViewModel>> GetArticles();

    //   Task<PaginatedDataViewModel<CustomerViewModel>> GetPaginatedCustomers(int pageNumber, int pageSize);
    Task<ArticleViewModel> GetArticle(int id);
    Task<bool> IsExists(string key, string value);
    Task<bool> IsExistsForUpdate(int id, string key, string value);
    Task<ArticleViewModel> Create(ArticleViewModel model);
    Task Update(ArticleViewModel model);
    Task Delete(int id);
}

