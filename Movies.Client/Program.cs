using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Movies.Client.ApiServices;
using Movies.Client.Data;
using Movies.Client.HttpHandler;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MoviesClientContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesClientContext") 
    ?? throw new InvalidOperationException("Connection string 'MoviesClientContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IMovieApiService, MovieApiService>();

builder.Services.AddAuthentication(options => 
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options => 
    {
        options.Authority = "https://localhost:7213";
        options.ClientId = "movie_mvc_client";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        options.Scope.Add("openid");
        options.Scope.Add("profile");

        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;
    });


builder.Services.AddTransient<AuthenticationDelegatingHandler>();

builder.Services.AddHttpClient("MovieApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7180");
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
}).AddHttpMessageHandler<AuthenticationDelegatingHandler>();


builder.Services.AddHttpClient("IDPClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7213");
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});

builder.Services.AddSingleton(new ClientCredentialsTokenRequest 
{
    Address= "/connect/token",
    ClientId = "movieClient",
    ClientSecret = "secret",
    Scope = "movieAPI"
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
