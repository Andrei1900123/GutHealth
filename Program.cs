using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GutHealth.Models;
using GutHealth.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;






var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<GutHealthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GutHealthDbDatabase")));
builder.Services.AddDbContext<LibraryIdentityContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("GutHealthDbDatabase")));
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
 .AddRoles<IdentityRole>()
 .AddEntityFrameworkStores<LibraryIdentityContext>();


builder.Services.AddRazorPages();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});



    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminPolicy", policy =>
       policy.RequireRole("Admin"));
    });



