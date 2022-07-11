using System.Collections.Generic;

namespace Planenprogramm.Entities
{
	class TarpCategory
	{
		public int Id
		{
			get; set;
		}

		public string Name
		{
			get; set;
		} = null!;

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

		public int TarpTypeId
		{
			get; set;
		}

		public TarpType TarpType
		{
			get; set;
		} = null!;

		public List<Tarp> Tarps
		{
			get; set;
		} = null!;
	}
}
