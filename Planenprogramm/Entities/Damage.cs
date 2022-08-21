using System.Collections.Generic;

namespace Planenprogramm.Entities
{
	class Damage
	{
		public int Id
		{
			get; set;
		}

		public char Code
		{
			get; set;
		}

		public string Description
		{
			get; set;
		} = null!;

		public List<TarpDamage> TarpDamages
		{
			get; set;
		} = null!;
	}
}
