import { AuthComponent } from './auth.component';
import { LOGIN_COMPONENTS, LoginComponent } from './+login';
export * from './+login';
export var AUTH_COMPONENTS = [
    AuthComponent
].concat(LOGIN_COMPONENTS);
export var AUTH_ROUTES = [
    {
        path: '',
        component: LoginComponent
    }
];
//# sourceMappingURL=index.js.map