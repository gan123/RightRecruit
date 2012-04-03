/// <reference path="../libs/jquery-1.7.1.js" />
/// <reference path="../libs/jquery-ui-1.8.18.custom.min.js" />
/// <reference path="../libs/knockout.js" />
/// <reference path="../libs/knockout.mapping-latest.js" />
/// <reference path="../watermark/jquery.watermark.js" />

$(function () {
    $("#recruiters").toggleClass('menuItemClicked', true);
    $("#recruiters").children().toggleClass('menuItemAnchor', true);

    var rightrecruit = {};

    $("#add").button({
        icons: {
            primary: 'ui-icon-person'
        }
    }).css('font-size', '9pt').css('height', '23px').click(function () {
        rightrecruit.recruitersViewModel.addRecruiter();
    });

    $("form").html5formvalidation({});

    $("#proceed").button()
    .css('font-size', '9pt')
    .css('height', '23px');

    $("button.delete").button({
        icons: {
            primary: 'ui-icon-trash'
        },
        text: false
    }).css('width', '20px').css('height', '20px');

    rightrecruit.SaveModel = function () {
        var plan, endDate, recuiters;
    };

    rightrecruit.LineItem = function () {
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

    rightrecruit.recruitersViewModel = function () {
        var roles = ko.observableArray([]),
        plans = ko.observableArray([]),
        products = ko.observableArray([]),
        recruiters = ko.observableArray([]),
        plan = ko.observable(),
        monthlyEndDates = ko.observableArray([]),
        annualEndDates = ko.observableArray([]),
        endDates = ko.computed(function () {
            if (plan() == "Monthly") {
                console.log(monthlyEndDates);
                return monthlyEndDates();
            }
            else if (plan() == "Annual") {
                return annualEndDates();
            }
        }),
        endDate = ko.observable(),
        showEndDates = ko.computed(function () {
            if (plan() == "Monthly" || plan() == "Annual") {
                return true;
            }
            return false;
        }),
        grandTotal = ko.computed(function () {
            var total = 0;
            $.each(recruiters(), function () {
                total += this.cost();
            });
            return total == 0 ? 200 : total;
        }),
        addRecruiter = function () {
            rightrecruit.recruitersViewModel.recruiters.push(new rightrecruit.LineItem());
        },
        removeRecruiter = function (line) {
            rightrecruit.recruitersViewModel.recruiters.remove(line);
        };
        return {
            roles: roles,
            plans: plans,
            products: products,
            recruiters: recruiters,
            plan: plan,
            grandTotal: grandTotal,
            addRecruiter: addRecruiter,
            removeRecruiter: removeRecruiter,
            monthlyEndDates: monthlyEndDates,
            annualEndDates: annualEndDates,
            endDates: endDates,
            endDate: endDate,
            showEndDates: showEndDates
        }
    } ();

    rightrecruit.recruitersViewModel.proceed = function () {
        console.log('inside proceed');
        var saveData = new rightrecruit.SaveModel();
        saveData.plan = rightrecruit.recruitersViewModel.plan;
        saveData.endDate = rightrecruit.recruitersViewModel.endDate;
        saveData.recruiters = rightrecruit.recruitersViewModel.recruiters;
        console.log(saveData);
        console.log(ko.toJSON(saveData));
        $.ajax({
            url: "../admin/recruiters/save",
            type: "POST",
            data: ko.toJSON(saveData),
            contentType: 'application/json',
            beforeSend: function (jqXHR, settings) {
                $("#overlay").show();
            },
            success: function () {
                $("#overlay").hide();
            }
        });
    };

    var url = "../admin/recruiters/load";
    $.getJSON(
    url,
    null,
    function (data) {
        rightrecruit.recruitersViewModel.roles(data.Roles);
        rightrecruit.recruitersViewModel.plans(data.Plans);
        rightrecruit.recruitersViewModel.products(data.Products);
        rightrecruit.recruitersViewModel.monthlyEndDates(data.MonthlyEndDates);
        rightrecruit.recruitersViewModel.annualEndDates(data.AnnualEndDates);
        console.log('recruiters ' + data.Recruiters);
        if (data.Recruiters == null) {
            rightrecruit.recruitersViewModel.addRecruiter();
        }
        else {
            $.each(data.Recruiters, function (i, p) {
                rightrecruit.recruitersViewModel.recruiters.push(new rightrecruit.LineItem()
                .id(p.Id)
                .name(p.Name)
                .email(p.Email)
                .product(p.Product)
                .role(p.Role)
            );
            });
        }
    });

    ko.applyBindings(rightrecruit.recruitersViewModel);
});