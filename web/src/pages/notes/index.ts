import * as $ from "jquery";
import {Command} from "helper/command";
import {FoldersApi, FolderDTO} from "apis/folders-api";
import {autoinject} from "aurelia-framework";
import { RoutableComponentActivate, NavigationInstruction, RouteConfig, IObservable } from "aurelia-router";

@autoinject()
export class Index implements RoutableComponentActivate {

    public folders: FolderDTO[];

    constructor(private readonly foldersApi: FoldersApi) {
    }

    public async activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction): Promise<void> {
        this.folders = await this.foldersApi.getRootFolders();
    }
}
