//function jQueryAjaxPost(form)
//{
//    var ajaxconfig = {
//        type: 'post',
//        url: form.action,
//        data: new FormData(form),
//        contentType: false,
//        processData: false,
//        dataType: 'html',
//        success: function (result)
//        {
//            alert("sajfhg");
//            console.log("Result is " + result);
//            $("#firsttab").html(result);
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        },
//        complete: function () {
//            alert("fghtrehtrh");
//        }
//    }
//    $.ajax(ajaxconfig);

//}





function jQueryAjaxPost(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var ajaxConfig = {
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (result) {
                if (result.success == true) {   
                    $("#firsttab").html(result.html);
                    refreshaddnewtab($(form).attr('data-resturl'), true);  
                }
                else {
                    //$.notify(response.message, "error");
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

function refreshaddnewtab(resetUrl, showViewTab) {
    $.ajax({
        type: 'GET',
        url: resetUrl,
        success: function (response) {
            $("#secondtab").html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Add New');
            if (showViewTab)
                $('ul.nav.nav-tabs a:eq(0)').tab('show');
        }
    });
}

function Edit(url) {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (response) {
            $("#secondtab").html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Edit');
            $('ul.nav.nav-tabs a:eq(1)').tab('show');
        }
    });
}

function Delete(url) {
    if (confirm('Are you sure to delete this record ?') == true) {
        $.ajax({
            type: 'POST',
            url: url,
            success: function (response) {
                if (response.success) {
                    $("#firsttab").html(response.html);
                    $.notify(response.message, "warn");
                    if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
                        activatejQueryTable();
                }
                else {
                    $.notify(response.message, "error");
                }
            }
        });
    }
}
