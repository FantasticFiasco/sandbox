var express = require('express');
var bodyParser = require('body-parser');
var helloWorldController = require('./controllers/helloWorldController');

var app = express();
app.use(bodyParser.json());

helloWorldController.init(app);

app.listen(3000, function() {
	console.log('Listening on port 3000');
})