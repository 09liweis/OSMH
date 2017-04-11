$(document).ready(function () {
    if ($('#dashboard').length > 0) {
        var dashboard = new Vue({
            el: '#dashboard',
            data: {
                appointments: [],
                schedules: {},
                view: 'appointments',
            },
            mounted() {
                this.getAppointments();
                this.getSchedules();
            },
            watch: {
            },
            methods: {
                changeView(view) {
                    this.view = view;
                },
                getAppointments() {
                    this.$http.get('/Doctor/getAppointments').then(function (appointments) {
                        this.appointments = appointments.data.map(function (a) {
                            return { AppointmentId: a.Id, Date: formatDate(a.Date), Time: renderFullTimeSlot(a.StartTime, a.EndTime), Patient: a.FirstName + ' ' + a.LastName }
                        });
                    });
                },

                getSchedules() {
                    this.$http.get('/Doctor/getSchedules').then(function (schedules) {

                        schedules.data.map(function (s) {
                            var date = formatDate(s.Date);
                            var time = renderFullTimeSlot(s.StartTime, s.EndTime);
                            var timeSlot = {Id: s.Id, Time: time, Booked: s.Booked};
                            if (this.schedules.hasOwnProperty(date)) {
                                this.schedules[date].push(timeSlot);
                            } else {
                                this.schedules[date] = [timeSlot];
                            }
                        }.bind(this));
                    });
                }
            }
        });
    }
});