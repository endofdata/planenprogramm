namespace Tarps.Datalayer.Entities
{
	public class Tarp
	{
		public int Id
		{
			get; set;
		}

		public int CategoryId
		{
			get; set;
		}

		public string? Annotation
		{
			get; set;
		} = null!;

		public int Number
		{
			get; set;
		}

		public TarpCategory Category
		{
			get; set;
		} = null!;

		public List<TarpDamage> TarpDamages
		{
			get; set;
		} = null!;
	}
}
