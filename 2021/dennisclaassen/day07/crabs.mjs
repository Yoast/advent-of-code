import {testInput as f,input} from './input.mjs';

const positions = input.split(',').map(position => parseInt(position));
console.log(positions);

const outerLeft = Math.min(...positions);
const outerRight = Math.max(...positions);

function calculateSimpleCost(currentPosition, possiblePosition) {
  return Math.abs( currentPosition - possiblePosition );
}

function calculateComplexCost(currentPosition, possiblePosition) {
  let nrOfSteps = calculateSimpleCost( currentPosition, possiblePosition );

  let cost = 0;
  for( let i = 0; i <= nrOfSteps; i++ ) {
    cost += i;
  }

  return cost;
}

const costOptions = [];
for( let possiblePosition = outerLeft; possiblePosition <= outerRight; possiblePosition++ ) {
  const totalCost = positions.reduce(
      (costSoFar, currentPosition) => {
        return costSoFar + calculateComplexCost( currentPosition, possiblePosition )
      },
      0
  )
  console.log(`For position ${possiblePosition}, this would take ${totalCost} fuel.`)
  costOptions.push(totalCost);
}

console.log(`Cheapest full investment takes ${Math.min(...costOptions)} fuel.`)
