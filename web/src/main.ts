import {Aurelia} from 'aurelia-framework'
import environment from './environment';
import {AuthService} from "./services/auth/auth-service";

export function configure(aurelia: Aurelia) {
  aurelia.use
    .standardConfiguration()
    .feature('resources');

  if (environment.debug) {
    aurelia.use.developmentLogging();
  }

  if (environment.testing) {
    aurelia.use.plugin('aurelia-testing');
  }

  aurelia.start().then(a => {
    let authService: AuthService = a.container.get(AuthService);

    let root = authService.isLoggedIn
      ? "app-logged-in"
      : "app-logged-out";

    aurelia.setRoot(root);
  });
}
