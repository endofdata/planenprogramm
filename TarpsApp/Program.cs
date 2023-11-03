using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Tarps.Datalayer;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddRazorPages();
services.AddServerSideBlazor();
services.AddDbContext<TarpsDbContext>(options => 
	options.UseSqlite(connectionString: "DataSource=D:\\Gamer I5\\Documents\\Projects\\Planenprogramm\\Planenprogramm\\data\\tarps.sqlite"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
