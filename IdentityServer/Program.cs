using IdentityServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddIdentityServer()
    .AddInMemoryClients(Config.clients)   
    .AddInMemoryApiScopes(Config.apiScopes)    
    .AddInMemoryIdentityResources(Config.identityResources)
    .AddTestUsers(Config.testUsers)
    .AddDeveloperSigningCredential();
    
var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
    

app.UseIdentityServer();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});


app.Run();
