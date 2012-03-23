/// <reference path="../libs/jquery-1.7.1.js" />
/// <reference path="../libs/jquery-ui-1.8.18.custom.min.js" />

$(function () {

    $(".menuItem").click(function () {
        $(".menuItem").each(function () {
            $(this).toggleClass('menuItemClicked', false);
            $(this).children().toggleClass('menuItemClickedAnchor', true);
        });
        $(this).toggleClass('menuItemClicked', true);
        $(this).children().toggleClass('menuItemAnchor', true);
    });
});