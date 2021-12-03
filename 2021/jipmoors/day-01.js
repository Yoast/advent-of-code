var fs = require( "fs" );

const data = fs.readFileSync( "day-01.txt", "utf8" );
const list = data.split( "\n" ).map( ( item ) => parseInt( item, 10 ) );

let increases = 0;

for( let index in list ) {
    index = parseInt( index, 10 );
    if ( index === list.length -2 ) {
        break;
    }

    increases += ( list[index+1] > list[index] ? 1 : 0 );
}

console.log('Part 1: ', increases);

// Reset.
increases = 0;

for( let index in list ) {
    index = parseInt( index, 10 );
    if ( index === list.length -4 ) {
        break;
    }

    const a = list[index] + list[index+1] + list[index+2];
    const b = list[index+1] + list[index+2] + list[index+3];

    increases += ( b > a ? 1 : 0 );
}

console.log(increases);
