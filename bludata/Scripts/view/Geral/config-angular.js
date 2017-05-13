var app = angular.module("appCadastro", ['ui.bootstrap']);

app
//    .config(function ($routeProvider, $locationProvider) {
//    //// Utilizando o HTML5 History API
//    //$locationProvider.html5Mode(true);


//})
    //.config(function ($controllerProvider, $provide, $compileProvider) {
    //// Since the "shorthand" methods for component
    //// definitions are no longer valid, we can just
    //// override them to use the providers for post-
    //// bootstrap loading.
    //console.log("Config method executed.");
    //// Let's keep the older references.
    //app._controller = app.controller;
    //app._service = app.service;
    //app._factory = app.factory;
    //app._value = app.value;
    //app._directive = app.directive;
    //app.controller = function (name, constructor) {
    //    console.log("controller...");
    //    console.log(name);
    //    console.dir(constructor);
    //    $controllerProvider.register(name, constructor);
    //    return (this);
    //};
    //// Provider-based service.
    //app.service = function (name, constructor) {
    //    $provide.service(name, constructor);
    //    return (this);
    //};
    //// Provider-based factory.
    //app.factory = function (name, factory) {
    //    $provide.factory(name, factory);
    //    return (this);
    //};
    //// Provider-based value.
    //app.value = function (name, value) {
    //    $provide.value(name, value);
    //    return (this);
    //};
    //// Provider-based directive.
    //app.directive = function (name, factory) {
    //    $compileProvider.directive(name, factory);
    //    return (this);
    //};
    //})
.service('UfService', function ($http) {
    var url = '/api/uf';
    this.obterUf = function (params) {
        params = params || {};
        var urlParams = url + '?' + $.param(params);
        return $http.get(urlParams);
    };
});

// create an injector
//var $injector = angular.injector(['ng', 'appCadastro']);

//// use the injector to kick off your application
//// use the type inference to auto inject arguments, or use implicit injection
//$injector.invoke(function ($rootScope, $compile, $document) {
//    $compile($document)($rootScope);
//    $rootScope.$digest();
//});
