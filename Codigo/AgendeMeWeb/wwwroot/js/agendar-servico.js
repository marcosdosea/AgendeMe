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

function removeErro(select) {
    if (select.value) {
        var mensagemErro = document.getElementById("erroSelectPrefeitura");
        mensagemErro.className = "d-none";
    }
}

function getServicos(url, id, nomeArea, iconeArea) { 
    $("#buttonBoxs").html('<div class="loading medium loading-areas"></div>');
        $.ajax({
            type: "GET",
            url: url,
            dataType: "HTML",
            data: { id: id, nomeArea: nomeArea, iconeArea: iconeArea },

            success: function (result) {
                $("#ajaxBox").html(result);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //TODO::
            },
        });
}

function getOrgaos(url, nomeServico, iconeServico) {
    $("#buttonBoxs").html('<div class="loading medium loading-areas"></div>');
    $.ajax({
        type: "GET",
        url: url,
        dataType: "HTML",
        data: { nomeServico: nomeServico, iconeServico: iconeServico },

        success: function (result) {
            $("#ajaxBox").html(result);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //TODO::
        },
    });
}

function getDias(url, idServico, nomeOrgao, nomeServico, idOrgao) {
    $("#buttonBoxs").html('<div class="loading medium loading-areas"></div>');
    $.ajax({
        type: "GET",
        url: url,
        dataType: "HTML",
        data: { idServico: idServico, nomeOrgao: nomeOrgao, nomeServico: nomeServico, idOrgao: idOrgao },

        success: function (result) {
            $("#ajaxBox").html(result);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //TODO::
        },
    });
}

function getHoras(url, idServico, data, diaSemana, nomeOrgao, nomeServico, idOrgao) {
    $("#buttonBoxs").html('<div class="loading medium loading-areas"></div>');
    $.ajax({
        type: "GET",
        url: url,
        dataType: "HTML",
        data: { idServico: idServico, dia: data, nomeDia: diaSemana, nomeOrgao: nomeOrgao, nomeServico: nomeServico, idOrgao: idOrgao },

        success: function (result) {
            $("#ajaxBox").html(result);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //TODO::
        },
    });
}

function getDadosAgendamento(url, id) {
    $("#buttonBoxs").html('<div class="loading medium loading-areas"></div>');
    $.ajax({
        type: "GET",
        url: url,
        dataType: "HTML",
        data: { idDiaAgendamento: id },

        success: function (result) {
            $("#ajaxBox").html(result);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //TODO::
        },
    });
}

/* temporario */
function getCidadao(url, button) {
    button.className = "br-button primary loading";
    $.ajax({
        type: "GET",
        url: url,
        dataType: "HTML",
        data: { idServico: idServico, dia: data, nomeDia: diaSemana, nomeOrgao: nomeOrgao, nomeServico: nomeServico, idOrgao: idOrgao },

        success: function (result) {
            $("#ajaxBox").html(result);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //TODO::
        },
    });
}