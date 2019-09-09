import { LoginService } from './login.service';
import { EventService } from './event.service';
import { EventOwnerService } from './event-owner.service';

export * from './login.service';
export * from './event.service';
export * from './event-owner.service';
export var AUTH_SERVICES = [
    LoginService, EventService, EventOwnerService 
];
//# sourceMappingURL=index.js.map