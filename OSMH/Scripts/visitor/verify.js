$(document).ready(function () {
    $("#submit").click(function () {
        var code = $("#code").val().trim();
        if (code == "") {
            $("#message").html("Code can't be empty");
        } else {
            $.ajax({
                type: "POST",
                url: "./verifyCode",
                data: { code: code },
                success: function (result) {
                    if (result.Success) {
                        $("#message").html("Success, code verified");
                    } else {
                        $("#message").html("Fail, code not valid");
                    }
                }
            });
        }
    });
})