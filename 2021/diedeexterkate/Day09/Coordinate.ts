export class Coordinate {
	/**
	 * The constructor.
	 */
	public constructor( public readonly x: number, public readonly y: number ) {
	}

	public left() {
		return new Coordinate( this.x - 1, this.y );
	}

	public right() {
		return new Coordinate( this.x + 1, this.y );
	}

	public up() {
		return new Coordinate( this.x, this.y + 1 );
	}

	public down() {
		return new Coordinate( this.x, this.y - 1 );
	}
}
