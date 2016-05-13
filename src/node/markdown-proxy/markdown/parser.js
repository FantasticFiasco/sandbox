/// <reference path="../typings/main.d.ts" />

var marked = require('marked');
var util = require('util');

var htmlFormat =
	'<!DOCTYPE html>\
	<html>\
		<head>\
			<link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/github-markdown-css/2.3.0/github-markdown.css">\
		</head>\
		<body>\
			<article class="markdown-body">\
				%s\
			<article>\
		</body>\
	</html>';

module.exports = {

	toHtml: function(markdownText) {
		var content = marked(markdownText);
		return util.format(htmlFormat, content);
	}
};