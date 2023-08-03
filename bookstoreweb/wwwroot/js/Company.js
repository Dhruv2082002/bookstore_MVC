$(document).ready(function () {
    loadTables();
});




function loadTables() {
    dataTable = $('#tbldata_comp').DataTable({
        "ajax": { url: '/admin/company/getall'},
        "columns": [
            { data: 'name', "width": '20%' },
            { data: 'streetAddress', "width": '20%' },
            { data: 'city', "width": '15%' },
            { data: 'state', "width": '10%' },
            { data: 'phoneNumber', "width": '10%' },
            {
                data: 'id',
                render: function (data) {
                    return `<div class = "w-75 btn-group" role = "group">
                    <a href = "company/upsert?id=${data}" class = " btn btn-primary mx-2"><i class=" bi bi-pencil-square"></i>Edit</a>
                    <a href = "company/delet?id=${data}" class = " btn btn-danger mx-2"><i class=" bi bi-trash-fill"></i>Delete</a>
                    </div>`
                }
            }

        ]
    });
}