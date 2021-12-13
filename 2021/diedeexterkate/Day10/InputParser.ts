export class InputParser {
	public parse( input: string ): string[] {
		return input.split( "\n" ).filter( ( x ) => x !== "" );
	}
}
