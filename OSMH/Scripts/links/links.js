$(document).ready(function () {
	var url = "http://" + location.host + "/staticpage/GetLinks";
	var links_servers_cure = $("#links_servers_cure");
	var links_servers_care = $("#links_servers_care");
	var links_patients = $("#links_patients");
	var links_visitors = $("#links_visitors");
	var links_abouts = $("#links_abouts"); 
	var links_resources = $("#links_resources");
	var links_additionals = $("#links_additionals");
	var links_legals = $("#links_legals");

	$.ajax({
		method: 'POST',
		url: url,
		success: displayLinks,
		error: function (xhr, textStatus, errorThrown) {
			console.log("Error: " + textStatus + " " + errorThrown);
		}
	})

	function displayLinks(data, status) {
		appendLinks(data.AdditionalLinks, links_additionals);
		appendLinks(data.LegalLinks, links_legals);
		appendLinks(data.ServiceCareLinks, links_servers_care);
		appendLinks(data.ServiceCureLinks, links_servers_cure);
		appendLinks(data.PatientsLinks, links_patients);
		appendLinks(data.VisitorsLinks, links_visitors);
		appendLinks(data.ResourcesLinks, links_resources);
		appendLinks(data.AboutLinks, links_abouts);
	}

	function appendLinks(dataArr, element) {
		if (dataArr && dataArr.length > 0) {
			dataArr.forEach(function (e) {
				var href = "http://" + location.host + "/staticpage/details/" + e['PageId'];
				var link = $('<li></li>');
				var a = $('<a></a>').text(e['PageTitle']).attr('href', href);
				link.append(a);
				element.append(link);
			})
		} else {
			return;
		}
	}

})