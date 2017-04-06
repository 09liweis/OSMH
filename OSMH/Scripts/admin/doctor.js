$(document).ready(function () {
    if ($('#admin-doctors').length > 0) {
        var dashboard = new Vue({
            el: '#admin-doctors',
            data: {
                UserName: '',
                FirstName: '',
                LastName: '',
                Email: '',
                Password: '',
                ConfirmPassword: '',
            },
            mounted() {
                this.getDoctors();
            },
            watch: {

            },
            methods: {
                getDoctors() {
                    this.$http.get('/Doctor/List').then(function (doctors) {
                        this.doctors = doctors.data;
                    });
                },
                cancelAppointment(appointment) {
                    this.$http.post('/Patient/CancelAppointment/' + appointment.AppointmentId).then(function (result) {
                        if (result.data == 'success') {
                            this.getAppointments();
                        }
                    });
                },
            }
        });
    }
});