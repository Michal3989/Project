import { Component, OnInit} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {  GuestsService } from '../services';


@Component({
  selector: 'app-guest-page',
  templateUrl: './guest-page.component.html',
  styleUrls: ['./guest-page.component.css'],
})
export class GuestPageComponent implements OnInit {

  numOfMale=0;
  numOfFemale=0;
  guestId=-1;
  eventPicture:string;

  constructor(private route: ActivatedRoute,private guestsService:GuestsService) { 

    this.route.queryParams.subscribe(params => {
      this.guestId = params['idGuest'];
      this.getEventIdByGuestId();
 
  });

  }
 
  ngOnInit() {
  
  }
  confirm(){
    
    this.guestsService.confirm(this.guestId,this.numOfMale,this.numOfFemale).subscribe(
      data=>{
        alert(data);
      }
    )
  }
  getEventIdByGuestId(){
  
    this.guestsService.getEventIdByGuestId(this.guestId).subscribe(
      data=>{
        this.eventPicture=data;
      
      }
    )
   }

}



