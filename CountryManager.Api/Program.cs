using CountryManager.Domain.Interfaces;
using CountryManager.Domain.Services;
using CountryManager.Infrastructure;
using CountryManager.Infrastructure.Interfaces;
using CountryManager.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:Default"], builder =>
{
    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
}));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Add valid issuer and audience if change first 2 options to true
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ICountryRepository, CountryRepository>();
builder.Services.AddTransient<IRegionRepository, RegionRepository>();
builder.Services.AddTransient<ICityRepository, CityRepository>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<ICountryService, CountryService>();
builder.Services.AddTransient<IRegionService, RegionService>();
builder.Services.AddTransient<ICityService, CityService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});


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
