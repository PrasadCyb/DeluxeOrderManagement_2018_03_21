$(document).ready(function () {
        //$(".chosen").chosen("destroy");
        $(".chosen").chosen({ max_selected_options: 1 });
        $(".chosen-deselect").chosen({ allow_single_deselect: true });
        $(".chosen").chosen().change();
        $(".chosen").trigger('liszt:updated');
    //

});

$(document).ready(function () {
    //$(".chosen").chosen("destroy");
    $(".chosen").chosen({ max_selected_options: 1 });
    $(".chosen-deselect").chosen({ allow_single_deselect: true });
    $(".chosen").chosen().change();
    $(".chosen").trigger('liszt:updated');
    //

});

function deleteButtonClick(userId) {
    var confirmStatus = confirm("Are you sure you want to delete?");
    if (confirmStatus) {
        var url = $("#deleteUserURL").val();
        var jsonData = JSON.stringify({ id: userId });
        var done = AllViews.post(url, jsonData, (response) => {
            if (response === 'true') {
                var indexUrl = $('#indexURL').val();
                window.location.href = indexUrl;
            }
        }, (error) => {
            alert('error: ' + error.message || error);
        });
    }
}

//function deleteButtonClick(userId) {
//    debugger;
//    var x = confirm("Are you sure you want to delete?");
//    if (x)
//    {
//        var url = $("#deleteUserURL").val();
//        var jsonData = JSON.stringify({ id: userId });
//        var promise = AllViews.ajaxPost(url, jsonData); //successcallback, 

//        promise.done(function (ajaxResponse) {
//            if (ajaxResponse == 'true'){
//                //var tr = $(this).closest('tr');
//                //tr.remove();
//                var indexUrl = $("#indexURL").val();
//                window.location.href = indexUrl;
//            }
//        });

//        promise.fail(function (jqXHR, textStatus, errorThrown) {
//            alert('error: ' + errorThrown)
//        });
//    }
//}