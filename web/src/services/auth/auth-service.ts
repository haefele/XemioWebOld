import {User} from "services/auth/user";
import {StorageService} from "services/storage/storage-service";
import {BrowserService} from "services/browser/browser-service";
import {autoinject} from "aurelia-framework";
import {UrlHelper} from "helper/url-helper";
import environment from "environment";
import "url-search-params-polyfill";

@autoinject()
export class AuthService {
    
    public get currentUser(): User {
        return this.storageService.getItem("CurrentUser");
    }
    public set currentUser(user: User) {
        this.storageService.setItem("CurrentUser", user);
    }    

    public get isLoggedIn(): boolean {
        return this.currentUser != null;
    }

    constructor(private readonly storageService: StorageService, private readonly browserService: BrowserService, private readonly urlHelper: UrlHelper) {
    }

    public loginWithFacebook(): Promise<void> {
        return this.login("facebook");
    }

    public loginWithGoogle(): Promise<void> {
        return this.login("google-oauth2");
    }

    public loginWithMicrosoft(): Promise<void> {
        return this.login("windowslive");
    }

    public tryFinishLogin(): void {
        let currentUrl = this.browserService.currentUrl();

        let correctState = <string>this.storageService.getItem("StateForAuth");
        let givenState = this.urlHelper.getParameter(currentUrl, "state");

        if (correctState == null || givenState == null)
            return;
        
        if (correctState !== givenState)
            throw new Error("Login failed.");

        this.currentUser = {
            accessToken: this.urlHelper.getParameter(currentUrl, "access_token"),
            idToken: this.urlHelper.getParameter(currentUrl, "id_token")
        };

        this.browserService.goToUrl(environment.url);
    }

    public logout(): void {
        this.currentUser = null;
        this.browserService.goToUrl(environment.url);
    }
    
    private async login(connection: string): Promise<void> {
        let state = this.newState();

        let query = new URLSearchParams();
        query.set("client_id", environment.auth0.clientId);
        query.set("redirect_uri", environment.auth0.redirectUrl);
        query.set("response_type", "token");
        query.set("connection", connection);
        query.set("scope", "openid");
        query.set("state", state);
        query.set("nonce", this.newState());

        let url = "https://" + environment.auth0.domain + "/authorize?" + query;

        this.storageService.setItem("StateForAuth", state);
        this.browserService.goToUrl(url);
    }

    private newState(): string {
        return Math.random().toString(32).slice(2);
    } 
}