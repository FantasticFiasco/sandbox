/// <reference path="typings/main.d.ts" />

var request = require('request');

module.exports = {
        
    get: function(url, callback) {
        request(url, function(error, response, body) {
            if (!error && response.statusCode == 200) {
                return callback(null, body);
            }
            
            if (error) {
                return callback(error);
            }
            
            return callback(response.statusCode + " " + response.statusMessage);
        });
    },
    
    getReferenceRedirectUrl: function(markdownUrl, referencePath) {
        // Regex for parsing e.g.
        //    'http://www.markdown.com'
        // from
        //    'http://<host>/?url=http://www.markdown.com/markdown_sample.md'
        var baseUrl = /\/\?url=(.+)(?:\/.+\.md)$/gi.exec(markdownUrl)[1];
        
        return baseUrl + referencePath;
    }
};