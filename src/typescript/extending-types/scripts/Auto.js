var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Engine = (function () {
    function Engine(horsePower, type) {
        this.horsePower = horsePower;
        this.type = type;
    }
    return Engine;
}());
var Auto = (function () {
    function Auto(engine) {
        this.engine = engine;
    }
    return Auto;
}());
var Truck = (function (_super) {
    __extends(Truck, _super);
    function Truck(engine, fourByFour) {
        _super.call(this, engine);
        this.fourByFour = fourByFour;
    }
    return Truck;
}(Auto));
window.onload = function () {
    var engine = new Engine(300, 'V8');
    var truck = new Truck(engine, true);
    alert('Type: ' + truck.engine.type);
    alert('4x4: ' + truck.fourByFour);
};
