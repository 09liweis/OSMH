$(document).ready(function () {
	var regex = /\/((P|p)atient|(S|s)taff)Suggestion(\/Index|\/Details)?/;
	if (regex.test(location.pathname)) {
		var upvotebutton = $('button.suggestionItem_vote_upvote');
		var downvotebutton = $('button.suggestionItem_vote_downvote');
		var editComment = $('a.edit-comment');
		var deleteComment = $('a.delete-comment');
		var urlUpvote;
		var urlDownvote;
		var controller = location.pathname.match(regex)[1];
		if (controller === "patient" || controller === "Patient") {
			urlUpvote = "http://" + location.host+ "/PatientSuggestion/Upvote";
			urlDownvote = "http://" + location.host + "/PatientSuggestion/Downvote";
		} else if (controller === "staff" || controller === "Staff") {
			urlUpvote = "http://" + location.host + "/StaffSuggestion/Upvote";
			urlDownvote = "http://" + location.host + "/StaffSuggestion/Downvote";
		}
		$.each(upvotebutton, function (index, value) {
			$(value).on('click', function (event) {
				var id = $(this).data('id');
				var me = this;
				var votes;
				$.ajax({
					method: "POST",
					url: urlUpvote,
					data: { "id": id }
				})
					.done(function (json) {
						if (json.Success === true) {
							votes = json.Upvotes;
							$(me).siblings('p').text(votes);
							$(me).siblings('button').removeClass('suggestionNotDisplay');
							$(me).addClass('suggestionNotDisplay');
							if ($('p.number-of-vote').length > 0) {
								$('p.number-of-vote').text(votes);
							}
						} else {
							console.log("Something went wrong!")
						}
					});
			})
		})
		$.each(downvotebutton, function (index, value) {
			$(value).on('click', function (event) {
				var id = $(this).data('id');
				var userName = $(this).data('current');
				var me = this;
				var votes;
				$.ajax({
					method: "POST",
					url: urlDownvote,
					data: {
						id: id,
						userName: userName
					}
				})
					.done(function (json) {
						if (json.Success === true) {
							votes = json.Upvotes;
							$(me).siblings('p').text(votes);
							$(me).siblings('button').removeClass('suggestionNotDisplay');
							$(me).addClass('suggestionNotDisplay');
							if ($('p.number-of-vote').length > 0) {
								$('p.number-of-vote').text(votes);
							}
						} else {
							console.log("Something went wrong!");
						}
					});
			})
			$.each(editComment, function (index, value) {
				$(value).on('click', function (event) {
					event.preventDefault();
					$(value).parent().addClass('suggestionNotDisplay');
					$(value).parent().siblings('div.suggestionComment_detail').children('div.comment-detail').addClass('suggestionNotDisplay');
					$(value).parent().siblings('div.suggestionComment_detail').children('form.edit-comment-form').removeClass('suggestionNotDisplay');
				})
			})
		})
	}
});