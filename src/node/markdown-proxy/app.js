/// <reference path="typings/main.d.ts" />

var app = require('express')();
var logger = require('./io/logger');
var network = require('./io/network');
var markdownParser = require('./markdown/parser');

/**
 * Express configuration that routes all incoming requests.
 */
app.get('/*', function(req, res) {
  
  logger.log(req.connection.remoteAddress, req.url);
    
  if (req.query.url) {
    // A request for a Markdown since the parameter 'url' has been specified
    requestMarkdown(req.query.url, res);
  }
  else if (req.header('Referer')) {
    // A request for referenced file from the Markdown, e.g. a path of an image
    redirectReference(req.header('Referer'), req.path, res);
  }
  else {
    // Invalid request, show usage
    showUsage(res);
  }
});

/**
 * Requests the Markdown and responds with the parsed HTML if successful: otherwise responds with
 * an error message. 
 * @param {string} url The location of the Markdown.
 * @param {Express.Response} res The response to finalize.
 */
function requestMarkdown(url, res) {
  network.get(url, function(error, markdownText) {
    if (error) {
      showError(res, `Reading Markdown failed due to: _${error}_`);
    }
    else {
      res.send(markdownParser.toHtml(markdownText));
    }
  });
}

/**
 * Redirects to the true location of the reference. Since references doesn't require Markdown
 * parsing, the proxy don't have to process the request.
 * @param {string} refererUrl The referer of the reference request.
 * @param {string} requestPath The path of the reference request.
 * @param {Express.Response} res The response to finalize.
 */
function redirectReference(referer, requestPath, res) {
  var redirectUrl = network.getReferenceRedirectUrl(referer, requestPath);
  res.redirect(redirectUrl);
}

/**
 * Responds 
 * @param {Express.Response} res The response to finalize.
 */
function showUsage(res) {
  showError(res, 'Invalid request, the request must have the parameter [/?url=[url]](/?url=[url]) where `[url]` is the location of the Markdown.');
}

/**
 * Responds with an error message.
 */
function showError(res, errorMessage) {
  res.send(markdownParser.toHtml(`# Error \n${errorMessage}`));
}

/**
 * Start the web server.
 */
var server = app.listen(3000, function () {
  console.log('Starting to listen for requests on port %s', server.address().port);
});