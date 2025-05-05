using Ecomerce.backend.Data;
using Ecomerce.backend.Repositories;
using Ecomerce.backend.Services;
using Ecommerce.Backend.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=LocalConnection"));
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//services capa de negocios
builder.Services.AddScoped<CategoriaService>();

//repositories capa de acceso a datos
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
//registrar el servicio de swagger
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //mostrar swagger;
    app.UseSwagger();
    app.UseSwaggerUI();
   
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
