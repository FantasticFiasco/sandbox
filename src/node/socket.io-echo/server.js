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

    socket.on('message', function(message) {
        console.log('message: ' + message);
        io.emit('message', message + ' (echo)');
    });

    socket.on('disconnect', function() {
        console.log('user disconnected');
    });
});

http.listen(port, function() {
    console.log('listening on *:' + port);
});