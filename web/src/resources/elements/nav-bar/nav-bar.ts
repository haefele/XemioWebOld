import {bindable, autoinject} from "aurelia-framework";
import {Router} from "aurelia-router";
import {AuthService} from "services/auth/auth-service";

@autoinject()
export class NavBar {

    private readonly _authService: AuthService;

    @bindable()
    public router: Router;

    public constructor(authService: AuthService) {
        this._authService = authService;
    }

    public logout(): void {
        this._authService.logout();
    }
}