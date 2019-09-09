import { Component, OnInit,ChangeDetectionStrategy,ChangeDetectorRef } from '@angular/core';
import { TablesWithGuestsDto } from '../models';
import {  TableService, CommonService } from '../services';

@Component({
  selector: 'app-shows-places',
  templateUrl: './shows-places.component.html',
  styleUrls: ['./shows-places.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ShowsPlacesComponent implements OnInit {

  tablesWithGuests:TablesWithGuestsDto[];
  constructor(private commonService: CommonService,private tableService:TableService,private _cdr: ChangeDetectorRef) {


    this.commonService.flagToChangeEvent.subscribe(data=>{
      if(this.commonService.currentEvent.id)
      {
        this.getTablesWithGuests(this.commonService.currentEvent.id);
      }
      
  }
    )
   }

  ngOnInit() {
   
   }
   
    getTablesWithGuests(eventId:number){

      this.tableService.getTablesWithGuests(this.commonService.currentEvent.id).subscribe(
        (data:TablesWithGuestsDto[])=>{
          this.tablesWithGuests=data; 

          this._cdr.detectChanges();
         
        }
      )}
  

}
