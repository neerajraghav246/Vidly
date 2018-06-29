$(document).ready(function () {
    var table = $("#movies").DataTable({
        ajax: {
            url: "/api/movies",
            dataSrc:""
        },
        columns: [
            {
                data: "Name"
            },
            {
                data:"AssociatedGenere.Name"
            }
        ]
    });
});