$(document).ready(function () {
    var table = $("#movies").DataTable({
        ajax: {
            url: "/api/movies",
            dataSrc:""
        },
        columns: [
            {
                data: "Name",
                render: function (data, type, movie) {
                    return `<a href="/api/movies/edit/${movie.Id}">${movie.Name}</a>`;
                }
            },
            {
                data:"AssociatedGenere.Name"
            },
            {
                data: "Id",
                render: function (data) {
                    return `<button class="btn btn-link js-delete" data-movie-id="${data}">Delete</button>`
                }
            }
        ]
    });

    $("#movies").on('click touchend', '.js-delete', function () {
        var button = $(this);
        ShowDialog('Delete Confirmation', 'Are you sure you want to delete this movie?', function () {
            $.ajax({
                url: '/api/movies/' + button.attr('data-movie-id'),
                method: 'DELETE',
                success: function () {
                    table.row(button.parents('tr')).remove().draw();
                    HideConfirmDialog();
                }
            });
        });
    });
});