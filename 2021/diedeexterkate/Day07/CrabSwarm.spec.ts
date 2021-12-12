import { Crab } from "./Crab";
import { CrabSwarm } from "./CrabSwarm";
import { LinearConsumptionSubmarineEngine } from "./LinearConsumptionSubmarineEngine";

describe( "The CrabSwarm", () => {
	describe( "getMostEfficientPosition function", () => {
		it( "should return the most optimal position to move to", () => {
			const crabSwarm = new CrabSwarm( [
				new Crab( 16, new LinearConsumptionSubmarineEngine() ),
				new Crab( 1, new LinearConsumptionSubmarineEngine() ),
				new Crab( 2, new LinearConsumptionSubmarineEngine() ),
				new Crab( 0, new LinearConsumptionSubmarineEngine() ),
				new Crab( 4, new LinearConsumptionSubmarineEngine() ),
				new Crab( 2, new LinearConsumptionSubmarineEngine() ),
				new Crab( 7, new LinearConsumptionSubmarineEngine() ),
				new Crab( 1, new LinearConsumptionSubmarineEngine() ),
				new Crab( 2, new LinearConsumptionSubmarineEngine() ),
				new Crab( 14, new LinearConsumptionSubmarineEngine() ),
			] );
			expect( crabSwarm.getMostEfficientPosition() ).toStrictEqual( {
				mostEfficientPosition: 2,
				fuelCost: 37,
			} );
		} );
	} );
} );
