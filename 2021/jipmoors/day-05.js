const fs = require("fs");

const input = fs.readFileSync("day-05.txt", "utf8").split("\n").filter(line => line).map(line => line.split(' -> ').map(parts => parts.split(',').map(value => parseInt(value, 10))));

let width = 0;
let height = 0;

for (let i in input) {
    width = Math.max(width, input[i][0][0]);
    width = Math.max(width, input[i][1][0]);

    height = Math.max(height, input[i][0][1]);
    height = Math.max(height, input[i][1][1]);
}

let map = [];

for (let y = 0; y <= height; y++) {
    map[y] = [];
    for (let x = 0; x <= width; x++) {
        map[y][x] = 0;
    }
}

// Star 1.
for (let row of input) {
    const minX = Math.min(row[0][0], row[1][0]);
    const maxX = Math.max(row[0][0], row[1][0]);

    const minY = Math.min(row[0][1], row[1][1]);
    const maxY = Math.max(row[0][1], row[1][1]);

    if (row[0][1] === row[1][1]) {
        for (let x = minX; x <= maxX; x++) {
            map[row[0][1]][x] += 1;
        }
    }

    if (row[0][0] === row[1][0]) {
        for (let y = minY; y <= maxY; y++) {
            map[y][row[0][0]] += 1;
        }
    }
}

const count = map.reduce( ( count, line ) => {
    for ( let x in line ) {
        if ( line[x] >= 2 ) {
            count ++;
        }
    }
    return count;
}, 0 );

console.log(count);

// Star 2 - add diagonal.
for (let row of input) {
    const minX = Math.min(row[0][0], row[1][0]);
    const maxX = Math.max(row[0][0], row[1][0]);

    const size = maxX - minX;
    const dirX = row[0][0] < row[1][0] ? 1 : -1;
    const dirY = row[0][1] < row[1][1] ? 1 : -1;

    if (row[0][0] !== row[1][0] && row[0][1] !== row[1][1] ) {
        for (let i = 0; i <= size; i++) {
            map[row[0][1] + (i*dirY)][row[0][0] + (i*dirX)] += 1;
        }
    }
}

const count2 = map.reduce( ( count, line ) => {
    for ( let x in line ) {
        if ( line[x] >= 2 ) {
            count ++;
        }
    }
    return count;
}, 0 );

console.log(count2);
