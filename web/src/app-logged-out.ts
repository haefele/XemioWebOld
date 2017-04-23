import {BrowserService} from "./services/browser/browser-service";
import {AuthService} from "./services/auth/auth-service";
import {User} from "./services/auth/user";
import {autoinject} from "aurelia-framework";

@autoinject()
export class AppLoggedOut  {
    
    constructor(private readonly browserService: BrowserService, private readonly authService: AuthService) {        
    }

    public login(): void {
        this.authService.currentUser = {
            accessToken: "",
            idToken: "",
            refreshToken: ""
        };

        this.browserService.reload();
    }
}