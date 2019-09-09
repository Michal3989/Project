import { NgModule } from "@angular/core";
import { /*MAIN_SERVICES,*/ AUTH_SERVICES } from "./Auth";

const SERVICES =[
    //...MAIN_SERVICES,
    ...AUTH_SERVICES
]
@NgModule({
    providers: [...SERVICES]
})
export class ServicesModule { }

