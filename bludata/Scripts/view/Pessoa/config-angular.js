app.service('PessoaService', function ($http) {
    var rootPath = angular.element("#root-path").val();
    var path = String(rootPath + '/api/pessoa').replace("//", "/");

    this.pesquisar = function (params) {
        var url = path + '/pesquisar?' + $.param(params);
        return $http.get(url);
    };

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

    this.delete = function (params) {
        return $http.delete(path + '/' + params);
    };
}).service('TelefoneService', function ($http) {
    var rootPath = angular.element("#root-path").val();
    var path = String(rootPath + '/api/telefone').replace("//", "/");

    this.pesquisar = function (params) {
        var url = path + '/pesquisar/' + params;
        return $http.get(url);
    };

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
}).controller('CadastrarController', function ($scope, PessoaService, TelefoneService, TipoTelefoneService) {
    $scope.dados = $scope.dados || {};
    $scope.err = $scope.err || {};

    $scope.regexCpf = /([\d]{3}).([\d]{3}).([\d]{3})-([\d]{2})/;
    $scope.regexTelefone = /([\d]{5})-([\d]{4}|[\d]{3})/;
    $scope.regexNaoNumerico = /([^\d])/g;

    $scope.doRegra = function () {
        if ($scope.Cliente.ufCodigo === 'PR') {
            if ($scope.dados.pessoaDataNascimento === undefined
                || $scope.dados.pessoaDataNascimento === null
                || $scope.dados.pessoaDataNascimento === '')
                return false;

            var formatoUtc = String($scope.dados.pessoaDataNascimento).replace(/([\d]{2})\/([\d]{2})\/([\d]{4})/, '$3-$2-$1');
            var dataNascimento = new Date(formatoUtc);
            var dataHoje = new Date();

            var diferenca = Math.abs(dataHoje.getTime() - dataNascimento.getTime());
            var anos = 1000 * 3600 * 24 * 365;
            var total = Math.floor(diferenca / anos);

            return total >= 18;
        }
        else {
            return true;
        }
    };

    $scope.obterTelefoneFormatado = function (entrada) {
        return entrada.replace(/([\d]{4})([\d]+)/, '$1-$2');
    }

    $scope.carregar = function (pessoa) {
        $scope.dados = pessoa || {};

        if (typeof $scope.dados.pessoaDataNascimento === 'string')
            $scope.dados.pessoaDataNascimento = new Date($scope.dados.pessoaDataNascimento);

        if ($scope.UF == undefined)
            UfService.obterUf().then(function (res) {
                $scope.UF = res.data;
            });

        if ($scope.Cliente !== undefined)
            $scope.dados.clienteCodigo = $scope.Cliente.clienteCodigo;

        TipoTelefoneService.get().then(function (res) {
            $scope.tipoTelefones = res.data;
        });

        $scope.regra = $scope.doRegra();

        $scope.dados.telefones = [];
        if (($scope.dados.pessoaCodigo || 0) > 0)
            TelefoneService.pesquisar($scope.dados.pessoaCodigo).then(function (res) {
                $scope.dados.telefones = res.data;
            });
    };

    $scope.verificaTelefone = function (tel) {
        return tel !== undefined
            && tel !== null
            && tel.telefoneDdd !== undefined
            && tel.telefoneDdd !== null
            && tel.telefoneDdd !== ''
            && tel.telefoneNumero !== undefined
            && tel.telefoneNumero !== null
            && tel.telefoneNumero !== ''
            && tel.tipoTelefoneCodigo !== undefined
            && tel.tipoTelefoneCodigo !== null
            && tel.tipoTelefoneCodigo !== '';
    }

    $scope.addTelefone = function () {
        if ($scope.verificaTelefone($scope.telefoneTmp)) {
            $scope.telefoneTmp.telefoneNumero = $scope.telefoneTmp.telefoneNumero.replace($scope.regexTelefone, '$1$2').replace(/([^\d])/g,'');
            $scope.dados.telefones.push($scope.telefoneTmp);
            $scope.telefoneTmp = {};
        } else {
            alert('Telefone incopleto.')
        }
    }

    $scope.removerTelefone = function (tel) {
        var telsTmp = $scope.dados.telefones;
        $scope.dados.telefones = [];
        for (var i in telsTmp) {
            if (JSON.stringify(telsTmp[i]) !== JSON.stringify(tel))
                $scope.dados.telefones.push(telsTmp[i]);
        }
    };

    $scope.validar = function () {
        var resposta = true;
        if ($(FrmPessoa).hasClass('ng-invalid'))
            return false;
        angular.element(FrmPessoa).find('[required], [ng-required]').each(function () {
            resposta == resposta && (this.$error === undefined
            || this.$error.required === undefined);
        });

        return resposta;
    };

    $scope.cadastrar = function () {
        if ($scope.validar()) {
            var modal = $('<div>').modalAguarde();
            PessoaService.post($scope.dados).then(function (res) {
                console.info(res);
                $scope.dados.pessoaCodigo = res.data.id;
                modal.modal('hide');
                $('<div>').criarModal('Cadastro de Pessoas', res.data.mensagem, 'success');
                $scope.dados = {
                    clienteCodigo: $scope.Cliente.clienteCodigo,
                    cliente: $scope.Cliente,
                    telefones: []
                };
                $scope.telefoneTmp = {};
            }, function (res) {
                $scope.err = res;
                $scope.err.title = res.statusText;
                $scope.err.message = res.data.message;
                //alert(res.statusText);
                //$scope.err = res.statusText;
                //$('#modal').text(res.statusText).modal('show');
                modal.modal('hide');
                $('<div>').criarModal(res.statusText, res.data.message, 'danger');
            });
        } else {
            $('<div>').criarModal('Cadastro de Pessoas', 'Há um campo obrigatório incorreto', 'danger');
        }
        //else
        //    $scope.err = {
        //        title: 'Atenção!',
        //        message: 'Preencha todos os campos obrigatórios.'
        //    };
    };

    $scope.mudarCpf = function () {
        $scope.dados.pessoaCpf = $scope.dados.pessoaCpf.replace($scope.regexCpf, '$1$2$3$4');
    };

    $scope.alterar = function () {
        if ($scope.validar()) {
            var modal = $('<div>').modalAguarde();
            PessoaService.put($scope.dados).then(function (res) {
                modal.modal('hide');
                $('<div>').criarModal('Cadastro de Pessoas', res.data.mensagem, 'success');
            }, function (res) {
                modal.modal('hide');
                $('<div>').criarModal(res.statusText, res.data.message, 'danger');
            });
        } else {
            $('<div>').criarModal('Cadastro de Pessoas', 'Há um campo obrigatório incorreto', 'danger');
        }
    };
}).controller('PessoaListarController', function ($scope, PessoaService, $window) {
    $scope.carregar = function () {
        $scope.filtro = $scope.filtro || {
            cliente: $scope.Cliente.clienteCodigo
        };

        PessoaService.pesquisar($scope.filtro).then(function (res) {
            $scope.pessoaCollection = res.data;
        });
    };

    $scope.retiraMascaraCpf = function () {
        $scope.filtro.cpf = String($scope.filtro.cpf).replace(/([\d]{3}).([\d]{3}).([\d]{3})-([\d]{2})/, '$1$2$3$4')
    };

    $scope.enviar = function () {
        PessoaService.pesquisar($scope.filtro).then(function (res) {
            $scope.pessoaCollection = res.data;
        });
    };

    $scope.editar = function (item) {
        $('a[rel="PessoasNovo"]').trigger('click');
        var intervalo = setInterval(function () {
            try {
                var controlador = angular.element('[ng-controller="CadastrarController"]');
                controlador.scope().carregar(item);
                clearInterval(intervalo);
            } catch (ex) {
                console.warn(ex.message);
            }
        }, 100);
    };

    $scope.excluir = function (id) {
        var modal = $('<div>').modalAguarde();
        PessoaService.delete(id).then(function (res) {
            $scope.carregar();
            modal.modal('hide');
        }, function (res) {
            modal.modal('hide');
            $('<div>').criarModal('Pessoas', res.data.message, 'danger');
        });
    };
});