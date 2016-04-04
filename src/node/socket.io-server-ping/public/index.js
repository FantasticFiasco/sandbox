var socket = io();
var count = -1;

$(document).ready(function() {
    // Start
    $('#start').text(formatDate(new Date()));

    // Count
    incrementCount();

    // Start listening for messages
    startListeningForMessages();
});

var startListeningForMessages = function () {
    socket.on('ping', function(message){
        // Increment count
        incrementCount();

        // Update last received message
        $('#last').text(message);
    });
};

var incrementCount = function() {
    count++;
    $('#count').text(count);
};

var formatDate = function(date) {
    // Date
    var day = date.getDate();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();

    // Time
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var seconds = date.getSeconds();

    if (hours < 10)
        hours = "0" + hours;

    if (minutes < 10)
        minutes = "0" + minutes;

    if (seconds < 10)
        seconds = "0" + seconds;

    return hours + ":" + minutes + "." + seconds + " " + day + "/" + month + "/" + year;
};