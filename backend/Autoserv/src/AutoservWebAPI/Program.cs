using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.MappingProfiles;
using ApplicationCore.ModelsDTO.Brand;
using ApplicationCore.Services;
using AutoMapper;
using AutoservWebAPI.ExeptionFilters;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("AutoservConnectionDb");

builder.Services.AddDbContext<AutoservContext>(options =>
    options.UseSqlServer(connectionString)
    .UseSnakeCaseNamingConvention()
    .EnableSensitiveDataLogging(true));

var identityString = builder.Configuration.GetConnectionString("AutoservIdentityConnectionDb");

builder.Services.AddDbContext<AutoservIdentityContext>(options =>
    options.UseSqlServer(identityString)
    .UseSnakeCaseNamingConvention()
    .EnableSensitiveDataLogging(true));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(jwt =>
    {
        var key = Encoding.ASCII.GetBytes(AuthorizationConstants.JWT_SECRET_KEY);

        jwt.SaveToken = true;
        jwt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            RequireExpirationTime = false
        };
    });

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AutoservIdentityContext>();

MapperConfiguration mapperConfig = new(mc =>
{
    mc.AddProfile(new BrandProfile());
    //mc.AddProfile(new CategoryProfile());
    //mc.AddProfile(new ProductProfile());
    //mc.AddProfile(new ProviderProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers(o =>
                o.Filters.Add(typeof(ExeptionFilter)));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AutoServ.CRM.WebAPI",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Khrapal Maksym",
            Email = "khrapal_m_g@outlook.com",
            Url = new Uri("https://khrapal-max.github.io/homepage/")
        },
    });
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, Assembly.GetExecutingAssembly().GetName().Name + ".xml"));
});

builder.Services.AddScoped<IRepository<Brand>, BrandRepository>();
builder.Services.AddScoped<IBaseService<NewBrandDTO, BrandDTO>, BrandService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

/// <summary>
/// For tests
/// </summary>
public partial class Program { }
