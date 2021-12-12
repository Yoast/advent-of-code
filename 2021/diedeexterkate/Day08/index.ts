import fs from "fs";
import { Display } from "./Display";
import { InputParser } from "./InputParser";

const input = fs.readFileSync( __dirname + "/input.txt", "utf8" );
const inputParser = new InputParser();

const displays: Display[] = inputParser.parse( input );

const day1Count = displays.reduce( ( totalCount, display ) => {
	totalCount += display.countOutputWithLength([ 2, 4, 3, 7]);
	return totalCount;
}, 0 );

console.log(day1Count);

const total = displays.reduce((count, display)=>count+display.getCorrectedOutput(),0);
console.log(total);

