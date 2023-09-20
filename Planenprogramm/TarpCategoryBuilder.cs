using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Tarps.Datalayer.Entities;
using static Planenprogramm.Constants;

namespace Planenprogramm
{
	static class TarpCategoryBuilder
	{
		public static IEnumerable<TarpCategory> Build(Stream stream, TarpType[] collumnTarpTypes)
		{
			// Try to match values like 'Typ A (S Kothen)' to just a type code ('A')
			var categoryNameRegex = new Regex("Typ ([A-Z])");

			for (int collumn = StartCollumnCategories; collumn < StartCollumnCategories + CountCollumnCategories; collumn++)
			{
				var offset = collumn - StartCollumnCategories;

				if (offset >= collumnTarpTypes.Length)
				{
					throw new ArgumentException($"Nicht genügend 'TarpTypes' in Spalte {collumn}");
				}
				var tarpType = collumnTarpTypes[offset];
				var tarpTypeId = tarpType.Id;

				using (var reader = ExcelReaderFactory.CreateReader(stream, new ExcelReaderConfiguration { LeaveOpen = true }))
				{
					for (int i = 0; i < SkipStartCategories; i++)
					{
						if (!reader.Read())
						{
							throw new InvalidOperationException("Nicht genug Zeilen vorhanden!");
						}
					}

					var valueCount = 0;
					var noValueCount = 0;
					TarpCategory? category = null;

					while (reader.Read())
					{
						var cellValue = reader.GetString(collumn);
						if (!String.IsNullOrWhiteSpace(cellValue))
						{
							switch (valueCount)
							{
								case 0:
									// Use type codes like 'A', if regex matches. Otherwise the original cell value
									// to see, where the regex did not match
									var match = categoryNameRegex.Match(cellValue);
									var categoryName = match.Success ? match.Groups[1].Value : cellValue;
									category = new TarpCategory { Name = categoryName };
									break;
								case 1:
									// NFO: if value is greater zero, category is not null
									category!.Length = Parse(cellValue);
									break;
								case 2:
									category!.Width = Parse(cellValue);
									break;
								case 3:
									category!.Additional = Parse(cellValue);
									break;
								default:
									throw new NotSupportedException("Denk dir einen schönen Fehlertext aus");
							}
							valueCount++;
							noValueCount = 0;
						}
						else
						{
							if(category != null)
							{
								valueCount = 0;
								category.TarpTypeId = tarpTypeId;
								category.TarpType = tarpType;
								yield return category;
								category = null;
							}
							if (++noValueCount >= 3)
							{
								break;
							}
						}
					}
					if (category != null)
					{
						category.TarpTypeId = tarpTypeId;
						category.TarpType = tarpType;
						yield return category;
					}
				}
			}
		}

		private static int? Parse(string cellValue)
		{
			var match = Regex.Match(cellValue, @"(\d+),(\d+)\s*m");
			if (match.Success)
			{
				if(!int.TryParse(match.Groups[1].Value, out var meters))
				{
					throw new InvalidOperationException("Soooo nicht!");
				}
				if (!int.TryParse(match.Groups[2].Value, out var centimeters))
				{
					throw new InvalidOperationException("Soooo auch nicht!");
				}
				return meters * 100 + centimeters;
			}
			return null;
		}
	}
}
