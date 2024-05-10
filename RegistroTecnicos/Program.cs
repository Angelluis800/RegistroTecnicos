using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Components;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//Obtenemos el ConStr para usarlo en el contexto
var ConStr = builder.Configuration.GetConnectionString("ConStr");

builder.Services.AddDbContext<Contexto>(Options => Options.UseSqlite(ConStr));

//builder.Services.AddScoped<TecnicoService>();

builder.Services.AddScoped<Tecnicos>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
