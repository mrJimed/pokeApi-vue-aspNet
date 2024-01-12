using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PokeApi.Server.DbContexts;
using PokeApi.Server.Services.Classes;
using PokeApi.Server.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Database config
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Redis config
var redisConfig = builder.Configuration.GetSection("Redis:Configuration").Value;
var redisInstanceName = builder.Configuration.GetSection("Redis:InstanceName").Value;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
        options.SlidingExpiration = true;
    });

// Redis cache
builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = redisConfig;
    options.InstanceName = redisInstanceName;
});

// My services
builder.Services.AddDbContext<PokeApiDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddTransient<IPokemonService, PokemonService>();
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
