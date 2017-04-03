$(document).ready(function () {
    new Vue({
        el: "#today",
        data: {
            todayDay: null,
            todayMax: null,
            todayStart: null,
            todayEnd: null,
            todayReg: 0,
            regStatues: "Can't register now."
        },
        mounted() {
            this.readToday();
        },
        methods: {
            readToday: function () {
                this.$http.post('./readAdmin').then(function (result) {
                    console.log(result.data);
                    this.todayMax = result.data.visitorLimit.VisitorLimit_max;
                    this.todayStart = result.data.visitorLimit.VisitorLimit_start + ":00";
                    this.todayEnd = result.data.visitorLimit.VisitorLimit_end + ":00";
                    this.todayReg = result.data.visitorRegs;
                    var date = new Date();
                    this.todayDay = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
                    var hour = date.getHours();
                    if (this.todayMax > this.todayReg && hour >= result.data.visitorLimit.VisitorLimit_start && hour < result.data.visitorLimit.VisitorLimit_end) {
                        this.regStatues = "You can register now!"
                    }
                });
            },
            regToday: function () {
                this.$http.post('./regVisitor').then(function (result) {
                    console.log(result.data);
                });
            }
        }
    })
})