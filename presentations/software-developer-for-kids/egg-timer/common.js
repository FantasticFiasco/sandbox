const imageToAscii = require('image-to-ascii');
const player = require('play-sound')(opts = {});


var antal_sekunder = 5;

function skriv_ut_ägg_klocka() {
    imageToAscii('./egg-timer.png', { size: { height: 20 }, bg: true }, (err, converted) => {
        console.log(converted);
    });
}

function gör_så_länge(condition, callback, endCallback) {
    const handle = setInterval(
        () => {
            if (condition()) {
                callback();
            } else {
                clearInterval(handle);

                if(endCallback) {
                    endCallback();
                }
            }
        },
        1000);
}

function antal_sekunder_är_större_än_0() {
    return antal_sekunder > 0;
}

function skriv_ut_antal_sekunder_kvar() {
    if (antal_sekunder === 1) {
        console.log(`${antal_sekunder} sekund kvar`);
    }
    else {
        console.log(`${antal_sekunder} sekunder kvar`);
    }
}

function räkna_ner_antal_sekunder() {
    antal_sekunder--;
}

function spela_ljud() {
    player.play('./rooster.wav', { mplayer: ['-ao', 'alsa'] });
}

module.exports = {
    antal_sekunder,
    skriv_ut_ägg_klocka,
    gör_så_länge,
    antal_sekunder_är_större_än_0,
    skriv_ut_antal_sekunder_kvar,
    räkna_ner_antal_sekunder,
    spela_ljud
};
