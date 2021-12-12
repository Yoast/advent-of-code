import { Display } from "./Display";

describe( "The Display class", () => {
	describe( "crackCode function", () => {
		it( "should yield the input for a non-scrambled set", () => {

			const map = {
				0: "abcefg",
				1: "cf",
				2: "acdeg",
				3: "acdfg",
				4: "bcdf",
				5: "abdfg",
				6: "abdefg",
				7: "acf",
				8: "abcdefg",
				9: "abcdfg",
			};
			const pattern = Object.values( map );
			const instance = new Display( pattern, [] );

			const expected = {
				0: map[ 0 ].split( "" ),
				1: map[ 1 ].split( "" ),
				2: map[ 2 ].split( "" ),
				3: map[ 3 ].split( "" ),
				4: map[ 4 ].split( "" ),
				5: map[ 5 ].split( "" ),
				6: map[ 6 ].split( "" ),
				7: map[ 7 ].split( "" ),
				8: map[ 8 ].split( "" ),
				9: map[ 9 ].split( "" ),
			};
			expect( instance.crackCode() ).toStrictEqual( expected );
		} );

		it( "should match the example", () => {
			const pattern = [ "acedgfb", "cdfbe", "gcdfa", "fbcad", "dab", "cefabd", "cdfgeb", "eafb", "cagedb", "ab" ];
			const instance = new Display( pattern, [] );
			const expected = {
				 8: "acedgfb".split(""),
				 5: "cdfbe".split(""),
				 2: "gcdfa".split(""),
				 3: "fbcad".split(""),
				 7: "dab".split(""),
				 9: "cefabd".split(""),
				 6: "cdfgeb".split(""),
				 4: "eafb".split(""),
				 0: "cagedb".split(""),
				 1: "ab".split(""),
			};
			expect(instance.crackCode()).toStrictEqual(expected);
		} );
	} );
} );
