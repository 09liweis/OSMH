var x, y;
$(document).ready(function () {
    var regex = /\/(A|a)dmin(\/Index)?/;
    if (regex.test(location.pathname)) {
		var dheader = $('#dheader');
		var data_patient_suggestion = $('#data-patient-suggestion');
		var data_stuffs_uggestion = $('#data-stuff-suggestion');
		var data_vistors = $('#data-vistors');
		var data_blog = $('#data-blog');
		var data_job = $('#data-job');
		var data_account = $('#data-account');
		var data_message = $('#data-message');
		var data_email = $('#data-email');
		var data_testimonials = $('#data-testimonials');
		var date = new Date();
		dheader.text(date.toDateString());

		$('.feature-data').on("mouseover", function (e) {
			var hightLight = $(e.currentTarget).children('.new');
			if (!hightLight.hasClass('nondisplay')) {
				hightLight.addClass('nondisplay');
			}
		})

		var oldData = {
			AccountsTotal: parseInt(data_account.text().trim()),
			BlogTotal: parseInt(data_blog.text().trim()),
			MessageTotal: parseInt(data_message.text().trim()),
			PatientSuggestions: parseInt(data_patient_suggestion.text().trim()),
			StuffSuggestions: parseInt(data_stuffs_uggestion.text().trim()),
			TestimonialsTotal: parseInt(data_testimonials.text().trim()),
			VistorsTotal: parseInt(data_vistors.text().trim()),
			JobTotal: parseInt(data_job.text().trim()),
			EmailSubscribers: parseInt(data_email.text().trim()),
		}

		var links = {
			AccountsTotal: data_account,
			BlogTotal: data_blog,
			MessageTotal: data_message,
			PatientSuggestions: data_patient_suggestion,
			StuffSuggestions: data_stuffs_uggestion,
			TestimonialsTotal: data_testimonials,
			VistorsTotal: data_vistors,
			JobTotal: data_job,
			EmailSubscribers: data_email,
		}

		function displayNewData(element, data) {
			element.parent().siblings().removeClass('nondisplay');
			element.text(data);
		}

        function getData() {
			$.ajax({
				method: 'POST',
				url: "./Admin/ReadData",
				success: displayData,
				error: function (xhr, textStatus, errorThrown) {
					console.log("Error: " + textStatus + " " + errorThrown);
				}
			})
		}

		function objIsEqual(obj1, obj2) {
			var compare = true;
			var results = [];
			for (var key1 in obj1) {
				for (var key2 in obj2) {
					compare = obj1.hasOwnProperty(key2);
					if (!compare) {
						results.push(compare);
						return results;
					}
					if (key1 === key2) {
						var temp = obj1[key1] === obj2[key2];
						if (!temp) {
							results.push(key1);
						}
					}
				}
			}
			if (results.length > 0) {
				compare = false;
				results.unshift(compare);
			}
			return results;
		}

		function displayData(data, status) {
			var compare = objIsEqual(oldData, data);
			if (!compare[0]) {
				compare.shift();
				compare.forEach(function (e) {
					displayNewData(links[e], data[e]);
				})
				oldData = data;
			}
		}

		setInterval(getData, 10000);
	}
});