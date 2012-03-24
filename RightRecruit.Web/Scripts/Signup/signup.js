/// <reference path="~/Scripts/libs/jquery-1.7.1.js" />
/// <reference path="~/Scripts/libs/jquery-ui-1.8.18.custom.min.js" />
/// <reference path="../watermark/jquery.watermark.js" />

$(function () {
    $("#website").watermark('http://wwww.mycompany.com');

    $("#email").watermark('email@mycompany.com');

    $("#next").button({
        icons: {
            primary: 'ui-icon-disk'
        }
    }).click(function () {
        $("#overlay").show();
    });
});