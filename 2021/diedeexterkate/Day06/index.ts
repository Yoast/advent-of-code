import { Lanternfish } from "./Lanternfish";

const input: number[] = [ 1, 3, 3, 4, 5, 1, 1, 1, 1, 1, 1, 2, 1, 4, 1, 1, 1, 5, 2, 2, 4, 3, 1, 1, 2, 5, 4, 2, 2, 3, 1, 2, 3, 2, 1, 1, 4, 4, 2, 4, 4, 1, 2, 4, 3, 3, 3, 1, 1, 3, 4, 5, 2, 5, 1, 2, 5, 1, 1, 1, 3, 2, 3, 3, 1, 4, 1, 1, 4, 1, 4, 1, 1, 1, 1, 5, 4, 2, 1, 2, 2, 5, 5, 1, 1, 1, 1, 2, 1, 1, 1, 1, 3, 2, 3, 1, 4, 3, 1, 1, 3, 1, 1, 1, 1, 3, 3, 4, 5, 1, 1, 5, 4, 4, 4, 4, 2, 5, 1, 1, 2, 5, 1, 3, 4, 4, 1, 4, 1, 5, 5, 2, 4, 5, 1, 1, 3, 1, 3, 1, 4, 1, 3, 1, 2, 2, 1, 5, 1, 5, 1, 3, 1, 3, 1, 4, 1, 4, 5, 1, 4, 5, 1, 1, 5, 2, 2, 4, 5, 1, 3, 2, 4, 2, 1, 1, 1, 2, 1, 2, 1, 3, 4, 4, 2, 2, 4, 2, 1, 4, 1, 3, 1, 3, 5, 3, 1, 1, 2, 2, 1, 5, 2, 1, 1, 1, 1, 1, 5, 4, 3, 5, 3, 3, 1, 5, 5, 4, 4, 2, 1, 1, 1, 2, 5, 3, 3, 2, 1, 1, 1, 5, 5, 3, 1, 4, 4, 2, 4, 2, 1, 1, 1, 5, 1, 2, 4, 1, 3, 4, 4, 2, 1, 4, 2, 1, 3, 4, 3, 3, 2, 3, 1, 5, 3, 1, 1, 5, 1, 2, 2, 4, 4, 1, 2, 3, 1, 2, 1, 1, 2, 1, 1, 1, 2, 3, 5, 5, 1, 2, 3, 1, 3, 5, 4, 2, 1, 3, 3, 4 ];

let fishes = input.map( ( number ) => new Lanternfish( 7, 2, number ) );

const numberOfDays = 80;
for ( let day = 1; day <= numberOfDays; day++ ) {
	fishes = fishes.reduce( ( allFishes, fish ) => {
		const newborn = fish.age();
		if ( newborn ) {
			allFishes.push( newborn );
		}
		return allFishes;
	}, fishes );
}

console.log( fishes.length );

// part 2

type Fishes = {
	[ days: number ]: number;
}
let numberOfFish = input.reduce( ( acc: Fishes, nr: number ): Fishes => {
	acc[ nr ] = acc[ nr ] || 0;
	acc[ nr ]++;
	return acc;
}, {} );

const numberOfDays2 = 256;
for ( let day = 1; day <= numberOfDays2; day++ ) {
	let numberOfNewborns = 0;
	for ( const numberOfFishKey in numberOfFish ) {
		numberOfFish[ parseInt( numberOfFishKey, 10 ) - 1 ] = numberOfFish[ numberOfFishKey ];
		numberOfFish[ numberOfFishKey ] = 0;
	}
	if ( numberOfFish[ -1 ] ) {
		numberOfNewborns = numberOfFish[ -1 ];
		numberOfFish[ 6 ] = ( numberOfFish[ 6 ] || 0 ) + numberOfFish[ -1 ];
	}
	delete numberOfFish[ "-1" ];
	numberOfFish[ 8 ] = numberOfNewborns || 0;
	console.log( "day " + day, numberOfFish );
}

let count = 0;
for ( const numberOfFishKey in numberOfFish ) {
	count += numberOfFish[ numberOfFishKey ];
}
console.log( count );

