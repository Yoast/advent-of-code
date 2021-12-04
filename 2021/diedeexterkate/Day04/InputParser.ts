export class InputParser {
	public parse( input: string ): { cards: number[][][], numbers: number[] } {
		const lines = input.split( "\n" );
		const firstLine = lines.shift();
		if ( ! firstLine ) {
			throw new Error( "no first line" );
		}

		const cards: number[][][] = [];
		let cardIndex = -1;
		for ( const line of lines ) {
			if ( line === "" ) {
				cardIndex++;
				cards[ cardIndex ] = [];
				continue;
			}
			cards[ cardIndex ].push( line.split( " " ).filter( x => x ).map( this.toNumber ) );
		}

		return {
			// filter empty values
			cards: cards.filter( x => x.length ),
			numbers: firstLine.split( "," ).map( this.toNumber ),
		};
	}

	private toNumber( number: string ) {
		return parseInt( number, 10 );
	}
}
