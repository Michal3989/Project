import * as tslib_1 from "tslib";
import { NgModule } from "@angular/core";
import { /*MAIN_SERVICES,*/ AUTH_SERVICES } from "./Auth";
var SERVICES = AUTH_SERVICES.slice();
var ServicesModule = /** @class */ (function () {
    function ServicesModule() {
    }
    ServicesModule = tslib_1.__decorate([
        NgModule({
            providers: SERVICES.slice()
        })
    ], ServicesModule);
    return ServicesModule;
}());
export { ServicesModule };
//# sourceMappingURL=services.module.js.map