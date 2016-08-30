var Point = (function () {
    function Point(x, y) {
        this.x = x;
        this.y = y;
    }
    Point.prototype.getDist = function () {
        return Math.sqrt(this.x * this.x);
    };
    return Point;
}());
var p = new Point(3, 4);
var dist = p.getDist();
alert('distance = ' + dist);
