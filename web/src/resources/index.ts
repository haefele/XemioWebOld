import {FrameworkConfiguration} from 'aurelia-framework';

export function configure(config: FrameworkConfiguration) {
  config.globalResources([
    //Bootstrap
    "bootstrap/css/bootstrap.css",
    "resources/global.css",

    //Attributes
    "resources/attributes/button-command",

    //Elements
    "resources/elements/nav-bar/nav-bar"
  ]);
}
