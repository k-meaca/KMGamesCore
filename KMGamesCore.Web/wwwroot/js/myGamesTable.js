let table;

$(document).ready(function () {

    table = $("#myGames").DataTable({
        "ajax": {
            "url" : "/Customer/PurchasedGames/GetMyGames"
        },
        "columns": [
            {
                "data": "Game.Image",
                "render": function (data) {
                    return `
                        <img src=${data} class="img-fluid"/>
                    `
                }
            },
            {
                "data": "Game.Game"
            },
            { "data": "Game.Developer.Developer" },
            {
                "data": "Purchased"
            },
            {
                "data": "GameId",
                "render": function (data) {
                    return `<a href="#" class="btn btn-outline-success">Play</a>`;
                }
            }
        ]
    })

});