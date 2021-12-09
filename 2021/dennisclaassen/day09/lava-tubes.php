<?php

$input = require_once 'input.php';

$inpxut = <<<'INPUT'
2199943210
3987894921
9856789892
8767896789
9899965678
INPUT;

$lines = array_map( fn( string $row ): array => ( str_split( $row ) ), explode( PHP_EOL, $input ) );

$points    = [];
$nrOfLines = count( $lines );
for ( $rowIndex = 0; $rowIndex < $nrOfLines; $rowIndex ++ ) {
	$points[$rowIndex] = [];
	$rowLength = count($lines[$rowIndex]);
	for( $pointIndex = 0; $pointIndex < $rowLength; $pointIndex++ ) {
		$points[$rowIndex][$pointIndex] = new Point(
			$lines[$rowIndex][$pointIndex],
//			[
//				$lines[$rowIndex-1][$pointIndex] ?? null,
//				$lines[$rowIndex][$pointIndex+1] ?? null,
//				$lines[$rowIndex+1][$pointIndex] ?? null,
//				$lines[$rowIndex][$pointIndex-1] ?? null,
//			]
		);
	}
}

for ( $rowIndex = 0; $rowIndex < $nrOfLines; $rowIndex ++ ) {
	$rowLength = count($lines[$rowIndex]);
	for( $pointIndex = 0; $pointIndex < $rowLength; $pointIndex++ ) {
		$points[$rowIndex][$pointIndex]->setAdjacentPoints(
			[
				$points[$rowIndex-1][$pointIndex] ?? null,
				$points[$rowIndex][$pointIndex+1] ?? null,
				$points[$rowIndex+1][$pointIndex] ?? null,
				$points[$rowIndex][$pointIndex-1] ?? null,
			]
		);
	}
}

$id = 0;
$basins = [];
for ( $rowIndex = 0; $rowIndex < $nrOfLines; $rowIndex ++ ) {
	for ( $pointIndex = 0; $pointIndex < $rowLength; $pointIndex ++ ) {
		$point = $points[$rowIndex][$pointIndex];
		if( !$point->canBePartOfBasin()) {
			echo '-';
			continue;
		}

		if ( $point->isPartOfBasin()) {
			echo "'";//$point->getBasin()->id;
			continue;
		}

		$leftPoint = $points[$rowIndex-1][$pointIndex] ?? null;
		$abovePoint = $points[$rowIndex][$pointIndex-1] ?? null;

		if( isset( $leftPoint ) )
			$basin = $leftPoint->getBasin();
		if (!isset($basin) && isset( $abovePoint ))
			$basin = $abovePoint->getBasin();

		if( !isset($basin))
			$basins[] = $basin = new Basin(++$id);

		$point->putInBasin($basin);
		echo $basin->id;
		$basin = null;
	}
	echo PHP_EOL;
}

$basinSizes = array_map( fn( Basin $basin ): int => ( $basin->getSize() ), $basins );
asort($basinSizes);
$basinSizes = array_reverse($basinSizes);
$basinSizes = array_slice($basinSizes, 0, 3);
var_dump( $basinSizes );
var_dump( array_reduce( $basinSizes, fn(int $carry, int $size):int => ($carry * $size),1) );

//
//$riskLevelSum = 0;
//foreach( $points as $point ) {
//	if( $point->isLowPoint() ) {
//		$riskLevelSum += $point->getRiskLevel();
//	}
//}
//
//printf('Risk level is %d', $riskLevelSum);

class Point {
	public int $nr;
	public array $adjacentLocations;
	public array $points;
	public ?Basin $basin = null;

	public function __construct(int $nr, array $adjacentLocations=[]) {
		$this->nr = $nr;
		$this->adjacentLocations = array_filter( $adjacentLocations, fn(?int $location):bool => ($location !== null) );
	}

	public function setAdjacentPoints(array $points):void {
		$this->points = array_filter( $points, fn(?Point $point):bool => ($point !== null) );;
	}

	public function isLowPoint():bool {
		return $this->nr < min($this->adjacentLocations);
	}
	public function getRiskLevel():int {
		return $this->nr + 1;
	}
	public function canBePartOfBasin():bool {
		return $this->nr !== 9;
	}
	public function putInBasin(Basin $basin):void {
		$this->basin = $basin;
		$basin->addPoint($this);

		foreach( $this->points as $otherPoint ) {
			if( !$otherPoint->canBePartOfBasin() )
				continue;

			if( $otherPoint->isPartOfBasin() )
				continue;

			$otherPoint->putInBasin($basin);
		}
	}
	public function isPartOfBasin(): bool {
		return $this->basin !== null;
	}
	public function getBasin():?Basin {
		return $this->basin;
	}
}

class Basin {
	public array $points = [];
	public int $id;

	public function __construct(int $id) {
		$this->id = $id;
	}

	public function addPoint(Point $point): void {
		$this->points[] = $point;
	}

	public function getSize(): int {
		return count($this->points);
	}
}


