import {RouterConfiguration, RouteConfig, Router, ConfiguresRouter} from "aurelia-router";

export class App implements ConfiguresRouter {

  public router: Router;

  public configureRouter(config: RouterConfiguration, router: Router): void | Promise<void> | PromiseLike<void> {
    this.router = router;

    config.map([
      { route: ["", "notes"], moduleId: "pages/notes/index", name: "notes", title: "Notes", nav: true },
      { route: "todo", moduleId: "pages/todo/index", name: "todo", title: "Todo", nav: true}
    ]);
  }
}
