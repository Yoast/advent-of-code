import { ScanTransformer } from "./ScanTransformer";

export class DepthScanResult {

	constructor( private readonly depths: number[], private readonly scanTransformer: ScanTransformer ) {

	}

	get numberOfIncreases() {
		return this.scanTransformer.transformScans( this.depths )
			.reduce( ( count, currentDepth, currentIndex, depths ) => {
				if ( ! depths.hasOwnProperty( currentIndex - 1 ) ) {
					return count;
				}
				if ( depths[ currentIndex - 1 ] < currentDepth ) {
					count++;
				}
				return count;
			}, 0 );
	}
}
