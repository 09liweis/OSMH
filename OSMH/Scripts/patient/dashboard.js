$(document).ready(function () {
    if ($('#dashboard').length > 0) {
        var dashboard = new Vue({
            el: '#dashboard',
            data: {
                view: 'appointments',
                doctorId: null,
                date: null,
                timeslot: null,
                doctors: [],
                dates: [],
                timeslots: [],
                appointments: [],
            },
            mounted() {
                this.getAppointments();
                this.getDoctors();
            },
            watch: {
                doctorId: function (newId) {
                    this.doctorId = newId;
                    this.date = null;
                    if (this.doctorId != null) {
                        this.getDates();
                    }
                },
                date: function (newDate) {
                    this.date = newDate;
                    if (this.date != null) {
                        this.getTimeSlots();
                    }
                },
            },
            methods: {
                resetData() {
                    this.doctorId = null;
                    this.date = null;
                    this.timeslot = null;
                    this.dates = [];
                    this.timeslots = [];
                },
                changeView(view) {
                    this.view = view;
                },
                getAppointments() {
                    this.$http.get('/Patient/getAppointments').then(function (appointments) {
                        this.appointments = appointments.data.map(function (a) {
                            return { AppointmentId: a.Id, ScheduleId: a.Schedule_Id, Date: formatDate(a.Date), Time: renderFullTimeSlot(a.StartTime, a.EndTime), Doctor: a.FirstName }
                        });
                    });
                },
                getDoctors() {
                    this.$http.get('/Doctor/List').then(function (doctors) {
                        this.doctors = doctors.data;
                    });
                },
                getDates() {
                    this.$http.get('/Schedules/Doctor/' + this.doctorId).then(function (dates) {
                        this.dates = formatDates(dates.data);
                    });
                },
                bookAppointment() {
                    if (this.timeslot != null) {
                        this.$http.post('/Patient/BookAppointment/' + this.timeslot).then(function (result) {
                            if (result.data == 'success') {
                                this.resetData();
                                this.getAppointments();
                                this.changeView('appointments');
                            }
                        });
                    }
                },
                cancelAppointment(appointment) {
                    this.$http.post('/Patient/CancelAppointment/' + appointment.AppointmentId).then(function (result) {
                        if (result.data == 'success') {
                            this.getAppointments();
                        }
                    });
                },
                getTimeSlots() {
                    this.$http.get('/Schedules/TimeSlot/' + this.date + '?doctorId=' + this.doctorId).then(function (timeslots) {
                        this.timeslots = timeslots.data.map(function (t) {
                            return { Id: t.Id, time: renderFullTimeSlot(t.StartTime, t.EndTime) }
                        });
                    });
                },
            }
        });
    }
});