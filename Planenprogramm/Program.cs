using System;
using System.IO;
using ExcelDataReader;
using System.Linq;
using static Planenprogramm.Constants;
using System.Collections.Generic;
//using System.Text.Encoding.CodePages;

namespace Planenprogramm
{
	class Program
	{
		static string filePath = @"D:\Gamer\Documents\Planenprogramm\Planen.xlsx";
		static void Main(string[] args)
		{
			System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

			using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
			{
				var database = new Database();

				// Ensures that the basic tarp types and categories are in the database
				PrepareTarpTypesAndCategories(stream, database);

				// Reads the excel document to fill the tarps database
				using (var reader = ExcelReaderFactory.CreateReader(stream))
				{
					do
					{
						for(int i = 0; i < SkipStart; i++)
						{
							if (!reader.Read())
							{
								throw new InvalidOperationException("Nicht genug Zeilen vorhanden!");
							}                            
						}

						while (reader.Read())
						{
							var tarpTypeName = reader.GetString(IndexTarpType);

							if(!String.IsNullOrWhiteSpace(tarpTypeName))
							{
								var tarpNumber = (int)reader.GetDouble(IndexTarpNumber);
								var tarpAnnotation = reader.GetString(IndexTarpAnnotation);
								var tarpType = GetTarpType(database, tarpTypeName.Trim());
								var tarpCategoryName = reader.GetString(IndexTarpCategory);
								var tarpCategory = GetTarpCategory(database, tarpType, tarpCategoryName);

								database.Tarps.Add(new Tarp 
								{ 
									TarpTypeId = tarpType.TarpTypeId , 
									TarpType = tarpType,
									TarpCategoryId = tarpCategory.TarpCategoryId, 
									TarpCategory = tarpCategory,
									Number = tarpNumber, 
									Annotation = tarpAnnotation});
							}
							
						}
					} while (reader.NextResult());

					database.SaveChanges();
				}

				DisplayDatabase(database);
			}

		}

		/// <summary>
		/// Displays the current database items on the console.
		/// </summary>
		/// <param name="database"></param>
		private static void DisplayDatabase(Database database)
		{
			if (database is null)
			{
				throw new ArgumentNullException(nameof(database));
			}

			Console.WriteLine("TarpTypes:");

			foreach (var tarpType in database.TarpTypes)
			{
				Console.WriteLine($"{tarpType.TarpTypeId}: '{tarpType.Name}'");
			}

			Console.WriteLine();
			Console.WriteLine("TarpCategories:");

			foreach (var tarpCategory in database.TarpCategories)
			{
				Console.WriteLine($"{tarpCategory.TarpCategoryId}: '{tarpCategory.TarpType.Name}' '{tarpCategory.Name}'  '{tarpCategory.Length}' '{tarpCategory.Width}' '{tarpCategory.Additional}'");
			}

			Console.WriteLine();
			Console.WriteLine("Tarps:");

			foreach (var tarp in database.Tarps)
			{
				var tarpType = database.TarpTypes.First(t => t.TarpTypeId == tarp.TarpTypeId).Name;

				Console.WriteLine($"{tarp.TarpId}: '{tarp.Number}' '{tarpType}' '{tarp.Annotation}'");
			}
		}

		/// <summary>
		/// Gets the tarp type with the specified <paramref name="tarpTypeName"/>.
		/// </summary>
		/// <param name="database">Database</param>
		/// <param name="tarpTypeName">Tarptype name</param>
		/// <returns>Tarp type</returns>
		/// <remarks>
		/// A new tarp type item is added if not matching tarp type was found.
		/// </remarks>
		private static TarpType GetTarpType(Database database, string tarpTypeName)
		{
			var item = database.TarpTypes.FirstOrDefault(t => t.Name.Equals(tarpTypeName));

			if(item == null)
			{
				item = database.TarpTypes.Add(new TarpType { Name = tarpTypeName }).Entity;
			}
			return item;
		}

		/// <summary>
		/// Gets the tarp category with the specified <paramref name="tarpTypeId"/> and <<paramref name="tarpCategoryName"/>.
		/// </summary>
		/// <param name="database">Database</param>
		/// <param name="tarpType">Category tarp type</param>
		/// <param name="tarpCategoryName">Category name</param>
		/// <returns>Tarp category</returns>
		/// <remarks>
		/// A new category item is added if no matching category was found.
		/// </remarks>
		private static TarpCategory GetTarpCategory(Database database, TarpType tarpType, string tarpCategoryName)
		{
			var item = database.TarpCategories.FirstOrDefault(c => 
				c.TarpTypeId == tarpType.TarpTypeId && 
				c.TarpType.Name == tarpType.Name &&
				c.Name == tarpCategoryName);

			if (item == null)
			{
				item = database.TarpCategories.Add(new TarpCategory 
				{
					Name = tarpCategoryName, 
					TarpTypeId = tarpType.TarpTypeId,
					TarpType = tarpType
				}).Entity;
			}
			return item;
		}

		/// <summary>
		/// Ensures that the tarp types for the tarp category definitions are available
		/// </summary>
		/// <param name="database">Database</param>
		/// <returns>Category tarp types in the order defined by <see cref="CategoryTarpTypes"/></returns>
		/// <remarks>
		/// <para>The excel document contains an area in which tarp categories are defined. The area is limited by the constants
		/// <see cref="SkipStartCategories"/> and <see cref="CountCollumnCategories"/>. Each category is related to a certain
		/// tarp type e.g., a tarp category 'A' may be defined for 'Kothenplane' and 'Jurtenopi'. 
		/// </para>
		/// <para>Before the <see cref="TarpCategoryBuilder"/> can create the category definitions, the corresponding tarp types
		/// must be available. This method creates all entities for <see cref="CategoryTarpTypes"/> which do not exist yet and
		/// returns the collection of tarp type IDs.
		/// </para>
		/// </remarks>
		private static IEnumerable<TarpType> GetCategoryTarpTypes(Database database)
		{
			// Create the tarp types for which category definitions are available, if they do not exist in database
			foreach (var tarpType in CategoryTarpTypes)
			{
				GetTarpType(database, tarpType);
			}

			database.SaveChanges();

			foreach (var tarpType in CategoryTarpTypes)
			{
				yield return GetTarpType(database, tarpType);
			}
		}

		/// <summary>
		/// Ensures that the database contains the known tarp types and categories.
		/// </summary>
		/// <param name="stream">Input document</param>
		/// <param name="database">Database</param>
		/// <remarks>
		/// <para>The collection <see cref="CategoryTarpTypes"/> defines for which tarp types categories are defined in the
		/// input document. This method invokes <see cref="GetCategoryTarpTypes(Database)"/> first to ensure that the
		/// corresponding database entities are available.
		/// </para>
		/// <para>In a second step <see cref="TarpCategoryBuilder.Build(Stream, int[])"/> is called to parse the category
		/// definitions from the input document and add them to the database, if they do not exist.
		/// </para>
		/// </remarks>
		private static void PrepareTarpTypesAndCategories(Stream stream, Database database)
		{
			var categoryTarpTypes = GetCategoryTarpTypes(database).ToArray();

			foreach (var category in TarpCategoryBuilder.Build(stream, categoryTarpTypes.ToArray()))
			{
				if (!database.TarpCategories.Any(cat => cat.Name == category.Name && cat.TarpTypeId == category.TarpTypeId))
				{
					database.TarpCategories.Add(category);
				}
			}

			database.SaveChanges();
		}


	}
}
