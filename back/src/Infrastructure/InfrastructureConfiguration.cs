using System.Reflection;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SQLConnection"), b => b.MigrationsAssembly(typeof(SqlDbContext).Assembly.FullName)));

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        
        services.AddScoped<IDomainEventHandler, DomainEventHandler>();

        //services.AddScoped<SqlDbContext>();

        services.AddScoped<IRepository<Books>, Repository<Books>>();
        services.AddScoped<BooksService, BooksService>();
        services.AddScoped<IBookRepository, BooksRepository>();

        return services;
    }
}