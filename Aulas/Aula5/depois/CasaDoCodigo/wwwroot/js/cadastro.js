class Cadastro {
    getEndereco(elemento) {
        var cep = $(elemento).val();

        //Nova variável "cep" somente com dígitos.
        var cep = cep.replace(/\D/g, '');

        //Verifica se campo cep possui valor informado.
        if (cep != "") {

            //Expressão regular para validar o CEP.
            var validacep = /^[0-9]{8}$/;

            //Valida o formato do CEP.
            if (validacep.test(cep)) {

                var token = $('input[name=__RequestVerificationToken]').val();

                var header = {};
                header['RequestVerificationToken'] = token;

                $.get({
                    url: '/pedido/getEndereco?cep=' + cep,
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
    }
}

var cadastro = new Cadastro();