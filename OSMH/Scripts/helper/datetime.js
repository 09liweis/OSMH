function renderTimeslot(timeslot) {
    return timeslot.Hours + ':' + (timeslot.Minutes == 0 ? '00' : timeslot.Minutes);
}

function renderFullTimeSlot(start, end) {
    return renderTimeslot(start) + ' - ' + renderTimeslot(end);
}

function formatDates(data) {
    var dates = [];
    data.map(function (date) {
        var fd = formatDate(date);
        dates.push(fd);
    });
    return dates;
}

function formatDate(date) {
    var d = new Date(parseInt(date.substr(6)));
    var mm = d.getMonth() + 1;
    var month = (mm > 9 ? '' : '0') + mm;
    var dd = d.getDate();
    var day = (dd > 9 ? '' : '0') + dd
    var formatDate = d.getFullYear() + '-' + month + '-' + day;
    return formatDate;
}
