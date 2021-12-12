import { maxBy, minBy } from "lodash";
import { Crab } from "./Crab";

export class CrabSwarm {
	/**
	 * The constructor.
	 */
	public constructor( private readonly crabs: Crab[] ) {
		if ( ! crabs.length ) {
			throw Error( "No crabs in swarm" );
		}
	}

	public getMostEfficientPosition() {
		const max: number = maxBy( this.crabs, "horizontalPosition" )!.horizontalPosition;
		const min: number = minBy( this.crabs, "horizontalPosition" )!.horizontalPosition;
		let lowestFuelCost = 999999999999999;
		let optimalPosition = 0;
		for ( let x = min; x < max; x++ ) {
			const totalFuelCost = this.crabs.reduce( ( totalCost: number, crab: Crab ) => {
				return totalCost + crab.getFuelCostIndication( x );
			}, 0 );
			if ( totalFuelCost < lowestFuelCost ) {
				lowestFuelCost = totalFuelCost;
				optimalPosition = x;
			}
		}
		return { mostEfficientPosition: optimalPosition, fuelCost: lowestFuelCost };
	}
}
