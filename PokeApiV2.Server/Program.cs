using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PokeApiV2.Server.DbContexts;
using PokeApiV2.Server.Services.Classes;
using PokeApiV2.Server.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Database config
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Redis config
var redisConfig = builder.Configuration.GetSection("Redis:Configuration").Value;
var redisInstanceName = builder.Configuration.GetSection("Redis:InstanceName").Value;

// Mail config
var fromEmail = builder.Configuration.GetSection("Mail:From").Value;
var password = builder.Configuration.GetSection("Mail:Password").Value;
var host = builder.Configuration.GetSection("Mail:Host").Value;
var port = int.Parse(builder.Configuration.GetSection("Mail:Port").Value);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
builder.Services.AddSingleton<IPokemonService, PokemonService>();
builder.Services.AddSingleton<IPasswordHelper, PasswordHelper>();
builder.Services.AddSingleton<ICacheService, CacheService>();
builder.Services.AddSingleton<IMailService>(new MailService(fromEmail, password, host, port));

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
