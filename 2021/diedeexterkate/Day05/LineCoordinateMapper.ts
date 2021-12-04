import { Coordinate } from "./Coordinate";
import { Line } from "./Line";

export interface LineCoordinateMapper {
	getCoordinates( line: Line ): Coordinate[];
}
