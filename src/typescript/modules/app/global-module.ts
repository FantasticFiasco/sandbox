interface IPoint {
	getDist(): number;
}

class Point implements IPoint {
	constructor(public x: number, public y: number) {
	}

	getDist(): number {
		return Math.sqrt(this.x * this.x);
	}
}

var p: IPoint = new Point(3, 4);
var dist = p.getDist();
alert('distance = ' + dist);