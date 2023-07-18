using ETicaretAPI.Application;
using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Abstractions.Storage.Local;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.Validators.Products;
using ETicaretAPI.Infrastructure;
using ETicaretAPI.Infrastructure.Filter;
using ETicaretAPI.Infrastructure.Services.Storage;
using ETicaretAPI.Infrastructure.Services.Storage.Azure;
using ETicaretAPI.Infrastructure.Services.Storage.Local;
using ETicaretAPI.Persistence;  
using ETicaretAPI.Persistence.Contexts;
using ETicaretAPI.Persistence.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

//builder.Services.AddStorage(StorageType.Azure);
//builder.Services.AddStorage<LocalStorage>();
builder.Services.AddStorage<AzureStorage>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod();
        });
});


builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options=>options.SuppressModelStateInvalidFilter=true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
builder.Services.AddScoped<IProductReadRepository, ProductReadRepository>();

builder.Services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
builder.Services.AddScoped<IOrderReadRepository, OrderReadRepository>();

builder.Services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
builder.Services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
//******
builder.Services.AddScoped<IFileReadRepository, FileReadRepository>();
builder.Services.AddScoped<IFileWriteRepository, FileWriteRepository>();

builder.Services.AddScoped<IProductImageFileReadRepository,ProductImageFileReadRepository>();
builder.Services.AddScoped<IProductImageFileWriteRepository,ProductImageFileWriteRepository>();

builder.Services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository >();
builder.Services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
//******
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<ILocalStorage, LocalStorage>();
//builder.Services.AddScoped<IStorage, T>();

//builder.Services.AddScoped<IFileService,FileService>();


builder.Services.AddDbContext<ETicaretAPIDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
