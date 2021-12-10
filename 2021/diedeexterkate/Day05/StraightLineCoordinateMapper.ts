import { Coordinate } from "./Coordinate";
import { Line } from "./Line";
import { LineCoordinateMapper } from "./LineCoordinateMapper";

export class StraightLineCoordinateMapper implements LineCoordinateMapper {
	public getCoordinates( line: Line ): Coordinate[] {
		const coordinates = [];
		if ( line.start.x === line.end.x ) {
			// line goes down.
			const highest = Math.max( line.start.y, line.end.y );
			const lowest = Math.min( line.start.y, line.end.y );
			for ( let y = lowest; y <= highest; y++ ) {
				coordinates.push( new Coordinate( line.start.x, y ) );
			}
		} else if ( line.start.y === line.end.y ) {
			// line goes sideways.
			const highest = Math.max( line.start.x, line.end.x );
			const lowest = Math.min( line.start.x, line.end.x );
			for ( let x = lowest; x <= highest; x++ ) {
				coordinates.push( new Coordinate( x, line.start.y ) );
			}
		}
		return coordinates;
	}
}
