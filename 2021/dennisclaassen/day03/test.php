<?php

$inpfut = <<<'INPUT'
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
$data = array_map( fn(string $byteRow) => ( str_split( $byteRow ) ), $data );

$gamma = '';
$epsilon = '';
$length = count( $data[0] );
var_dump( $length );
for( $i = 0; $i < $length; $i++ ) {
	$row = array_count_values( array_column( $data, $i ) );
	$gamma .= array_search( max( $row ), $row );
	$epsilon .= array_search( min( $row ), $row );
}
$gammaRate = bindec( $gamma );
$epsilonRate = bindec( $epsilon );
//var_dump( $gammaRate, $epsilonRate );

var_dump( $gammaRate * $epsilonRate );
