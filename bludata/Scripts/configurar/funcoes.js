function tratamentoErro(res) {
    /// <param name="res" type="Object">Description</param>
    var $div = $('<div>').addClass("alert alert-danger").attr({ role: "alert" }).text(res.status + ' ' + res.statusText);
    $('#principal').append($div);
    console.info(res);
    setTimeout(function () {
        $div.remove();
    }, 3000);
}

function criarTab(elemento, e, html) {
    /// <param name="elemento" type="HTMLLinkElement">Description</param>
    /// <param name="e" type="Event">Description</param>
    /// <param name="titulo" type="String">Description</param>
    /// <param name="html" type="String">Description</param>
    /// <returns type="jQuery" />

    var $ul = $('#tabs > ul');
    var height = $ul.get(0).offsetHeight;
    var key = elemento.rel;

    var $tab = $('<li>').attr('role', 'presentation');
    var $link = $('<a>').attr({
        href: '#',
        rel: elemento.rel + 'Tab'
    }).css({
        //height: height
        margin: 0,
        padding: 0
    }).html(elemento.innerHTML)
        .append($('<span>').addClass('badge').text('X').css({ cursor: 'pointer' }));

    $tab.append($link);
    $ul.append($tab);

    var hLink = $link.get(0).offsetHeight;
    $link.css({
        padding: (height / 2) - (hLink / 2)
    });

    var $conteudo = $('<div>').attr({ id: elemento.rel + 'Tab' }).show().html(html);
    var controller = $conteudo.find("[ng-controller]");
    if (controller.length > 0) {
        html = controller.get(0).outerHTML;
        controller.remove();
        angular.element('[ng-controller="ConteudoController"]').scope().carregar($conteudo, html);
    }
    $('#conteudo').append($conteudo);

    //$conteudo.load(elemento.href);

    //angular.element(document).injector(app).invoke(function ($compile) {
    //    var scope = angular.element($conteudo).scope();
    //    $compile($conteudo)(scope);
    //});


    return $link;
}

$.fn.modalAguarde = function () {
    var self = this;
    var idString = "modal-aguarde";
    var modal = document.getElementById(idString);
    if (modal != null) $(modal).remove();

    var $progress = $('<div>').addClass('progress');
    $progress.append($("<div>").attr({
        'role': "progressbar",
        'aria-valuenow': 99,
        'aria-valuemin': 0,
        'aria-valuemax': 100
    }).addClass("progress-bar progress-bar-infos progress-bar-striped active")
        .css({ width: '99%' }));

    self.attr('id', idString)
        .addClass("modal fade")
        .append($('<div>').addClass('modal-dialog')
            .attr('role', 'document')
            .append($('<div class="modal-content">')
                .append($('<div class="modal-body">').append($progress))))
        .modal('show');

    return self;
}