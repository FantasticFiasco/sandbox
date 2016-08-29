module demo {

	// Objects
	var points1 = { x: 10, y: 20 };

	var points2: {};
	points2 = { x: 10, y: 20 };

	var points3: Object = { x: 1 };

	var rectangle = {
		h: 10,
		w: 20,
		calcArea: function() {
			return this.h * this.w;
		}
	};

	console.log('rect area = ' + rectangle.calcArea());

	// Functions
	var squareIt1 = (x: number) => x * x;
	var val1 = squareIt1(2);
	console.log('squareIt1 = ' + val1);
	val1 = squareIt1(8);
	console.log('squareIt1 = ' + val1);

	var squareIt = (rect: { h: number; w?: number; })  => {
		if (rect.w === undefined) {
			return rect.h * rect.h;
		}

		return rect.h * rect.w;
	}

	var sq1 : number = squareIt({ h: 10 });
	console.log('rectangle h and w of 10 = ' + sq1);

	var sq2 : number = squareIt({ h: 10, w: 40 });
	console.log('rectangle h and w of 10 and 40 = ' + sq2);
}