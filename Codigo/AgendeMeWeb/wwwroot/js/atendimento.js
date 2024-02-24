document.querySelector("#btnFullScreen").addEventListener("click", (e, a) => {
    if (document.fullscreenElement) {
        document.querySelector("#btnFullScreenIcon").classList.add("fa-expand")
        document.querySelector("#btnFullScreenIcon").classList.remove("fa-compress")
        document.exitFullscreen();
    } else {
        document.querySelector("#btnFullScreenIcon").classList.remove("fa-expand")
        document.querySelector("#btnFullScreenIcon").classList.add("fa-compress")
        document.querySelector("body").requestFullscreen();
    }
})