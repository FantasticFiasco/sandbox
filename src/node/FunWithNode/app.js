console.log('Hello world');

var x = 10;
var y = 25;

console.log(x * y);

var msgs = require('./msgs.js');
console.log(msgs.first);
console.log(msgs.second);
console.log(msgs.third);

var functions = require('./functions.js');
var implementation = new functions();
console.log(implementation.first);

var logger = require('./logger');
logger.log('This is from the logger');

var underscore = require('underscore');