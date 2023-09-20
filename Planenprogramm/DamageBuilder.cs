using ExcelDataReader;
using Tarps.Datalayer.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using static Planenprogramm.Constants;

namespace Planenprogramm
{
	static class DamageBuilder
	{
		public static IEnumerable<Damage> Build(Stream stream)
		{
			// Try to match values like 'K = Knopf fehlt' to a code ('K') and a description ('Knopf fehlt')
			var damageRegex = new Regex(@"(?<code>\w)\s*=\s*(?<desc>.*)");

			using (var reader = ExcelReaderFactory.CreateReader(stream, new ExcelReaderConfiguration { LeaveOpen = true }))
			{
				for (int row = 0; row < RowDamages; row++)
				{
					if (!reader.Read())
					{
						throw new InvalidOperationException($"Nicht genügend Zeilen bis zur 'Fehlercode'-Definition ({row} statt {RowDamages}).");
					}
				}

				for (int row = 0; row < NumDamages; row++)
				{
					if (!reader.Read())
					{
						throw new InvalidOperationException($"Nicht genügend 'Fehlercode'-Definitionen ({row} statt {NumDamages}).");
					}
					var cellValue = reader.GetString(IndexDamages);

					if (cellValue == null)
					{
						throw new InvalidOperationException($"Die 'Fehlercode'-Definition {row+1} fehlt.");
					}

					var match = damageRegex.Match(cellValue);

					if (!match.Success)
					{
						throw new InvalidOperationException($"Die 'Fehlercode'-Definition '{cellValue}' hat ein nicht unterstütztes Format.");
					}

					yield return new Damage { Code = char.ToUpper(match.Groups["code"].Value[0], CultureInfo.CurrentCulture), Description = match.Groups["desc"].Value };
				}
			}
		}
	}
}
