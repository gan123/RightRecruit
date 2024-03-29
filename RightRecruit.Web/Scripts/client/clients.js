﻿/// <reference path="../libs/jquery-1.7.1.js" />
/// <reference path="../libs/jquery-ui-1.8.18.custom.min.js" />
/// <reference path="../libs/knockout.js" />
/// <reference path="../libs/knockout.mapping-latest.js" />

$(function () {
    $("#clients").toggleClass('menuItemClicked', true);
    $("#clients").children().toggleClass('menuItemAnchor', true);

    $("#addClient").button({
        icons: {
            primary: 'ui-icon-plus'
        }
    })
    .css('font-size', '9pt')
    .css('height', '23px')
    .click(function () {
        window.location = "client/create";
    });
});