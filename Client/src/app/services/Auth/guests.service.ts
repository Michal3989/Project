import { Injectable } from "@angular/core";

import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';

import { BaseHttpService, BaseApiService } from "../../shared";

import { GuestsForViewDto } from '../../models/dto/GuestsForViewDto';
import { EventDto,GuestDto, CategoryDto } from '../../models';

import { HttpClient } from "@angular/common/http";
// import {URLSearchParams} from '@angular/http';


@Injectable()
export class GuestsService extends BaseApiService {

    constructor( private baseHttpService: BaseHttpService,private http:HttpClient) {
        super('Guest');
      }
     // params:any[];
    getGuestsDetails(eventId:number):Observable<GuestsForViewDto[]>{
       let url = this.actionUrl('GetGuestsDetails');
       return this.baseHttpService.get<GuestsForViewDto[]>(url,eventId);
     }
   

    sendInvitations(myEvent:EventDto):Observable<boolean>{
    let url = this.actionUrl('SendInvitationsToGuests');
       return this.baseHttpService.post<boolean>(url,myEvent);
     }
     confirm(guestId:number,numOfMale:number,numOfFemale:number):Observable<number>{

      let url = this.actionUrl('Confirm');
      let params: URLSearchParams = new URLSearchParams();
      params.set('guestId',guestId.toString());
      params.set('numOfMale',numOfMale.toString());
      params.set('numOfFemale',numOfFemale.toString());
      return this.baseHttpService.get<number>(url,params);
     }

     uploadGuestFile(formData:FormData):Observable<boolean>{
  
      let url = this.actionUrl('UploadGuestFile');
     return this.http.post<boolean>(url, formData);
     }
     getEventIdByGuestId(guestId:number):Observable<string>{
     
      let url = this.actionUrl('GetEventPictureByGuestId');
      return this.baseHttpService.get<string>(url,guestId);
     }

    
     addGuest(newGuest:GuestDto):Observable<GuestsForViewDto[]>{
      let url = this.actionUrl('PostGuest');
      return this.baseHttpService.post<GuestsForViewDto[]>(url,newGuest);
     }
     
     getCategories(eventTypeId:number):Observable<CategoryDto[]>{
      let url = this.actionUrl('GetCategories');
      return this.baseHttpService.get<CategoryDto[]>(url,eventTypeId);
     }

     saveChanges(guestList:GuestsForViewDto[]):Observable<number>{
       debugger;
      let url = this.actionUrl('UpdateGuests');
      return this.baseHttpService.post<number>(url,guestList);
     }
     }
