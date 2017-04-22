define('app',["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    var App = (function () {
        function App() {
            this.message = 'Hello World!';
        }
        App.prototype.configureRouter = function (config, router) {
            this.router = router;
            config.map([
                { route: ["", "notes"], moduleId: "pages/notes/index", name: "notes", title: "Notes", nav: true },
                { route: "todo", moduleId: "pages/todo/index", name: "todo", title: "Todo", nav: true }
            ]);
        };
        return App;
    }());
    exports.App = App;
});

//# sourceMappingURL=data:application/json;charset=utf8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7SUFFQTtRQUFBO1lBR1MsWUFBTyxHQUFHLGNBQWMsQ0FBQztRQVVsQyxDQUFDO1FBUlEsNkJBQWUsR0FBdEIsVUFBdUIsTUFBMkIsRUFBRSxNQUFjO1lBQ2hFLElBQUksQ0FBQyxNQUFNLEdBQUcsTUFBTSxDQUFDO1lBRXJCLE1BQU0sQ0FBQyxHQUFHLENBQUM7Z0JBQ1QsRUFBRSxLQUFLLEVBQUUsQ0FBQyxFQUFFLEVBQUUsT0FBTyxDQUFDLEVBQUUsUUFBUSxFQUFFLG1CQUFtQixFQUFFLElBQUksRUFBRSxPQUFPLEVBQUUsS0FBSyxFQUFFLE9BQU8sRUFBRSxHQUFHLEVBQUUsSUFBSSxFQUFFO2dCQUNqRyxFQUFFLEtBQUssRUFBRSxNQUFNLEVBQUUsUUFBUSxFQUFFLGtCQUFrQixFQUFFLElBQUksRUFBRSxNQUFNLEVBQUUsS0FBSyxFQUFFLE1BQU0sRUFBRSxHQUFHLEVBQUUsSUFBSSxFQUFDO2FBQ3ZGLENBQUMsQ0FBQztRQUNMLENBQUM7UUFDSCxVQUFDO0lBQUQsQ0FiQSxBQWFDLElBQUE7SUFiWSxrQkFBRyIsImZpbGUiOiJhcHAuanMiLCJzb3VyY2VzQ29udGVudCI6WyJpbXBvcnQge1JvdXRlckNvbmZpZ3VyYXRpb24sIFJvdXRlQ29uZmlnLCBSb3V0ZXIsIENvbmZpZ3VyZXNSb3V0ZXJ9IGZyb20gXCJhdXJlbGlhLXJvdXRlclwiO1xyXG5cclxuZXhwb3J0IGNsYXNzIEFwcCBpbXBsZW1lbnRzIENvbmZpZ3VyZXNSb3V0ZXIge1xyXG5cclxuICBwdWJsaWMgcm91dGVyOiBSb3V0ZXI7XHJcbiAgcHVibGljIG1lc3NhZ2UgPSAnSGVsbG8gV29ybGQhJztcclxuXHJcbiAgcHVibGljIGNvbmZpZ3VyZVJvdXRlcihjb25maWc6IFJvdXRlckNvbmZpZ3VyYXRpb24sIHJvdXRlcjogUm91dGVyKTogdm9pZCB8IFByb21pc2U8dm9pZD4gfCBQcm9taXNlTGlrZTx2b2lkPiB7XHJcbiAgICB0aGlzLnJvdXRlciA9IHJvdXRlcjtcclxuXHJcbiAgICBjb25maWcubWFwKFtcclxuICAgICAgeyByb3V0ZTogW1wiXCIsIFwibm90ZXNcIl0sIG1vZHVsZUlkOiBcInBhZ2VzL25vdGVzL2luZGV4XCIsIG5hbWU6IFwibm90ZXNcIiwgdGl0bGU6IFwiTm90ZXNcIiwgbmF2OiB0cnVlIH0sXHJcbiAgICAgIHsgcm91dGU6IFwidG9kb1wiLCBtb2R1bGVJZDogXCJwYWdlcy90b2RvL2luZGV4XCIsIG5hbWU6IFwidG9kb1wiLCB0aXRsZTogXCJUb2RvXCIsIG5hdjogdHJ1ZX1cclxuICAgIF0pO1xyXG4gIH1cclxufVxyXG4iXSwic291cmNlUm9vdCI6InNyYyJ9

define('environment',["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = {
        debug: true,
        testing: true
    };
});

