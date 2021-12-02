import {input} from './input.mjs';

const inpfut = [
  199,
  200,
  208,
  210,
  200,
  207,
  240,
  269,
  260,
  263,
];

let increases = 0;
input.reduce(
    function({current, prev}, number, currentIndex ) {
      switch(currentIndex) {
        case 0:
          prev.push(number);
          break;
        case 1:
        case 2:
          prev.push(number);
          current.push(number);
          break;
        default:
          current.push(number);

          let totalPrev = prev.reduce( (total, num) => ( total + num ), 0 );
          let totalCurrent = current.reduce( (total, num) => ( total + num ), 0 );

          if(totalCurrent > totalPrev) {
            increases++;
          }


          prev.push(number);
          prev.shift();
          current.shift();


          break;
      }

      return {current, prev};
    },
    {current: [], prev: []},
);
console.log(increases);
