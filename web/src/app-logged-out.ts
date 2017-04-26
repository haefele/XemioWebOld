import {BrowserService} from "services/browser/browser-service";
import {AuthService} from "services/auth/auth-service";
import {User} from "services/auth/user";
import {autoinject} from "aurelia-framework";
import {Command} from "helper/command";
import { RoutableComponentActivate, RouteConfig, NavigationInstruction, IObservable } from "aurelia-router";

@autoinject()
export class AppLoggedOut implements RoutableComponentActivate {

    public microsoftLoginCommand: Command;
    public facebookLoginCommand: Command;
    public googleLoginCommand: Command;

    constructor(private readonly browserService: BrowserService, private readonly authService: AuthService) {        
        this.microsoftLoginCommand = new Command(() => this.authService.loginWithMicrosoft());
        this.facebookLoginCommand = new Command(() => this.authService.loginWithFacebook());
        this.googleLoginCommand = new Command(() => this.authService.loginWithGoogle());
    }

    public activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction): void | Promise<void> | PromiseLike<void> | IObservable {
        this.authService.tryFinishLogin();
    }
}