//# sourceMappingURL=data:application/json;charset=utf8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImVudmlyb25tZW50LnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7OztJQUFBLGtCQUFlO1FBQ2IsS0FBSyxFQUFFLElBQUk7UUFDWCxPQUFPLEVBQUUsSUFBSTtLQUNkLENBQUMiLCJmaWxlIjoiZW52aXJvbm1lbnQuanMiLCJzb3VyY2VzQ29udGVudCI6WyJleHBvcnQgZGVmYXVsdCB7XG4gIGRlYnVnOiB0cnVlLFxuICB0ZXN0aW5nOiB0cnVlXG59O1xuIl0sInNvdXJjZVJvb3QiOiJzcmMifQ==

define('main',["require", "exports", "./environment"], function (require, exports, environment_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    function configure(aurelia) {
        aurelia.use
            .standardConfiguration()
            .feature('resources');
        if (environment_1.default.debug) {
            aurelia.use.developmentLogging();
        }
        if (environment_1.default.testing) {
            aurelia.use.plugin('aurelia-testing');
        }
        aurelia.start().then(function () { return aurelia.setRoot(); });
    }
    exports.configure = configure;
});

//# sourceMappingURL=data:application/json;charset=utf8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIm1haW4udHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7O0lBR0EsbUJBQTBCLE9BQWdCO1FBQ3hDLE9BQU8sQ0FBQyxHQUFHO2FBQ1IscUJBQXFCLEVBQUU7YUFDdkIsT0FBTyxDQUFDLFdBQVcsQ0FBQyxDQUFDO1FBRXhCLEVBQUUsQ0FBQyxDQUFDLHFCQUFXLENBQUMsS0FBSyxDQUFDLENBQUMsQ0FBQztZQUN0QixPQUFPLENBQUMsR0FBRyxDQUFDLGtCQUFrQixFQUFFLENBQUM7UUFDbkMsQ0FBQztRQUVELEVBQUUsQ0FBQyxDQUFDLHFCQUFXLENBQUMsT0FBTyxDQUFDLENBQUMsQ0FBQztZQUN4QixPQUFPLENBQUMsR0FBRyxDQUFDLE1BQU0sQ0FBQyxpQkFBaUIsQ0FBQyxDQUFDO1FBQ3hDLENBQUM7UUFFRCxPQUFPLENBQUMsS0FBSyxFQUFFLENBQUMsSUFBSSxDQUFDLGNBQU0sT0FBQSxPQUFPLENBQUMsT0FBTyxFQUFFLEVBQWpCLENBQWlCLENBQUMsQ0FBQztJQUNoRCxDQUFDO0lBZEQsOEJBY0MiLCJmaWxlIjoibWFpbi5qcyIsInNvdXJjZXNDb250ZW50IjpbImltcG9ydCB7QXVyZWxpYX0gZnJvbSAnYXVyZWxpYS1mcmFtZXdvcmsnXG5pbXBvcnQgZW52aXJvbm1lbnQgZnJvbSAnLi9lbnZpcm9ubWVudCc7XG5cbmV4cG9ydCBmdW5jdGlvbiBjb25maWd1cmUoYXVyZWxpYTogQXVyZWxpYSkge1xuICBhdXJlbGlhLnVzZVxuICAgIC5zdGFuZGFyZENvbmZpZ3VyYXRpb24oKVxuICAgIC5mZWF0dXJlKCdyZXNvdXJjZXMnKTtcblxuICBpZiAoZW52aXJvbm1lbnQuZGVidWcpIHtcbiAgICBhdXJlbGlhLnVzZS5kZXZlbG9wbWVudExvZ2dpbmcoKTtcbiAgfVxuXG4gIGlmIChlbnZpcm9ubWVudC50ZXN0aW5nKSB7XG4gICAgYXVyZWxpYS51c2UucGx1Z2luKCdhdXJlbGlhLXRlc3RpbmcnKTtcbiAgfVxuXG4gIGF1cmVsaWEuc3RhcnQoKS50aGVuKCgpID0+IGF1cmVsaWEuc2V0Um9vdCgpKTtcbn1cbiJdLCJzb3VyY2VSb290Ijoic3JjIn0=

define('resources/index',["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    function configure(config) {
        config.globalResources([
            "bootstrap/css/bootstrap.css",
            "./elements/nav-bar/nav-bar"
        ]);
    }
    exports.configure = configure;
});

