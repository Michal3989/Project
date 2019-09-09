import { Routes } from '@angular/router';
import { AuthComponent } from './auth.component';
import { LOGIN_COMPONENTS, LoginComponent } from './+login';


export * from './+login';


export const AUTH_COMPONENTS =[
    AuthComponent,
    ...LOGIN_COMPONENTS
]

export const AUTH_ROUTES: Routes = [
    {
        path: 'login',
        component:LoginComponent
    }
]