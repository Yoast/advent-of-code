import fs from "fs";
import { BingoCard } from "./BingoCard";
import { InputParser } from "./InputParser";

const input = fs.readFileSync( __dirname + "/input.txt", "utf8" );
const inputParser = new InputParser();

const parsedInput = inputParser.parse( input );

const cards = parsedInput.cards.map( ( numberMap: number[][] ) => new BingoCard( numberMap ) );

function getFirstBingoBoard( cards: BingoCard[] ) {

	for ( const number of parsedInput.numbers ) {
		for ( const card of cards ) {
			card.markNumber( number );
			if ( card.hasBingo() ) {
				return card;
			}
		}
	}
	return cards[ 0 ];
}

function getLastBingoBoard( cards: BingoCard[] ) {
	let last = cards[ 0 ];
	for ( const number of parsedInput.numbers ) {
		for ( const card of cards ) {
			if ( ! card.hasBingo() ) {
				card.markNumber( number );
				if ( card.hasBingo() ) {
					last = card;
				}
			}
		}
	}
	return last;
}

const firstBingoBoard = getFirstBingoBoard( cards );
const lastBingoBoard = getLastBingoBoard( cards );

console.log( firstBingoBoard.getScore() );
console.log( lastBingoBoard.getScore() );

