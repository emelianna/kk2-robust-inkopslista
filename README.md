# Felrapport

1. Programmet gick inte att starta. Jag läste felkoden och förstod att det hade med listan att göra då jag såg berörda kodrader. Programmet klarade inte av att hantera den tomma raden i txt-filen. Jag la till en if-sats i Load som hoppar över eventuell tom rad.

2. Jag testade att köra debuggern med breakpoint på rad 84 i Load för att se om jag kan få reda på varför inte varunamnen skrivs ut. Det kraschade och jag fick svar att filen items.txt inte hittas. Jag provade att göra try catch och döpte om filen för att testa. Nu säger den till om det inte finns någon fil istället för att krascha. Jag hittade ett fel, men inte det jag sökte efter.

3. Felet med att varunamnet inte skrevs ut berodde på att det slank med \r intill varje varunamn i Load. \r läggs till i Save tillsammans med \n. Det senare tas bort då det delas upp. För att få bort \r la jag till Trim(). till parts[1]. Det som hände tidigare var att markören hoppade till början av raden igen då den stötte på \r. Den skrev då över varunamnet med priset.

4. Totalpriset stämde inte. Det visade sig att index 0 hoppades över eftersom räknaren i Total startade på 1 istället för 0. Nu räknas alla varor med.

5. Fixade så att felmeddelande ges om man inte skriver positivt heltal i pris när ny vara läggs till. Använde mig av whileloop och TryParse efter tips i artikel på bloggen.

   
