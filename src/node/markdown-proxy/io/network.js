/// <reference path="../typings/main.d.ts" />

var request = require('request');
var url = require('url');
var fs = require('fs');
var config = require('./../configuration/config')

module.exports = {
        
    get: function(url, callback) {
        var options = {
          url: url,
          rejectUnauthorized: !config.acceptInvalidCertificates
        };
        
        request(options, function(error, response, body) {
            if (!error && response.statusCode == 200) {
                return callback(null, body);
            }
            
            if (error) {
                return callback(error);
            }
            
            return callback(response.statusCode + " " + response.statusMessage);
        });
    },
    
    getReferenceRedirectUrl: function(refererUrl, referencePath) {
        var referer = url.parse(refererUrl);
        
        // Regex for parsing e.g.
        //    'http://www.markdown.com'
        // from
        //    '?url=http://www.markdown.com/markdown_sample.md'
        var baseUrl = /\/\?url=(.+)(?:\/.+\.md)$/gi.exec(referer.path)[1];
        
        return baseUrl + referencePath;
    }
};