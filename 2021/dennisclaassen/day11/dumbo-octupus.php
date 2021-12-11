<?php

declare(strict_types=1);

$input = require_once './input.php';

$finput = <<<'INPUT'
5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526
INPUT;


$rows = explode( PHP_EOL, $input );
$octopy = array_map(
	function ( string $row ): array {
		return array_map(
			[ DumboOctopus::class, 'fromEnergyLevel' ],
			str_split($row)
		);
	},
	$rows
);

$cavern = Cavern::fromMap($octopy);
echo 'Before any steps:' . PHP_EOL;
$flashes = 0;
$nrOfSteps = 0;
while( $flashes !== 100 ) {
	//$cavern->render(); echo PHP_EOL;
	$flashes = $cavern->step();
	$nrOfSteps += 1;
	if( $flashes === 100 ) {
		echo 'After step ' . $nrOfSteps . ' (' . $flashes . ') flashes' . PHP_EOL;
		break;
	}
}
//$cavern->render();
//$flashes = $cavern->step();
//echo PHP_EOL . 'After step 2 (' . $flashes . ') flashes' . PHP_EOL;
//$cavern->render();
//$flashes = $cavern->step();
//echo PHP_EOL . 'After step 3 (' . $flashes . ') flashes' . PHP_EOL;
//$cavern->render();

class Cavern {
	/** @var DumboOctopus[][] $map */
	private array $map;

	/** @param DumboOctopus[][] $map */
	public static function fromMap(array $map):self {
		return new self($map);
	}

	/** @param DumboOctopus[][] $map */
	public function __construct(array $map) {
		$this->map = $map;

		$nrOfRows = count($map);
		for( $row = 0; $row < $nrOfRows; $row++ ) {
			$nrOfCells = count($map[$row]);
			for( $cell = 0; $cell < $nrOfCells; $cell++ ) {
				$octopus = $map[$row][$cell];
				$octopus->introduceNeighbours(
					$map[$row-1][$cell-1] ?? null,
					$map[$row-1][$cell] ?? null,
					$map[$row-1][$cell+1] ?? null,
					$map[$row][$cell-1] ?? null,
					$map[$row][$cell+1] ?? null,
					$map[$row+1][$cell-1] ?? null,
					$map[$row+1][$cell] ?? null,
					$map[$row+1][$cell+1] ?? null,
				);
			}
		}
	}

	private int $stepNumber = 0;

	public function step(): int {
		$this->stepNumber++;
		$flashes = 0;
		foreach( $this->map as $row ) {
			foreach( $row as $octopus ) {
				$flashes += $octopus->increaseEnergyLevel( $this->stepNumber );
			}
		}
		return $flashes;
	}

	public function render():void {
		foreach( $this->map as $row ) {
			foreach( $row as $octopus ) {
				echo $octopus->getEnergyLevel();
			}
			echo PHP_EOL;
		}

	}
}


class DumboOctopus {
	private int $energyLevel;
	/** @var DumboOctopus[] */
	private array $neighbours = [];
	private int $lastFlashStep = -1;

	public static function fromEnergyLevel(string $energyLevel):self {
		return new self((int)$energyLevel);
	}

	public function __construct(int $energyLevel) {
		$this->energyLevel = $energyLevel;
	}

	public function introduceNeighbours(
		$leftHigher, $middleHigher, $rightHigher,
		$left, $right,
		$leftBottom, $middleBottom, $rightBottom
	) {
		$this->neighbours = array_filter(
			[
				$leftHigher, $middleHigher, $rightHigher,
				$left, $right,
				$leftBottom,$middleBottom, $rightBottom
			]
		);
	}

	public function getEnergyLevel(): int {
		return $this->energyLevel;
	}

	public function increaseEnergyLevel( int $stepNumber ): int {
		if( $this->lastFlashStep === $stepNumber ) {
			return 0;
		}

		$this->energyLevel++;
		if( $this->energyLevel <= 9  ) {
			return 0;
		}

		$this->lastFlashStep = $stepNumber;

		// flash!
		$flashes = 1;
		foreach( $this->neighbours as $neighbour ) {
			$flashes += $neighbour->increaseEnergyLevel( $stepNumber );
		}

		$this->energyLevel = 0;

		return $flashes;
	}
}
