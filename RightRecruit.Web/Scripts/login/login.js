/// <reference path="../libs/jquery-1.7.1.js" />
/// <reference path="../watermark/jquery.watermark.js" />
/// <reference path="../watermark/jquery.data.js" />
/// <reference path="../libs/jquery-ui-1.8.18.custom.min.js" />

$(function () {
    $("span[class='checkbox']").addClass("unchecked");

	$(".checkbox").click(function(){
        if($(this).children("input").attr("checked")){
			// uncheck
			$(this).children("input").attr({checked: ""});
			$(this).removeClass("checked");
			$(this).addClass("unchecked");
		}else{
			// check
			$(this).children("input").attr({checked: "checked"});
			$(this).removeClass("unchecked");
			$(this).addClass("checked");
		}
	});

    $("#login").watermark('User name');
    $("#password").watermark('Password');
    $("#menuDiv").hide();
    $("#loginButton").button();
    
});