import { SubmarineEngineInterface } from "./SubmarineEngineInterface";

export class Crab {
	/**
	 * The constructor.
	 */
	public constructor( public horizontalPosition: number, private readonly engine: SubmarineEngineInterface ) {
	}

	public getFuelCostIndication( targetHorizontalPosition: number ) {
		const distance = Math.abs( this.horizontalPosition - targetHorizontalPosition );
		return this.engine.getFuelCostIndication( distance );
	}
}
