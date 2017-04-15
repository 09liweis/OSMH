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
                oldEnd = visitorLimit.VisitorLimit_end + ":59",
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
                                if (this.newStart <= this.newEnd) {
                                    this.oldMax = this.newMax;
                                    this.oldStart = this.newStart + ":00";
                                    this.oldEnd = this.newEnd + ":59";
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
                                    this.todayError = "End hour must larger than start hour";
                                }
                            } else {
                                this.todayError = "Visit Hours should between 0 - 23";
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
                        specialEnd = result.VisitorLimit_end + ":59",
                        changeMax = result.VisitorLimit_max,
                        changeStart = result.VisitorLimit_start,
                        changeEnd = result.VisitorLimit_end;
                        specialDate = selectedDate;
                    var special = new Vue({
                        el: "#special",
                        data: {
                            specialMax: specialMax,
                            specialStart: specialStart,
                            specialEnd: specialEnd,
                            specialButton: "Edit",
                            specialDate: specialDate,
                            showSpecial: false,
                            changeStart: changeStart,
                            changeMax: changeMax,
                            changeEnd: changeEnd,
                            specialError: ""
                        },
                        methods: {
                            editSpecial: function () {
                                if (this.showSpecial === false) {
                                    this.specialButton = "Close";
                                    this.showSpecial = true;
                                } else {
                                    this.specialButton = "Edit";
                                    this.showSpecial = false;
                                }
                            },
                            saveSpecial: function () {
                                this.changeMax = parseInt(this.changeMax);
                                this.changeStart = parseInt(this.changeStart);
                                this.changeEnd = parseInt(this.changeEnd);
                                if (this.changeMax >= 0) {
                                    if (this.changeStart >= 0 && this.changeStart <= 23 && this.changeEnd >= 0 && this.changeEnd <= 23) {
                                        if (this.changeStart <= this.changeEnd) {
                                            this.specialMax = this.changeMax;
                                            this.specialStart = this.changeStart + ":00";
                                            this.specialEnd = this.changeEnd + ":59";
                                            this.specialButton = "Edit";
                                            this.showSpecial = false;
                                            $.ajax({
                                                type: "POST",
                                                contentType: "application/json",
                                                url: "./updateToday",
                                                data: JSON.stringify({
                                                    "VisitorLimit_max": this.changeMax,
                                                    "VisitorLimit_start": this.changeStart,
                                                    "VisitorLimit_end": this.changeEnd,
                                                    "VisitorLimit_date": this.specialDate
                                                }),
                                                error: function (request, status, error) {
                                                    console.log(request.responseText);
                                                }
                                            });
                                        } else {
                                            this.specialError = "End hour must larger than start hour";
                                        }
                                    } else {
                                        this.specialError = "Visit Hours should between 0 - 23";
                                    }
                                } else {
                                    this.specialError = "Maximun Visitors must > 0";
                                }
                            }
                        }
                    });
                },
                error: function (request, status, error) {
                    console.log(request.responseText);
                }
            });
        }
    });

    var preset = new Vue({
        el: "#preset",
        data: {
            presetDay: null,
            presetMax: null,
            presetStart: null,
            presetEnd: null,
            showDisplay: false,
            showPreset: false,
            presetButton: "Edit",
            presetError: "",
            finalMax: null,
            finalStart: null,
            finalEnd: null,
            presetId: null
        },
        methods: {
            searchPreset: function (num) {
                this.$http.post('./readPreset', { date: num }).then(function (result) {
                    console.log(result.data);
                    this.presetDay = IdToDay(result.data.VisitorLimit_id);
                    this.presetMax = result.data.VisitorLimit_max;
                    this.presetStart = result.data.VisitorLimit_start + ":00";
                    this.presetEnd = result.data.VisitorLimit_end + ":59";
                    this.presetId = result.data.VisitorLimit_id;
                    this.showDisplay = true;
                    this.finalMax = result.data.VisitorLimit_max;
                    this.finalStart = result.data.VisitorLimit_start;
                    this.finalEnd = result.data.VisitorLimit_end;
                });
            },
            editPreset: function () {
                if (this.showPreset === false) {
                    this.presetButton = "Close";
                    this.showPreset = true;
                } else {
                    this.presetButton = "Edit";
                    this.showPreset = false;
                }
            },
            savePreset: function () {
                this.finalMax = parseInt(this.finalMax);
                this.finalStart = parseInt(this.finalStart);
                this.finalEnd = parseInt(this.finalEnd);
                if (this.finalMax >= 0) {
                    if (this.finalStart >= 0 && this.finalStart <= 23 && this.finalEnd >= 0 && this.finalEnd <= 23) {
                        if (this.finalStart <= this.finalEnd) {
                            this.presetMax = this.finalMax;
                            this.presetStart = this.finalStart + ":00";
                            this.presetEnd = this.finalEnd + ":59";
                            this.presetButton = "Edit";
                            this.showPreset = false;
                            $.ajax({
                                type: "POST",
                                contentType: "application/json",
                                url: "./updatePreset",
                                data: JSON.stringify({
                                    "VisitorLimit_max": this.finalMax,
                                    "VisitorLimit_start": this.finalStart,
                                    "VisitorLimit_end": this.finalEnd,
                                    "VisitorLimit_id": this.presetId
                                }),
                                error: function (request, status, error) {
                                    console.log(request.responseText);
                                }
                            });
                        } else {
                            this.presetError = "End hour must larger than start hour";
                        }
                    } else {
                        this.presetError = "Visit Hours should between 0 - 23";
                    }
                } else {
                    this.presetError = "Maximun Visitors must > 0";
                }
            }
        }
    });
});

function IdToDay(num) {
    var result = "";
    switch (num) {
        case 0:
            result = "Sunday";
            break;
        case 1:
            result = "Monday";
            break;
        case 2:
            result = "Tuesday";
            break;
        case 3:
            result = "Wednesday";
            break;
        case 4:
            result =  "Thursday";
            break;
        case 5:
            result = "Friday";
            break;
        case 6:
            result = "Saturday";
            break;
    }
    return result;
}