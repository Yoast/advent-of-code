import { Display } from "./Display";

export class InputParser {
	public parse( input: string ): Display[] {
		const lines = input.split( "\n" );
		const result: Display[] = [];
		for ( const line of lines ) {
			if ( line === "" ) {
				continue;
			}
			const [ patternsString, outputValueString ] = line.split( " | " );
			const patterns = patternsString.split( " " );
			const outputValue = outputValueString.split( " " );
			result.push( new Display( patterns, outputValue ) );
		}

		return result;
	}
}
