import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import {  EventService, CommonService } from '../services';
import { Router } from "@angular/router";
import { OwnerDto } from '../models';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import Swal from 'sweetalert2'


@Component({
  selector: 'app-new-login',
  templateUrl: './new-login.component.html',
  styleUrls: ['./new-login.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NewLoginComponent implements OnInit {
  email: string;
  password: string;
  loginSucceed: boolean;
  loginForm:FormGroup

  constructor(private router: Router, private commonService: CommonService,private eventService:EventService) {
    this.loginForm=new FormGroup({ 
      email: new FormControl('',[Validators.required, Validators.email]),
      password: new FormControl('',[Validators.required,Validators.minLength(4)]),
    })
  }
  ngOnInit() {
    // this.email="michal3989@gmail.com";
    // this.password="5463";
  }

   login() {
     this.eventService.login(this.loginForm.controls.email.value,this.loginForm.controls.password.value).subscribe((data: OwnerDto) => {
      this.commonService.setCurrentOwner(data);
      this.loginSucceed = this.commonService.currentOwner.isAuthorized;

      if (this.commonService.currentOwner.isAuthorized) {
       this.router.navigateByUrl("/menu");
      }
      else{
      Swal.fire({
        type: 'error',
        title: '!אופס',
        text: 'שם משתמש או סיסמא שגויים',
      })}
        
    },);
  
}
 

  signOut() {
    this.commonService.signOut();
  }
  
}