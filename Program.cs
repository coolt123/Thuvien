using ThuvienMvc.Models;
using Microsoft.EntityFrameworkCore;
using ThuvienMvc.Services.Implements;
using ThuvienMvc.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Data>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Mydb"));
});
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGenreservice, GenresService>();
builder.Services.AddScoped<IAuthorService, Authorservice>();
builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
