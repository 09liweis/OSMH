$(document).ready(function () {
    new Vue({
        el: "#today",
        data: {
            todayDay: null,
            todayMax: null,
            todayStart: null,
            todayEnd: null,
            todayReg: 0,
            regStatues: "Can't register now.",
            showReg: false,
            showSuccess: false,
            successMessage: "Success, just display the email you received when you arraive!"
        },
        mounted() {
            this.readToday();
        },
        methods: {
            readToday: function () {
                this.$http.post('visitor/readAdmin').then(function (result) {
                    console.log(result.data);
                    this.todayMax = result.data.visitorLimit.VisitorLimit_max;
                    this.todayStart = result.data.visitorLimit.VisitorLimit_start + ":00";
                    this.todayEnd = result.data.visitorLimit.VisitorLimit_end + ":59";
                    this.todayReg = result.data.visitorRegs;
                    var date = new Date();
                    this.todayDay = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
                    var hour = date.getHours();
                    if (this.todayMax > this.todayReg && hour >= result.data.visitorLimit.VisitorLimit_start && hour < result.data.visitorLimit.VisitorLimit_end) {
                        this.regStatues = "You can register now!";
                        this.showReg = true;
                    }
                });
            },
            regToday: function () {
                this.$http.post('visitor/regVisitor', { email: document.getElementById("today-display-email").value }).then(function (result) {
                    if (result.data.Success == "true") {
                        this.showReg = false;
                        this.showSuccess = true;
                        this.todayReg += 1;
                        this.successMessage = "Success, just display the email you received when you arraive!";
                    } else if (result.data.Success == "duplicate") {
                        this.showReg = false;
                        this.showSuccess = true;
                        this.successMessage = "Sorry, one email can only register once per day";
                    }
                });
            }
        }
    })
})