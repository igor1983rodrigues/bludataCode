$(document).ready(function () {
    $('.cpf').inputmask('999.999.999-99');
    $('.data').inputmask('99/99/9999');
    $('.ddd').inputmask('999');
    $('.telefone').inputmask('9999[9]-9999');
    $('input[numero]').on('keypress', function (e) {
        setTimeout(function (self) {
            /// <param name="self" type="HtmlInputElement">Description</param>
            self.value = String(self.value).replace(/[^\d]/, '');
        }, 1, this);
    });
});
