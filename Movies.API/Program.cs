using Microsoft.Build.Logging;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Writers;
using Movies.API.Data;

using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<MoviesAPIContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesAPIContext") 
//    ?? throw new InvalidOperationException("Connection string 'MoviesAPIContext' not found.")));

builder.Services.AddDbContext<MoviesAPIContext>(options =>
    options.UseInMemoryDatabase("Movies"));

builder.Services.AddAuthentication("JWTBearer")
    .AddJwtBearer("JWTBearer", options =>
    {
        options.Authority = "https://localhost:7213";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false           
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("clientIdPolicy", policy => policy.RequireClaim("client_id", "movieClient"));
});



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var movieApiContext = scope.ServiceProvider.GetRequiredService<MoviesAPIContext>();
MoviesContextSeed.SeedAsync(movieApiContext);


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

