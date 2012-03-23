/// <reference path="../libs/jquery-1.7.1.js" />
/// <reference path="../watermark/jquery.watermark.js" />
/// <reference path="../watermark/jquery.data.js" />
/// <reference path="../libs/jquery-ui-1.8.18.custom.min.js" />

$(function () {
    $("#login").watermark('User name');
    $("#password").watermark('Password');
    $("#menuDiv").hide();
    $("#loginButton").button().click(function () {
        $("#overlay").show();
    });

});