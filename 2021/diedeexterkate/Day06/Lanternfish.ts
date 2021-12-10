export class Lanternfish {

	public reproducesInDays: number = 7;

	constructor( private readonly daysToReproduce: number = 7, private readonly newbornOffset: number = 2, reproducesInDays: number | null = null ) {
		if ( reproducesInDays !== null ) {
			this.reproducesInDays = reproducesInDays;
		} else {
			this.reproducesInDays = ( this.daysToReproduce - 1 ) + this.newbornOffset;
		}
	}

	public age(): Lanternfish | null {
		this.reproducesInDays--;
		if ( this.reproducesInDays < 0 ) {
			this.reproducesInDays = this.daysToReproduce -1;
			return new Lanternfish( this.daysToReproduce, this.newbornOffset );
		}
		return null;
	}
}
