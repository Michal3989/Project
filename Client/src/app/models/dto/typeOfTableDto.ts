export class TypeOfTableDto{
    
id:number;
eventId:number;
amount:number;
numOfPeople:number;
male:boolean;
title:string;

constructor(_eventId:number,_amount:number,_numOfPeople:number,_male:boolean){
        this.eventId=_eventId;
        this.amount=_amount;
        this.numOfPeople=_numOfPeople;
        this.male=_male;
    }
}