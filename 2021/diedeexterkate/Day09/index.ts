import fs from "fs";
import { sum } from "lodash";
import { InputParser } from "./InputParser";
import { SmokeMap } from "./Map";

const input = fs.readFileSync( __dirname + "/input.txt", "utf8" );
const inputParser = new InputParser();

const mapData = inputParser.parse( input );

const map = new SmokeMap( mapData );

const depths = map.getLowPoints().map( lowPoint => map.getDepth( lowPoint ) );
const riskSum = sum( depths ) + depths.length;

console.log( "Risk " + riskSum );


const basinSizes = map.getBasins()
	.sort( ( n1, n2 ) => n1 - n2 )
	.reverse()
	.slice( 0, 3 )
	.reduce( ( product, basinsize ) => basinsize * product, 1 );

console.log( "basin sizes " + basinSizes );
//1280496
