/// <reference path="shapes.ts" />
/// <reference path="loggers.ts" />
var p = new Shapes.Point(3, 4);
var dist = p.getDist();
var logger = new Loggers.Logger();
logger.log("distance: " + dist);
