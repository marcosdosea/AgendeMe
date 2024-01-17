function getSessionCookie() {
    $.ajax({
        type: "GET",
        url: "/AgendarServico/GetSession",
        dataType: "HTML",
        data: { },

        success: function (result, status) {
            return true;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            return false;
        },
    });
}

function submitForm(button) {
    location.reload()
    if (getSessionCookie()){
        button.parentNode.submit()
    }
}