var globalAccessToken = "access_token";
var globalProfileNamespace = "profile";

$(document).ready(function () {
	$(".dialog").click(function (e) {
		e.preventDefault();
		$("#photoModalCenter .modal-body").html("Загрузка...");
		$("#photoModalCenter .modal-body").load($(this).attr("href"));
		$("#photoModalCenter").modal("show");
	});

	$(".phone-number").click(function (e) {
		e.preventDefault();
		let href = $(this).attr("href");

		if (href !== "") {
			$(this).load(href);
			$(this).attr("href", "");
		}
	});
});