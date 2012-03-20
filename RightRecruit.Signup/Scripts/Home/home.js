/// <reference path="../libs/jquery-1.7.1.js" />
/// <reference path="../libs/jquery-ui-1.8.18.custom.min.js" />

$(function () {
    $("#mail").button().click(function () {
        alert('in button');
        $.ajax({
            url: 'http://localhost/rs/mail',
            type: 'POST',
            success: function () {
                alert('success');
            }
        });
    });
});