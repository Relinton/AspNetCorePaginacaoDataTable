$(document).ready(function () {
    var generateCustomerTable = $("#ItemsTable")
        .dataTable({
            dom: 'Bfrtip',
            "processing": true,
            "serverSide": true,
            "ajax": {
                "url": "/api/items",
                "method": "POST"
            },
            "columns": [
                { "data": "itemId" },
                { "data": "nome" },
                { "data": "descricao" }
            ],
            "ordering": true,
            "paging": true,
            "pagingType": "full_numbers",
            "pageLength": 5,

            buttons: [
                'copy', 'csv', 'excel', 'pdf', 'print'
            ]
        })
});
