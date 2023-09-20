namespace Tarps.Datalayer.Entities
{
	public class TarpDamage
	{
		public int Id
		{
			get; set;
		}

		public int TarpId
		{
			get; set;
		}

		public int DamageId
		{
			get; set;
		}

		public Tarp Tarp
		{
			get; set;
		} = null!;

		public Damage Damage
		{
			get; set;
		} = null!;
	}
}
