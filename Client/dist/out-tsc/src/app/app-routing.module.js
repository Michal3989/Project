import * as tslib_1 from "tslib";
import { NgModule } from "@angular/core";
import { RouterModule } from '@angular/router';
import { AppComponent } from "./app.component";
import { MAIN_ROUTES } from "./+main";
import { AUTH_ROUTES } from "./+auth";
var APP_ROUTES = [
    {
        path: 'home',
        component: AppComponent,
    }
].concat(AUTH_ROUTES, MAIN_ROUTES);
var AppRoutingModule = /** @class */ (function () {
    function AppRoutingModule() {
    }
    AppRoutingModule = tslib_1.__decorate([
        NgModule({
            imports: [
                RouterModule.forRoot(APP_ROUTES /*, { useHash: true }*/)
            ],
            exports: [
                RouterModule
            ]
        })
    ], AppRoutingModule);
    return AppRoutingModule;
}());
export { AppRoutingModule };
//# sourceMappingURL=app-routing.module.js.map