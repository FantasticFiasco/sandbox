class Engine {
	constructor(public horsePower: number,
				public type: string) { }
}

class Auto {
	constructor(public engine: Engine) { }
}

class Truck extends Auto {
	constructor(engine: Engine,
				public fourByFour: boolean) {
		super(engine);
	}
}

window.onload = function() {
	var engine  = new Engine(300, 'V8');
	var truck = new Truck(engine, true);
	alert('Type: ' + truck.engine.type);
	alert('4x4: ' + truck.fourByFour);
}