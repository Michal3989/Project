import * as tslib_1 from "tslib";
import { Component, ViewChild } from '@angular/core';
import { Menu } from "primeng/components/menu/menu";
import { Router } from "@angular/router";
import { LoginService } from '../services';
var MainComponent = /** @class */ (function () {
    function MainComponent(router, serviceLogin) {
        this.router = router;
        this.serviceLogin = serviceLogin;
    }
    MainComponent.prototype.ngOnInit = function () {
        var _this = this;
        var handleSelected = function (event) {
            var allMenus = jQuery(event.originalEvent.target).closest('ul');
            var allLinks = allMenus.find('.menu-selected');
            allLinks.removeClass("menu-selected");
            var selected = jQuery(event.originalEvent.target).closest('a');
            selected.addClass('menu-selected');
        };
        this.menuItems = [];
        this.miniMenuItems = [];
        this.menuItems.forEach(function (item) {
            var miniItem = { icon: item.icon, routerLink: item.routerLink };
            _this.miniMenuItems.push(miniItem);
        });
    };
    MainComponent.prototype.selectInitialMenuItemBasedOnUrl = function () {
        var path = document.location.pathname;
        var menuItem = this.menuItems.find(function (item) { return item.routerLink[0] == path; });
        if (menuItem) {
            var selectedIcon = this.bigMenu.container.querySelector("." + menuItem.icon);
            jQuery(selectedIcon).closest('li').addClass('menu-selected');
        }
    };
    MainComponent.prototype.ngAfterViewInit = function () {
        this.selectInitialMenuItemBasedOnUrl();
    };
    tslib_1.__decorate([
        ViewChild('bigMenu'),
        tslib_1.__metadata("design:type", Menu)
    ], MainComponent.prototype, "bigMenu", void 0);
    tslib_1.__decorate([
        ViewChild('smallMenu'),
        tslib_1.__metadata("design:type", Menu)
    ], MainComponent.prototype, "smallMenu", void 0);
    MainComponent = tslib_1.__decorate([
        Component({
            selector: 'main',
            templateUrl: './main.component.html',
            styleUrls: ['./main.component.scss']
        }),
        tslib_1.__metadata("design:paramtypes", [Router, LoginService])
    ], MainComponent);
    return MainComponent;
}());
export { MainComponent };
//# sourceMappingURL=main.component.js.map