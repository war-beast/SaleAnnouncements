var globalAccessToken = "access_token";
var globalProfileNamespace = "profile";
var globalWindowObject = window;

$(document).ready(function () {
	$.ajaxSetup({ cache: false });

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

	$(".message-btn").click(function (e) {
		e.preventDefault();
		$("#OfferOwnerId").val($(this).data("ownerid"));
		$("#OfferId").val($(this).data("offerid"));
	});


	$("#sendMessage").click(function (e) {
		e.preventDefault();
		clearSubmitValidationMessages();
		sendMessage();
	});
});

function sendRequest(url, method, data, sendControl, resolve) {
	$("#submittingError").html("");

	let sendButtonInitial = $(sendControl).html();
	$(sendControl).attr("disabled", "disabled");
	$(sendControl).html("<img src='/img/loader.gif' />");

	$.ajax({
		async: true,
		type: method,
		dataType: "json",
		contentType: "application/json;charset=utf-8",
		cache: false,
		url: url,
		data: JSON.stringify(data),
		complete: function (data) {
			if (data.status !== 200) {
				if (data.responseJSON !== undefined) {
					let errorValues = $.map(data.responseJSON.errors, function (v) {
						return v;
					}).join(", ");

					$("#submittingError").html(errorValues);
				} else {
					$("#submittingError").html(data.responseText);
				}
			}
			else if (data.statusText.toLocaleLowerCase() !== "error") {
				if (resolve === null) {
					location.reload();
				} else {
					if (data.responseJSON != null) {
						resolve(data.responseJSON);
					} else {
						resolve(data.responseText);
					}
				}
			}
			else {
				$("#submittingError").html(data.responseJSON.error_description);
			}
			$(sendControl).removeAttr("disabled");
			$(sendControl).html(sendButtonInitial);
		}
	});
}

function clearSubmitValidationMessages() {
	$("#submittingSuccess").text("");
	$("#submittingError").text("");
}

function sendMessage() {
	if ($("#Message").val() === "") {
		{
			$("#submittingError").text("Заполните текст сообщения");
			return;
		}
	}

	const data = {
		CurrentCustomerId: $("#CurrentCustomerId").val(),
		OfferOwnerId: $("#OfferOwnerId").val(),
		Message: $("#Message").val(),
		OfferId: $("#OfferId").val()
	}

	let showSuccessMessage = function () {
		$("#submittingSuccess").text("Сообщение отправлено успешно");
		$("#Message").val("");
	}

	sendRequest("/api/common/saveMessage", "Post", data, $("#sendMessage"), showSuccessMessage);
}