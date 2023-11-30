let countriesTable;

$(document).ready(function () {

    countriesTable = $('#tblCountries').DataTable({
        "ajax": {
            "url": "/Admin/Countries/GetCountries",
        },
        "columns": [
            { "data": "Country" },
            {
                "data": "CountryId",
                "render": function (data) {
                    return `
                        <a class="btn btn-outline-warning rounded-circle pt-1 pb-1 pe-2 ps-2" href="/Admin/Countries/Edit?id=${data}" >
                            <i class="bi bi-brilliance"></i>
                        </a>
                        <a class="btn btn-outline-danger ms-2 rounded-circle pt-1 pb-1 pe-2 ps-2" 
                                onclick=Delete('/Admin/Countries/DeleteCountry/${data}')>
                            <i class="bi bi-x-lg"></i>
                        </a>
                    `
                }
            }
        ]
    });

});

function Delete(url) {
    console.log(url);
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {

            // Ajax make a petition for url provided
            // and expect something for response
            

            $.ajax({
                "url": url,
                "type": 'DELETE',
                "success": function (data) {
                    console.log(data);
                    if (data.success) {
                        countriesTable.ajax.reload();
                        toastr.warning(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }

            });
        }
    })
};