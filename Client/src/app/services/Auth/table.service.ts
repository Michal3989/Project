import { Injectable } from "@angular/core";

import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';
import {TypeOfTableDto,TablesWithGuestsDto } from "../../models"
import { BaseHttpService, BaseApiService } from "../../shared";



@Injectable()
export class TableService extends BaseApiService {

    constructor( private baseHttpService: BaseHttpService) {
        super('Table');
      }
     // params:any[];
 
     saveTables(tables:TypeOfTableDto[]):Observable<boolean>{
 
        let url = this.actionUrl('FillTablesDetails');
        return this.baseHttpService.post<boolean>(url,tables);
     }

     setSeating(eventId:number):Observable<boolean>{
    
       let url = this.actionUrl('SetSeating');
       return this.baseHttpService.get<boolean>(url,eventId);
    }
    getTablesWithGuests(eventId:number):Observable<TablesWithGuestsDto[]>{

        let url=this.actionUrl('GetTablesWithGuests');
        return this.baseHttpService.get<TablesWithGuestsDto[]>(url,eventId);
    }
}
