import * as $ from "jquery";
import {Command} from "../../helper/command";
import {RoutableComponentCanDeactivate, NavigationCommand} from "aurelia-router";

export class Index implements RoutableComponentCanDeactivate {   

    public name: string;

    public testCommand: Command;

    constructor() {
        this.testCommand = new Command(() => this.test(), () => this.canTest());
    }

    public canDeactivate(): boolean | Promise<boolean> | PromiseLike<boolean> | NavigationCommand {
        return this.testCommand.isExecuting !== true;
    }

    private canTest() : boolean {
        return this.name !== undefined 
            && this.name !== null 
            && this.name !== "";
    }
    private async test() : Promise<any> {
        await new Promise((resolve, reject) => {
            setTimeout(() => {
                resolve();
            }, 2000);
        });

        this.name = "";
    }
}