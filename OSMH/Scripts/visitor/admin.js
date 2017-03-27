$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "./readAdmin",
        success: function (result) {
            console.log(result);

            var visitorLimit = result.visitorLimit;
            visitorLimit.VisitorLimit_start = visitorLimit.VisitorLimit_start + ":00";
            visitorLimit.VisitorLimit_end = visitorLimit.VisitorLimit_end + ":00";
            if (!visitorLimit.VisitorLimit_date) {
                var date = new Date();
                var today = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
                visitorLimit.VisitorLimit_date = today;
            }

            var today = new Vue({
                el: "#today",
                data: {
                    limitations: visitorLimit,
                    registered: result.visitorRegs
                }
            })

        }
    });
});