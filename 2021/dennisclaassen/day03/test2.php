<?php

$inpuxt = <<<'INPUT'
00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010
INPUT;

$input = require_once 'input.php';

$data = explode( PHP_EOL, $input );
$data = array_map( fn( string $byteRow ) => ( str_split( $byteRow ) ), $data );

$oxygenRatingData  = $data;
$co2ScrubberRating = $data;

$length = count( $data[0] );
for ( $i = 0; $i < $length; $i ++ ) {
	if( count( $oxygenRatingData ) > 1 ) {
		$oxygenRow        = array_replace(
			[ 0, 0 ],
			array_count_values( array_column( $oxygenRatingData, $i ) )
		);
		$commonBit        = (string) ( $oxygenRow[1] >= $oxygenRow[0] ? 1 : 0 );
		$oxygenRatingData = array_filter( $oxygenRatingData, fn( array $row ): bool => ( $row[ $i ] === $commonBit ) );
	}

	if( count( $co2ScrubberRating ) > 1 ) {
		$co2Row            = array_replace(
			[ 0, 0 ],
			array_count_values( array_column( $co2ScrubberRating, $i ) )
		);
		$commonBit         = (string) ( $co2Row[0] <= $co2Row[1] ? 0 : 1 );

		$co2ScrubberRating = array_filter( $co2ScrubberRating, fn( array $row ): bool => ( $row[ $i ] === $commonBit ) );
	}
}

$oxygenRating = bindec( implode( '', reset( $oxygenRatingData ) ) );
$co2Rating = bindec( implode( '', reset( $co2ScrubberRating ) ) );


var_dump( $oxygenRating * $co2Rating );

//var_dump( $gammaRate, $epsilonRate );

