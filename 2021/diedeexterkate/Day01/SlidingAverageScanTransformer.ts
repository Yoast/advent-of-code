import { ScanTransformer } from "./ScanTransformer";

export class SlidingAverageScanTransformer implements ScanTransformer {
	/**
	 * The constructor.
	 */
	public constructor( private readonly groupSize: number ) {
	}

	public transformScans( scans: number[] ): number[] {
		return scans.reduce( ( transformedScans: number[], scan, currentIndex, scans ) => {
			if ( scans.hasOwnProperty( currentIndex + 1 ) && scans.hasOwnProperty( currentIndex + 2 ) ) {
				transformedScans.push( scan + scans[ currentIndex + 1 ] + scans[ currentIndex + 2 ] );
			}
			return transformedScans;
		}, [] );
	}
}
