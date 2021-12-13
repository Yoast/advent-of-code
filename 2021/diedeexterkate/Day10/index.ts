import fs from "fs";
import { InputParser } from "./InputParser";

const inputParser = new InputParser();
const input = fs.readFileSync( __dirname + "/input.txt", "utf8" );

const lines = inputParser.parse( input );

type Syntax = {
	open: string;
	close: string;
	errorScore: number;
	autocompleteScore: number;
}
const matchingTokens: Syntax[] = [
	{ open: "(", close: ")", errorScore: 3, autocompleteScore: 1 },
	{ open: "[", close: "]", errorScore: 57, autocompleteScore: 2 },
	{ open: "{", close: "}", errorScore: 1197, autocompleteScore: 3 },
	{ open: "<", close: ">", errorScore: 25137, autocompleteScore: 4 },
];

function getCorruptedLineErrorScore( line: string ) {
	const stack: Syntax[] = [];
	for ( const token of line.split( "" ) ) {
		const openingTag = matchingTokens.find( syntax => token === syntax.open );
		if ( openingTag ) {
			stack.push( openingTag );
			continue;
		}
		const closingTag = matchingTokens.find( syntax => token === syntax.close );
		if ( closingTag ) {
			// Matches expected closing tag?
			if ( ! stack.length || stack[ stack.length - 1 ].close === token ) {
				stack.pop();
			} else {
				return closingTag.errorScore;
			}
		}
	}
	return 0;
}

const score = lines.reduce( ( score, line ) => score + getCorruptedLineErrorScore( line ), 0 );

console.log( score );

function getMissingCharacters( line: string ): Syntax[] {
	const stack: Syntax[] = [];
	for ( const token of line.split( "" ) ) {
		const openingTag = matchingTokens.find( syntax => token === syntax.open );
		if ( openingTag ) {
			stack.push( openingTag );
			continue;
		}
		const closingTag = matchingTokens.find( syntax => token === syntax.close );
		if ( closingTag ) {
			stack.pop();
		}
	}
	return stack;
}

const scores = lines.map( ( line ) => {
	if ( getCorruptedLineErrorScore( line ) ) {
		//ignore
		return 0;
	}

	const autoCompleteCharacters: Syntax[] = getMissingCharacters( line );

	return autoCompleteCharacters.reverse().reduce( ( score, character ) => score * 5 + character.autocompleteScore, 0 );
}, 0 );

const sortedScores = scores
	.filter(x=>x)
	.sort( ( n1, n2 ) => n1 - n2 );
const score2 = sortedScores[ Math.floor( sortedScores.length / 2 ) ];
console.log(sortedScores);
console.log( score2 );
