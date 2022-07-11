using System.Collections.Generic;

namespace Planenprogramm
{
	static class Constants
	{
		/// <summary>
		/// Anzahl der Zeilen, die nicht zur Planentabelle gehören und daher übersprungen werden müssen.
		/// </summary>
		public const int SkipStart = 16;
		/// <summary>
		/// Anzahl der Zeilen, die nicht zur Legende von Maßen der Kategorien gehören.
		/// </summary>
		public const int SkipStartCategories = 15;
		/// <summary>
		/// Nummer der Spalte, ab der die Legende von Maßen der Planenkategorien beginnt
		/// </summary>
		public const int StartCollumnCategories = 10;
		/// <summary>
		/// Anzahl der Spalten der Legende von Maßen der Planenkategorien
		/// </summary>
		public const int CountCollumnCategories = 7;

		/// <summary>
		/// Index der Spalte für Nummern
		/// </summary>
		public const int IndexTarpNumber = 1;
		/// <summary>
		/// Index der Spalte für Planenarten
		/// </summary>
		public const int IndexTarpType = 2;
		/// <summary>
		/// Index der Spalte für Planen-Kategorie
		/// </summary>
		public const int IndexTarpCategory = 4;
		/// <summary>
		/// Index der Spalte für Anmerkungen
		/// </summary>
		public const int IndexTarpAnnotation = 5;
		
		/// <summary>
		/// TarpType für die Spalten im Bereich der Kategorie-Definitionen
		/// </summary>
		public static readonly IReadOnlyCollection<string> CategoryTarpTypes = new string[]
		{
			"Kothenplane",			// Spalte 'Typen der Planen Kothe'
			"Kaikoplane",			// Spalte 'Typen der Planen Kaikoplanen'
			"Jurtenplane",			// Spalte 'Typen der Jurtenplanen'
			"Doppeljurtenplane",	// Spalte 'Typen der doppelten Jurtenplanen'
			"Theaterplane",			// Spalte 'Typen der Theaterjurtenplanen'
			"Dach",					// Spalte 'Typen der Dächer'
			"Jurtenopi"				// Spalte 'Typen der Jurtenopis'
		};
	}
}
