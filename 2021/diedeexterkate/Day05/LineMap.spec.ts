import { Coordinate } from "./Coordinate";
import { DiagonalLineCoordinateMapper } from "./DiagonalLineCoordinateMapper";
import { Line } from "./Line";
import { LineMap } from "./LineMap";
import { StraightLineCoordinateMapper } from "./StraightLineCoordinateMapper";

describe( "The LineMap", () => {
	describe( "addLine function", () => {
		it( "should add a line to the map in the correct position", () => {
			const instance = new LineMap( new StraightLineCoordinateMapper() );
			instance.addLine( new Line( new Coordinate( 1, 6 ), new Coordinate( 1, 9 ) ) );
			expect(instance).toMatchInlineSnapshot(`
LineMap {
  "coordinateMapper": StraightLineCoordinateMapper {},
  "mapMatrix": Array [
    ,
    ,
    ,
    ,
    ,
    ,
    Array [
      ,
      Object {
        "lines": Array [
          Line {
            "end": Coordinate {
              "x": 1,
              "y": 9,
            },
            "start": Coordinate {
              "x": 1,
              "y": 6,
            },
          },
        ],
      },
    ],
    Array [
      ,
      Object {
        "lines": Array [
          Line {
            "end": Coordinate {
              "x": 1,
              "y": 9,
            },
            "start": Coordinate {
              "x": 1,
              "y": 6,
            },
          },
        ],
      },
    ],
    Array [
      ,
      Object {
        "lines": Array [
          Line {
            "end": Coordinate {
              "x": 1,
              "y": 9,
            },
            "start": Coordinate {
              "x": 1,
              "y": 6,
            },
          },
        ],
      },
    ],
    Array [
      ,
      Object {
        "lines": Array [
          Line {
            "end": Coordinate {
              "x": 1,
              "y": 9,
            },
            "start": Coordinate {
              "x": 1,
              "y": 6,
            },
          },
        ],
      },
    ],
  ],
}
`);
		} );
	} );

	describe( "the getNumberOfOverlappingPoints function", () => {
		it( "should match the example for straight lines", () => {

			const instance = new LineMap( new StraightLineCoordinateMapper() );

			const lines = [
				new Line( new Coordinate( 0, 9 ), new Coordinate( 5, 9 ) ),
				new Line( new Coordinate( 8, 0 ), new Coordinate( 0, 8 ) ),
				new Line( new Coordinate( 9, 4 ), new Coordinate( 3, 4 ) ),
				new Line( new Coordinate( 2, 2 ), new Coordinate( 2, 1 ) ),
				new Line( new Coordinate( 7, 0 ), new Coordinate( 7, 4 ) ),
				new Line( new Coordinate( 6, 4 ), new Coordinate( 2, 0 ) ),
				new Line( new Coordinate( 0, 9 ), new Coordinate( 2, 9 ) ),
				new Line( new Coordinate( 3, 4 ), new Coordinate( 1, 4 ) ),
				new Line( new Coordinate( 0, 0 ), new Coordinate( 8, 8 ) ),
				new Line( new Coordinate( 5, 5 ), new Coordinate( 8, 2 ) ),
			];
			for ( const line of lines ) {
				instance.addLine( line );
			}

			expect(instance.getPrintableMap()).toMatchInlineSnapshot(`
".......1..
..1....1..
..1....1..
.......1..
.112111211
..........
..........
..........
..........
222111....
"
`);
			expect( instance.getNumberOfOverlappingPoints() ).toBe( 5 );
		} );

		it( "should match the example for diagonal lines", async () => {
			const instance = new LineMap( new DiagonalLineCoordinateMapper( new StraightLineCoordinateMapper() ) );

			const lines = [
				new Line( new Coordinate( 0, 9 ), new Coordinate( 5, 9 ) ),
				new Line( new Coordinate( 8, 0 ), new Coordinate( 0, 8 ) ),
				new Line( new Coordinate( 9, 4 ), new Coordinate( 3, 4 ) ),
				new Line( new Coordinate( 2, 2 ), new Coordinate( 2, 1 ) ),
				new Line( new Coordinate( 7, 0 ), new Coordinate( 7, 4 ) ),
				new Line( new Coordinate( 6, 4 ), new Coordinate( 2, 0 ) ),
				new Line( new Coordinate( 0, 9 ), new Coordinate( 2, 9 ) ),
				new Line( new Coordinate( 3, 4 ), new Coordinate( 1, 4 ) ),
				new Line( new Coordinate( 0, 0 ), new Coordinate( 8, 8 ) ),
				new Line( new Coordinate( 5, 5 ), new Coordinate( 8, 2 ) ),
			];
			for ( const line of lines ) {
				instance.addLine( line );
			}

			expect(instance.getPrintableMap()).toMatchInlineSnapshot(`
"1.1....11.
.111...2..
..2.1.111.
...1.2.2..
.112313211
...1.2....
..1...1...
.1.....1..
1.......1.
222111....
"
`);
			expect( instance.getNumberOfOverlappingPoints() ).toBe( 12 );
		} );
	} );

} );
