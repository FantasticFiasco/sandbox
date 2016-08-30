/// <reference path="shapes.ts" />
/// <reference path="loggers.ts" />

var p: Shapes.IPoint = new Shapes.Point(3, 4);
var dist = p.getDist();

var logger: Loggers.ILogger = new Loggers.Logger();
logger.log(`distance: ${dist}`);