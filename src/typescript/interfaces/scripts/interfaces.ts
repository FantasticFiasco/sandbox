module demo {

	interface SquareNumber {
		(x: number): number;
	}

	var squareBasic: SquareNumber = (num) => num * num;

	// interface rectangle
	interface Rectangle {
		h: number;
		w?: number;
	}

	var squareIt: (rect: Rectangle) => number;
	var rectA = { h: 7 };
	var rectB = { h: 7, w: 12 };

	squareIt = function(rect) {
		if (rect.w === undefined) {
			return rect.h * rect.h;
		}

		return rect.h * rect.w;
	}

	console.log(squareIt(rectA));
	console.log(squareIt(rectB));

	// person interface
	interface Person {
		name: string;
		age?: number;
		kids: number;
		calcPets: () => number;
		makeYounger: (years: number) => void;
		greet: (msg: string) => string;
	}

	var p: Person = {
		name: 'Coleen',
		age: 40,
		kids: 4,
		calcPets: function() {
			return this.kids * 2;
		},
		makeYounger: function(years: number) {
			this.age -= years;
		},
		greet: function(msg: string) {
			return msg + ', ' + this.name;
		}
	};

	var pets = p.calcPets();
	console.log(pets);
	p.makeYounger(15);
	var newAge = p.age;
	console.log(newAge);

	var msg = p.greet('Good day to you');
	console.log(msg);
}