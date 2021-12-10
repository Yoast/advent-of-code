var fs = require("fs");

const data = fs.readFileSync("day-04.txt", "utf8").split("\n");
const [first, empty, ...rest] = data;

const numbers = first.split(',').map(number => parseInt(number, 10));

let board = 0;
const boards = rest.reduce((accumulator, line) => {
    if (line === '') {
        board++;
        accumulator[board] = [];
    } else {
        let characters = line.split('');
        let numbers = [];
        for (let x = 0; x < characters.length; x += 3) {
            numbers.push(parseInt(characters[x] + characters[x + 1], 10));
        }
        accumulator[board].push(numbers);
    }

    return accumulator
}, [[]]).filter( board => board.length > 0 );

const pluck = (input, index) => input.map(row => row[index]);
const bingo = "a".repeat(boards[0].length);

const findBingo = ( boards ) => {
    let last;

    for (let n = 0; n < numbers.length; n++) {
        const current = numbers[n];
        last = current;
        boards = boards.map(board => board.map(row => row.map(value => value === current ? "a" : value)));

        for (const board of boards) {
            for (let y = 0; y < board.length; y++) {
                if (board[y].join('') === bingo || pluck(board,y).join('') === bingo) {
                    return {board, last};
                }
            }
        }
    }
}

const findLastBingo = (boards) => {
    let last;

    for (let n = 0; n < numbers.length; n++) {
        const current = numbers[n];
        last = current;
        boards = boards.map(board => board.map(row => row.map(value => value === current ? "a" : value)));

        if ( boards.length > 1 ) {
            boards = boards.filter(board => {
                for (let y = 0; y < board.length; y++) {
                    if (board[y].join('') === bingo || pluck(board, y).join('') === bingo) {
                        return false;
                    }
                }
                return true;
            });
        }

        if ( boards.length === 1 ) {
            const board = boards[0];

            for (let y = 0; y < board.length; y++) {
                if (board[y].join('') === bingo || pluck(board,y).join('') === bingo) {
                    return {board, last};
                }
            }
        }
    }
}

const calculateBoardValue = ( board ) => {
    return board.map( row => row.filter( value => value !== 'a' ).reduce((a, b) => a+b, 0)).reduce(( a, b) => a+b, 0)
}

const winner = findBingo( [...boards] );
const last = findLastBingo( [...boards] );

console.log(winner.last * calculateBoardValue(winner.board));
console.log(last.last * calculateBoardValue(last.board));
