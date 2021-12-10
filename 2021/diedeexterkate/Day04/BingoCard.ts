type Number = { value: number, marked: boolean }

export class BingoCard {

	public readonly numberMap: Number[][];
	public lastNumber: number = 0;

	public constructor( numbersMap: number[][] ) {
		this.numberMap = numbersMap.map( ( numberRow: number[] ): Number[] => numberRow.map( ( number ): Number => ( {
			value: number,
			marked: false,
		} ) ) );
	}

	public markNumber( numberToMark: number ): void {
		for ( const numberRow of this.numberMap ) {
			for ( const number of numberRow ) {
				if ( number.value === numberToMark ) {
					number.marked = true;
					this.lastNumber = numberToMark;
				}
			}
		}
	}

	public hasBingo() {
		return this.columnHasBingo( this.numberMap ) || this.rowHasBingo( this.numberMap );
	}

	public getScore() {
		let unmarkedSum = 0;
		for ( const numberRow of this.numberMap ) {
			for ( const number of numberRow ) {
				if ( ! number.marked ) {
					unmarkedSum += number.value;
				}
			}
		}
		return unmarkedSum * this.lastNumber;
	}

	private columnHasBingo( numberMap: Number[][] ) {
		const transposed = numberMap[ 0 ].map( ( _, index ) => numberMap.map( row => row[ index ] ) );
		return this.rowHasBingo( transposed );
	}

	private rowHasBingo( numberMap: Number[][] ) {
		for ( const numberRow of numberMap ) {
			let rowHasUnmarked = false;
			for ( const number of numberRow ) {
				if ( ! number.marked ) {
					rowHasUnmarked = true;
					break;
				}
			}
			if ( ! rowHasUnmarked ) {
				return true;
			}
		}
		return false;
	}

}
