import { DefaultScanTransformer } from "./DefaultScanTransformer";
import { DepthScanResult } from "./DepthScanResult";

describe( "DepthScanResult", () => {
	describe( "numberOfIncreases", () => {
		it( "should count the number of depths that have a higher number than the previous", () => {
			const instance = new DepthScanResult( [ 1, 43, 99, 2, 4 ], new DefaultScanTransformer() );
			expect( instance.numberOfIncreases ).toBe( 3 );
		} );
		it( "should not consider equal values an increase", () => {
			const instance = new DepthScanResult( [ 200, 200, 200 ], new DefaultScanTransformer() );
			expect( instance.numberOfIncreases ).toBe( 0 );
		} );
		it( "should not consider the first value an increase", () => {
			const instance = new DepthScanResult( [ 200 ], new DefaultScanTransformer() );
			expect( instance.numberOfIncreases ).toBe( 0 );
		} );
		it( "should allow empty lists", async () => {
			const instance = new DepthScanResult( [], new DefaultScanTransformer() );
			expect( instance.numberOfIncreases ).toBe( 0 );
		} );
	} );

} );
