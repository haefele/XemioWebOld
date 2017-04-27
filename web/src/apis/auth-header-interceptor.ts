import {HttpClient, Interceptor} from "aurelia-fetch-client";
import {AuthService} from "services/auth/auth-service";

export class AuthHeaderInterceptor implements Interceptor  {
    
    constructor(private readonly authService: AuthService) {        
    }

    public request (request: Request) : Request | Response | Promise<Request | Response> {
        request.headers.set("Authorization", `Bearer ${this.authService.currentUser.idToken}`);

        return request;
    }
}
