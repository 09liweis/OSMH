$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "./readAdmin",
        success: function (result) {
            console.log(result);
            var visitorLimit = result.visitorLimit;
            oldMax = visitorLimit.VisitorLimit_max;
            newMax = visitorLimit.VisitorLimit_max;
            oldStart = visitorLimit.VisitorLimit_start + ":00";
            newStart = visitorLimit.VisitorLimit_start;
            oldEnd = visitorLimit.VisitorLimit_end + ":00";
            newEnd = visitorLimit.VisitorLimit_end;
            if (!visitorLimit.VisitorLimit_date) {
                var date = new Date();
                var today = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
            }

            var today = new Vue({
                el: "#today",
                data: {
                    today: today,
                    registered: result.visitorRegs,
                    editToday: false,
                    oldMax: oldMax,
                    oldStart: oldStart,
                    oldEnd: oldEnd,
                    newMax: newMax,
                    newStart: newStart,
                    newEnd: newEnd
                },
                methods: {
                    saveToday: function () {
                        this.oldMax = this.newMax;
                        this.oldStart = this.newStart + ":00";
                        this.oldEnd = this.newEnd + ":00";
                        this.editToday = false;
                        $.ajax({
                            type: "POST",
                            contentType: "application/json",
                            url: "./updateToday",
                            data: JSON.stringify({
                                "VisitorLimit_max": this.newMax,
                                "VisitorLimit_start": this.newStart,
                                "VisitorLimit_end": this.newEnd,
                                "VisitorLimit_date": new Date()
                            }),
                            error: function (request, status, error) {
                                console.log(request.responseText);
                            }
                        });
                    }
                }
            })

        }
    });
});