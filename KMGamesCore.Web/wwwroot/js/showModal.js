let buttons = document.querySelectorAll(".signIn");

const showModal = function (event) {
    $("#modalSignIn").modal('show');
}


buttons.forEach(button => {
    button.addEventListener("click", showModal);
})

