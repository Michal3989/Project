import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
import { EventDto } from "../models/dto/eventDto";
import { EventService } from "../services"; //yyyyy
var EventDetailsComponent = /** @class */ (function () {
    function EventDetailsComponent(/*private router: Router,*/ eventService) {
        this.eventService = eventService;
        this.currentEvent = new EventDto();
    }
    EventDetailsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.eventService.getTypes().subscribe(function (data) {
            _this.types = data;
        });
    };
    EventDetailsComponent.prototype.saveEvent = function () {
        this.eventService.saveEvent(this.currentEvent).subscribe(function (data) {
            alert(data);
        });
    };
    EventDetailsComponent.prototype.onsubmit = function () {
    };
    EventDetailsComponent = tslib_1.__decorate([
        Component({
            selector: 'app-event-details',
            templateUrl: './event-details.component.html',
            styleUrls: ['./event-details.component.css']
        }),
        tslib_1.__metadata("design:paramtypes", [EventService])
    ], EventDetailsComponent);
    return EventDetailsComponent;
}());
export { EventDetailsComponent };
//# sourceMappingURL=event-details.component.js.map