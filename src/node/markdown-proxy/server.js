/// <reference path="typings/main.d.ts" />

var app = require('express')();
var request = require('request');
var marked = require('marked');

app.get('/', function(req, res) {
  var url = req.query.url;
  
  // Validate that parameter named 'url' exist
  if (!url) {
    sendMarkdownError(res, 'Please specify a parameter named `url` that locates the markdown file.');
    return;
  }
  
  // Create request for reading markdown
  request(url, function(error, response, body) {
    if (!error && response.statusCode == 200) {
      sendMarkdownSuccess(res, body);
    }
    else {
      sendMarkdownError(res, `Reading markdown failed due to status: __${response.statusMessage}__`);
    }
  });
});

function sendMarkdownError(response, errorMessage) {
  response.send(marked(`# Error \n${errorMessage}`));
}

function sendMarkdownSuccess(response, message) {
  response.send(marked(message));
}

app.listen(3000, function () {
  console.log('Starting to listen for requests on port 3000');
});