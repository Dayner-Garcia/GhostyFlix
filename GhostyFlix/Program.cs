using Application.App_Management.IRepository;
using Application.App_Management.IServices;
using Application.App_Management.Repository;
using Application.App_Management.Services;
using Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connecString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connecString));

// Inyección de dependencias
builder.Services.AddScoped<ISeriesRepository, SeriesRepository>();
builder.Services.AddScoped<ISeriesServices, SeriesServices>();
builder.Services.AddScoped<IGendersRepostory, GendersRepository>();
builder.Services.AddScoped<IGendersServices, GendersServices>();
builder.Services.AddScoped<IProducersRepostory, ProducersRepository>();
builder.Services.AddScoped<IProducersServices, ProducersServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
