﻿using Microsoft.EntityFrameworkCore;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Persistence.Repositories;
using ETicaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using ETicaretAPI.Application.Features.Commands.AppUser.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Persistence.Services;
using ETicaretAPI.Application.Abstractions.Services.Authentications;

namespace ETicaretAPI.Persistence
{
    public static class ServiceRegistration
    {

        //public static void OnConfiguring(DbContextOptionsBuilder services)
        //{
        //    services.UseSqlServer(Configuration.ConnectionString, providerOptions => { providerOptions.EnableRetryOnFailure(); });

        //}


        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<ETicaretAPIDbContext>(provider =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<ETicaretAPIDbContext>();
                optionsBuilder.UseNpgsql(Configuration.ConnectionString);
                return new ETicaretAPIDbContext(optionsBuilder.Options);
            });
            //services.AddDbContext<ETicaretAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString), ServiceLifetime.Singleton);
            //services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<ETicaretAPIDbContext>().AddDefaultTokenProviders();

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            //******
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();

            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository,InvoiceFileWriteRepository>();
            //******
            services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
            services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();

            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
            //******
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();

            services.AddHttpClient();

            services.AddScoped<IBasketService, BasketService>();
            //******

        }
    }
}
