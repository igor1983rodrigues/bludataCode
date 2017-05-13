app.filter('ufsClientes', [function () {
    return function (objeto) {
        var arr = [];
        angular.forEach(objeto, function (item) {
            if (item.ufCadastro)
                arr.push(item);
        });
        return arr;
    };
}]).service('ClienteService', function ($http) {
    var rootPath = angular.element("#root-path").val();
    var path = String(rootPath + '/api/cliente').replace("//", "/");

    this.get = function () {
        return $http.get(path);
    };

    this.getId = function (id) {
        return $http.get(path + '/' + id);
    };

    this.post = function (params) {
        return $http.post(path, params);
    };

    this.put = function (params) {
        return $http.put(path, params);
    };
}).controller('ClienteController', ['$scope', 'ClienteService', 'UfService', '$timeout', function ($scope, ClienteService, UfService, $timeout) {
    $scope.dados = $scope.dados || {};
    $scope.err = $scope.err || {};

    $scope.regexCnpj = /([\d]{2}).([\d]{3}).([\d]{3})\/([\d]{4})-([\d]{2})/;

    if ($scope.UF == undefined)
        UfService.obterUf().then(function (res) {
            $scope.UF = res.data;
        });

    $scope.carregar = function () {
        if ($scope.Cliente !== undefined) {
            $scope.dados = {};
            for (var i in $scope.Cliente)
                $scope.dados[i] = $scope.Cliente[i];
        } else {
            ClienteService.get().then(function (res) {
                $scope.dados = res.data;
            });
        }
    };

    $scope.validar = function () {
        return FrmCliente.$error === undefined
            || FrmCliente.$error.required === undefined;
    };

    $scope.cadastrar = function () {
        if ($scope.validar()) {
            var modal = $('<div>').modalAguarde();
            ClienteService.post($scope.dados).then(function (res) {
                modal.modal('hide');
                $('<div>').criarModal('Configuração', res.data.mensagem, 'primary');
                console.info(res);
            }, function (res) {
                $scope.err = res;
                $scope.err.title = res.statusText;
                $scope.err.message = res.data.message;
                //alert(res.statusText);
                //$scope.err = res.statusText;
                //$('#modal').text(res.statusText).modal('show');
            });
        } else {
            $scope.err = {
                title: 'Atenção!',
                message: 'Preencha todos os campos obrigatórios.'
            };
        }
    };

    $scope.mudarCnpj = function () {
        $scope.dados.clienteCnpj = $scope.dados.clienteCnpj.replace($scope.regexCnpj, '$1$2$3$4$5');
    };

    $scope.alterar = function () {
        if ($scope.validar()) {
            var modal = $('<div>').modalAguarde();
            ClienteService.put($scope.dados).then(function (res) {
                modal.modal('hide');
                $(document).criarModal('Configuração', res.data.mensagem, 'primary');
                angular.element('[ng-controller="ConteudoController"]').scope().carregarCliente($scope.dados.clienteCodigo);
            }, function (res) {
                modal.modal('hide');
                $scope.err = res;
                $scope.err.title = res.statusText;
                $scope.err.message = res.data.message;
                //alert(res.statusText);
                //$scope.err = res.statusText;
                //$('#modal').text(res.statusText).modal('show');
            });
        } else {
            $scope.err = {
                title: 'Atenção!',
                message: 'Preencha todos os campos obrigatórios.'
            };
        }
    };
}]);

//app.controller('ClienteController', ClienteController);
//ClienteController.$inject = ['$scope', 'ClienteService'];

//function ClienteController ($scope, ClienteService) {
//    $scope.modelo = 'teste';
//}

//setTimeout(function () {
//    // create an injector
//    var $injector = angular.injector(['ng', 'appCadastro']);

//    //// use the injector to kick off your application
//    //// use the type inference to auto inject arguments, or use implicit injection
//    //$injector.invoke(function ($rootScope, $compile, $document) {
//    //    $compile($document)($rootScope);
//    //    $rootScope.$digest();
//    //});


//    $injector.instantiate(ClienteController);
//}, 3000);
//var $div = $('div[ng-controller="ClienteController"]');

//angular.element(document).injector().invoke(function ($compile) {
//    var scope = angular.element($div).scope();
//    $compile($div)(scope);
//});