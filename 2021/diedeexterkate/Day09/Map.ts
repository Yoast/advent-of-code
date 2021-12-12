import { Coordinate } from "./Coordinate";

export class SmokeMap {
	private checkedCoordinates: Coordinate[];

	/**
	 * The constructor.
	 */
	public constructor(
		private readonly map: number[][],
	) {
		this.checkedCoordinates = [];
	}

	public getDepth( coordinate: Coordinate ) {
		return this.map[ coordinate.y ][ coordinate.x ];
	}

	public hasPoint( coordinate: Coordinate ) {
		return this.map[ coordinate.y ] !== undefined && this.map[ coordinate.y ][ coordinate.x ] !== undefined;
	}

	public getLowPoints(): Coordinate[] {
		let lowPoints: Coordinate[] = [];

		for ( let y = 0; y < this.map.length; y++ ) {
			for ( let x = 0; x < this.map[ y ].length; x++ ) {
				const neighbours = [];
				// up
				if ( y > 0 ) {
					neighbours.push( this.map[ y - 1 ][ x ] );
				}
				// down
				if ( y < this.map.length - 1 ) {
					neighbours.push( this.map[ y + 1 ][ x ] );
				}
				// left
				if ( x > 0 ) {
					neighbours.push( this.map[ y ][ x - 1 ] );
				}
				// right
				if ( x < this.map[ y ].length - 1 ) {
					neighbours.push( this.map[ y ][ x + 1 ] );
				}
				if ( ! neighbours.some( ( neighbour ) => neighbour <= this.map[ y ][ x ] ) ) {
					lowPoints.push( new Coordinate( x, y ) );
				}
			}
		}

		return lowPoints;
	}

	public getBasins() {
		this.checkedCoordinates = [];
		const lowPoints = this.getLowPoints();
		const basins: number[] = [];
		for ( const lowPoint of lowPoints ) {
			basins.push( this.checkAdjacent( lowPoint ) );
		}
		return basins;
	}

	public checkAdjacent( target: Coordinate, origin: Coordinate | null = null ): number {
		if ( ! this.hasPoint( target ) ) {
			return 0;
		}
		if ( this.checkedCoordinates.some( ( checked ) => checked.x === target.x && checked.y === target.y ) ) {
			return 0;
		}
		if ( origin && this.getDepth( target ) <= this.getDepth( origin ) ) {
			return 0;
		}
		if ( this.getDepth( target ) === 9 ) {
			return 0;
		}

		this.checkedCoordinates.push( target );
		const left = this.checkAdjacent( target.left(), target );
		const top = this.checkAdjacent( target.up(), target );
		const right = this.checkAdjacent( target.right(), target );
		const bottom = this.checkAdjacent( target.down(), target );
		return 1 + left + top + right + bottom;
	}
}
