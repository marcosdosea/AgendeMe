function loadTime() {
    date = new Date()
    document.querySelector("#time").textContent = `${date.getHours()}:${date.getMinutes()}`
    month = date.getMonth() + 1
    document.querySelector("#date").textContent  = `${date.getDate()}/${month < 10 ? `0${month}` : month}/${date.getFullYear()}`
}
loadTime()

setInterval(loadTime, 1000);

function getAtendimentos() {
    let id = window.location.pathname.charAt(window.location.pathname.length - 1)
    
    if (!Number(id)) {
        return
    }
    const url = "/AgendarServico/GetAtendimentos";
    $.ajax({
        type: "GET",
        url: url,
        dataType: "HTML",
        data: { id: id },

        success: function (result) {
            $("#painelAtendimento").html(result);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //TODO::
        },
    });
}
getAtendimentos()

document.querySelector("#btnFullScreen").addEventListener("click", (e, a) => {
    if (document.fullscreenElement) {
        document.querySelector("#btnFullScreenIcon").classList.add("fa-expand")
        document.querySelector("#btnFullScreenIcon").classList.remove("fa-compress")

        autoSize(false);

        document.exitFullscreen();
    } else {
        document.querySelector("#btnFullScreenIcon").classList.remove("fa-expand")
        document.querySelector("#btnFullScreenIcon").classList.add("fa-compress")

        autoSize(true);

        document.querySelector("body").requestFullscreen();
    }
})

function autoSize(expand) {
    const paddingClass = "py-2"; 
    const marginClassLow = "my-1";
    const marginClassHigh = "my-2";

    if (expand){
        document.querySelectorAll(".ag-atendimento").forEach(
            (element) => { 
                element.classList.add(paddingClass)
                element.querySelectorAll("div").forEach(
                    (child) => { 
                        child.classList.replace(marginClassLow, marginClassHigh)
                    })
        })
    } else {
        document.querySelectorAll(".ag-atendimento").forEach(
            (element) => { 
                element.classList.remove(paddingClass)
                element.querySelectorAll("div").forEach(
                    (child) => { 
                        child.classList.replace(marginClassHigh, marginClassLow)
                    })
        })
    }
}