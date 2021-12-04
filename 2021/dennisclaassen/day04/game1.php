<?php

$input = require_once './input2.php';

class Board {
	/**
	 * @var int[][]
	 */
	private array $rows;

	private array $marked = [
		[ 0, 0, 0, 0, 0 ],
		[ 0, 0, 0, 0, 0 ],
		[ 0, 0, 0, 0, 0 ],
		[ 0, 0, 0, 0, 0 ],
		[ 0, 0, 0, 0, 0 ],
	];

	public static function fromInput( string $input ): Board {
		$inputRows =
			array_map(
				fn (array $row): array => (array_map('intval', array_map('trim', $row))),
				array_map(
					fn( string $row ): array => ( str_split( $row, 3 ) ),
					explode( PHP_EOL, $input )
			)
		);

		return new Board( $inputRows );
	}

	/**
	 * @param array<array<int>> $rows
	 */
	public function __construct( array $rows ) {
		$this->rows = $rows;
	}

	public function mark( int $number ) {
		foreach ( $this->rows as $rowId => $rowData ) {
			foreach ( $rowData as $celId => $celData ) {
				if ( $celData === $number ) {
					$this->marked[ $rowId ][ $celId ] = 1;
					break;
				}
			}
		}
	}

	public function hasCompleteRowOrColumn(): bool {
		for ( $i = 0; $i < 5; $i ++ ) {
			$rowMarks = array_count_values( $this->marked[ $i ] );
			if ( isset( $rowMarks[1] ) && $rowMarks[1] === 5 ) {
				return true;
			}

			$columnMarks = array_count_values( array_column( $this->marked, $i ) );
			if ( isset( $columnMarks[1] ) && $columnMarks[1] === 5 ) {
				return true;
			}

		}

		return false;
	}

	public function getUnmarkedScore(): int {
		$finalScore = 0;
		for ( $i = 0; $i < 5; $i ++ ) {
			for ( $j = 0; $j < 5; $j ++ ) {
				if ( $this->marked[ $i ][ $j ] === 0 ) {
					$finalScore += $this->rows[ $i ][ $j ];
				}
			}
		}

		return $finalScore;
	}
}

class Game {
	/**
	 * @var Board[]
	 */
	private array $boards;
	/**
	 * @var int[]
	 */
	private array $drawNumbers;

	private int $lastNumber;

	public static function fromInput( string $input ): Game {
		$inputParts  = explode( PHP_EOL . PHP_EOL, $input );
		$drawNumbers = array_map(
			'intval',
			explode(
				',',
				array_shift( $inputParts )
			)
		);

		$boards = array_map( [ Board::class, 'fromInput' ], $inputParts );

		return new Game( $boards, $drawNumbers );
	}

	/**
	 * @param Board[] $boards
	 * @param int[]   $drawNumbers
	 */
	public function __construct( array $boards, array $drawNumbers ) {
		$this->boards      = $boards;
		$this->drawNumbers = $drawNumbers;
	}

	public function play(): Board {
		foreach ( $this->drawNumbers as $number ) {
			foreach ( $this->boards as $board ) {
				$board->mark( $number );

				if ( $board->hasCompleteRowOrColumn() ) {
					$this->lastNumber = $number;

					return $board;
				}
			}
		}
	}

	public function getLastDrawnNumber(): int {
		return $this->lastNumber;
	}
}


$game = Game::fromInput( $input );
$board = $game->play();

//var_dump( $board );

var_dump( $game->getLastDrawnNumber() * $board->getUnmarkedScore() );
