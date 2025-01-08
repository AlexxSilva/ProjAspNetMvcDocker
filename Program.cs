using Microsoft.EntityFrameworkCore;
using ProjAspNetMvcDocker.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();

// Adiciona o DbContext e configura a string de conexao
var server = builder.Configuration["DbServer"] ?? "localhost";
var port = builder.Configuration["DbPort"] ?? "1450"; // Default SQL Server port
var user = builder.Configuration["DbUser"] ?? "SA"; // Warning do not use the SA account
var password = builder.Configuration["Password"] ?? "123mudar@@";
var database = builder.Configuration["Database"] ?? "AptDb";

var connectionString = $"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password};Encrypt=False";

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build(); //criacao do app

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    DbInitializer.IniciaMigracao(app);
    DbInitializer.adicionadaDados(context);
}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Produtos}/{action=Index}/{id?}");

app.Run();
