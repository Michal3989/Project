// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.
export var environment = {
    production: false,
    development: true,
    apiServer: {
        port: 52718,
        useHttps: false,
        serverUrl: 'localhost',
        fullAddress: function () {
            var protocol = environment.apiServer.useHttps ? 'https' : 'http';
            var address = environment.apiServer.serverUrl;
            var port = environment.apiServer.port && environment.apiServer.port > 0 ? ":" + environment.apiServer.port : '';
            return protocol + "://" + address + port + "/";
        }
    },
};
//# sourceMappingURL=environment.js.map