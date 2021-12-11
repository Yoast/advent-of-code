<?php

$input = require './input.php';

$finput = <<<'INPUT'
[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]
INPUT;

/** @var Line[] $lines */
$lines = array_map( [ Line::class, 'fromInput' ], explode( PHP_EOL, $input ) );
$score = 0;
foreach ( $lines as $line ) {
	try {
		$line->validate();
	} catch ( \InvalidSyntax $t ) {
		echo ( $t->getMessage() ) . PHP_EOL;
		$score += $t->getSyntaxErrorScore();
	}
}

var_dump( 'Score:', $score);

class Line {
	private string $lineText;

	public $separators = [
		'(' => ')',
		'[' => ']',
		'{' => '}',
		'<' => '>'
	];

	public static function fromInput( string $lineInput ): self {
		return new Line( $lineInput );
	}

	public function __construct( string $lineText ) {
		$this->lineText = $lineText;
	}

	public function validate() {
		$lineLength = strlen( $this->lineText );
		$open       = [];
		for ( $i = 0; $i < $lineLength; $i ++ ) {
			$char = $this->lineText[ $i ];
//			echo $char;
			if ( array_key_exists( $char, $this->separators ) ) {
//				echo ' is an opener' . PHP_EOL;
				$open[] = $char;
				continue;
			}

			$lastOpener = end( $open );
			$lastCloser = $this->separators[ $lastOpener ];
//			echo $char .' === '. $lastCloser . PHP_EOL;
			if ( $char === $lastCloser ) {
				array_pop( $open );
				continue;
			}

			throw new InvalidSyntax(
				$char,
				"Expected $lastCloser, but found $char instead."
			);
		}

	}
}


class InvalidSyntax extends Exception
{
	private $char;

	public function __construct( $char, $message = "", $code = 0, Throwable $previous = null ) {
		parent::__construct( $message, $code, $previous );
		$this->char = $char;
	}

	public function getSyntaxErrorScore(): ?int {
		$scores = [
			')' => 3,
			']' => 57,
			'}' => 1197,
			'>' => 25137,
		];

		return $scores[$this->char] ?? null;
	}
}
