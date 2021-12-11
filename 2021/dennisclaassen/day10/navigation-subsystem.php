<?php

$input = require './input.php';

$infput = <<<'INPUT'
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
$syntaxErrorScore = 0;
$autocompleteScores = [];
foreach ( $lines as $line ) {
	try {
		$autocompleteScores[] = $line->getAutocompleteScore();
//		echo 'Valid!' . PHP_EOL;
	} catch ( \InvalidSyntax $t ) {
//		echo ( $t->getMessage() ) . PHP_EOL;
		$syntaxErrorScore += $t->getSyntaxErrorScore();
	}
}

printf( '$syntaxErrorScore: %d%s', $syntaxErrorScore, PHP_EOL);

asort($autocompleteScores);

// this is terrible, but will remove the 2 outer values until we only have the middle one left.
// Luckily we're sure the number of autocomplete scores will be odd, so we can take this approach.
while( count( $autocompleteScores ) !== 1 ) {
	array_shift( $autocompleteScores );
	array_pop( $autocompleteScores );
}

printf( '$autocompleteScore: %d%s', reset($autocompleteScores), PHP_EOL);

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

	public function getAutocompleteScore():int {
		$lineLength = strlen( $this->lineText );
		$open       = [];
		for ( $i = 0; $i < $lineLength; $i ++ ) {
			$char = $this->lineText[ $i ];
//			echo $char;
			if ( array_key_exists( $char, $this->separators ) ) {
				$open[] = $char;
				continue;
			}

			$lastOpener = end( $open );
			$lastCloser = $this->separators[ $lastOpener ];
			if ( $char === $lastCloser ) {
				array_pop( $open );
				continue;
			}

			throw new InvalidSyntax(
				$char,
				"Expected $lastCloser, but found $char instead."
			);
		}

		$score = 0;
//		echo PHP_EOL;
		$completer = '';
		$reversedOpen = array_reverse( $open );
		foreach( $reversedOpen as $opener ) {
//			echo 'Multiply ' . $score . ' by 5 to get ';
			$score = $score * 5;
			$closer = $this->separators[$opener];
			$completer .= $closer;

//			echo $score . ', then add the value of ' . $closer;
			switch ( $closer ) {
				case ')':
					$score += 1;
//					echo ' (1) ';
					break;
				case ']':
					$score += 2;
//					echo ' (2) ';
					break;
				case '}':
					$score += 3;
//					echo ' (3) ';
					break;
				case '>':
					$score += 4;
//					echo ' (4) ';
					break;
			}
//			echo ' to get a new total score of ' . $score . '.' . PHP_EOL;
		}
		echo $completer . ' - ' . $score . '  total points.' . PHP_EOL;
		return $score;
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
