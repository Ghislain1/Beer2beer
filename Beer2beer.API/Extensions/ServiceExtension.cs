

namespace Beer2beer.API.Controllers;
using AutoMapper;
using Beer2beer.Core.Entities;
using Beer2beer.Core.Interfaces;
using Beer2beer.Core.Mapper;
using Beer2beer.Core.Services;
using Beer2beer.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
public static class ServiceExtension
{
    public static IServiceCollection RegisterService(this IServiceCollection services)
    {
        #region Services
        services.AddScoped<ICustomerService, CustomerService>();
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
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Customer, CustomerViewModel>();
            cfg.CreateMap<CustomerViewModel, Customer>();

            //cfg.CreateMap<Product, ProductViewModel>();
            //cfg.CreateMap<ProductViewModel, Product>();

            //cfg.CreateMap<Order, OrderViewModel>();
            //cfg.CreateMap<OrderViewModel, Order>();
        });

        IMapper mapper = configuration.CreateMapper();

        // Register the IMapperService implementation with your dependency injection container
        services.AddSingleton<IBaseMapper<Customer, CustomerViewModel>>(new BaseMapper<Customer, CustomerViewModel>(mapper));
        services.AddSingleton<IBaseMapper<CustomerViewModel, Customer>>(new BaseMapper<CustomerViewModel, Customer>(mapper));

        //services.AddSingleton<IBaseMapper<Product, ProductViewModel>>(new BaseMapper<Product, ProductViewModel>(mapper));
        //services.AddSingleton<IBaseMapper<ProductViewModel, Product>>(new BaseMapper<ProductViewModel, Product>(mapper));

        //services.AddSingleton<IBaseMapper<Order, OrderViewModel>>(new BaseMapper<Order, OrderViewModel>(mapper));
        //services.AddSingleton<IBaseMapper<OrderViewModel, Order>>(new BaseMapper<OrderViewModel, Order>(mapper));

        #endregion

        return services;
    }
}

