//For Image Preview
function ShowImagePreview(imageUploader, previewImage) {
    if (imageUploader.files && imageUploader.files[0]) {
        var reader = new FileReader();
        reader.onload = function(e) {
            $(previewImage).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUploader.files[0]);
    }
}
//For Image Preview End

//For  client side validation
function jQueryAjaxPost(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var ajaxConfig = {
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            success: function (response) {
                if (response.success) {
                    $("#firstTab").html(response.html);
                    refreshAddNewTab($(form).attr('data-restUrl'), true);
                    //success Message
                    $.notify(response.message,"success");
                } else {
                    //error Message
                    $.notify(response.message, "error");
                }

            }
        }
        if ($(form).attr('enctype') == "multipart/form-data") {
            ajaxConfig["contentType"] = false;
            ajaxConfig["processData"] = false;
        }
        $.ajax(ajaxConfig);
    }
    return false;
}
//For  client side validation End

function refreshAddNewTab(resetUrl, showViewTab) {
    $.ajax
    ({
        type: 'GET',
        url: resetUrl,
        success: function(response) {
            $("#secondTab").html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Add New');
            if (showViewTab)
                $('ul.nav.nav-tabs a:eq(0)').tab('show');
        }
    });
}