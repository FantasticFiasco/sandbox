/// <reference path="typings/main.d.ts" />

var marked = require('marked');
var util = require('util');

module.exports = {
    
    toHtml: function(markdownText) {
        var content = marked(markdownText);
        return util.format(htmlFormat, content);
    }
};

var htmlFormat =
        '<!DOCTYPE html>\
        <html>\
            <head>\
            </head>\
            <body>\
                %s\
            </body>\
        </html>';