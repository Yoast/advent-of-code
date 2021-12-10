import {testInput as foo,input} from './input.mjs';

const fishTimers = input.split(',').map((timer) => parseInt(timer));

let fishAge = {
  0: 0,
  1: 0,
  2: 0,
  3: 0,
  4: 0,
  5: 0,
  6: 0,
  7: 0,
  8: 0,
};

fishTimers.forEach((fishTimer) => {
  fishAge[fishTimer]++;
});

for (let day = 1; day <= 256; day++) {
  fishAge = {
    0: fishAge[1],
    1: fishAge[2],
    2: fishAge[3],
    3: fishAge[4],
    4: fishAge[5],
    5: fishAge[6],
    6: fishAge[0] + fishAge[7],
    7: fishAge[8],
    8: fishAge[0],
  };

  // console.log(`After ${day} days`, JSON.stringify(fishAge));
}

let nrOfFishies = fishAge[0] +
fishAge[1] +
fishAge[2] +
fishAge[3] +
fishAge[4] +
fishAge[5] +
fishAge[6] +
fishAge[7] +
fishAge[8];

console.log(
    `There are ${nrOfFishies} fishies.`,
);
