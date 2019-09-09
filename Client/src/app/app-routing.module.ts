import { NgModule } from "@angular/core";
import { Routes, RouterModule } from '@angular/router';


import { AppComponent } from "./app.component";

import { MAIN_ROUTES } from "./+main";
import {AUTH_ROUTES} from "./+auth";
import { RegistrationComponent } from './registration/registration.component';
import { EventDetailsComponent } from './event-details/event-details.component';
import { NewLoginComponent } from './new-login/new-login.component';
import { GuestsComponent } from './guests/guests.component';
import { GuestPageComponent } from './guest-page/guest-page.component';
import { SeatsComponent } from './seats/seats.component';
import { MenuComponent } from './menu/menu.component';
import { ShowsPlacesComponent } from './shows-places/shows-places.component';

const APP_ROUTES: Routes = [
    {path:'home',component:AppComponent},
    {path:'new-user',component:RegistrationComponent},
    {path:'new-event',component:EventDetailsComponent},
    {path: '',component:NewLoginComponent},
    {path:'eventDetails', component:EventDetailsComponent},
    {path:'seats', component:SeatsComponent},
    {path:'guests', component:GuestsComponent},
    {path:'menu', component:MenuComponent},
    {path:'guest-page', component:GuestPageComponent},
    {path:'show-places', component:ShowsPlacesComponent},
  

    ...AUTH_ROUTES, 
    ...MAIN_ROUTES    
];


@NgModule({
    imports: [
        RouterModule.forRoot(APP_ROUTES/*, { useHash: true }*/)
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule { }
