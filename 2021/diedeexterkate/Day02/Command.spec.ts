import { Command } from "./Command";

describe( "The Command class", () => {
	describe( "fromString function", () => {
		it( "should create a command object for a string", () => {
			const expected = new Command( "back", 5 );
			expect( Command.fromString( "back 5" ) ).toStrictEqual( expected );
		} );
	} );
} );
