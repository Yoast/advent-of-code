import { IncreasingConsumptionSubmarineEngine } from "./IncreasingConsumptionSubmarineEngine";

describe( "the IncreasingConsumptionSubmarineEngine class", () => {
	describe( "getFuelCostIndication function", () => {
		it( "should consume 1 more with every unit of distance passed", () => {
			const instance = new IncreasingConsumptionSubmarineEngine();
			expect(instance.getFuelCostIndication(1)).toStrictEqual(1);
			expect(instance.getFuelCostIndication(2)).toStrictEqual(3);
			expect(instance.getFuelCostIndication(5)).toStrictEqual(15);
		} );
	} );
} );
