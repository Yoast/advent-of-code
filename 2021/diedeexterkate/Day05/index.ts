import fs from "fs";
import { DiagonalLineCoordinateMapper } from "./DiagonalLineCoordinateMapper";
import { InputParser } from "./InputParser";
import { LineMap } from "./LineMap";
import { StraightLineCoordinateMapper } from "./StraightLineCoordinateMapper";

const input = fs.readFileSync( __dirname + "/input.txt", "utf8" );
const inputParser = new InputParser();

const parsedInput = inputParser.parse( input );

const map = new LineMap( new StraightLineCoordinateMapper() );
const diagonalMap = new LineMap( new DiagonalLineCoordinateMapper( new StraightLineCoordinateMapper() ) );

for ( const line of parsedInput ) {
	map.addLine( line );
	diagonalMap.addLine( line );
}

console.log( map.getNumberOfOverlappingPoints() );
console.log( diagonalMap.getNumberOfOverlappingPoints() );
