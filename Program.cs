using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies; // para manejo de cookies
using SISLOG.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Servicio para controlar las cookies reveer su uso
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Acceso/Login";
    options.AccessDeniedPath = "/Acceso/NoAutorizado";
    options.LogoutPath = "/Acceso/Logout";
    //options.ReturnUrlParameter = "/Clientes/Create";
    options.ExpireTimeSpan = new System.TimeSpan(2, 0, 0);
    options.SlidingExpiration = true;
});

//Entity Framework Core usa el contexto de la base de datos, junto con la cadena de conexión, para establecer una conexión con la base de datos. Debe indicar a Entity Framework Core qué contexto, cadena de conexión y proveedor de base de datos deben utilizar en el método.
var connectionString = builder.Configuration.GetConnectionString("SislogContext");
builder.Services.AddSqlite<SislogContext>(connectionString);

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Login}/{id?}");

app.Run();
