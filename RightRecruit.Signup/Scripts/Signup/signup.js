/// <reference path="~/Scripts/libs/jquery-1.7.1.js" />
/// <reference path="~/Scripts/libs/jquery-ui-1.8.18.custom.min.js" />
/// <reference path="../watermark/jquery.watermark.js" />
/// <reference path="../libs/jquery.validate.unobtrusive.js" />

$(function () {
    $("#website").watermark('http://wwww.mycompany.com');

    $("#email").watermark('email@mycompany.com');

    $("#next").button();
});