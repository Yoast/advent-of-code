import { SlidingAverageScanTransformer } from "./SlidingAverageScanTransformer";

describe( "The SlidingAverageScanTransformer", () => {
	describe( "transformScans function", () => {
		it( "sums up 3 consecutive scans", async () => {
			const instance = new SlidingAverageScanTransformer( 3 );
			expect( instance.transformScans( [ 1, 10, 100 ] ) ).toStrictEqual( [ 111 ] );
			expect( instance.transformScans( [ 1, 10, 100, 1000 ] ) ).toStrictEqual( [ 111, 1110 ] );
			expect( instance.transformScans( [ 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 ] ) ).toStrictEqual( [ 607, 618, 618, 617, 647, 716, 769, 792 ] );
		} );
		it( "ignores scans that can't form a full group", () => {
			const instance = new SlidingAverageScanTransformer( 3 );
			expect( instance.transformScans( [ 1, 10 ] ) ).toStrictEqual( [] );
		} );
	} );
} );
