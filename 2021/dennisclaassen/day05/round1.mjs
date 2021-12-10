import {input} from './input.mjs';

const exampleinput = `
0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2
`;

const validLines = input.split('\n').
    filter(ventLine => ventLine !== '').
    map((ventLine) => {
      const [, x1, y1, x2, y2] = ventLine.match(/(\d+),(\d+) -> (\d+),(\d+)/);
      return {
        x1: parseInt(x1),
        y1: parseInt(y1),
        x2: parseInt(x2),
        y2: parseInt(y2),
      };
    });
    // filter for part 1
    // }).    filter(line => line.x1 === line.x2 || line.y1 === line.y2);

const verticalLineTraveller = function(line) {
  if(line.x1 !== line.x2 ) {
    throw new Error('Not a vertical line');
  }

  const yMin = Math.min(line.y1, line.y2);
  const yMax = Math.max(line.y1, line.y2);

  const pointsOnLine = [];
  for (let y = yMin; y <= yMax; y++) {
    pointsOnLine.push( {x: line.x1, y} );
  }
  return pointsOnLine;
}
const horizontalLiveTraveller = function(line) {
  if(line.y1 !== line.y2 ) {
    throw new Error('Not a horizontal line');
  }

  const xMin = Math.min(line.x1, line.x2);
  const xMax = Math.max(line.x1, line.x2);

  const pointsOnLine = [];
  for (let x = xMin; x <= xMax; x++) {
    pointsOnLine.push( {x, y: line.y1} );
  }
  return pointsOnLine;
}

const diagonalLineTraveller = function(line) {
  const xMin = Math.min(line.x1, line.x2);
  const xMax = Math.max(line.x1, line.x2);
  const yMin = Math.min(line.y1, line.y2);
  const yMax = Math.max(line.y1, line.y2);

  const pointsOnLine = [];
  let x = line.x1, y = line.y1;
  pointsOnLine.push( {x, y} );
  do {
    if(line.x1 < line.x2) {
      x += 1;
    } else {
      x -= 1;
    }

    if(line.y1 < line.y2) {
      y += 1;
    } else {
      y -= 1;
    }

    pointsOnLine.push( {x, y} );
  } while ( x !== line.x2 && y !== line.y2 );

  return pointsOnLine;
}

const lineTraveller = function(line) {
  const xMin = Math.min(line.x1, line.x2);
  const xMax = Math.max(line.x1, line.x2);

  if( xMin === xMax ) {
    return verticalLineTraveller(line);
  }

  const yMin = Math.min(line.y1, line.y2);
  const yMax = Math.max(line.y1, line.y2);

  if(yMin === yMax) {
    return horizontalLiveTraveller(line);
  }

  return diagonalLineTraveller(line);
}

// console.log(validLines);
let highestNumber = 0;
const diagram = validLines.reduce(
    (diagram, validLine) => {
      const pointsOnLine = lineTraveller(validLine);
      pointsOnLine.forEach(
          ({x,y}) => {
            if(!diagram[x]) {
              diagram[x] = [];
            }
            if (!diagram[x][y]) {
              diagram[x][y] = 0;
            }

            diagram[x][y] += 1;
          }
      );
      return diagram;
    },
    [],
);

console.log(
    diagram.reduce(
        (initial, row) => {
          return row.reduce(
              (initial, cell) => {
                return cell >= 2 ? initial + 1 : initial;
              },
              initial,
          );
        },
        0,
    ),
);
