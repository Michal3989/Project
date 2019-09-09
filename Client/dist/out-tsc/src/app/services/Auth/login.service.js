import * as tslib_1 from "tslib";
import { Injectable } from "@angular/core";
import 'rxjs/add/operator/map';
import { BaseHttpService, BaseApiService } from "../../shared";
import { Router } from "@angular/router";
var LoginService = /** @class */ (function (_super) {
    tslib_1.__extends(LoginService, _super);
    function LoginService(router, baseHttpService) {
        var _this = _super.call(this, 'User') || this;
        _this.router = router;
        _this.baseHttpService = baseHttpService;
        return _this;
        // this.userName='aaa';
    }
    LoginService.prototype.getUserName = function () {
        //debugger;
        return this.currentUser.userName;
    };
    LoginService.prototype.setCurrentUser = function (user) {
        this.currentUser = user;
    };
    LoginService.prototype.login = function (userName, password) {
        this.userName = userName;
        return this.signIn(userName, password);
    };
    LoginService.prototype.signIn = function (userName, password) {
        var url = this.actionUrl('Login');
        var params = new URLSearchParams();
        if (typeof userName === "undefined" || typeof password === "undefined") {
            userName = "";
            password = "";
        }
        params.set('userName', userName);
        params.set('password', password);
        return this.baseHttpService.get(url, params);
    };
    LoginService.prototype.signOut = function () {
        this.currentUser = null;
        this.router.navigateByUrl('');
    };
    LoginService.prototype.setPage = function () {
        this.userName = 'bbb';
        this.page = PagesRouter.AbsorptionOfACandidate_viewing;
        //this.page=page;
        //true 3 p-1/2
        //false 1-1
    };
    LoginService.prototype.getPage = function () {
        //debugger;
        return this.page.valueOf();
    };
    LoginService = tslib_1.__decorate([
        Injectable(),
        tslib_1.__metadata("design:paramtypes", [Router, BaseHttpService])
    ], LoginService);
    return LoginService;
}(BaseApiService));
export { LoginService };
export var PagesRouter;
(function (PagesRouter) {
    PagesRouter[PagesRouter["AbsorptionOfACandidate_viewing"] = 1] = "AbsorptionOfACandidate_viewing";
    PagesRouter[PagesRouter["AbsorptionOfACandidate_editing"] = 2] = "AbsorptionOfACandidate_editing";
    PagesRouter[PagesRouter["ManageOffers_viewing"] = 3] = "ManageOffers_viewing";
    PagesRouter[PagesRouter["ManageOffers_editing"] = 4] = "ManageOffers_editing";
    PagesRouter[PagesRouter["FindingCandidates_viewing"] = 5] = "FindingCandidates_viewing";
})(PagesRouter || (PagesRouter = {}));
//# sourceMappingURL=login.service.js.map