var express = require('express');
var app = express();
var http = require('http').Server(app);
var io = require('socket.io')(http);
var port = 8080;

// Set the public static resource folder
app.use(express.static(__dirname + "/public"));

app.get('/', function(req, res) {
    res.sendFile(__dirname + '/public/index.html');
});

io.on('connection', function(socket) {
    console.log('a user connected');

    socket.on('disconnect', function() {
        console.log('user disconnected');
    });
});

http.listen(port, function() {
    console.log('listening on *:' + port);
});

// Send pings to clients
setInterval(function () {
    var message = formatDate(new Date());
    console.log('ping: ' + message);
    io.emit('ping', message);
},
15000);

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

    return hours + ":" + minutes + "." + seconds + " " + day + "/" + month + "/" + year;
};