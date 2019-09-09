export var environment = {
    production: true,
    development: true,
    apiServer: {
        useHttps: true,
        serverUrl: 'localhost',
        fullAddress: function () {
            var protocol = environment.apiServer.useHttps ? 'https' : 'http';
            var address = environment.apiServer.serverUrl;
            //let port = environment.apiServer.port && environment.apiServer.port > 0 ? `:${environment.apiServer.port}` : '';
            return protocol + "://" + address + "/";
        }
    },
};
//# sourceMappingURL=environment.prod.js.map