function getAreas(url) {
    var prefeitura = document.getElementById("prefeitura");
    if (prefeitura.value) {
        $.ajax({
            type: "GET",
            url: url,
            dataType: "HTML",
            data: { prefeitura: prefeitura.value },

            success: function (result) {
                $("#ajaxBox").html(result);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //TODO::
            },
        });
    }
    else {
        var mensagemErro = document.getElementById("erroSelectPrefeitura");
        mensagemErro.className = "";
    }
}

function removeErro(select) {
    if (select.value) {
        var mensagemErro = document.getElementById("erroSelectPrefeitura");
        mensagemErro.className = "d-none";
    }
}
