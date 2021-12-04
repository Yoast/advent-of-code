import { Coordinate } from "./Coordinate";
import { Line } from "./Line";
import { LineCoordinateMapper } from "./LineCoordinateMapper";

type MapSegment = {
	lines: Line[],
}

export class LineMap {

	private readonly mapMatrix: MapSegment[][] = [];

	/**
	 * The constructor.
	 */
	public constructor( private readonly coordinateMapper: LineCoordinateMapper ) {
	}

	public addLine( line: Line ): void {
		const coordinates = this.coordinateMapper.getCoordinates( line );
		for ( const coordinate of coordinates ) {
			this.markCoordinate( coordinate, line );
		}
	}

	private markCoordinate( coordinate: Coordinate, line: Line ) {
		this.mapMatrix[ coordinate.y ] = this.mapMatrix[ coordinate.y ] || [];
		this.mapMatrix[ coordinate.y ][ coordinate.x ] = this.mapMatrix[ coordinate.y ][ coordinate.x ] || { lines: [] };
		this.mapMatrix[ coordinate.y ][ coordinate.x ].lines.push( line );
	}

	public getNumberOfOverlappingPoints() {
		let numberOfPoints = 0;
		for ( const mapRow of this.mapMatrix ) {
			if ( ! mapRow ) {
				continue;
			}
			for ( const mapSegment of mapRow ) {
				if ( ! mapSegment ) {
					continue;
				}
				if ( mapSegment.lines.length > 1 ) {
					numberOfPoints++;
				}
			}
		}
		return numberOfPoints;
	}

	public getPrintableMap() {
		let result = "";
		const maxX = this.getMaxX();
		for ( let y = 0; y < this.mapMatrix.length; y++ ) {
			for ( let x = 0; x < maxX; x++ ) {
				if ( this.mapMatrix[ y ] && this.mapMatrix[ y ][ x ] ) {
					result += this.mapMatrix[ y ][ x ].lines.length;
				} else {
					result += ".";
				}
			}
			result += "\n";
		}
		return result;
	}

	private getMaxX() {
		let maxX = 0;
		for ( const mapRow of this.mapMatrix ) {
			if ( ! mapRow ) {
				continue;
			}
			maxX = Math.max( maxX, mapRow.length );
		}
		return maxX;
	}
}
