using Tarps.Datalayer.Entities;

namespace Tarps.WebAPI.Model
{
	public class CategoryResponse
	{
		public string? Name
		{
			get; set;
		}

		public int? Length
		{
			get; set;
		}

		public int? Width
		{
			get; set;
		}

		public int? Additional
		{
			get; set;
		}

		public TypeResponse? TarpType
		{
			get; set;
		}

	}
}
