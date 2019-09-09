import { Component } from '@angular/core';
import { CommonService } from './services';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  //styleUrls: ['./app.component.css'],
})
export class AppComponent {
  constructor(private commonService:CommonService){

  }
  isLogin(){
if(this.commonService.isLogin==true)
return true;
else
return false;

  }
}
