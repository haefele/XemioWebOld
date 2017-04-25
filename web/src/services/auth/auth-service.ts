import {User} from "services/auth/user";
import {StorageService} from "services/storage/storage-service";
import {BrowserService} from "services/browser/browser-service";
import {autoinject} from "aurelia-framework";
import {OAuthHelper} from "helper/oauth-helper";
import {UrlHelper} from "helper/url-helper";
import environment from "environment";

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

    constructor(private readonly storageService: StorageService, private readonly browserService: BrowserService, private readonly oauthHelper: OAuthHelper, private readonly urlHelper: UrlHelper) {
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

    public logout(): void {
        this.currentUser = null;
        this.browserService.reload();
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

        let startUrl = "https://" + environment.auth0.domain + "/authorize?" + query;
        let endUrl = environment.auth0.redirectUrl;

        let redirectedUrl = await this.oauthHelper.showOAuthPopup("Login", startUrl, endUrl);

        let givenState = this.urlHelper.getParameter(redirectedUrl, "state");

        if (state !== givenState)
            throw new Error("Login failed.");

        this.currentUser = {
            accessToken: this.urlHelper.getParameter(redirectedUrl, "access_token"),
            idToken: this.urlHelper.getParameter(redirectedUrl, "id_token")
        };

        this.browserService.reload();
    }

    private newState(): string {
        return Math.random().toString(32).slice(2);
    } 
}