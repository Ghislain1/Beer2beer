

namespace Beer2beer.API.Controllers;
using AutoMapper;
using Beer2beer.Core.Entities;
using Beer2beer.Core.Interfaces;
using Beer2beer.Core.Mapper;
using Beer2beer.Core.Services;
using Beer2beer.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public static class ServiceExtension
{
    public static IServiceCollection RegisterService(this IServiceCollection services)
    {
        #region Services
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IAppUserService, AppUserService>();
        //services.AddScoped<IProductService, ProductService>();
        //services.AddScoped<IOrderService, OrderService>();

        #endregion

        #region Repositories
        services.AddTransient<ICustomerRepository, CustomerRepository>();
        //services.AddTransient<IProductRepository, ProductRepository>();
        //services.AddTransient<IOrderRepository, OrderRepository>();
        //services.AddTransient<IOrderDetailsRepository, OrderDetailsRepository>();

        #endregion

        #region Mapper
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Customer, CustomerViewModel>();
            cfg.CreateMap<CustomerViewModel, Customer>();

            //cfg.CreateMap<Product, ProductViewModel>();
            //cfg.CreateMap<ProductViewModel, Product>();

            //cfg.CreateMap<Order, OrderViewModel>();
            //cfg.CreateMap<OrderViewModel, Order>();
        }, loggerFactory);

        IMapper mapper = configuration.CreateMapper();

        // Register the IMapperService implementation with your dependency injection container
        services.AddSingleton<IBaseMapper<Customer, CustomerViewModel>>(new BaseMapper<Customer, CustomerViewModel>(mapper));
        services.AddSingleton<IBaseMapper<CustomerViewModel, Customer>>(new BaseMapper<CustomerViewModel, Customer>(mapper));

        services.AddSingleton<IBaseMapper<AppUserViewModel, AppUser>>(new BaseMapper<AppUserViewModel, AppUser>(mapper));
        services.AddSingleton<IBaseMapper<AppUser, AppUserViewModel>>(new BaseMapper<AppUser, AppUserViewModel>(mapper));

        // SCOPED
        services.AddScoped<IAppUserRepository, AppUserRepository>();
      

        #endregion

        return services;
    }
}

