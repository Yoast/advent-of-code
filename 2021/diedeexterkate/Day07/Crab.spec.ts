import { Crab } from "./Crab";
import { LinearConsumptionSubmarineEngine } from "./LinearConsumptionSubmarineEngine";

describe( "The Crab", () => {
	describe( "getFuelCostIndication function", () => {
		it.each( [
			{ from: 16, to: 2, expectedFuel: 14 },
			{ from: 1, to: 2, expectedFuel: 1 },
			{ from: 2, to: 2, expectedFuel: 0 },
			{ from: 0, to: 2, expectedFuel: 2 },
			{ from: 4, to: 2, expectedFuel: 2 },
			{ from: 2, to: 2, expectedFuel: 0 },
			{ from: 7, to: 2, expectedFuel: 5 },
			{ from: 1, to: 2, expectedFuel: 1 },
			{ from: 2, to: 2, expectedFuel: 0 },
			{ from: 14, to: 2, expectedFuel: 12 },
		] )( "should match the example %s", async ( { from, to, expectedFuel } ) => {
			const crab = new Crab( from, new LinearConsumptionSubmarineEngine() );
			expect( crab.getFuelCostIndication( to ) ).toBe( expectedFuel );
		} );
	} );
} );
