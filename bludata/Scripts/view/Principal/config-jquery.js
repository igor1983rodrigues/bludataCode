(function ($) {
    $(document).ready(function () {
        $('.cnpj').inputmask('99.999.999/9999-99');
        $('.cpf').inputmask('999.999.999-99');
        $('.telefone').inputmask('(["+"99 ]99) [9]9999-9999');

        $('body').on('keypress', 'input, select, textarea', function (e) {
            setTimeout(function (self) {
                var valor = String(self.value).replace('_', '');
                var maxlength = parseInt($(self).attr('maxlength') || '0');
                if (maxlength > 0 && maxlength <= valor.length)
                    $(self).next().focus();
            }, 1, this);
        });

        $('body').on('click', 'a.abrir-aba', function (e) {
            var self = this;
            if (self.href[self.href.length - 1] !== '#') {
                /// <param name="e" type="Event">Description</param>
                e.stopImmediatePropagation();
                e.preventDefault();

                var $tab = $('a[rel="' + self.rel + 'Tab"]');

                if ($tab.length > 0) {
                    $tab.trigger('click');
                } else {
                    var $modal = $('<div>').modalAguarde();
                    $.get(self.href, function (res) {
                        $tab = criarTab(self, e, res);
                        $div = $('#' + self.rel + 'Tab').find('ng-controller');
                        //console.info(angular.element(document));

                        //var injecao = angular.bootstrap($div, ['appCadastro']);
                        //ClienteController.invo
                        //injecao.invoke(function ($scope, $compile, $scopeProvider ) {
                        //});

                        //angular.element(document).injector().invoke(function ($scope, $compile) {
                        //    var scope = angular.element($div).scope();
                        //    $compile($div)(scope);
                        //});
                    }).fail(function (res) {
                        var html = String(res.responseText);
                        var eliminar = '<body bgcolor="white">';
                        var i = html.indexOf(eliminar) + eliminar.length;

                        html = html.substring(i, html.length);
                        html = html.replace('</body>', "\0");

                        $tab = criarTab(self, e, html);
                    }).always(function () {
                        $tab.trigger('click');
                        $modal.modal('hide');
                    });
                }

                return false;
            }
        }).on('click', '#tabs ul.nav.nav-tabs li > a', function (e) {
            e.stopImmediatePropagation();
            e.preventDefault();

            var tabConteudo = document.getElementById(this.rel);

            $('#tabs ul.nav.nav-tabs li.active').removeClass('active');
            $('#conteudo > div').hide();

            $(this).parents('li').addClass('active');
            $(tabConteudo).show();

            return false;
        }).on('click', '#tabs ul.nav.nav-tabs li > a .badge', function (e) {
            /// <param name="e" type="Event">Description</param>
            e.preventDefault();
            e.stopImmediatePropagation();

            var link = $(this).parents('a').get(0);
            var div = document.getElementById(link.rel);
            var $li = $(link).parents('li');

            if ($li.hasClass('active')) {
                var $ul = $li.parents('ul.nav.nav-tabs').find('li');
                var index = $ul.index($li) - 1;
                $ul.eq(index).find('a').trigger('click');
            }

            $(div).remove();
            $li.remove();

            return false;
        });
    });
    $.fn.criarModal = function (strTitulo, strMensagem, tipo) {
        $('#modal').remove();
        strTitulo = strTitulo || '&nbsp;';
        tipo = tipo || '';
        var modalBody = $('<div>').append($('<p>').text(strMensagem));
        var modalHeader = $('<div>').append('<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>')
            .append($('<h4 class="modal-title">').html(strTitulo));
        var modalContent = $('<div>').append(modalHeader).append(modalBody);
        var modalDialog = $('<div>').append(modalContent);
        var principal = $('<div>').append(modalDialog);

        modalBody.addClass('modal-body');
        modalHeader.addClass('modal-header').addClass('panel-heading');
        modalContent.addClass('modal-content').addClass('panel-' + tipo);
        modalDialog.addClass('modal-dialog').attr('role', 'document');
        principal.addClass('modal fade').attr({
            id: "modal",
            tabindex: -1,
            role: 'dialog'
        }).modal();

        /*
    <div class="modal fade" id="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header warning">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title">Modal title</h4>
                </div>
                <div class="modal-body">
                    <p>One fine body&hellip;</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                </div>
            </div><!-- /.modal-content -->
        </div><!-- /.modal-dialog -->
    </div><!-- /.modal -->
        */
    };
})(jQuery);

