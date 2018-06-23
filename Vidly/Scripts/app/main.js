function ShowDialog(title, body, callback) {
    var $VidlyConfirmationDialog = $("#VidlyConfirmationDialog");
    $VidlyConfirmationDialog.find('#VidlyModalTitle').text(title);
    $VidlyConfirmationDialog.find('.modal-body').html(body);
    $VidlyConfirmationDialog.find('#confirmButton').on('click touchend', function () {
        //disable buttons untill process complete
        $(this).attr("disabled", true).addClass("disabled");
        callback();
    });
    $VidlyConfirmationDialog.modal('show');
    $VidlyConfirmationDialog.find('#confirmButton').removeAttr("disabled").removeClass("disabled");
}

function HideConfirmDialog() {
    $("#VidlyConfirmationDialog").modal('hide');
}