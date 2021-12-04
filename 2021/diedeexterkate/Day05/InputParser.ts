import { Coordinate } from "./Coordinate";
import { Line } from "./Line";

export class InputParser {
	public parse( input: string ): Line[] {
		const lines = input.split( "\n" );
		const result: Line[] = [];
		for ( const line of lines ) {
			if ( line === "" ) {
				continue;
			}
			const [ start, end ] = line.split( " -> " );
			const [ startX, startY ] = start.split( "," ).map( this.toNumber );
			const [ endX, endY ] = end.split( "," ).map( this.toNumber );
			const startCoordinate = new Coordinate( startX, startY );
			const endCoordinate = new Coordinate( endX, endY );
			result.push( new Line( startCoordinate, endCoordinate ) );
		}

		return result;
	}

	private toNumber( number: string ) {
		return parseInt( number, 10 );
	}
}
