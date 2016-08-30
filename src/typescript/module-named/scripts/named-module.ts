interface IRectangle {
	height: number;
	width: number;
	getArea(): number;
}

namespace Shapes {
	export class Rectangle implements IRectangle {
		constructor(public height: number,
					public width: number) {
		}

		getArea() {
			return this.height * this.width;
		}
	}
}

var rect : IRectangle = new Shapes.Rectangle(10, 4);
var area = rect.getArea();
alert(area);