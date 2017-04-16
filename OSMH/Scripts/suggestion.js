$(document).ready(function () {
	var regex = /\/(P|p)atientSuggestion(\/Index|\/Details)?/;
	if (regex.test(location.pathname)) {
		var upvotebutton = $('button.suggestionItem_vote_upvote');
		var downvotebutton = $('button.suggestionItem_vote_downvote');
		var editComment = $('a.edit-comment');
		var deleteComment = $('a.delete-comment');
		$.each(upvotebutton, function (index, value) {
			$(value).on('click', function (event) {
				var id = $(this).data('id');
				var me = this;
				var votes;
				$.ajax({
					method: "POST",
					url: "http://localhost:50367/PatientSuggestion/Upvote",
					data: { id: id }
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
					url: "http://localhost:50367/PatientSuggestion/downvote",
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