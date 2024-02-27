function loadTime() {
    date = new Date()
    hour = date.getHours()
    minutes = date.getMinutes()
    document.querySelector("#time").textContent = `${hour < 10 ? `0${hour}` : hour}:${minutes < 10 ? `0${minutes}` : minutes}`
    month = date.getMonth() + 1
    day = date.getDate()
    document.querySelector("#date").textContent  = `${day < 10 ? `0${day}` : day}/${month < 10 ? `0${month}` : month}/${date.getFullYear()}`
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
            first = document.querySelector("#first");
            if (first) {
                $("#painelAtendimento").html(result);
                fullScreenEventKey()
                firstResult = document.querySelector("#first");
                if (first.dataset.value != firstResult.dataset.value) {
                    addBlink()
                    setTimeout(removeBlink, 3000)
                } 
                // else {
                //     /* Apenas para testes */
                //     addBlink()
                //     setTimeout(removeBlink, 3000)
                // }
            } else {
                $("#painelAtendimento").html(result);
                fullScreenEventKey()
            }
            
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //TODO::
        },
    });
}

function addBlink() {
    document.querySelectorAll(".ag-atendimento")[0].classList.add("blink")
    //document.querySelectorAll(".ag-atendimento").forEach(c => c.classList.add("blink"))
}

function removeBlink() {
    document.querySelectorAll(".ag-atendimento")[0].classList.remove("blink")
    //document.querySelectorAll(".ag-atendimento").forEach(c => c.classList.remove("blink"))
}


getAtendimentos()

setInterval(getAtendimentos, 5000);

document.addEventListener('fullscreenchange', fullScreenEventKey);

document.querySelector("#btnFullScreen").addEventListener("click", fullScreenEventButton)

function fullScreenEventKey() {
    if (!document.fullscreenElement) {
        document.querySelector("#btnFullScreenIcon").classList.add("fa-expand")
        document.querySelector("#btnFullScreenIcon").classList.remove("fa-compress")

        autoSize(false);
    } else {
        document.querySelector("#btnFullScreenIcon").classList.remove("fa-expand")
        document.querySelector("#btnFullScreenIcon").classList.add("fa-compress")

        autoSize(true);
    }
}

function fullScreenEventButton() {
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
}

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