$(document).ready(function () {
	var newAlert;
	var alertText;
	var alterHtml = $("<div id='activeAlert' class='alert alert-warning row' role='alert'></div>");

	//The localhost's port may change
	$.ajax(
		{
			method: "GET",
			url: "http://localhost:50367/Alert/GetActive"
		}
	).done(function (data) {
		if (data.Active === true) {
			alertText = "<h3 id='activeAlert_title' class='col-sm-6'>Warning: " + data.Title + "</h3><div id='activeAlert_body' class='col-sm-6'><p>" + data.Message + "</p><p>Published at: " + data.PublishingTime + "</p></div>";
			alterHtml.html(alertText);
			$("body").prepend(alterHtml);
		}
	});
});