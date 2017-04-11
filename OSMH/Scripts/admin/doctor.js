$(document).ready(function () {
    var dashboard = new Vue({
        el: '#admin-doctors',
        data: {
            doctor: {
                UserName: 'TestDoctor',
                FirstName: 'DFirst',
                LastName: 'DLast',
                Email: 'doctor@doctor.com',
                Password: 'humber',
                ConfirmPassword: 'humber',
                ProfileImage: '',
            },
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
            previewImage(e) {
                console.log(e.target.value);
            },
            submit() {
                this.$http.post('/Admin/CreateDoctor', this.doctor).then(function (result) {
                    console.log(result.data);
                })
            }
        }
    });
});