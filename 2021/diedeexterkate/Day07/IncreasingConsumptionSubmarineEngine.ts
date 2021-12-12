import { SubmarineEngineInterface } from "./SubmarineEngineInterface";

export class IncreasingConsumptionSubmarineEngine implements SubmarineEngineInterface {
	public getFuelCostIndication( distance: number ): number {
		return ( distance * ( distance + 1 ) ) / 2;
	};
}
