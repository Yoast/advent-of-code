import { SubmarineEngineInterface } from "./SubmarineEngineInterface";

export class LinearConsumptionSubmarineEngine implements SubmarineEngineInterface {
	public getFuelCostIndication(distance: number): number{
		return distance;
	};
}
