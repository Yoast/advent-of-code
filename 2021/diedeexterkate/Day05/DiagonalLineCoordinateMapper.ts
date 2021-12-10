import { Coordinate } from "./Coordinate";
import { Line } from "./Line";
import { LineCoordinateMapper } from "./LineCoordinateMapper";
import { StraightLineCoordinateMapper } from "./StraightLineCoordinateMapper";

export class DiagonalLineCoordinateMapper implements LineCoordinateMapper {
	constructor( private readonly straightLineCoordinateMapper: StraightLineCoordinateMapper ) {
	}

	public getCoordinates( line: Line ): Coordinate[] {
		if ( line.start.x === line.end.x || line.start.y === line.end.y ) {
			return this.straightLineCoordinateMapper.getCoordinates( line );
		}

		// horizontal mapping
		const highest = Math.max( line.start.y, line.end.y );
		const lowest = Math.min( line.start.y, line.end.y );

		let highestCoordinate = [ line.start, line.end ].find( line => line.y === highest ) || new Coordinate(0,0);
		let lowestCoordinate = [ line.start, line.end ].find( line => line.y === lowest ) || new Coordinate(0,0);

		const coordinates = [];

		const goesLeft = lowestCoordinate.x > highestCoordinate.x;
		const direction = goesLeft ? -1 : 1;

		let x = lowestCoordinate.x;
		for ( let y = lowestCoordinate.y; y <= highest; y++ ) {
			coordinates.push( new Coordinate( x, y ) );
			x = x + direction;
		}

		return coordinates;
	}
}
