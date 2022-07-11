using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace Planenprogramm
{
	internal class CommandLine
	{
		public string? DataDirectory
		{
			get; set;
		}

		public string? InputPath
		{
			get; set;
		}

		public bool ShowHelp
		{
			get; set;
		}

		public static CommandLine Parse(IEnumerable<string>? args)
		{
			var commandLine = new CommandLine();

			if (args?.Any() != true)
			{
				commandLine.ShowHelp = true;
			}
			else
			{
				var enumerator = args.GetEnumerator();

				while (enumerator.MoveNext())
				{
					switch (enumerator.Current)
					{
					case "--input":
					case "-i":
						commandLine.InputPath = Path.GetFullPath(GetNext(enumerator));
						break;
					case "--data":
					case "-d":
						commandLine.DataDirectory = Path.GetFullPath(GetNext(enumerator));
						break;
					case "--help":
					case "-h":
					case "-?":
					case "/?":
						commandLine.ShowHelp = true;
						break;
					default:
						throw new ArgumentException($"Unerwarteter Parameter '{enumerator.Current}'.");
					}
				}
			}
			return commandLine;
		}

		[MemberNotNullWhen(true, nameof(InputPath), nameof(DataDirectory))]
		public bool Validate()
		{
			if (!ShowHelp)
			{
				if (InputPath == null)
				{
					throw new ArgumentException($"Fehlender Parameter '--input <Eingabedatei>'.");
				}

				if (!File.Exists(InputPath))
				{
					throw new ArgumentException($"Die Eingabedatei '{InputPath}' ist nicht vorhanden.");
				}

				if (!Directory.Exists(DataDirectory))
				{
					throw new ArgumentException($"Das Datenverzeichnis '{DataDirectory}' ist nicht vorhanden.");
				}

				if (DataDirectory == null)
				{
					throw new ArgumentException($"Fehlender Parameter '--data <Datenverzeichnis>'.");
				}
				return true;
			}
			return false;
		}

		public static void ShowUsage()
		{
			Console.WriteLine("Aufruf: Planenprogramm.exe --input <Eingabedatei> --data <Datenverzeichnis>");
		}

		private static string GetNext(IEnumerator<string> enumerator)
		{
			var key = enumerator.Current;

			if (enumerator.MoveNext())
			{
				return enumerator.Current;
			}
			throw new ArgumentException($"Hinter '{key}' fehlt der Parameterwert.");
		}
	}
}
