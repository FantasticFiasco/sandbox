/// <reference path="../typings/main.d.ts" />

var fs = require('fs');

module.exports = {
  
  log: function(client, url) {
      var message = client + ',' + url + '\n';
      
      fs.appendFile('logs.txt', message, (error) => {
          if (error) {
              console.log(error);
          }
      })
  }  
};