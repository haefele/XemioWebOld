import {User} from "./user";
import {StorageService} from "../storage/storage-service";
import {BrowserService} from "../browser/browser-service";
import {autoinject} from "aurelia-framework";

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

    constructor(private readonly storageService: StorageService, private readonly browserService: BrowserService) {
    }

    public login(): void {
    }

    public logout(): void {
        this.currentUser = null;
        this.browserService.reload();
    }
}