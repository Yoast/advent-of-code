<?php

$input = require_once 'input.php';

$xinput = <<<'INPUT'
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
	$rowLength = count($lines[$rowIndex]);
	for( $pointIndex = 0; $pointIndex < $rowLength; $pointIndex++ ) {
		$points[] = new Point(
			$lines[$rowIndex][$pointIndex],
			[
				$lines[$rowIndex-1][$pointIndex] ?? null,
				$lines[$rowIndex][$pointIndex+1] ?? null,
				$lines[$rowIndex+1][$pointIndex] ?? null,
				$lines[$rowIndex][$pointIndex-1] ?? null,
			]
		);
	}
}

$riskLevelSum = 0;
foreach( $points as $point ) {
	if( $point->isLowPoint() ) {
//		var_dump( $point->nr);
		$riskLevelSum += $point->getRiskLevel();
	}
}

printf('Risk level is %d', $riskLevelSum);

class Point {
	public int $nr;
	public array $adjacentLocations;

	public function __construct(int $nr, array $adjacentLocations) {
		$this->nr = $nr;
		$this->adjacentLocations = array_filter( $adjacentLocations, fn(?int $location):bool => ($location !== null) );
	}

	public function isLowPoint():bool {
		return $this->nr < min($this->adjacentLocations);
	}
	public function getRiskLevel():int {
		return $this->nr + 1;
	}
}
