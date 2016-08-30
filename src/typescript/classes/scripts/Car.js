var Engine = (function () {
    function Engine(horsePoser, engineType) {
        this.horsePoser = horsePoser;
        this.engineType = engineType;
    }
    return Engine;
}());
var Car = (function () {
    function Car(engine) {
        this.engine = engine;
    }
    Object.defineProperty(Car.prototype, "engine", {
        get: function () {
            return this._engine;
        },
        set: function (engine) {
            if (engine == undefined)
                throw 'Please supply an engine';
            this._engine = engine;
        },
        enumerable: true,
        configurable: true
    });
    Car.prototype.start = function () {
        alert('Car engine started ' + this._engine.engineType);
    };
    return Car;
}());
window.onload = function () {
    var engine = new Engine(300, 'V8');
    var car = new Car(engine);
    car.start();
};
