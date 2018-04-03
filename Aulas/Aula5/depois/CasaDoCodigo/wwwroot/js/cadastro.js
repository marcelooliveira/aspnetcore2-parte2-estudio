class Cadastro {
    getEndereco(elemento) {
        var token = $('input[name=__RequestVerificationToken]').val();

        var header = {};
        header['RequestVerificationToken'] = token;

        var cep = $(elemento).val();
        debugger;
        $.ajax({
            url: '/pedido/getEndereco',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ CEP: cep }),
            headers: header
        }).done(function (response) {
            $('#endereco').val(response.logradouro + ', ');
            $('#bairro').val(response.bairro);
            $('#municipio').val(response.localidade);
            $('#uf').val(response.uf);
            $('#endereco').focus();
        }.bind(this))
    }
}

var cadastro = new Cadastro();