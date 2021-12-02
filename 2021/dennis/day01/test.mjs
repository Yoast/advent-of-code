import {input} from './input.mjs';

const total = input.reduce(
    function( output, number, currentIndex ) {
      if (currentIndex === 0) {
        return 0;
      }
      if(number > input[currentIndex-1]) {
        output++;
      }
      return output;
    },
    input,
    0
);
console.log(total);
