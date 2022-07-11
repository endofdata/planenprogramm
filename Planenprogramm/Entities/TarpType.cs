using System.Collections.Generic;

namespace Planenprogramm.Entities
{
	class TarpType
	{
		public int Id
		{
			get; set;
		}
		public string Name
		{
			get; set;
		} = null!;

		public List<TarpCategory> Categories
		{
			get; set;
		} = null!;
	}
}
