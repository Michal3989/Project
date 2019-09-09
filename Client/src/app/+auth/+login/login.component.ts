import { Component , OnInit, ViewChild, AfterViewInit} from '@angular/core';
import {MenuItem} from "primeng/primeng";
import {Menu} from "primeng/components/menu/menu";
import { LoginService } from '../../services';
import { Router } from "@angular/router";
import {UserDto } from  "../../models"
import { OwnerDto } from '../../models/dto/ownerDto';
declare var jQuery :any;

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
  //styleUrls: ['../../main.component.scss']
})
export class LoginComponent implements OnInit, AfterViewInit {
  menuItems: MenuItem[];
  miniMenuItems: MenuItem[];
  
  @ViewChild('bigMenu') bigMenu : Menu;
  @ViewChild('smallMenu') smallMenu : Menu;

  email: string;
  password: string;
  loginSucceed :boolean;
  currentOwner:OwnerDto;

  constructor(private router: Router, private loginService: LoginService) {

  }

  ngOnInit() {

    let handleSelected = function(event) {
      let allMenus = jQuery(event.originalEvent.target).closest('ul');
      let allLinks = allMenus.find('.menu-selected');

      allLinks.removeClass("menu-selected");
      let selected = jQuery(event.originalEvent.target).closest('a');
      selected.addClass('menu-selected');
    }

    this.menuItems = [
    ]

    this.miniMenuItems = [];
    this.menuItems.forEach( (item : MenuItem) => {
      let miniItem = { icon: item.icon, routerLink: item.routerLink }
      this.miniMenuItems.push(miniItem);
    })

  }

  selectInitialMenuItemBasedOnUrl() {
    let path = document.location.pathname;
    let menuItem = this.menuItems.find( (item) => { return item.routerLink[0] == path });
    if (menuItem) {
      let selectedIcon = this.bigMenu.container.querySelector(`.${menuItem.icon}`);
      jQuery(selectedIcon).closest('li').addClass('menu-selected');
    }
  }

  ngAfterViewInit() {
    this.selectInitialMenuItemBasedOnUrl();
  }

  login() {

    this.loginService.login(this.email, this.password).subscribe(
      (data: OwnerDto) => {
        this.currentOwner = data; 
        this.loginService.setCurrentOwner(data);
        this.loginSucceed = this.currentOwner.isAuthorized;    
        if (this.currentOwner.isAuthorized) {
            this.router.navigateByUrl("/main");
        }
      },
      fail => alert("User not found")); 
  }

  newOwner(){
    //אמורה לשנות ניתוב 
  }
  signOut(){
    this.loginService.signOut();
  }
}
