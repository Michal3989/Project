import { Injectable, } from "@angular/core";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';

import { BaseHttpService, BaseApiService } from "../../shared";
import { Router } from "@angular/router";
import { EventDto ,EventTypeDto, OwnerDto} from "../../models"
import { LoginService } from './login.service';
import { CommonService } from './common.service';

import { HttpClient } from "@angular/common/http";



@Injectable()
export class EventService extends BaseApiService {

    constructor(private router: Router, private baseHttpService: BaseHttpService,private commonService:CommonService, private http:HttpClient) {
        super('Event');
      }
  
      login( email: string,  password: string) : Observable<OwnerDto>
    {        
        
        let url = this.actionUrl('Login');
        let params: URLSearchParams = new URLSearchParams();
        if (typeof email === "undefined" || typeof password === "undefined") // עדיף לא לשלוח בכלל לשרת. יש לטפל בobservable במקרה כזה
        {
            email = "";
            password =  "";
        }
        params.set('email', email);
        params.set('password', password);

        return this.baseHttpService.get<OwnerDto>(url, params);
    }

      saveOwnerDetails(eventOwnerDetails:OwnerDto):Observable<number>{
        let url = this.actionUrl('AddOwner');
        return this.baseHttpService.post<number>(url,eventOwnerDetails);
      }
       
   
    getEventsList(ownerId:number):Observable<EventDto[]>{
        let url=this.actionUrl('GetEvents');
        return this.baseHttpService.get<EventDto[]>(url,ownerId);
    }

    convertEventCodeToEventDescription(typeCode:number):string{
        for(let i in this.commonService.eventTypes)
        {
          if(this.commonService.eventTypes[i].id==typeCode)
         {   
         return this.commonService.eventTypes[i].description;
         }
        }       
    }

    changeEvent(eventId:number)
    {
        let url=this.actionUrl('GetSelectedEvent');
        return this.baseHttpService.get<EventDto>(url,eventId);       
    }
    
    getDefaultEvent(ownerId:number):Observable<EventDto>{
        let url=this.actionUrl('GetDefaultEvent');
        return this.baseHttpService.get<EventDto>(url,ownerId);
    }

    getEventTypes():Observable<EventTypeDto[]>{
        let url = this.actionUrl('GetEventTypes');
        return this.baseHttpService.get<EventTypeDto[]>(url)
     }

    saveNewEvent(eventToSave:EventDto,frmData:FormData):Observable<number>{   

       let eventString=JSON.stringify(eventToSave)
       let url = "http://localhost:52718/api/Event/PostEvent";

       
       return this.http.post<number>(url+"?stringEvent="+eventString,frmData)
    }

    updateEvent(eventToSave:EventDto,frmData:FormData):Observable<boolean>{   
        let eventString=JSON.stringify(eventToSave)
        let url = "http://localhost:52718/api/Event/PutEvent";
        return this.http.put<boolean>(url+"?stringEvent="+eventString,frmData);
    }

    upload(frmData:FormData):Observable<boolean>{
        let url = this.actionUrl('UploadFiles');
 
        return this.http.post<boolean>(url,frmData);
    }

}

