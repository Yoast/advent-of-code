import {course} from './course.mjs';

let xcourse = [
    "forward 5",
    "down 5",
    "forward 8",
    "up 3",
    "down 8",
    "forward 2",
];

const position = {
    horizontal: 0,
    vertical: 0,
    aim: 0,
};

class Command {
    constructor(units) {
        this.units = parseInt(units);
    }
}

class ForwardCommand extends Command {
    execute(position) {
        position.horizontal += this.units;
        position.vertical += position.aim * this.units;
    }
}

class DownCommand extends Command {
    execute(position) {
        position.aim += this.units;
    }
}

class UpCommand extends Command {
    execute(position) {
        position.aim -= this.units;
    }
}

class CommandFactory {
    static create(cmd) {
        let [type, units] = cmd.split(" ");
        switch( type ) {
            case "forward":
                return new ForwardCommand(units);
            case "up":
                return new UpCommand(units);
            case "down":
                return new DownCommand(units);
        }
    }
}

course.forEach((cmd) => {
    const command = CommandFactory.create(cmd);
    command.execute(position);
})

console.log(position);

console.log(position.horizontal * position.vertical);
