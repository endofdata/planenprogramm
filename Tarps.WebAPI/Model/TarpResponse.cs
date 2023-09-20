using Tarps.Datalayer.Entities;

namespace Tarps.WebAPI.Model
{
	public class TarpResponse
	{
		public int Id
		{
			get; set;
		}

		public string? Annotation
		{
			get; set;
		}

		public int Number
		{
			get; set;
		}

		public CategoryResponse? Category
		{
			get; set;
		}

		public IReadOnlyList<DamageResponse?> Damages
		{
			get; set;
		} = null!;
	}
}
