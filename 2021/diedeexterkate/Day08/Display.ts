import { difference, isEqual } from "lodash";

export class Display {
	/**
	 * The constructor.
	 */
	public constructor( private readonly pattern: string[], private readonly output: string[] ) {
	}

	public countOutputWithLength( lengths: number[] ): number {
		return this.output.reduce( ( count, number ) => {
			if ( lengths.includes( number.length ) ) {
				count++;
			}
			return count;
		}, 0 );
	}

	public getCorrectedOutput(): number {
		const code = this.crackCode();
		const string = this.output.reduce( ( acc, input ) => {
			return acc + this.getNumber( input, code );
		}, "" );
		return parseInt( string, 10 );
	}

	getNumber( input: string, code: Record<number, string[]> ): string {
		const entries = Object.entries( code );
		for ( const entry of entries ) {
			if ( isEqual( entry[ 1 ].sort(), input.split( "" ).sort() ) ) {
				return entry[ 0 ];
			}
		}
		throw new Error( "failed getting number " + input + " with code " + JSON.stringify( code ) );
	}

	public crackCode() {
		let code: Record<number, string[]> = {};

		code[ 1 ] = this.pattern.find( string => string.length === 2 )!.split( "" );
		code[ 7 ] = this.pattern.find( string => string.length === 3 )!.split( "" );
		code[ 4 ] = this.pattern.find( string => string.length === 4 )!.split( "" );
		code[ 8 ] = this.pattern.find( string => string.length === 7 )!.split( "" );

		const aaaa = code[ 7 ].filter( ( character ) => ! code[ 1 ].includes( character ) )[ 0 ];
		code[ 3 ] = this.pattern.find( string => {
			if ( string.length !== 5 ) {
				return false;
			}
			// Has cc and ff.
			return string.includes( code[ 1 ][ 0 ] ) && string.includes( code[ 1 ][ 1 ] );
		} )!.split( "" );

		const eeee = code[ 8 ]
			.filter( ( character ) => ! code[ 4 ].includes( character ) )
			.filter( ( character ) => ! code[ 3 ].includes( character ) )
			[ 0 ];

		code[ 2 ] = this.pattern.find( string => {
			if ( string.length !== 5 ) {
				return false;
			}
			return string.includes( eeee );
		} )!.split( "" );

		code[ 5 ] = this.pattern.find( string => {
			if ( string.length !== 5 ) {
				return false;
			}
			const characters = string.split( "" );
			// not 2 and not 3.
			return characters.some( ( character ) => ! [ ...code[ 2 ], ...code[ 3 ] ].includes( character ) );
		} )!.split( "" );

		const cccc = code[ 1 ].filter( ( character ) => ! code[ 5 ].includes( character ) )[ 0 ];

		code[ 6 ] = this.pattern.find( string => {
			if ( string.length !== 6 ) {
				return false;
			}
			return ! string.includes( cccc );
		} )!.split( "" );

		code[ 9 ] = this.pattern.find( string => {
			if ( string.length !== 6 ) {
				return false;
			}
			return ! string.includes( eeee );
		} )!.split( "" );

		code[ 0 ] = this.pattern.find( string => {
			if ( string.length !== 6 ) {
				return false;
			}
			return string.includes( cccc ) && string.includes( eeee );
		} )!.split( "" );

		return code;
	}
}
