$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "./readAdmin",
        success: function (result) {
            console.log(result);
            var visitorLimit = result.visitorLimit,
                oldMax = visitorLimit.VisitorLimit_max,
                newMax = visitorLimit.VisitorLimit_max,
                oldStart = visitorLimit.VisitorLimit_start + ":00",
                newStart = visitorLimit.VisitorLimit_start,
                oldEnd = visitorLimit.VisitorLimit_end + ":00",
                newEnd = visitorLimit.VisitorLimit_end;
            var date = new Date();
            var now = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
            var today = new Vue({
                el: "#today",
                data: {
                    today: now,
                    registered: result.visitorRegs,
                    showToday: false,
                    todayButton: "Edit",
                    oldMax: oldMax,
                    oldStart: oldStart,
                    oldEnd: oldEnd,
                    newMax: newMax,
                    newStart: newStart,
                    newEnd: newEnd,
                    todayError: ""
                },
                methods: {
                    editToday: function () {
                        if (this.showToday === false) {
                            this.todayButton = "Close";
                            this.showToday = true;
                        } else {
                            this.todayButton = "Edit";
                            this.showToday = false;
                        }
                    },
                    saveToday: function () {
                        this.newMax = parseInt(this.newMax);
                        this.newStart = parseInt(this.newStart);
                        this.newEnd = parseInt(this.newEnd);
                        if (this.newMax >= 0) {
                            if (this.newStart >= 0 && this.newStart <= 23 && this.newEnd >= 0 && this.newEnd <= 23) {
                                this.oldMax = this.newMax;
                                this.oldStart = this.newStart + ":00";
                                this.oldEnd = this.newEnd + ":00";
                                this.todayButton = "Edit";
                                this.showToday = false;
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
                            } else {
                                this.todayError = "Visit Hours should between 0 - 23"
                            }
                        } else {
                            this.todayError = "Maximun Visitors must > 0";
                        }
                    }
                }
            });
        },
        error: function (request, status, error) {
            console.log(request.responseText);
        }
    });
    
    $("#datepicker").datepicker({
        dateFormat: "yy-mm-dd",
        onSelect: function (selectedDate) {
            $.ajax({
                type: "POST",
                url: "./searchDay",
                data: { date: selectedDate },
                success: function (result) {
                    console.log(result);
                    var specialMax = result.VisitorLimit_max,
                        specialStart = result.VisitorLimit_start + ":00",
                        specialEnd = result.VisitorLimit_end + ":00";
                    var special = new Vue({
                        el: "#special",
                        data: {
                            specialMax: specialMax,
                            specialStart: specialStart,
                            specialEnd: specialEnd
                        }
                    });
                },
                error: function (request, status, error) {
                    console.log(request.responseText);
                }
            });
        }
    });
});