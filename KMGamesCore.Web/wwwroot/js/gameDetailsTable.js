let table;
let saleId;

$(document).ready(function () {

    saleId = $("#SaleId").val();

    table = $("#gamesDetailsTable").DataTable({

        "ajax": {
            "url": `/Admin/Games/GetGamesDetails?saleId=${saleId}`
        },
        "columns": [
            {
                "data": "Image",
                "render": function (data) {
                    return `
                        <img src=${data} class="img-fluid"/>
                    `
                }
            },
            {
                "data": "Game"
            },
            { "data": "Developer.Developer" },
            {
                "data": "Release"
            },
            {
                "data": "Price",
                "render": function (data) {
                    return `$ ${data}`;
                }
            }
        ]
    });

});