//# sourceMappingURL=data:application/json;charset=utf8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInJlc291cmNlcy9pbmRleC50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7SUFFQSxtQkFBMEIsTUFBOEI7UUFDdEQsTUFBTSxDQUFDLGVBQWUsQ0FBQztZQUNyQiw2QkFBNkI7WUFDN0IsNEJBQTRCO1NBQzdCLENBQUMsQ0FBQztJQUNMLENBQUM7SUFMRCw4QkFLQyIsImZpbGUiOiJyZXNvdXJjZXMvaW5kZXguanMiLCJzb3VyY2VzQ29udGVudCI6WyJpbXBvcnQge0ZyYW1ld29ya0NvbmZpZ3VyYXRpb259IGZyb20gJ2F1cmVsaWEtZnJhbWV3b3JrJztcblxuZXhwb3J0IGZ1bmN0aW9uIGNvbmZpZ3VyZShjb25maWc6IEZyYW1ld29ya0NvbmZpZ3VyYXRpb24pIHtcbiAgY29uZmlnLmdsb2JhbFJlc291cmNlcyhbXG4gICAgXCJib290c3RyYXAvY3NzL2Jvb3RzdHJhcC5jc3NcIixcbiAgICBcIi4vZWxlbWVudHMvbmF2LWJhci9uYXYtYmFyXCJcbiAgXSk7XG59XG4iXSwic291cmNlUm9vdCI6InNyYyJ9

define('services/auth-service',["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    var AuthService = (function () {
        function AuthService() {
        }
        AuthService.prototype.isLoggedIn = function () {
            return false;
        };
        AuthService.prototype.login = function () {
        };
        AuthService.prototype.logout = function () {
        };
        return AuthService;
    }());
    exports.AuthService = AuthService;
});

//# sourceMappingURL=data:application/json;charset=utf8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNlcnZpY2VzL2F1dGgtc2VydmljZS50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7SUFBQTtRQUFBO1FBYUEsQ0FBQztRQVhVLGdDQUFVLEdBQWpCO1lBQ0ksTUFBTSxDQUFDLEtBQUssQ0FBQztRQUNqQixDQUFDO1FBRU0sMkJBQUssR0FBWjtRQUVBLENBQUM7UUFFTSw0QkFBTSxHQUFiO1FBRUEsQ0FBQztRQUNMLGtCQUFDO0lBQUQsQ0FiQSxBQWFDLElBQUE7SUFiWSxrQ0FBVyIsImZpbGUiOiJzZXJ2aWNlcy9hdXRoLXNlcnZpY2UuanMiLCJzb3VyY2VzQ29udGVudCI6WyJleHBvcnQgY2xhc3MgQXV0aFNlcnZpY2Uge1xyXG5cclxuICAgIHB1YmxpYyBpc0xvZ2dlZEluKCk6IGJvb2xlYW4ge1xyXG4gICAgICAgIHJldHVybiBmYWxzZTtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgbG9naW4oKTogdm9pZCB7XHJcbiAgICAgICAgXHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGxvZ291dCgpOiB2b2lkIHtcclxuXHJcbiAgICB9XHJcbn0iXSwic291cmNlUm9vdCI6InNyYyJ9

define('pages/notes/index',["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    var Index = (function () {
        function Index() {
        }
        Index.prototype.clickMe = function () {
            alert("Hoi");
        };
        return Index;
    }());
    exports.Index = Index;
});

//# sourceMappingURL=data:application/json;charset=utf8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInBhZ2VzL25vdGVzL2luZGV4LnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7OztJQUFBO1FBQUE7UUFLQSxDQUFDO1FBSEcsdUJBQU8sR0FBUDtZQUNJLEtBQUssQ0FBQyxLQUFLLENBQUMsQ0FBQztRQUNqQixDQUFDO1FBQ0wsWUFBQztJQUFELENBTEEsQUFLQyxJQUFBO0lBTFksc0JBQUsiLCJmaWxlIjoicGFnZXMvbm90ZXMvaW5kZXguanMiLCJzb3VyY2VzQ29udGVudCI6WyJleHBvcnQgY2xhc3MgSW5kZXgge1xyXG4gICAgXHJcbiAgICBjbGlja01lKCkgOiB2b2lkIHtcclxuICAgICAgICBhbGVydChcIkhvaVwiKTtcclxuICAgIH1cclxufSJdLCJzb3VyY2VSb290Ijoic3JjIn0=

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
define('resources/elements/nav-bar/nav-bar',["require", "exports", "aurelia-framework", "aurelia-router", "../../../services/auth-service"], function (require, exports, aurelia_framework_1, aurelia_router_1, auth_service_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    var NavBar = (function () {
        function NavBar(authService) {
            this.authService = authService;
        }
        return NavBar;
    }());
    __decorate([
        aurelia_framework_1.bindable(),
        __metadata("design:type", aurelia_router_1.Router)
    ], NavBar.prototype, "router", void 0);
    NavBar = __decorate([
        aurelia_framework_1.autoinject(),
        __metadata("design:paramtypes", [auth_service_1.AuthService])
    ], NavBar);
    exports.NavBar = NavBar;
});

