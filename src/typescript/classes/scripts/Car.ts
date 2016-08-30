class Engine {
	constructor(public horsePoser: number,
				public engineType: string) {}
}

class Car {
	private _engine: Engine;

	constructor(engine: Engine) {
		this.engine = engine;
	}

	get engine() : Engine {
		return this._engine;
	}

	set engine(engine: Engine) {
		if (engine == undefined) throw 'Please supply an engine';
		this._engine = engine;
	}

	start() : void {
		alert('Car engine started ' + this._engine.engineType);
	}
}

window.onload = function() {
	var engine = new Engine(300, 'V8');
	var car = new Car(engine);
	car.start();
}