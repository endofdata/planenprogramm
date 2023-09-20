using Microsoft.EntityFrameworkCore;
using Tarps.Datalayer;

namespace Tarps.WebAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var services = builder.Services;

			services.AddDbContext<TarpsDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("tarpsDatabase")));

			services.AddControllers();
			services.AddAuthorization();

			var app = builder.Build();

			app.UseHttpsRedirection();
			app.UseAuthorization();
			app.MapControllers();			

			app.Run();
		}
	}
}