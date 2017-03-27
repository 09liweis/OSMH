$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "./readAdmin",
        success: function (result) {
            result.VisitorLimit_start = result.VisitorLimit_start + ":00";
            result.VisitorLimit_end = result.VisitorLimit_end + ":00";
            if (!result.VisitorLimit_date) {
                var date = new Date();
                var today = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
                result.VisitorLimit_date = today;
            }
            var today = new Vue({
                el: '#today',
                data: {
                    message: result
                }
            })
        }
    });
});