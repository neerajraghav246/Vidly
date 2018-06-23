$(document).ready(function () {
    var table=$("#customers").DataTable({
        ajax: {
            url: '/api/customers',
            dataSrc:""
        },
        columns: [
            {
                data: "Name",
                render: function (data, type, customer) {
                    return `<a href='/customers/edit/${customer.Id}'>${customer.Name}</a>`;
                }
            },
            {
                data: "AssociatedMembershipType.Name"
            },
            {
                data: "Id",
                render: function (data) {
                    return `<button class='btn btn-link js-delete' data-customer-id='${data}'>Delete</button>`
                }
            }
        ]
    });
    $("#customers").on("click touchend", ".js-delete", function () {
        var button = $(this);
        ShowDialog('Delete Confirmation', 'Are you sure you want to delete this customer?', function () {
            $.ajax({
                url: "/api/customers/" + button.attr("data-customer-id"),
                method: "DELETE",
                success: function () {
                    table.row(button.parents("tr")).remove().draw();
                    HideConfirmDialog();
                }
            });
        });
    });
});