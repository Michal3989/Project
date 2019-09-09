import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';

import { GuestsService, CommonService} from '../services';
import { GuestsForViewDto } from '../models/dto/GuestsForViewDto';
import { Router } from '@angular/router';
import { FormGroup, FormControl,Validators } from '@angular/forms';
import { GuestDto, CategoryDto } from '../models';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-guests',
  templateUrl: './guests.component.html',
  styleUrls: ['./guests.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class GuestsComponent implements OnInit {

  constructor( private guestsService: GuestsService, private commonService:CommonService,private router:Router,private _cdr: ChangeDetectorRef) {
    this.newGuest=new GuestDto();
    this.commonService.flagToChangeEvent.subscribe(data=>{
      if(this.commonService.currentEvent.id)
      {
       this.getGuestsDetails();
       this.getCategories();
      }   
    }
  )
  this.guestForm=new FormGroup({ 
    degreeBefore: new FormControl(),
    degreeAfter: new FormControl(),
    firstName: new FormControl(),
    lastName: new FormControl('',[Validators.required]),
    email: new FormControl('',[Validators.required,Validators.email]),
    categoryCode: new FormControl('',[Validators.required])
  })
  }
  guestsDetailsList:GuestsForViewDto[];
  categoriesList:CategoryDto[];
  guestForm:FormGroup;
  newGuest:GuestDto;
  

  ngOnInit() {
    // for(var i=0;i<10;i++)
    // {
    //   this.guestsDetailsList.push(new GuestsDetailsDto());
    // }
    
  }
  
  getGuestsDetails(){
    
    this.guestsService.getGuestsDetails(this.commonService.currentEvent.id).subscribe(
      (data:GuestsForViewDto[])=>{  
          this.guestsDetailsList=data;
    
          this._cdr.detectChanges();
          
      }
    )
  }

  sendInvitations(){
    
    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
      },
      buttonsStyling: false,
    })
    
    swalWithBootstrapButtons.fire({
      title: '???האם אתה בטוח רוצה לשלוח את ההזמנות',
      text: "לא תהיה אפשרות נוספת להוספת אורחים ושליחת הזמנות",
      type: 'warning',
      showCancelButton: true,
      confirmButtonText: 'כן, שלח הזמנות',
      cancelButtonText: 'לא, אל תשלח',
      reverseButtons: true
    }).then((result) => {
      if (result.value) {
        this.guestsService.sendInvitations(this.commonService.currentEvent).subscribe(
          
          (data:boolean)=>{  
             
                 if(data){
                   
                  swalWithBootstrapButtons.fire(
                    'כל המיילים נשלחו בהצלחה',
                    
                  )
                 }
                 else
                 {
                   Swal.fire({
                   
                  type: 'error',
                  title: '...אופס',
                  text: 'לא כל המיילים נשלחו',
                })
                 } 
        })
        
      } 
      else{
      }
    })




    }

  addGuest(){
    debugger;
    this.newGuest.idEvent=this.commonService.currentEvent.id;
    this.newGuest.degreeBefore=this.guestForm.controls.degreeBefore.value;
    this.newGuest.degreeAfter=this.guestForm.controls.degreeAfter.value;
    this.newGuest.firstName=this.guestForm.controls.firstName.value;
    this.newGuest.lastName=this.guestForm.controls.lastName.value;
    this.newGuest.email=this.guestForm.controls.email.value;
    this.newGuest.categoryCode=this.guestForm.controls.categoryCode.value;
    debugger;
    this.guestsService.addGuest(this.newGuest).subscribe(
      data=>{
        this.guestsDetailsList=data;
        this.guestForm.reset();
        if(data)
        {
          Swal.fire({
            type: 'success',
            title: 'אורח נוסף בהצלחה',
            showConfirmButton: false,
            timer: 1500
          })  
        }
        else
        {
          Swal.fire({
            type: 'error',
            title: 'תקלה בהוספת אורח',
            showConfirmButton: false,
            timer: 1500
          })  
        }
       
        this._cdr.detectChanges();

      }
    )
  }

  getCategories(){
  
    this.guestsService.getCategories(this.commonService.currentEvent.eventTypeCode).subscribe(
      data=>{
        this.categoriesList=data;

      }
    )
  }

  saveChanges(){
    this.guestsService.saveChanges(this.guestsDetailsList).subscribe(
      data=>{
        console.log(data);
      }
    )
  }


}
