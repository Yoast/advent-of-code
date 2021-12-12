export class InputParser {
	public parse( input: string ): number[][] {
		const lines = input.split( "\n" );
		const result: number[][] = [];
		for ( const line of lines ) {
			if ( line === "" ) {
				continue;
			}

			result.push( line.split( "" ).map( this.toNumber ) );
		}

		return result;
	}

	private toNumber( number: string ) {
		return parseInt( number, 10 );
	}
}
