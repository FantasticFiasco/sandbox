require('underscore')(global).extend(require('./common'))

// Funktioner som vi kan använda:
//   1. skriv_ut_ägg_klocka
//   2. gör_så_länge
//   3. antal_sekunder_är_större_än_0
//   4. skriv_ut_antal_sekunder_kvar
//   5. räkna_ner_antal_sekunder
//   6. spela_ljud

antal_sekunder = 5;

skriv_ut_ägg_klocka();

function skriv_ut_och_räkna_ner() {
    skriv_ut_antal_sekunder_kvar();
    räkna_ner_antal_sekunder();
}

gör_så_länge(antal_sekunder_är_större_än_0,
    skriv_ut_och_räkna_ner,
    spela_ljud);