using System.Text;
using CloudinaryDotNet;
using Ecomerce.backend.Data;
using Ecomerce.backend.Repositories;
using Ecomerce.backend.Services;
using Ecommerce.Backend.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=LocalConnection"));
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//services capa de negocios
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<IFilesService, FilesService>();

//repositories capa de acceso a datos
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

//registrar el servicio de swagger
builder.Services.AddSwaggerGen();

//CONFIGURACION JWT
var key = builder.Configuration["Jwt:key"];

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        RoleClaimType = "role",
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(key!))
    };
});
// agregamos los servicios de la api
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddOpenApi();
// seedDB datos insertados al bd por defecto
builder.Services.AddTransient<SeedDb>();

// inyectamos cloudinari
var cloudinaryConfig = builder.Configuration.GetSection("Cloudinary");
var cloudinary = new Cloudinary(new Account(
   cloudinaryConfig["CloudName"],//dwexvcqmn
   cloudinaryConfig["ApiKey"], // 435681718952774
   cloudinaryConfig["ApiSecret"]
));
builder.Services.AddSingleton(cloudinary);

var app = builder.Build();
// cargar set data
SeedData(app);
static void SeedData(WebApplication app)
{
    IServiceScopeFactory? scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using IServiceScope scope = scopedFactory!.CreateScope();
    SeedDb? service = scope.ServiceProvider.GetService<SeedDb>();
    service!.SeedAsync().Wait();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //mostrar swagger;
    app.UseSwagger();
    app.UseSwaggerUI();
   
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
// JWT
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
