import * as tslib_1 from "tslib";
import { Injectable } from "@angular/core";
import 'rxjs/add/operator/map';
import { BaseHttpService, BaseApiService } from "../../shared";
import { Router } from "@angular/router";
var EventService = /** @class */ (function (_super) {
    tslib_1.__extends(EventService, _super);
    function EventService(router, baseHttpService) {
        var _this = _super.call(this, 'Event') || this;
        _this.router = router;
        _this.baseHttpService = baseHttpService;
        return _this;
    }
    EventService.prototype.getTypes = function () {
        var url = this.actionUrl('GetEventTypes');
        return this.baseHttpService.get(url);
    };
    EventService.prototype.saveEvent = function (eventToSave) {
        var url = this.actionUrl('AddEvent', eventToSave);
        return this.baseHttpService.post(url);
    };
    EventService = tslib_1.__decorate([
        Injectable(),
        tslib_1.__metadata("design:paramtypes", [Router, BaseHttpService])
    ], EventService);
    return EventService;
}(BaseApiService));
export { EventService };
//# sourceMappingURL=event.service.js.map