var Shapes;
(function (Shapes) {
    var Point = (function () {
        function Point(x, y) {
            this.x = x;
            this.y = y;
        }
        Point.prototype.getDist = function () {
            return Math.sqrt(this.x * this.x + this.y * this.y);
        };
        return Point;
    }());
    Shapes.Point = Point;
    ;
    var Rectangle = (function () {
        function Rectangle(width, height) {
            this.width = width;
            this.height = height;
        }
        return Rectangle;
    }());
    Shapes.Rectangle = Rectangle;
})(Shapes || (Shapes = {}));
