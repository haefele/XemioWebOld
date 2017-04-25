import {BrowserService} from "services/browser/browser-service";
import {AuthService} from "services/auth/auth-service";
import {User} from "services/auth/user";
import {autoinject} from "aurelia-framework";
import {Command} from "helper/command";

@autoinject()
export class AppLoggedOut {

    public microsoftLoginCommand: Command;
    public facebookLoginCommand: Command;
    public googleLoginCommand: Command;

    constructor(private readonly browserService: BrowserService, private readonly authService: AuthService) {        
        this.microsoftLoginCommand = new Command(() => this.authService.loginWithMicrosoft());
        this.facebookLoginCommand = new Command(() => this.authService.loginWithFacebook());
        this.googleLoginCommand = new Command(() => this.authService.loginWithGoogle());
    }
}