namespace Planenprogramm.Entities
{
	class Tarp
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
	}
}
