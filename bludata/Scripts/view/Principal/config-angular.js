app.service("serviceNavbar", function ($location) {
    this.path = $location.absUrl();
    this.navbar = [
        {
            fa: 'fa-cog',
            label: 'Configurações',
            link: 'Cliente/index',
            key: 'Cliente'
        },
        {
            fa: 'fa-user',
            label: 'Pessoas',
            link: '#',
            key: 'Pessoas',
            subitens: [
                {
                    fa: 'fa-list-alt',
                    label: 'Listar',
                    link: 'Pessoa/Index',
                    key: 'PessoasListar',
                },
                {

                    fa: 'fa-plus',
                    label: 'Cadastrar',
                    link: 'Pessoa/Novo',
                    key: 'PessoasNovo'
                }
            ]
        }
    ];
}).service('TipoTelefoneService', function ($http) {
    var rootPath = angular.element("#root-path").val();
    var path = String(rootPath + '/api/tipotelefone').replace("//", "/");
    this.get = function () {
        return $http.get(path);
    };
}).controller("navbar", function ($scope, serviceNavbar, ClienteService) {
    $scope.carregarCliente = function (clienteCodigo) {
        ClienteService.getId(clienteCodigo).then(function (res) {
            $scope.Cliente = res.data;
        }, function (res) {
            alert(res.data.message);
        });
    };

    $scope.carregar = function () {
        $scope.rootPath = angular.element("#root-path").val();
        $scope.navbar = serviceNavbar.navbar;
    };
}).controller("ConteudoController", function ($scope, $compile, UfService, ClienteService) {
    $scope.dados = {};
    $scope.rootPath = angular.element("#root-path").val();

    UfService.obterUf().then(function (res) {
        $scope.UF = res.data;
    });

    $scope.carregar = function (elemento, html) {
        var compilado = $compile(html)($scope);
        elemento.append(compilado);
        compilado.scope().carregar();
    };

    $scope.carregarCliente = function (clienteCodigo) {
        ClienteService.getId(clienteCodigo).then(function (res) {
            $scope.Cliente = res.data;
        }, function (res) {
            alert(res.data.message);
        });
    };
});

