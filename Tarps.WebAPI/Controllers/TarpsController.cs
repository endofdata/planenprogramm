using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarps.Datalayer;
using Tarps.Datalayer.Entities;
using Tarps.WebAPI.Model;

namespace Tarps.WebAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TarpsController : ControllerBase
	{
		private readonly ILogger<TarpsController> _logger;
		private readonly TarpsDbContext _dbContext;

		public TarpsController(ILogger<TarpsController> logger, TarpsDbContext dbContext)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		[HttpGet(Name = "GetTarps")]
		public IEnumerable<TarpResponse> Get()
		{
			return _dbContext.Tarps
				.Select(t => new TarpResponse
				{
					Id = t.Id,
					Number = t.Number,
					Annotation = t.Annotation,
					Category = _dbContext.Categories
						.Where(cat => cat.Id == t.CategoryId)
						.Select(cat => new CategoryResponse
						{
							Name = cat.Name,
							Length = cat.Length,
							Width = cat.Width,
							Additional = cat.Additional,
							TarpType = _dbContext.TarpTypes
								.Where(tt => tt.Id == cat.TarpTypeId)
								.Select(tt => new TypeResponse
								{
									Id = tt.Id,
									Name = tt.Name
								}).FirstOrDefault()
						}).FirstOrDefault(),
					Damages = t.TarpDamages.Select(td => _dbContext.Damages
						.Where(dmg => dmg.Id == td.DamageId)
						.Select(dmg => new DamageResponse
						{
							Id = dmg.Id,
							Code = dmg.Code,
							Description = dmg.Description
						}).FirstOrDefault()).ToList().AsReadOnly()
				})
			;
		}
	}
}
