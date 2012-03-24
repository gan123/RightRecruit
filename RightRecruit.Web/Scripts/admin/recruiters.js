/// <reference path="../libs/jquery-1.7.1.js" />
/// <reference path="../libs/jquery-ui-1.8.18.custom.min.js" />
/// <reference path="../libs/knockout.js" />
/// <reference path="../libs/knockout.mapping-latest.js" />
/// <reference path="../watermark/jquery.watermark.js" />

$(function () {
    $("#recruiters").toggleClass('menuItemClicked', true);
    $("#recruiters").children().toggleClass('menuItemAnchor', true);

    $("#test").button().click(function () {
        recruitersViewModel.recruiters.push();
        console.log(ko.toJSON(recruitersViewModel.recruiters));
    });

    $("input.name[type=text]").watermark('Recruiter Name');
    $("input.email[type=text]").watermark('Recruiter Email');

    var LineItem = function () {
        var self = this;
        self.id = ko.observable();
        self.name = ko.observable();
        self.email = ko.observable();
        self.product = ko.observable();
        self.role = ko.observable();
        self.cost = ko.computed(function () {
            return self.product() == "Basic" ? 200 : self.product() == "Intermediate" ? 600 : 1000;
        });
    };

    var recruitersViewModel = {
        roles: ko.observableArray([]),
        plans: ko.observableArray([]),
        products: ko.observableArray([]),
        recruiters: ko.observableArray([])
    };

    var url = "../admin/load";
    $.getJSON(
    url,
    null,
    function (data) {
        recruitersViewModel.roles(data.Roles);
        recruitersViewModel.plans(data.Plans);
        recruitersViewModel.products(data.Products);
        //recruitersViewModel.recruiters(data.Recruiters);
        $.each(data.Recruiters, function (i, p) {
            recruitersViewModel.recruiters.push(new LineItem()
                .id(p.Id)
                .name(p.Name)
                .email(p.Email)
                .product(p.Product)
                .role(p.Role)
            );
        });
    });

    ko.applyBindings(recruitersViewModel);
});