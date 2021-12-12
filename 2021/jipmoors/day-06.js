const fs = require("fs");

const input = fs.readFileSync("day-06.txt", "utf8").split("\n").filter(line => line)[0].split(',').map(part => parseInt(part, 10));

const pool = [];

class fish {
    constructor(timer = 8) {
        this.timer = timer;
    }

    tick() {
        this.timer -= 1;
        if (this.timer < 0) {
            this.timer = 7 + this.timer;
            pool.push(new fish());
        }
    }
}

input.map(timer => {
    pool.push(new fish(timer));
});

const days = 80;

for (let d = 0; d < days; d++) {
    pool.map(fish => fish.tick());
}

console.log(pool.length);

const pool2 = [
    0, 0, 0, 0, 0, 0, 0
];

for (let i of input) {
    pool2[i] += 1;
}

const wait = [0, 0];

const dayTick = (day) => {
    const r = (day % 7);
    const n = pool2[r];

    pool2[r] += wait[0];
    wait[0] = wait[1];
    wait[1] = n;
}

for (let d = 0; d <= 257; d++) {
    dayTick(d);
}

console.log(pool2.reduce((total, item) => total + item, 0));
