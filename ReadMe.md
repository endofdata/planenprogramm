# Planenprogramm

Um die Zeltplanensammlung der Pfadfinder zu verwalten, wurde eine Excel-Tabelle erstellt, in der Planen verschiedenen Type, Kategorien und Qualitäts-Stufen zugeordnet werden. Ziel des *Planenprogramm*s ist das Auslesen der Excel-Tabelle um daraus eine Datenbank (z.Zt. SQLite) aufzubauen.

# Status

Es werden drei Datenbanktabellen befüllt: ```TarpTypes``` für die Planentypen, ```TarpCategories``` für die Kategorien und ```Tarps``` für die eigentlichen Planen. Die Kategorien-Namen ('Typ A', 'Typ B'...) wurden für die unterschiedlichen Planentypen wiederverwendet. Daher verweiste jede ```TarpCategory``` auf einen ```TarpType```.

Das Daten-Modell sollte also noch vereinfacht werden:

- ```TarpCategory``` verweist auf einen ```TarpType```
- ```Tarp``` verweist auf eine ```TarpCategory``` *und* einen ```TarpType``` obwohl der sich aus der ```TarpCategory``` schon ergibt

Der Name der Datenbankdatei ist noch mit vollem Dateipfad im Programmquelltext festgelegt.

# Die Quelldaten 

Die Daten in der Excel-Tabelle enthalten noch ein paar Ungereimtheiten:

- es gibt unterschiedliche / fehlerhafte Schreibweisen der Planentypen
- es wird auf Kategorien verwiesen, zu denen keine Definition existiert

Das Programm ergänzt fehlende Kategorien mit unvollständigen Definitionen. Unterschiedliche Schreibweisen der Planentypen werden nicht korrigiert oder zusammengeführt.

Für das Aufräumen der Excel-Tabelle können die erstellten Datenbanken hilfreich sein. 

