import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { Router } from "@angular/router";
import {OwnerDto, EventDto} from '../models';
import {  EventService, CommonService } from '../services';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RegistrationComponent implements OnInit {
  newOwner:OwnerDto=new OwnerDto();
  newEvent:EventDto=new EventDto();
  constructor(private router: Router,private eventService:EventService, private commonService:CommonService) {
    this.registrationForm=new FormGroup({ 
      firstName: new FormControl('',[Validators.required]),
      lastName: new FormControl('',[Validators.required]),
      phone: new FormControl('',[Validators.required]),
      email: new FormControl('',[Validators.required,Validators.email]),
      password: new FormControl('',[Validators.required,Validators.minLength(4)]),
      confirmPassword: new FormControl('')
    })
  }
  registrationForm:FormGroup;

  ngOnInit() {
  }


  saveOwnerDetails(){
    this.newOwner.firstName=this.registrationForm.controls.firstName.value;
    this.newOwner.lastName=this.registrationForm.controls.lastName.value;
    this.newOwner.phone=this.registrationForm.controls.phone.value;
    this.newOwner.email=this.registrationForm.controls.email.value;
    this.newOwner.password=this.registrationForm.controls.phone.value;
    this.eventService.saveOwnerDetails(this.newOwner).subscribe(
      (data:number)=>{      
        alert("id new owner:"+data);
        this.newOwner.id=data;
        this.commonService.setCurrentOwner(this.newOwner);
        this.router.navigateByUrl("/eventDetails");
        });
    }
    
  
}
