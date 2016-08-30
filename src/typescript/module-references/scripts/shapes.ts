namespace Shapes {
	export interface IPoint {
		getDist(): number;
	}

	export class Point implements IPoint {
		constructor(public x: number,
					public y: number) { }

		getDist() {
			return Math.sqrt(this.x * this.x + this.y * this.y);
		}
	} 

	export interface IRectngle {
		height: number;
		width: number;
	};

	export class Rectangle implements IRectngle {
		constructor(public width: number,
					public height: number) { }
	}
}