// common ajax functions
$(window).load(function () {
    $('#loading').hide();
});
var AllViews = (function () {
    return {
        get: get,
        post: post
    }

    function get(url, successCallback, failureCallback) {
        return this.sendRequest(url, 'GET', null, successCallback, failureCallback);
    }

    function post(url, jsonData, successCallback, failureCallback) {
        return this.sendRequest(url, 'POST', null, successCallback, failureCallback);
    }

    function sendRequest(url, method, jsonData, successCallback, failureCallback) {
        return $.ajax({
            type: method,
            contentType: 'application/json; charset-utf-8',
            url: url,
            data: jsonData,
            dataType: 'json',
            done: successCallback,
            fail: failureCallback
        })
    }
}());

// common ajax functions
//var AllViews = (function () {
//    return {
//        ajaxPost: ajaxPost
//    }

//    function ajaxPost(url, jsonData) {
//        return $.ajax({
//            type: "POST",
//            contentType: "application/json; charset=utf-8", //set content type
//            url: url,
//            data: jsonData,
//            async: false, //wheather to call async or not
//            dataType: "json"
//        });
//    }




//}());

$(document).ready(function () {    
    $('.navbar-inverse .navbar-right > li > a').mouseover(function () {
        $('.log_off_pop_up').css("display", "block");
    });
    $('.navbar-inverse .navbar-right > li > a').mouseout(function () {
        $('.log_off_pop_up').css("display", "none");
    });
    $('.log_off_pop_up').mouseover(function () {
        $(this).css("display", "block");
    });
    $('.log_off_pop_up').mouseout(function () {
        $(this).css("display", "none");
    });
});

