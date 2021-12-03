import * as fs from "fs";

const input = fs.readFileSync( __dirname + "/input.txt", "utf8" );
const diagnosticsReport = input.split( "\n" ).filter( ( x ) => x !== "" );

type CharacterCount = { [ character: string ]: number };
type CharacterCountPerIndex = { [ indexIndex: number ]: CharacterCount };

function getCharacterCountPerIndex( strings: string[] ) {
	const result: CharacterCountPerIndex = {};
	// Assuming that all binary values have the same length.
	const stringLength = strings[ 0 ].length;
	for ( let characterIndex = 0; characterIndex < stringLength; characterIndex++ ) {
		result[ characterIndex ] = strings.reduce( ( acc: CharacterCount, binaryNumber ) => {
			acc[ binaryNumber.charAt( characterIndex ) ] = ( acc[ binaryNumber.charAt( characterIndex ) ] || 0 ) + 1;
			return acc;
		}, {} );
	}
	return result;
}

const occurrencesPerIndex = getCharacterCountPerIndex( diagnosticsReport );
const gammaBinary = Object.entries( occurrencesPerIndex ).reduce( ( result: string, entry: [ string, CharacterCount ] ) => {
	const count: CharacterCount = entry[ 1 ];
	let highestValue = { character: "", count: 0 };
	for ( const number of Object.entries( count ) ) {
		if ( number[ 1 ] > highestValue.count ) {
			highestValue.character = number[ 0 ];
			highestValue.count = number[ 1 ];
		}
	}
	result += highestValue.character;
	return result;
}, "" );

const epsilonBinary = Object.entries( occurrencesPerIndex ).reduce( ( result: string, entry: [ string, CharacterCount ] ) => {
	const count: CharacterCount = entry[ 1 ];
	let lowestValue = { character: "", count: 9999999 };
	for ( const number of Object.entries( count ) ) {
		if ( number[ 1 ] <= lowestValue.count ) {
			lowestValue.character = number[ 0 ];
			lowestValue.count = number[ 1 ];
		}
	}
	result += lowestValue.character;
	return result;
}, "" );

const gamma = parseInt( gammaBinary, 2 );
const epsilon = parseInt( epsilonBinary, 2 );
const powerConsumption = gamma * epsilon;
console.log( "power consumption: " + powerConsumption );

// part 2

function getOxygenGeneratorRating( remainingReport: string[], characterIndex = 0 ): string {
	const occurrencesPerIndex: CharacterCountPerIndex = getCharacterCountPerIndex( remainingReport );
	console.log(occurrencesPerIndex);
	const countsForLine = occurrencesPerIndex[ characterIndex ];
	let highestValue = { character: "", count: 0 };
	for ( const value of Object.entries( countsForLine ) ) {
		const preference = "1";
		if ( value[ 1 ] > highestValue.count || ( value[ 1 ] >= highestValue.count && value[ 0 ] === preference ) ) {
			highestValue.character = value[ 0 ];
			highestValue.count = value[ 1 ];
		}
	}
	console.log(highestValue);
	const newRemainingReport = remainingReport.filter( ( binary: string ) => binary.charAt( characterIndex ) === highestValue.character );
	if ( newRemainingReport.length === 1 ) {
		return newRemainingReport[ 0 ];
	}
	return getOxygenGeneratorRating( newRemainingReport, characterIndex + 1 );
}

function getCO2Rating( remainingReport: string[], characterIndex = 0 ): string {
	const occurrencesPerIndex: CharacterCountPerIndex = getCharacterCountPerIndex( remainingReport );
	const countsForLine = occurrencesPerIndex[ characterIndex ];
	let lowestValue = { character: "", count: 9999999 };
	for ( const value of Object.entries( countsForLine ) ) {
		const preference = "0";
		if ( value[ 1 ] < lowestValue.count || ( value[ 1 ] <= lowestValue.count && value[ 0 ] === preference ) ) {
			lowestValue.character = value[ 0 ];
			lowestValue.count = value[ 1 ];
		}
	}
	const newRemainingReport = remainingReport.filter( ( binary: string ) => binary.charAt( characterIndex ) === lowestValue.character );
	if ( newRemainingReport.length === 1 ) {
		return newRemainingReport[ 0 ];
	}
	return getCO2Rating( newRemainingReport, characterIndex + 1 );
}

const oxygenGeneratorRatingBinary = getOxygenGeneratorRating( diagnosticsReport );
const co2RatingBinary = getCO2Rating( diagnosticsReport );

const oxygenGeneratorRating = parseInt( oxygenGeneratorRatingBinary, 2 );
const co2Rating = parseInt( co2RatingBinary, 2 );
const lifeSupportRating = oxygenGeneratorRating * co2Rating;

console.log( "life support rating: " + lifeSupportRating );