//# sourceMappingURL=data:application/json;charset=utf8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInJlc291cmNlcy9lbGVtZW50cy9uYXYtYmFyL25hdi1iYXIudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7Ozs7Ozs7Ozs7O0lBS0EsSUFBYSxNQUFNO1FBS2YsZ0JBQTBCLFdBQXlCO1lBQXpCLGdCQUFXLEdBQVgsV0FBVyxDQUFjO1FBQ25ELENBQUM7UUFDTCxhQUFDO0lBQUQsQ0FQQSxBQU9DLElBQUE7SUFKRztRQURDLDRCQUFRLEVBQUU7a0NBQ0ksdUJBQU07MENBQUM7SUFIYixNQUFNO1FBRGxCLDhCQUFVLEVBQUU7eUNBTStCLDBCQUFXO09BTDFDLE1BQU0sQ0FPbEI7SUFQWSx3QkFBTSIsImZpbGUiOiJyZXNvdXJjZXMvZWxlbWVudHMvbmF2LWJhci9uYXYtYmFyLmpzIiwic291cmNlc0NvbnRlbnQiOlsiaW1wb3J0IHtiaW5kYWJsZSwgYXV0b2luamVjdH0gZnJvbSBcImF1cmVsaWEtZnJhbWV3b3JrXCI7XHJcbmltcG9ydCB7Um91dGVyfSBmcm9tIFwiYXVyZWxpYS1yb3V0ZXJcIjtcclxuaW1wb3J0IHtBdXRoU2VydmljZX0gZnJvbSBcIi4uLy4uLy4uL3NlcnZpY2VzL2F1dGgtc2VydmljZVwiO1xyXG5cclxuQGF1dG9pbmplY3QoKVxyXG5leHBvcnQgY2xhc3MgTmF2QmFyIHtcclxuXHJcbiAgICBAYmluZGFibGUoKVxyXG4gICAgcHVibGljIHJvdXRlcjogUm91dGVyO1xyXG5cclxuICAgIHB1YmxpYyBjb25zdHJ1Y3RvcihwdWJsaWMgYXV0aFNlcnZpY2UgOiBBdXRoU2VydmljZSkge1xyXG4gICAgfVxyXG59Il0sInNvdXJjZVJvb3QiOiJzcmMifQ==

define('pages/todo/index',["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    var Index = (function () {
        function Index() {
        }
        return Index;
    }());
    exports.Index = Index;
});

//# sourceMappingURL=data:application/json;charset=utf8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInBhZ2VzL3RvZG8vaW5kZXgudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7O0lBQUE7UUFBQTtRQUVBLENBQUM7UUFBRCxZQUFDO0lBQUQsQ0FGQSxBQUVDLElBQUE7SUFGWSxzQkFBSyIsImZpbGUiOiJwYWdlcy90b2RvL2luZGV4LmpzIiwic291cmNlc0NvbnRlbnQiOlsiZXhwb3J0IGNsYXNzIEluZGV4IHtcclxuICAgIFxyXG59Il0sInNvdXJjZVJvb3QiOiJzcmMifQ==

define('text!app.html', ['module'], function(module) { module.exports = "<template><nav-bar router.bind=\"router\"></nav-bar><router-view></router-view></template>"; });
define('text!pages/notes/index.html', ['module'], function(module) { module.exports = "<template><button click.delegate=\"clickMe()\">Ok</button></template>"; });
define('text!resources/elements/nav-bar/nav-bar.html', ['module'], function(module) { module.exports = "<template><nav class=\"navbar navbar-default\"><div class=\"container-fluid\"><div class=\"navbar-header\"><button type=\"button\" class=\"navbar-toggle collapsed\" data-toggle=\"collapse\" data-target=\"#bs-example-navbar-collapse-1\" aria-expanded=\"false\"><span class=\"sr-only\">Toggle navigation</span> <span class=\"icon-bar\"></span> <span class=\"icon-bar\"></span> <span class=\"icon-bar\"></span></button> <a class=\"navbar-brand\" href=\"#\">Xemio</a></div><div class=\"collapse navbar-collapse\" id=\"bs-example-navbar-collapse-1\"><ul class=\"nav navbar-nav\"><li repeat.for=\"route of router.navigation\" class=\"${route.isActive ? 'active' : ''}\"><a href.bind=\"route.href\">${route.title}</a></li></ul><ul class=\"nav navbar-nav navbar-right\"><li><a href=\"#\">Link</a></li></ul></div></div></nav></template>"; });
define('text!pages/todo/index.html', ['module'], function(module) { module.exports = "<template><button>Hey</button></template>"; });
//# sourceMappingURL=app-bundle.js.map