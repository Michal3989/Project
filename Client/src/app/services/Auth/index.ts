import { LoginService } from './login.service';
import {EventService} from './event.service';
//import {OwnerService} from './owner.service';
import {CommonService} from './common.service';
import {GuestsService} from './guests.service';
import {TableService} from './table.service';
export * from './login.service';
export * from './event.service';
export * from './common.service';
//export * from './owner.service';
export * from './guests.service';
export * from './table.service';

export const AUTH_SERVICES = [
    LoginService,EventService,
  //  OwnerService,
    GuestsService,TableService,CommonService
]
