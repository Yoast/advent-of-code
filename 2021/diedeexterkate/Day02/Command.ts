export class Command {
	/**
	 * The constructor.
	 */
	public constructor( public readonly direction: string, public readonly amount: number ) {
	}

	public static fromString( commandString: string ): Command {
		const [ direction, amount ] = commandString.split( " " );
		return new Command( direction, parseInt( amount, 10 ) );
	}
}
