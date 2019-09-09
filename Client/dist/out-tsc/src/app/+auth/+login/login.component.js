// import * as tslib_1 from "tslib";
// import { Component, ViewChild } from '@angular/core';
// import { Menu } from "primeng/components/menu/menu";
// import { LoginService } from '../../services';
// import { Router } from "@angular/router";
// var LoginComponent = /** @class */ (function () {
//     function LoginComponent(router, loginService) {
//         this.router = router;
//         this.loginService = loginService;
//         this.loginSucceed = true;
//     }
//     LoginComponent.prototype.ngOnInit = function () {
//         var _this = this;
//         var handleSelected = function (event) {
//             var allMenus = jQuery(event.originalEvent.target).closest('ul');
//             var allLinks = allMenus.find('.menu-selected');
//             allLinks.removeClass("menu-selected");
//             var selected = jQuery(event.originalEvent.target).closest('a');
//             selected.addClass('menu-selected');
//         };
//         this.menuItems = [];
//         this.miniMenuItems = [];
//         this.menuItems.forEach(function (item) {
//             var miniItem = { icon: item.icon, routerLink: item.routerLink };
//             _this.miniMenuItems.push(miniItem);
//         });
//     };
//     LoginComponent.prototype.selectInitialMenuItemBasedOnUrl = function () {
//         var path = document.location.pathname;
//         var menuItem = this.menuItems.find(function (item) { return item.routerLink[0] == path; });
//         if (menuItem) {
//             var selectedIcon = this.bigMenu.container.querySelector("." + menuItem.icon);
//             jQuery(selectedIcon).closest('li').addClass('menu-selected');
//         }
//     };
//     LoginComponent.prototype.ngAfterViewInit = function () {
//         this.selectInitialMenuItemBasedOnUrl();
//     };
//     LoginComponent.prototype.login = function () {
//         var _this = this;
//         this.loginService.login(this.userName, this.password).subscribe(function (data) {
//             _this.currentUser = data;
//             _this.loginService.setCurrentUser(data);
//             _this.loginSucceed = _this.currentUser.isAuthorized;
//             if (_this.currentUser.isAuthorized) {
//                 _this.router.navigateByUrl("/main");
//             }
//         }, function (fail) { return alert("User not found"); });
//     };
//     LoginComponent.prototype.signOut = function () {
//         this.loginService.signOut();
//     };
//     tslib_1.__decorate([
//         ViewChild('bigMenu'),
//         tslib_1.__metadata("design:type", Menu)
//     ], LoginComponent.prototype, "bigMenu", void 0);
//     tslib_1.__decorate([
//         ViewChild('smallMenu'),
//         tslib_1.__metadata("design:type", Menu)
//     ], LoginComponent.prototype, "smallMenu", void 0);
//     LoginComponent = tslib_1.__decorate([
//         Component({
//             selector: 'login',
//             templateUrl: './login.component.html',
//             styleUrls: ['./login.component.scss']
//             //styleUrls: ['../../main.component.scss']
//         }),
//         tslib_1.__metadata("design:paramtypes", [Router, LoginService])
//     ], LoginComponent);
//     return LoginComponent;
// }());
// export { LoginComponent };
// //# sourceMappingURL=login.component.js.map