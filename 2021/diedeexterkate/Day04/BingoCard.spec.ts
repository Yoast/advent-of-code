import { BingoCard } from "./BingoCard";

describe( "The BingoCard", () => {
	describe( "constructor", () => {
		it( "should create simple objects for all fields", () => {
			const input = [
				[ 42, 35, 10, 20, 9 ],
				[ 49, 39, 40, 41, 73 ],
				[ 3, 48, 91, 81, 88 ],
				[ 59, 55, 82, 58, 71 ],
				[ 61, 51, 17, 26, 72 ],
			];

			expect( new BingoCard( input ) ).toMatchInlineSnapshot( `
BingoCard {
  "numberMap": Array [
    Array [
      Object {
        "marked": false,
        "value": 42,
      },
      Object {
        "marked": false,
        "value": 35,
      },
      Object {
        "marked": false,
        "value": 10,
      },
      Object {
        "marked": false,
        "value": 20,
      },
      Object {
        "marked": false,
        "value": 9,
      },
    ],
    Array [
      Object {
        "marked": false,
        "value": 49,
      },
      Object {
        "marked": false,
        "value": 39,
      },
      Object {
        "marked": false,
        "value": 40,
      },
      Object {
        "marked": false,
        "value": 41,
      },
      Object {
        "marked": false,
        "value": 73,
      },
    ],
    Array [
      Object {
        "marked": false,
        "value": 3,
      },
      Object {
        "marked": false,
        "value": 48,
      },
      Object {
        "marked": false,
        "value": 91,
      },
      Object {
        "marked": false,
        "value": 81,
      },
      Object {
        "marked": false,
        "value": 88,
      },
    ],
    Array [
      Object {
        "marked": false,
        "value": 59,
      },
      Object {
        "marked": false,
        "value": 55,
      },
      Object {
        "marked": false,
        "value": 82,
      },
      Object {
        "marked": false,
        "value": 58,
      },
      Object {
        "marked": false,
        "value": 71,
      },
    ],
    Array [
      Object {
        "marked": false,
        "value": 61,
      },
      Object {
        "marked": false,
        "value": 51,
      },
      Object {
        "marked": false,
        "value": 17,
      },
      Object {
        "marked": false,
        "value": 26,
      },
      Object {
        "marked": false,
        "value": 72,
      },
    ],
  ],
}
` );
		} );

		describe( "hasBingo", () => {
			it( "should check if a row has bingo", () => {
				const input = [
					[ 42, 35, 10, 20, 9 ],
					[ 49, 39, 40, 41, 73 ],
					[ 3, 48, 91, 81, 88 ],
					[ 59, 55, 82, 58, 71 ],
					[ 61, 51, 17, 26, 72 ],
				];

				const card = new BingoCard(input);
				card.markNumber(41);
				card.markNumber(3);
				card.markNumber(48);
				card.markNumber(81);
				card.markNumber(91);
				card.markNumber(88);
				expect(card.hasBingo()).toBeTruthy();
			} );

			it( "should check if a column has bingo", () => {
				const input = [
					[ 42, 35, 10, 20, 9 ],
					[ 49, 39, 40, 41, 73 ],
					[ 3, 48, 91, 81, 88 ],
					[ 59, 55, 82, 58, 71 ],
					[ 61, 51, 17, 26, 72 ],
				];

				const card = new BingoCard( input );
				card.markNumber( 41 );
				card.markNumber( 20 );
				card.markNumber( 81 );
				card.markNumber( 58 );
				card.markNumber( 26 );
				expect( card.hasBingo() ).toBeTruthy();
			} );
			it( "should return false for no bingo", () => {
				const input = [
					[ 42, 35, 10, 20, 9 ],
					[ 49, 39, 40, 41, 73 ],
					[ 3, 48, 91, 81, 88 ],
					[ 59, 55, 82, 58, 71 ],
					[ 61, 51, 17, 26, 72 ],
				];

				const card = new BingoCard(input);
				card.markNumber(42);
				card.markNumber(2);
				card.markNumber(48);
				card.markNumber(81);
				card.markNumber(91);
				card.markNumber(88);
				expect(card.hasBingo()).toBeFalsy();
			} );
		} );
	} );
} );
