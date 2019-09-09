import { Injectable } from "@angular/core";
import 'rxjs/add/operator/map';
import { BaseHttpService, BaseApiService } from "../../shared";
import { Router } from "@angular/router";
import { OwnerDto,EventDto,EventTypeDto } from "../../models";
import {Subject} from 'rxjs/Subject';


@Injectable()
export class CommonService extends BaseApiService {
  
    public currentOwner: OwnerDto;
    public currentEvent:EventDto=new EventDto();
    public eventTypes:EventTypeDto[]; 
    public isLogin:boolean;  
    public flagToChangeEvent:Subject<boolean>;
    
    constructor(private router: Router,private baseHttpService: BaseHttpService) {      
        super('Event'); 
        this.flagToChangeEvent=new Subject<boolean>();    
    }
    
    setCurrentOwner(owner:OwnerDto){      
        this.currentOwner = owner;
        this.isLogin=true;
    }
    setCurrentEvent(event:EventDto){
        this.currentEvent = event;
    }

   signOut(){ 
         this.currentOwner=null; 
         this.router.navigateByUrl('');
       
    }

   

    

    
}

