/// <reference path="~/Scripts/libs/jquery-1.7.1.js" />
/// <reference path="~/Scripts/libs/jquery-ui-1.8.18.custom.min.js" />
/// <reference path="../libs/knockout.js" />
/// <reference path="../libs/jquery.html5formvalidation.js" />

$(function () {
    var rightrecruit = {};

    rightrecruit.Agency = function () {
        var CompanyName = ko.observable(),
        AdminId = ko.observable(),
        Website = ko.observable(),
        Email = ko.observable(),
        Phone = ko.observable();
        return {
            CompanyName: CompanyName,
            AdminId: AdminId,
            Website: Website,
            Email: Email,
            Phone: Phone
        };
    } ();

    rightrecruit.Agency.Signup = function () {
        $("form").html5formvalidation({
            onFail: function () {
                alert('invalid');
            },
            onSuccess: function () {
                console.log(ko.toJSON(rightrecruit.Agency));
                $.ajax({
                    url: "signup/proceed",
                    type: "POST",
                    data: ko.toJSON(rightrecruit.Agency),
                    contentType: 'application/json',
                    beforeSend: function (jqXHR, settings) {
                        $("#overlay").show();
                    },
                    success: function (data, textStatus, jqXHR) {
                        alert(textStatus);
                        window.location = "login";
                    }
                });
            }
        });
    };

    $("#signup").button({
        icons: {
            primary: 'ui-icon-disk'
        }
    });

    ko.applyBindings(rightrecruit.Agency);
});