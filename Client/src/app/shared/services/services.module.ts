import { NgModule } from "@angular/core";
import { BaseHttpService } from './base-http.service';

export const SHARED_SERVICES=[
    BaseHttpService
]

@NgModule({
    providers: [...SHARED_SERVICES]
})
export class SharedServicesModule { }
