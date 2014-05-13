$(document).ready(function () {
    $('#testFile').on("click", function () {
        var theData = $("#userForm").serialize();


        var form = $('form')[0];
        var formData = new FormData(form);

        $.ajax({
            type: "POST",
            url: "/Subtitle/Create",
            data: formData,
            contentType: false,
            cache: false,
            processData: false,
            success: function (response, status, xhr) {
                alert("Virkar");
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $.each(jsonReponse, function (key, error) {

                    $('input[name="' + error.key + '"]').after(' ' + error.errors);

                });

                $('#messageHolder').html('Some fields were incorrect, please fix and resubmit');

            }
        })
    })
})