$(document).ready(function () {
    $("#confirm").click(function () {
        if (validateEmail(document.getElementById("email").value)) {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/Email/addEmail",
                data: JSON.stringify({
                    "Email": document.getElementById("email").value
                }),
                success: function (result) {
                    if (result.Success == "duplicate") {
                        document.getElementById("error").innerHTML = "Already subscribed us";
                    } else {
                        document.getElementById("error").innerHTML = "Success!";
                    }
                },
                error: function (request, status, error) {
                    console.log(request.responseText);
                }
            });
        } else {
            document.getElementById("error").innerHTML = "Not a valid email address";
        }
    });
})


//from http://stackoverflow.com/questions/46155/validate-email-address-in-javascript
function validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}