# Planenprogramm

Um die Zeltplanensammlung der Pfadfinder zu verwalten, wurde eine Excel-Tabelle erstellt, in der Planen verschiedenen Type, Kategorien und Qualitäts-Stufen zugeordnet werden. Ziel des *Planenprogramm*s ist das Auslesen der Excel-Tabelle um daraus eine Datenbank (z.Zt. SQLite) aufzubauen.

Das Programm benötigt zwei Aufruf-Parameter
```
Planenprogramm.exe --data <Datenpfad> --input <ExcelDatei>
```
Für das Debuggen in VisualStudio können die Parameter relativ zur ausführbaren Datei ```.\bin\Debug\net6.0\Planenprogramm.exe``` angegeben werden:
```
--data ..\..\..\data --input ..\..\..\..\Planen.xlsx
```

# Status

Es werden drei Datenbanktabellen befüllt: ```TarpTypes``` für die Planentypen, ```TarpCategories``` für die Kategorien und ```Tarps``` für die eigentlichen Planen. Die Kategorien-Namen ('Typ A', 'Typ B'...) wurden für die unterschiedlichen Planentypen wiederverwendet. Daher verweiste jede ```TarpCategory``` auf einen ```TarpType```.

# In Planung

## Abfrageprogramm

Ein Programm für ```Raspberry Pi``` soll eine Reihe von vordefinierten Datenbank-Anfragen ermöglichen und das Ergebnis tabellarisch darstellen.

Der Datenbestand soll auch in Tabellenform (wenn möglich: Excel-Format) exportierbar sein.

## Datenpflege

Der Datenbestand soll auch nachbearbeitet werden können. Der Zugriff für die Bearbeitung soll über Benutzername / Passwort geschützt sein. Unter Umständen ist hierfür ein eigenes Programm sinnvoll.


# Die Quelldaten 

Die Daten in der Excel-Tabelle enthalten noch ein paar Ungereimtheiten:

- es gibt unterschiedliche / fehlerhafte Schreibweisen der Planentypen
- es wird auf Kategorien verwiesen, zu denen keine Definition existiert

Das Programm ergänzt fehlende Kategorien mit unvollständigen Definitionen. Unterschiedliche Schreibweisen der Planentypen werden nicht korrigiert oder zusammengeführt.

Für das Aufräumen der Excel-Tabelle können die erstellten Datenbanken hilfreich sein. 

# Howto

## EF Core Tools installieren

Für die ```dotnet ef``` commands müssen die EFCore tools installiert werden, z.B. global mit:

```PS
dotnet tool install --global dotnet-ef
```

Außerdem wird noch das Design-Time Paket installiert. Dieser Befehl muss im Projektverzeichnis (da, wo die *.csproj-Datei liegt) ausgeführt werden:

```PS
dotnet add package Microsoft.EntityFrameworkCore.Design
```

## Datenbank-Migration erstellen

Für das Anlegen der Datenbank aus dem Modell sowie nach Veränderungen am Modell können *Migrationen* erstellt werden. Der folgende Befehl erstellt eine Migration mit dem Namen *Initial*. Weitere Migrationen sollten andere sprechende Namen verwenden. Dabei werden die Parameter hinter dem ```--``` an die Methode ```DatabaseFactory.CreateContext()``` übergeben, die den ersten (und einzigen) Parameter als Datenpfad für die SQLite-Datenbank verwendet.

```PS
dotnet ef migrations add Initial -- .\data
```

## Datenbank initialisieren oder aktualisieren

Mit dem folgenden Befehl wird die Datenbank entsprechend der Migrationen aktualisiert. Die Parameter hinter dem ```--``` werden auch hier an die ```DatabaseFactory``` übergeben.

```PS
dotnet ef database update -- .\data
```
