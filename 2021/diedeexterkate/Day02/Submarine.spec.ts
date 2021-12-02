import { Command } from "./Command";
import { Submarine } from "./Submarine";

describe( "The submarine", () => {
	describe( "moveRealistically", () => {
		it( "should match the example", async () => {
			const commands = [
				new Command( "forward", 5 ),
				new Command( "down", 5 ),
				new Command( "forward", 8 ),
				new Command( "up", 3 ),
				new Command( "down", 8 ),
				new Command( "forward", 2 ),
			];

			const instance = new Submarine();
			for ( const command of commands ) {
				instance.moveRealistically( command );
			}
			expect( instance.getPosition() ).toStrictEqual( { depth: 60, horizontalPosition: 15 } );
		} );
	} );
} );
