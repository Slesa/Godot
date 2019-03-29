# Tisch

- [X] Jeder Tisch hat eine Tisch- und eine Parteinummer. Beide Nummern sind größer 0. Jeder Tisch hat eine Liste von Tischereignissen (Events).
- [X] Jeder TischEvent hat eine Id, ein Datum, wann er aufgetreten ist, sowie die Daten des Kellners, der das Command und damit den Event ausgelöst hat.
- [o] TischEvents, die sich auf dem befinden, können nicht mehr entfernt oder geändert werden.
- [X] Der erste Tischevent ist das Erzeugen des Tisches. Er wird erst bei der ersten Bestellung auf den Tisch eingetragen.


## Bestellen

- [X] Eine Bestellung wird durch ein BestellCommand ausgelöst
- [ ] Wird die selbe Bestellung (laut Identität) erneut abgesetzt, wird sie nicht in den Tisch eingetragen
- [ ] Eine Bestellung gehört zu einer Runde. Solange eine Bestellrunde nicht abgeschlossen ist, bleibt die Runde gleich. Danach wird sie bei der nächsten Bestellung erhöht.

## Stornieren

## Bezahlen

## Splitten

## Wechseln
