import { Command } from "./Command";

export class Submarine {

	/**
	 * The constructor.
	 */
	public constructor(
		private depth: number = 0,
		private horizontalPosition: number = 0,
		private aim: number = 0,
	) {
	}

	public move( command: Command ): void {
		if ( command.direction === "forward" ) {
			this.horizontalPosition += command.amount;
		}
		if ( command.direction === "down" ) {
			this.depth += command.amount;
		}
		if ( command.direction === "up" ) {
			this.depth -= command.amount;
		}
	}

	public moveRealistically( command: Command ): void {
		if ( command.direction === "forward" ) {
			this.horizontalPosition += command.amount;
			this.depth = this.depth + this.aim * command.amount;
		}
		if ( command.direction === "down" ) {
			this.aim += command.amount;
		}
		if ( command.direction === "up" ) {
			this.aim -= command.amount;
		}
	}

	public getPosition() {
		return { depth: this.depth, horizontalPosition: this.horizontalPosition };
	}
}
