var socket = io();
var count = -1;

$(document).ready(function() {
    // Start
    $('#start').text(formatDate(new Date()));

    // Count
    incrementCount();

    // Start listening for messages
    startListeningForMessages();

    // Start timer sending messages to server
    startSendingMessages();
});

var startListeningForMessages = function () {
    socket.on('message', function(message){
        // Increment count
        incrementCount();

        // Update last received message
        $('#last').text(message);
    });
};

var startSendingMessages = function() {
    setInterval(function() {
        socket.emit('message', formatDate(new Date()));
    },
    15000);
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

    if (minutes < 10)
        minutes = "0" + minutes

    return hours + ":" + minutes + "." + seconds + " " + day + "/" + month + "/" + year;
};