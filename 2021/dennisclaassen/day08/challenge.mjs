import {input} from './input.mjs';

const entries = input.split('\n');
const totals = entries.map(entry => {
  let [uniqueSignalPattern, fourDigitOutputValue] = entry.split('|');
  uniqueSignalPattern = uniqueSignalPattern.trim();
  fourDigitOutputValue = fourDigitOutputValue.trim();

  const signalDigits = uniqueSignalPattern.split(' ');
  const splitter = /./g;
  // 1 is the only with 2 digits
  // 4 is the only with 4 digits
  // 7 is the only with 3 digits
  // 8 is the only with 7 digits
  const signalOne = signalDigits.find(digit => digit.length === 2).match(splitter);
  const signalFour = signalDigits.find(digit => digit.length === 4).match(splitter);
  const signalSeven = signalDigits.find(digit => digit.length === 3).match(splitter);
  const signalEight = signalDigits.find(digit => digit.length === 7).match(splitter);

  // 9, 0 and 6 all are 6 digits:
  // - 9 includes the entire 4
  // - 0 includes the entire 7
  // - 6 is the only one left with 6 digits
  const nineZeroSiz = signalDigits.filter(digit => digit.length === 6);
  const signalNine = nineZeroSiz.find(digit => {
        let digits = digit.match(splitter);
        return signalFour.every(digit => digits.includes(digit));
      }).match(splitter);
  const signalZero = nineZeroSiz
      .filter(digit => digit !== signalNine.join(''))
      .find(digit => {
        let digits = digit.match(splitter);
        return signalSeven.every(digit => digits.includes(digit));
      }).match(splitter);
  const signalSix = nineZeroSiz
      .filter(digit => digit !== signalNine.join(''))
      .find(digit => {
        let digits = digit.match(splitter);
        return !signalSeven.every(digit => digits.includes(digit));
      }).match(splitter);


  // 2, 3 and 5 all have 5 digits
  // - 3 includes the entire 1
  // - 2 does not includes the only digits not in 6 and not in 9
  // - 5 includes the only digit in 6 and in  9
  const threeTwoFive = signalDigits.filter(digit => digit.length === 5);

  const signalThree =threeTwoFive.find(
      digits => {
        return signalOne.every(digit => digits.includes(digit));
      }
  );
  const twoFive = threeTwoFive.filter( digit => digit !== signalThree);
  const notInNine = 'abcdefg'.split('').find(digit => !signalNine.includes(digit));
  const signalTwo = twoFive.find( digit => {
    return digit.split('').includes(notInNine)
  });
  const signalFive = twoFive.find( digit => !digit.split('').includes(notInNine));
  // const signalFive = twoFive[1];

  const mapping = {
    0: signalZero.sort().join(''),
    1: signalOne.sort().join(''),
    2: signalTwo.split('').sort().join(''),
    3: signalThree.split('').sort().join(''),
    4: signalFour.sort().join(''),
    5: signalFive.split('').sort().join(''),
    6: signalSix.sort().join(''),
    7: signalSeven.sort().join(''),
    8: signalEight.sort().join(''),
    9: signalNine.sort().join(''),
  };


  const getNumber = function(pattern, mapping) {
    let alphabetical = pattern.split('').sort().join('');
    let number = Object.values(mapping).findIndex(value => value === alphabetical);
    return number >= 0 ? number : '?';
  }

  const outputValue = fourDigitOutputValue.split(' ').reduce(
      (outputValue, digit) => {
        return outputValue += getNumber(digit, mapping);
      },
      '',
  );

  return parseInt(outputValue);
});

console.log(totals.reduce((total, nr) => total + nr, 0));

// const digits = parsedEntries.reduce( (digits, [,digit]) => digits.concat(
// digit.split(' ') ), [] ); const easyDigits = digits.filter(digit =>
// ([2,3,4,7].includes(digit.length) ) ); console.log(easyDigits.length);
