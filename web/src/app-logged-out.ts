import {BrowserService} from "./services/browser/browser-service";
import {AuthService} from "./services/auth/auth-service";
import {User} from "./services/auth/user";
import {autoinject} from "aurelia-framework";
import {Command} from "./helper/command";
import * as A from "aurelia-router";

@autoinject()
export class AppLoggedOut implements A.RoutableComponentActivate {

    public loginCommand: Command;

    constructor(private readonly browserService: BrowserService, private readonly authService: AuthService) {        
        this.loginCommand = new Command(() => this.login(), () => true);
    }

    public activate(params: any, routeConfig: A.RouteConfig, navigationInstruction: A.NavigationInstruction): void | Promise<void> | PromiseLike<void> | A.IObservable {
        
    }

    private async login(): Promise<void> {
        await this.authService.startLogin();
    }
}