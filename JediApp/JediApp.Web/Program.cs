using JediApp.Database.Repositories;
using JediApp.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JediApp.Web.Areas.Identity.Data;
using JediApp.Database.Domain;
using Microsoft.Extensions.DependencyInjection;
using JediApp.Services;
using JediApp.Database.Interface;

//var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("JediAppDbContextConnection") ?? throw new InvalidOperationException("Connection string 'JediAppDbContextConnection' not found.");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<JediAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("JediAppDbContextConnection") ?? throw new InvalidOperationException("Connection string 'JediAppDbContextConnection' not found.")));

//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<JediAppDbContext>();
var connectionString = builder.Configuration.GetConnectionString("JediAppDbContextConnection") ?? throw new InvalidOperationException("Connection string 'JediAppDbContextConnection' not found.");


builder.Services.AddControllers();

builder.Services.AddDbContext<JediAppDbContext>(options =>
            options.UseSqlServer(connectionString));


//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
//    .AddEntityFrameworkStores<JediAppDbContext>();

//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
//   .AddEntityFrameworkStores<JediAppDbContext>();

builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<JediAppDbContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();

//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
//    .AddEntityFrameworkStores<JediAppDbContext>();

builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IExchangeOfficeBoardRepository, ExchangeOfficeBoardRepositoryDB>();
builder.Services.AddTransient<IExchangeOfficeBoardService, ExchangeOfficeBoardService>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<UserService>();


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
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
