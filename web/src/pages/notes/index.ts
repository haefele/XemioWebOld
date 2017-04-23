import * as $ from "jquery";
import {Command} from "../../helper/command";

export class Index {   

    public name: string;

    public testCommand: Command;

    constructor() {
        this.testCommand = new Command(() => this.test(), () => this.canTest());
    }

    private canTest() : boolean {
        return this.name !== undefined 
            && this.name !== null 
            && this.name !== "";
    }
    private async test() : Promise<void> {
        await new Promise((resolve, reject) => {
            setTimeout(() => {
                resolve();
            }, 2000);
        });

        this.name = "";
    }
}