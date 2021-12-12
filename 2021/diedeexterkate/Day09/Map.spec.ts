import { SmokeMap } from "./Map";

describe( "SmokeMap", () => {
	describe( "getBasisn", () => {
		it( "should match the example", () => {
			const input = [
				[ 2, 1, 9, 9, 9, 4, 3, 2, 1, 0 ],
				[ 3, 9, 8, 7, 8, 9, 4, 9, 2, 1 ],
				[ 9, 8, 5, 6, 7, 8, 9, 8, 9, 2 ],
				[ 8, 7, 6, 7, 8, 9, 6, 7, 8, 9 ],
				[ 9, 8, 9, 9, 9, 6, 5, 6, 7, 8 ],
			];
			const map = new SmokeMap( input );
			const basins = map.getBasins();
			const top4 = basins.sort((n1,n2) => n1 - n2).reverse().slice( 0, 4 );

			expect( top4 ).toStrictEqual( [ 14, 9, 9, 3 ] );
		} );
	} );
} );
