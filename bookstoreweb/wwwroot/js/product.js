$(document).ready(function () {
    loadTables();
});




function loadTables() {
    dataTable = $('#tbldata').DataTable({
        "ajax": { url: '/admin/product/getall'},
        "columns": [
            { data: 'title', "width": '20%' },
            { data: 'isbn', "width": '20%' },
            { data: 'author', "width": '15%' },
            { data: 'price', "width": '10%' },
            { data: 'category.name', "width": '10%' },
            {
                data: 'productId',
                render: function (data) {
                    return `<div class = "w-75 btn-group" role = "group">
                    <a href = "product/upsert?id=${data}" class = " btn btn-primary mx-2"><i class=" bi bi-pencil-square"></i>Edit</a>
                    <a href = "product/delet?id=${data}" class = " btn btn-danger mx-2"><i class=" bi bi-trash-fill"></i>Delete</a>
                    </div>`
                }
            }

        ]
    });
}