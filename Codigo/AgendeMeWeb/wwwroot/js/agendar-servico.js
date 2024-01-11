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

function getServicos(url, idArea) { 
    $("#buttonBoxs").html('<div class="loading medium loading-areas"></div>');
        $.ajax({
            type: "GET",
            url: url,
            dataType: "HTML",
            data: { idArea: idArea },

            success: function (result) {
                $("#ajaxBox").html(result);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //TODO::
            },
        });
}

function getOrgaos(idArea, url, nomeServico, iconeServico) {
    $("#buttonBoxs").html('<div class="loading medium loading-areas"></div>');
    $.ajax({
        type: "GET",
        url: url,
        dataType: "HTML",
        data: { idArea: idArea, nomeServico: nomeServico, iconeServico: iconeServico },

        success: function (result) {
            $("#ajaxBox").html(result);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //TODO::
        },
    });
}

function getDias(url, idServico, nomeOrgao, nomeServico, idOrgao, idArea, iconeServico) {
    $("#buttonBoxs").html('<div class="loading medium loading-areas"></div>');
    $.ajax({
        type: "GET",
        url: url,
        dataType: "HTML",
        data: { idServico: idServico, nomeOrgao: nomeOrgao, nomeServico: nomeServico, idOrgao: idOrgao, idArea: idArea, iconeServico: iconeServico },

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
    button.className = "br-button primary loading mr-3 mt-2";
    var cpf = document.getElementById("CPF").value;
    $.ajax({
        type: "GET",
        url: url,
        dataType: "HTML",
        data: { CPF: cpf },

        success: function (result) {
            $("#cpfBox").html(result);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //TODO::
            button.className = "br-button primary mr-3 mt-2";
        },
    });
}

/* temporario */
function verificaCidadao() {
    var cidadaoId = document.getElementById("idCidadao");
    if (cidadaoId != null) {
        document.getElementById("idCidadaoForm").value = cidadaoId.value;
    }
}

/* Voltar */
function voltarArea(url, idPrefeitura) {
    $.ajax({
        type: "GET",
        url: url,
        dataType: "HTML",
        data: { id: idPrefeitura },

        success: function (result) {
            $("#ajaxBox").html(result);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //TODO::
        },
    });
}