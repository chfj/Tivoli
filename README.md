# Tivoli

I forbindelse med GDPR skal vi anonymisere udvalgte kontaktinformationer, så man ikke længere kan se deres navn, adresser osv.
 
Til at køre dette, er der skrevet et anonymiseringsprogram, der anonymiserer 100.000 kontakter af gangen. Vi har I alt 3.000.000 kontakter, så anonymiseringsprogrammet skal køres 30 gange. Når anonymiseringsprogrammet kører, vil det generere en logfil over eventuelle fejl fra anonymiseringen.
 
Vedlagt er et eksempel på en logfil, du kan bruge som input.
 
Vi har behov for et C# konsolprogram, der kan indlæse logfilen og besvarer følgende spørgsmål:
·        Hvor mange rækker er der af hver statuskode
·        Hvad er gennemsnittet af millisecods spent
·        Største og mindste værdi af millisecods spent
·        Hvad er den mindste balance, der er udestående på en ordre
 
Skriv resultaterne ud på konsollen. Størrelsen på logfilerne vil være mellem 0 og 5 mb.
 
Opgaven løses uden NuGet pakker eller andre ”hjælpere” udefra.
 

Vi ses på fredag kl. 13 – vi glæder os!