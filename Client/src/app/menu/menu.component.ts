import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import {EventDetailsComponent} from '../event-details/event-details.component'
import {TabMenuModule} from 'primeng/tabmenu';
import {MenuItem} from 'primeng/api';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  // items: MenuItem[];
  // activeItem: MenuItem;

  //componentData:any;
  constructor(private router:Router) { }

  ngOnInit() {
    //    this.items =
    //     [
    //     {label: 'פרטי אירוע', icon: 'fa fa-fw fa-book',routerLink:"/new-event"},
    //     {label: 'מוזמנים', icon: 'fa fa-fw fa-calendar',routerLink:"/guests"},
    //     {label: 'סידור שולחנות', icon: 'fa fa-fw fa-book',routerLink:"/seats"},
    //     {label: 'מראה מקומות', icon: 'fa fa-fw fa-support',routerLink:"/show-places"},
    //     {label: 'דף אישור הגעה', icon: 'fa fa-fw fa-twitter',routerLink:"/guest-page"}
    //     ];
  
    // this.activeItem = this.items[0];
}
   
  

}
// export class TabMenuDemo {
    
//   items: MenuItem[];
  
//   activeItem: MenuItem;

//   // ngOnInit() {
//   //     this.items = [
//   //         {label: 'Stats', icon: 'fa fa-fw fa-bar-chart'},
//   //         {label: 'Calendar', icon: 'fa fa-fw fa-calendar'},
//   //         {label: 'Documentation', icon: 'fa fa-fw fa-book'},
//   //         {label: 'Support', icon: 'fa fa-fw fa-support'},
//   //         {label: 'Social', icon: 'fa fa-fw fa-twitter'}
//   //     ];
      
//   //     this.activeItem = this.items[2];
//   // }
// }