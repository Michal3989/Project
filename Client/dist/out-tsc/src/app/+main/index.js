import { MainComponent } from './main.component';
import { DASHBOARD_ROUTES, DASHBOARD_COMPONENTS } from './+dashboard';
//import { AuthGuard } from '../services/auto.guard'
export * from './+dashboard';
export var MAIN_COMPONENTS = [
    MainComponent
].concat(DASHBOARD_COMPONENTS);
export var MAIN_ROUTES = [
    {
        path: 'main',
        component: MainComponent,
        //canActivate: [AuthGuard],
        children: [
            {
                path: '',
                pathMatch: 'full',
                redirectTo: 'dashboard'
            }
        ].concat(DASHBOARD_ROUTES)
    }
];
//# sourceMappingURL=index.js.map