import { InputParser } from "./InputParser";

describe( "The inputParser", () => {

	const input = "94,21,58\n" +
		"\n" +
		"49 74 83 34 40\n" +
		"87 16 57 75  3\n" +
		"68 94 77 78 89\n" +
		"56 38 29 26 60\n" +
		"41 42 45 19  1\n" +
		"\n" +
		"42 35 10 20  9\n" +
		"49 39 40 41 73\n" +
		" 3 48 91 81 88\n" +
		"59 55 82 58 71\n" +
		"61 51 17 26 72\n" +
		"\n" +
		"31 49 21 84 83\n" +
		"18 86 53 75 29\n" +
		"85  2 51 76 52\n" +
		"48 28 24 69 12\n" +
		" 5 87 67 95 82\n" +
		"\n" +
		"54 21  0 63 13\n" +
		"84 29 27 12 82\n" +
		"55 86 33 90 95\n" +
		"72 96 24 88 37\n" +
		"38 51 35 46 50";
	let instance: InputParser;
	beforeEach( () => {
		instance = new InputParser();
	} );
	describe( "parse function", () => {
		it( "should consider the first line of numbers to be the list of numbers to mark", () => {
			expect( instance.parse( input ).numbers ).toStrictEqual( [ 94, 21, 58 ] );
		} );

		it( "should construct 2d arrays for every board that is split by a newline ", () => {
			const actualBoards = instance.parse(input).cards;
			expect(actualBoards[0]).toMatchInlineSnapshot(`
Array [
  Array [
    49,
    74,
    83,
    34,
    40,
  ],
  Array [
    87,
    16,
    57,
    75,
    3,
  ],
  Array [
    68,
    94,
    77,
    78,
    89,
  ],
  Array [
    56,
    38,
    29,
    26,
    60,
  ],
  Array [
    41,
    42,
    45,
    19,
    1,
  ],
]
`);
			expect(actualBoards[1]).toMatchInlineSnapshot(`
Array [
  Array [
    42,
    35,
    10,
    20,
    9,
  ],
  Array [
    49,
    39,
    40,
    41,
    73,
  ],
  Array [
    3,
    48,
    91,
    81,
    88,
  ],
  Array [
    59,
    55,
    82,
    58,
    71,
  ],
  Array [
    61,
    51,
    17,
    26,
    72,
  ],
]
`);
			expect(actualBoards[2]).toMatchInlineSnapshot(`
Array [
  Array [
    31,
    49,
    21,
    84,
    83,
  ],
  Array [
    18,
    86,
    53,
    75,
    29,
  ],
  Array [
    85,
    2,
    51,
    76,
    52,
  ],
  Array [
    48,
    28,
    24,
    69,
    12,
  ],
  Array [
    5,
    87,
    67,
    95,
    82,
  ],
]
`);
			expect(actualBoards[4]).toMatchInlineSnapshot(`undefined`);
		} );
	} );
} );
