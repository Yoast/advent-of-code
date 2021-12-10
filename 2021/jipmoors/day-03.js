var fs = require("fs");

const data = fs.readFileSync("day-03.txt", "utf8");
const list = data.split("\n").map((item) => item.split('')).filter((item) => item.length > 0);

const pluck = (input, index) => input.map(row => parseInt(row[index], 10));

let gamma = '';
let epsilon = '';

for (let i = 0; i < list[0].length; i++) {
    const j = pluck(list, i);
    const s = j.reduce((accumulator, item) => accumulator + item, 0);
    gamma += s > j.length / 2 ? '1' : '0';
    epsilon += s > j.length / 2 ? '0' : '1';
}

console.log(parseInt(gamma, 2) * parseInt(epsilon, 2));

let list_a = [...list];
let list_b = [...list];

for (let i = 0; i < list.length; i++) {
    for (let j = 0; j < list[i].length; j++) {
        if (list_a.length > 1) {
            const pa = pluck(list_a, j);
            const sa = pa.reduce((accumulator, item) => accumulator + item, 0);
            const ca = sa >= pa.length / 2 ? '1' : '0';
            list_a = list_a.filter( item => item[j] === ca );
        }
        if (list_b.length > 1) {
            const pb = pluck(list_b, j);
            const sb = pb.reduce((accumulator, item) => accumulator + item, 0);
            const cb = sb >= pb.length / 2 ? '0' : '1';
            list_b = list_b.filter( item => item[j] === cb );
        }
    }
}

console.log(parseInt(list_a[0].join(''),2) * parseInt(list_b[0].join(''), 2));
