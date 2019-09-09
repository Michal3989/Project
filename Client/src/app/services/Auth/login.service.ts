



//לא להשתמש בזה זה עוששה בעיות למחוק , אבל לא שימושי!!!!!!!!!!!!!!!!!!!!!!!!
import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';
import { BaseHttpService, BaseApiService } from "../../shared";
import { Router } from "@angular/router";
import { OwnerDto,EventDto,EventTypeDto } from "../../models"


@Injectable()
export class LoginService extends BaseApiService {
  
    public currentOwner: OwnerDto;
    public currentEvent:EventDto=new EventDto();
    public eventTypes:EventTypeDto[]; 
  
    constructor(private router: Router,private baseHttpService: BaseHttpService) {
        super('Event');     
    }
    
    setCurrentOwner(owner:OwnerDto){      
        this.currentOwner = owner;
    }
    setCurrentEvent(event:EventDto){
        this.currentEvent = event;
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

   

    signOut(){ 
         this.currentOwner=null; 
         this.router.navigateByUrl('');
       
    }

    
}

