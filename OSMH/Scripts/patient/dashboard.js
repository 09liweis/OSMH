$(document).ready(function () {
    $('#selectDoctor').change(function () {
        var id = $(this).val();
        $.ajax({
            url: '/Schedules/Doctor/' + id,
            type: 'GET',
            success: function (data) {
                var dates = [];
                data.map(function (date) {
                    var d = new Date(parseInt(date.substr(6)));
                    var mm = d.getMonth() + 1;
                    var month = (mm > 9 ? '' : '0') + mm;
                    var dd = d.getDate();
                    var day = (dd > 9 ? '' : '0') + dd
                    var formatDate = d.getFullYear() + '-' + month + '-' + day;
                    dates.push(formatDate);
                });
                renderDates(dates);
            }
        });
    });

    $('#makeAppointment').on('change', '#changeDate', function () {
        var date = $(this).val();
        $.ajax({
            url: '/Schedules/TimeSlot/' + date + '?doctorId=' + $('#selectDoctor').val(),
            type: 'GET',
            success: function (data) {
                renderTimeslots(data);
            }
        });
    });

    $('#bookAppointment').on('click', function () {
        var scheduleId = $('input[name=timeslot]:checked').data('schedule');
        $.ajax({
            url: '/Patient/BookAppointment/' + scheduleId,
            type: 'POST',
            success: function (data) {
                console.log(data);
            }
        });
    });

    $('#selectTime').on('change', 'input[name=timeslot]:checked', function () {
        console.log($(this).data('schedule'));
    });

    function renderDates(dates) {
        var datesSelector = '<select id="changeDate"><option>Select a date</option>';
        dates.map(function (date) {
            datesSelector += '<option value="' + date + '">' + date + '</option>';
        });
        datesSelector += '</select>';
        $('#selectDate').html(datesSelector);
    }

    function renderTimeslots(timeslots) {
        var timeSelector = '';
        timeslots.map(function (time) {
            timeSelector += '<p><input class="timeslot" type="radio" name="timeslot" data-schedule="' + time.Id +  '" />' + renderTimeslot(time.StartTime) + ' - ' + renderTimeslot(time.EndTime) + '</p>';
        });
        $('#selectTime').html(timeSelector);
    }

    function renderTimeslot(timeslot) {
        return timeslot.Hours + ':' + (timeslot.Minutes == 0 ? '00' : timeslot.Minutes);
    }
});