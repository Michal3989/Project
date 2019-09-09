import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { Router } from "@angular/router";
import { EventTypeDto,EventDto } from "../models"
import {EventService} from "../services/Auth/event.service"
import {  CommonService } from '../services';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import Swal from 'sweetalert2'
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-event-details',
  templateUrl: './event-details.component.html',
  styleUrls: ['./event-details.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EventDetailsComponent implements OnInit {

  flagFile:boolean;//ידלק כשיבחר קובץ חדש
  eventList:EventDto[];  
  eventDetailsForm:FormGroup;

  url:any;
  uploadForm: FormGroup;
  myFile: File;
  imageSrc: string;
  
  constructor(private datePipe: DatePipe,private router:Router,private _cdr: ChangeDetectorRef, private eventService: EventService, private commonService:CommonService, private formBuilder: FormBuilder) {
    //this.commonService.currentEvent.id=1;
    this.flagFile=false;

    this.setFormGroupsValues();
  }

   async ngOnInit() {

    this.commonService.flagToChangeEvent.next(true);
    this.uploadForm = this.formBuilder.group({
      profile: ['']
    });

     //רשימת אירועים של המשתמש:
    let _eventList:EventDto[] = await this.eventService.getEventsList(this.commonService.currentOwner.id).toPromise();
    this.eventList=_eventList;
    //סוגי אירועים מהשרת:
    let eventTypeList:EventTypeDto[] = await this.eventService.getEventTypes().toPromise();
    this.commonService.eventTypes=eventTypeList;
    //אירוע דיפולטיבי להצגה:
    let currentEvent:EventDto = await this.eventService.getDefaultEvent(this.commonService.currentOwner.id).toPromise();
    this.commonService.setCurrentEvent(currentEvent);
    this.commonService.flagToChangeEvent.next(true);
    //ריענון המסך :
    this.setFormGroupsValues();
    this._cdr.detectChanges();
  }



// אישור לאחר שינוי פרטי אירוע ששולח או להוספת אירוע או לעדכון אירוע קיים
  confirmEvent(){
    this.commonService.currentEvent.idEventOwner=this.commonService.currentOwner.id;
    this.commonService.currentEvent.eventTypeCode=this.eventDetailsForm.controls.eventTypeCode.value;
    this.commonService.currentEvent.date=this.eventDetailsForm.controls.date.value;
    this.commonService.currentEvent.name=this.eventDetailsForm.controls.name.value;
    this.commonService.currentEvent.freeText=this.eventDetailsForm.controls.freeText.value;
    debugger;
    if(this.commonService.currentEvent.id>=0)
    {
      this.updateEvent();
    }
    else
    {
      this.saveNewEvent();
    }
  }
//הוספת אירוע חדש:
   saveNewEvent(){        
        //שמירת הקובץ
        let frmData = new FormData();
        frmData.append("img", this.uploadForm.get('profile').value);

        this.eventService.saveNewEvent( this.commonService.currentEvent,frmData).subscribe(
        async  (data:number)=>{     
          this.commonService.currentEvent.id=data;
          //עידכון רשימת האירועים לאחר הוספת אירוע
          let _eventList:EventDto[] = await this.eventService.getEventsList(this.commonService.currentOwner.id).toPromise();
          this.eventList=_eventList;
          if(data!=-1)
          {
          Swal.fire({
            type: 'success',
            title: 'האירוע נוסף בהצלחה',
            showConfirmButton: false,
            timer: 1500
          })
          }
          else
         {
          Swal.fire({
            type: 'error',
            title: 'תקלה בהוספת אירוע, נסה שוב',
            showConfirmButton: false,
            timer: 1500
          })
         }
          this._cdr.detectChanges();       
      }
    )      
  }
//עידכון אירוע קיים
  updateEvent(){
       //שמירת הקובץ
       let frmData = new FormData();
       frmData.append("img", this.uploadForm.get('profile').value);
       this.url=null;
      debugger;
       this.eventService.updateEvent(this.commonService.currentEvent,frmData).subscribe(
       async (data:boolean)=>{
         
        if(data==true)
          {
          Swal.fire({
            type: 'success',
            title: 'האירוע עודכן בהצלחה',
            showConfirmButton: false,
            timer: 1500
          })
        }
        else
        {
          Swal.fire({
            type: 'error',
            title: 'תקלה בעדכון אירוע, נסה שוב',
            showConfirmButton: false,
            timer: 1500
          })
        }

        //עידכון רשימת האירועים לאחר עדכון האירוע
        let _eventList:EventDto[] = await this.eventService.getEventsList(this.commonService.currentOwner.id).toPromise();
        this.eventList=_eventList;
      debugger;
        this._cdr.detectChanges();
      }
    )
  }
 //הצגת אירוע אחר ע"פ בחירת המשתמש:
  changeEvent(_event:EventDto){
    this.eventService.changeEvent(_event.id).subscribe(
      (data:EventDto)=>{
        this.commonService.currentEvent=data;
        this.setFormGroupsValues();
        this._cdr.detectChanges();
        this.commonService.flagToChangeEvent.next(true);
      }
    )
  }   
  //ריקון פרטי האירוע הקודם
  createNewEvent(){
    this.commonService.setCurrentEvent(new EventDto());
    this.setFormGroupsValues();
    this.myFile=null;
    this.uploadForm.get('profile').setValue(null);
    this.url=null;
    this._cdr.detectChanges();
  }
  //השמת ערכים בפקדים בסמך
  setFormGroupsValues(){
    this.eventDetailsForm=new FormGroup({ 
      eventTypeCode: new FormControl(this.commonService.currentEvent.eventTypeCode,[Validators.required]),
      date: new FormControl(this.datePipe.transform(this.commonService.currentEvent.date,"yyyy-MM-dd"),[Validators.required]),
      name: new FormControl(this.commonService.currentEvent.name,[Validators.required]),
      freeText: new FormControl(this.commonService.currentEvent.freeText)
    })
  }
//פונ' עזר:
  convertEventCodeToEventDescription(typeCode:number):string{
    for(let i in this.commonService.eventTypes)
    {
      if(this.commonService.eventTypes[i].id==typeCode)
     {   
     return this.commonService.eventTypes[i].description;
     }
    }
    
}

// save() {
//   let frmData = new FormData();
//   frmData.append("img", this.uploadForm.get('profile').value);
//   this.eventService.upload(frmData).subscribe(
//     (data:boolean)=>{
//     }
//   )
// }
//העלאת קובץ
changeFile(event) {
  this.flagFile=true;
  if (event.target.files.length === 0)
  return;
  this.myFile = event.target.files[0];
  this.uploadForm.get('profile').setValue(this.myFile);
debugger;
  var reader = new FileReader();
  reader.readAsDataURL(this.myFile);
  reader.onload = (_event) => {
    this.url = reader.result;
    this._cdr.detectChanges();
}

  
}

}
