/// <reference path="typings/main.d.ts" />

var app = require('express')();
var request = require('request');
var marked = require('marked');
var url = require('url');

/**
 * Express configuration that routes all incoming requests.
 */
app.get('/*', function(req, res) {
  if (isMarkdownRequest(req)) {
    processMarkdownRequest(req, res);
  }
  else if (isReferenceRequest(req)) {
    processReferenceRequest(req, res);
  }
  else {
    processInvalidRequest(res);
  }
});

/**
 * Determines whether specified request is a request for a Markdown file.
 * @param {Express.Request} req The request to analyze.
 * @returns {Boolean} true if request is a Markdown request; otherwise false.
 */
function isMarkdownRequest(req) {
  return req.query.url;
}

/**
 * Processes the specified request and responds with rendered Markdown if successful: otherwise responds with an error message. 
 * @param  {Express.Request} req The Markdown request to process.
 * @param  {Express.Response} res The response to finalize.
 */
function processMarkdownRequest(req, res) {
  request(req.query.url, function(error, response, body) {
    if (!error && response.statusCode == 200) {
      res.send(marked(body));
    }
    else if (response) {
      sendMarkdownError(res, `Reading Markdown failed due to status: ${response.statusMessage}.`);
    }
    else {
      sendMarkdownError(res, 'Reading Markdown failed due to unknown error.');
    }
  });
}

/**
 * Determines whether specified request is a request for a reference. Example of a reference is an image defined in the Markdown file.
 * @param {Express.Request} req The request to analyze.
 * @returns {Boolean} true if request is a reference request; otherwise false.
 */
function isReferenceRequest(req) {
  return req.header('Referer');
}

/**
 * Processes the specified request and responds with redirect if successful: otherwise responds with an error message. 
 * @param  {Express.Request} req The request for a reference to process.
 * @param  {Express.Response} res The response to finalize.
 */
function processReferenceRequest(req, res) {
  var referer = url.parse(req.header('Referer'));
  
  // Regex for parsing e.g.
  //    'http://www.markdown.com'
  // from
  //    'http://<host>/?url=http://www.markdown.com/markdown_sample.md'
  var baseUrl = /\/\?url=(.+)(?:\/.+\.md)$/gi.exec(referer.path)[1];
  
  if (baseUrl) {
    var resourceUrl = baseUrl + req.path;
    res.redirect(resourceUrl);
  }
  else {
    sendMarkdownError(res, `Unable to parse the location of the Markdown from ${referer.path}.`);
  }
}

/**
 * Finalizes the specified response by sending an error message that displays the usage of the appliction. 
 * @param {Express.Response} res The response to finalize.
 */
function processInvalidRequest(res) {
  sendMarkdownError(res, 'Invalid request, the request must have the parameter [/?url=[url]](/?url=[url]) where `[url]` is the location of the markdown.');
}

/**
 * Finalizes the specified response with the specified error message.
 */
function sendMarkdownError(res, errorMessage) {
  res.send(marked(`# Error \n${errorMessage}`));
}

/**
 * Start the web server.
 */
app.listen(3000, function () {
  console.log('Starting to listen for requests on port 3000');
});