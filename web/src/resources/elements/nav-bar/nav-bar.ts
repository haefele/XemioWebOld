import {bindable, autoinject} from "aurelia-framework";
import {Router} from "aurelia-router";
import {AuthService} from "../../../services/auth-service";

@autoinject()
export class NavBar {

    @bindable()
    public router: Router;

    public constructor(public authService : AuthService) {
    }
}