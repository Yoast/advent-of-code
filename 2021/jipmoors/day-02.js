var fs = require("fs");

const data = fs.readFileSync("day-02.txt", "utf8");
const list = data.split("\n").map((item) => item.split(' '));

let x = 0;
let z = 0;

list.map(line => {
    const value = parseInt(line[1], 10);
    switch (line[0]) {
        case "forward":
            x += value;
            break;
        case "down":
            z += value;
            break;
        case "up":
            z -= value;
            z = Math.max( 0, z );
            break;
    }
});

console.log( x * z );

x = 0;
z = 0;
let aim = 0;

list.map(line => {
    const value = parseInt(line[1], 10);
    switch (line[0]) {
        case "forward":
            x += value;
            z += aim * value;
            break;
        case "down":
            aim += value;
            break;
        case "up":
            aim -= value;
            break;
    }
});

console.log( x * z );
