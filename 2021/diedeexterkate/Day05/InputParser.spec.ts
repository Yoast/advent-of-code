import { InputParser } from "./InputParser";

describe( "The inputParse", () => {
	describe( "parse function", () => {
		it( "should create lines for valid input", () => {
			const input = "645,570 -> 517,570\n" +
				"100,409 -> 200,409\n" +
				"945,914 -> 98,67";
			const instance = new InputParser();
			expect(instance.parse(input)).toMatchInlineSnapshot(`
Array [
  Line {
    "end": Coordinate {
      "x": 517,
      "y": 570,
    },
    "start": Coordinate {
      "x": 645,
      "y": 570,
    },
  },
  Line {
    "end": Coordinate {
      "x": 200,
      "y": 409,
    },
    "start": Coordinate {
      "x": 100,
      "y": 409,
    },
  },
  Line {
    "end": Coordinate {
      "x": 98,
      "y": 67,
    },
    "start": Coordinate {
      "x": 945,
      "y": 914,
    },
  },
]
`);
		} );
	} );
} );
