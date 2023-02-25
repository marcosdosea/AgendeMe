$(document).ready(() => {
    console.log("~/Views/Cidadao/Create.cshtml");
    $.ajax({
        type: "GET",
        url: '/Cidadao/Create',
        dataType: "HTML",

        success: function (result) {
            $("#cidadaoForm").html(result);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //TODO::
        },
    });
})


function getAreas(url, button) {
    var idPrefeitura = document.getElementById("prefeitura");
    if (idPrefeitura.value) {
        button.className = "br-button primary loading mx-auto d-block mt-5 mb-5";
        $.ajax({
            type: "GET",
            url: url,
            dataType: "HTML",
            data: { id: idPrefeitura.value },

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