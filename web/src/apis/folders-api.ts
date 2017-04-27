import {HttpClient, Interceptor} from "aurelia-fetch-client";
import environment from "environment";
import {AuthService} from "services/auth/auth-service";
import {autoinject} from "aurelia-framework";
import {AuthHeaderInterceptor} from "apis/auth-header-interceptor";

@autoinject()
export class FoldersApi {
    private client: HttpClient;

    constructor(authService: AuthService) {        
        this.client = new HttpClient();
        this.client.configure(f => f.withBaseUrl(environment.api).withInterceptor(new AuthHeaderInterceptor(authService)));
    }

    public async getRootFolders(): Promise<FolderDTO[]> {
        let response = await this.client.fetch("notes/folders");
        return response.json();
    }
}


export class FolderDTO {
    public id: string;
    public name: string;
    public parentFolderId: string;
    public notesCount: number;
    public subFoldersCount: number;
}
