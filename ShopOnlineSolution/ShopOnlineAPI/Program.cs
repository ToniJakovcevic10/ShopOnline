using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using ShopOnlineAPI.Data;
using ShopOnlineAPI.Repositories;
using ShopOnlineAPI.Repositories.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<ShopOnlineDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopOnlineConnection"))
    );

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
            policy.WithOrigins("https://localhost:7076", "https://localhost:7076/")
            .AllowAnyMethod()
            .WithHeaders(HeaderNames.ContentType)

    );

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
