import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import {  TableService, CommonService } from '../services';
import { TypeOfTableDto } from '../models';

@Component({
  selector: 'app-seats',
  templateUrl: './seats.component.html',
  styleUrls: ['./seats.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class SeatsComponent implements OnInit {  
  tableTypeList:TypeOfTableDto[]=[];
  newTable:TypeOfTableDto;
  gender:boolean;
  constructor(private commonService: CommonService,private tableService:TableService) { 
    this.newTable=new TypeOfTableDto(this.commonService.currentEvent.id,0,0,true);
    this.tableTypeList.push(this.newTable);
    this.newTable=new TypeOfTableDto(this.commonService.currentEvent.id,0,0,false);
    this.tableTypeList.push(this.newTable);
  }
  ngOnInit() {
    
   
  }
  addTableType(_gender:string){
    _gender=='male'?this.gender=true:this.gender=false;
    this.newTable=new TypeOfTableDto(this.commonService.currentEvent.id,0,0,this.gender);
   this.tableTypeList.push(this.newTable);
  }
  saveTables(){
    
    this.tableService.saveTables(this.tableTypeList).subscribe(
    (data:boolean)=>{
      alert(data);
    }
  )}

  setSeating(){
    this.tableService.setSeating(this.commonService.currentEvent.id).subscribe(
      (data:boolean)=>{
        alert(data);
        
      }
    );
  }
}
