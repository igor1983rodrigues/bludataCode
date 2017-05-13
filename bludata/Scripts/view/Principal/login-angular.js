var app = angular.module('appCadastro', []);

app.service('LoginService', function ($http, $window) {
    var rootPath = angular.element("#root-path").val();
    this.doLogin = function (params) {
        var cnpjCrypt = $window.btoa(params);
        var url = rootPath + '/api/cliente/login/' + cnpjCrypt;
        return $http.get(url.replace('//', '/'));
    };
}).controller('LoginController', function ($scope, LoginService, $window) {
    $scope.doLogin = function () {
        $scope.logando = true;
        LoginService.doLogin($scope.dados.cnpj.replace(/([\d]{2}).([\d]{3}).([\d]{3})\/([\d]{4})-([\d]{2})/, '$1$2$3$4$5')).then(function (res) {
            $window.location.href += '/' + res.data.clienteCodigo;
        }, function (res) {
            $scope.mensagem = res.data.message;
            $scope.logando = false;
        });
    };
});