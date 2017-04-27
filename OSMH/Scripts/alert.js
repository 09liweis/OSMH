$(document).ready(function () {
	var newAlert;
	var alertText;
	var alterHtml = $("<div id='activeAlert' class='alert alert-warning row' role='alert'></div>");
	var url = "http://" + location.host + "/Alert/GetActive";

	//The localhost's port may change
	function checkAlert() {
		$.ajax(
			{
				method: "POST",
				url: url,
				success: displayAlert,
				error: function (xhr, textStatus, errorThrown) {
					console.log("Error: " + textStatus + " " + errorThrown);
				}
			}
		)
	}

	function displayAlert(data, status) {
		if (data.Active === true) {
			alertText = "<div id='activeAlert'><h3 id='activeAlert_title' class='col-sm-6'>Warning: " + data.Title + "</h3><div id='activeAlert_body' class='col-sm-6'><p>" + data.Message + "</p><p>Published at: " + data.PublishingTime + "</p></div></div>";
			alterHtml.html(alertText);
			$("body").prepend(alterHtml);
		} else if (data.Active === false) {
			$("#activeAlert").remove();
		}
	}
	checkAlert();
	setInterval(checkAlert, 10000)
})