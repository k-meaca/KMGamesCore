let table;

$(document).ready(function () {

    table = $("#salesTable").DataTable({
        "ajax": {
            "url": "/Admin/Sales/GetSales"
        },
        "columns": [
            { "data": "PayPalId" },
            { "data": "ApplicationUser.Email" },
            { "data": "ApplicationUser.FirstName" },
            { "data": "ApplicationUser.LastName" },
            { "data": "Date" },
            {
                "data": "Total",
                "render": function (data) {
                    return `$ ${ data}`;
                }
            },
            {
                "data": "SaleId",
                "render": function (data) {
                    return `
                            <a href="/Admin/Sales/Details?saleId=${data}" class="btn btn-outline-info">
                                Details
                            </a>
                    `;
                }
            }
        ]
    })

});