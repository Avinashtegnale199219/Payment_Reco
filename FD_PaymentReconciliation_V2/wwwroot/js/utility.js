var stepGTag0 = false;
var stepGTag1 = false;
var stepGTag2 = false;
var stepGTag3 = false;
var stepGTag4 = false;

$(function () {
    //Number Only
    number_only();

    //Charactor Only
    charactor_only();

    //Only Char,Numbers and Space
    alphanumeric();

    //DisableCutCopyPaste();
    DisableAutoComplete();
});

//Slide Toggle
$('#btn-collapse').on('click', function () {
    $('#fd_Detail_Info').slideToggle();
    var symbol = $(this).text();
    if (symbol === '▼') {
        $(this).text('▲');
    } else {
        $(this).text('▼');
    }
});
//Ajax Code
//function ExtendedAjaxCall(sUrLWcfMethod, sParam, type, successCallBack, errorCallBack, beforeSendCall, completeCall, IsAsync) {
//    if (IsAsync == null) {
//        IsAsync = true;
//    }
//    if (errorCallBack == null) {
//        errorCallBack = OnError;
//    }
//    if (sParam != null && $.trim(sParam) != '') {
//        $.ajax({
//            url: sUrLWcfMethod,
//            dataType: "json",
//            async: IsAsync,
//            type: type,
//            data: JSON.stringify(sParam),
//            beforeSend: beforeSendCall,
//            contentType: "application/json; charset=utf-8",
//            success: successCallBack,
//            error: errorCallBack,
//            complete: completeCall
//        });
//    } else {
//        $.ajax({
//            url: sUrLWcfMethod,
//            dataType: "json",
//            type: type,
//            async: IsAsync,
//            beforeSend: beforeSendCall,
//            contentType: "application/json; charset=utf-8",
//            success: successCallBack,
//            error: errorCallBack,
//            complete: completeCall
//        });
//    }
//}
//function isAuthorize(event, jqXHR, settings) {
//    $.ajax({
//        url: 'Home.aspx/CheckValidUser',
//        dataType: "json",
//        type: 'GET',
//        beforeSend: function (request) {
//            var qbkey = sessionStorage.getItem('qbkey');
//            if (qbkey == '' || qbkey == null) {
//                event.abort();
//                request.abort();
//                window.location = 'AuthenticationError.aspx';
//            }
//            request.setRequestHeader("qbkey", qbkey);
//        },
//        async: false,
//        contentType: "application/json; charset=utf-8",
//        success: function (result) {
//            if (result.d == false) {
//                event.abort();
//                window.location = 'AuthenticationError.aspx';
//            }
//        },
//        error: function (e)
//        {
//            event.abort();
//            window.location = 'AuthenticationError.aspx';
//        }
//    });
//}
//function OnAuthorize(event, jqXHR, settings) {
//    $('#preloader').show();
//    isAuthorize(event, jqXHR, settings);
//}
//function OnError(jqXHR, textStatus, err) {
//    ExceptionLog(err, '');
//    if (jqXHR.status == 401) {
//        location = 'AuthenticationError.aspx';
//    }
//    $('#preloader').hide();
//}
//function OnComplete() {
//    $('#preloader').hide();
//}
//function beforeSendCall(event, jqXHR, settings) {
    
//    $('#preloader').show();

//    var qbkey = sessionStorage.getItem('qbkey');

//    event.setRequestHeader("qbkey", qbkey);

//    isAuthorize(event, jqXHR, settings);

//}

//Form Validation
function formValidator($form) {
    var isValid = true;
    $('' + $form + ' [val-type="text"]').each(function () {
        if (isValid) {
            isValid = ElementValidation($(this), 'text');
        } else {
            ElementValidation($(this), 'text');
        }
    });
    $('' + $form + ' [val-type="select"]').each(function () {
        if (isValid) {
            isValid = ElementValidation($(this), 'select');
        } else {
            ElementValidation($(this), 'text');
        }
    });
    $('' + $form + ' [val-type="check"]').each(function () {
        if (isValid) {
            isValid = ElementValidation($(this), 'check');
        } else {
            ElementValidation($(this), 'text');
        }
    });
    return isValid;
}
function ElementValidation(element, type) {
    var isValid = true;
    switch (type) {
        case 'text':
            isValid = VerifyText(element);
            break;

        case 'select':
            isValid = VerifyDropdown(element);
            break;

        case 'check':
            isValid = VerifyCheckbox(element);
            break;

        default:
            break;
    }

    return isValid;
}
function VerifyText(element) {
    var isValid = true;
    if ($(element).val() == '') {
        ShowError(element, 'required', $(element).attr('with-parent'))
        isValid = false;
    }
    else {
        if ($(element).attr('regex') != undefined) {
            if (!verifyInput($(element).val(), $(element).attr('regEx'))) {
                ShowError(element, 'Invalid ' + $(element).attr('name'), $(element).attr('with-parent'))
                isValid = false;
            }
                //Remove Error
            else {
                RemoveError(element, $(element).attr('with-parent'));
            }
        }
        else {
            RemoveError(element, $(element).attr('with-parent'));
        }
    }
    return isValid;
}
function ShowError(element, error, isErrorWithParent) {
    if (isErrorWithParent != undefined) {
        $(element).parent().siblings('.help-block').text(error);
    } else {
        $(element).siblings('.help-block').text(error);
    }
}
function RemoveError(element, isErrorWithParent) {
    if (isErrorWithParent != undefined) {
        $(element).parent().siblings('.help-block').text('');
    } else {
        $(element).siblings('.help-block').text('');
    }
}
function VerifyCheckbox(element) {
    var isValid = true;
    if (!$(element).prop('checked')) {
        ShowError(element, 'Please accept ' + $(element).attr('name'), $(element).attr('with-parent'))
        isValid = false;
    }
    else {
        RemoveError(element, $(element).attr('with-parent'));
    }
    return isValid;
}
function VerifyDropdown(element) {
    var isValid = true;

    if ($(element).val() == 'select') {
        ShowError(element, 'select ' + $(element).attr('name'), $(element).attr('with-parent'));
        isValid = false;
    }
    else {
        RemoveError(element, $(element).attr('with-parent'));
    }
    return isValid;
}

function verifyInput($input, $type) {
    var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    var phoneno = /^[6789]\d{9}$/;
    var passw = /^[A-Za-z0-9]+$/;
    var charactersOnly = /^[A-Za-z ]+$/;
    //var dob = /^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/;
    var dob = /^\d{4}\-(0?[1-9]|1[012])\-(0?[1-9]|[12][0-9]|3[01])$/;

    switch ($type) {
        case 'email':
            return mailformat.test($input);
            break;
        case 'mobile':
            return phoneno.test($input);
            break;
        case 'charactersOnly':
            return charactersOnly.test($input);
            break;
        case 'password':
            return passw.test($input);
            break;
        case 'dob':
            return dob.test($input);
            break;
        default:
            return false;
            break;
    }
}

//Client Side Error
function ExceptionLog(errormsg, errorsrc) {
    $.ajax({
        type: "POST",
        url: 'Error.aspx/ExceptionLog',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function (request) {
            var qbkey = sessionStorage.getItem('qbkey');
            if (qbkey == '' || qbkey == null) {
                event.abort();
                request.abort();
                window.location = 'AuthenticationError.aspx';
            }
            request.setRequestHeader("qbkey", qbkey);
        },
        data: JSON.stringify({ ErrorMsg: errormsg, Errsource: errorsrc })
    });
}

window.onerror = function (message, source, lineno) {
    ExceptionLog("Error Type : NonAjaxError; Error: " + message + " at line " + lineno + " in " + source, source);
}

function DisableCutCopyPaste() {
    if (!window.jQuery) {
        var inputElements = document.getElementsByTagName('input');
        for (var i = 0; i < inputElements.length; i++) {
            if (!inputElements[i].classList.contains('enablecutcopypaste')) {
                if (inputElements[i].type == 'text' || inputElements[i].type == 'password') {
                    inputElements[i].oncopy = function () { return false; };
                    inputElements[i].oncut = function () { return false; };
                    inputElements[i].onpaste = function () { return false; };
                }
            }
        }
    } else {
        $('input[type=text],input[type=password]').on("cut copy paste", function (e) {
            if (!$(this).hasClass('enablecutcopypaste')) {
                e.preventDefault();
            }
        });
    }
}

function DisableAutoComplete() {
    if (!window.jQuery) {
        var inputElements = document.getElementsByTagName('input');
        for (var i = 0; i < inputElements.length; i++) {
            if (inputElements[i].type == 'text' || inputElements[i].type == 'password') {
                inputElements[i].autocomplete = "off";
            }
        }
    } else {
        $('input[type=text],input[type=password]').attr("autocomplete", "off");
    }
}

function AppGTag(stepno) {

    if (stepno == 0) {
        if (!stepGTag0) {
            gtag('event', 'form start', {
                'event_category': 'fixed deposit quick buy',
                'event_label': 'step-1'
            });
            stepGTag0 = true;
        }
    } else if (stepno == 1) {
        if (!stepGTag1) {
            gtag('event', 'home', {
                'event_category': 'fixed deposit quick buy',
                'event_label': 'step-2'
            });
            stepGTag1 = true;
        }
    } else if (stepno == 2) {
        if (!stepGTag2) {
            gtag('event', 'fixed deposit configuration', {
                'event_category': 'fixed deposit quick buy',
                'event_label': 'step-3'
            });
            stepGTag2 = true;
        }
    } else if (stepno == 3) {
        if (!stepGTag3) {
            gtag('event', 'upload kyc', {
                'event_category': 'fixed deposit quick buy',
                'event_label': 'step-4',
            });
            stepGTag3 = true;
        }
    } else if (stepno == 4) {
        if (!stepGTag4) {
            gtag('event', 'payment request', {
                'event_category': 'fixed deposit quick buy',
                'event_label': 'step-5'
            });
            stepGTag4 = true;
        }
    }
}

function AppErrorTag(stepno, msg) {
    gtag('event', 'form error', {
        'event_category': 'fixed deposit quick buy',
        'event_label': stepno + ' | ' + msg
    });
}

function charactor_only() {
    $('.charactor_only').on("input", function () {
        var regexp = /[^a-zA-Z\' \b]/g;
        if ($(this).val().match(regexp)) {
            $(this).val($(this).val().replace(regexp, ''));
        }
    });
    $(".charactor_only").css("text-transform", "uppercase");


}

function alphanumeric() {
    $('.alphanumeric').on("input", function () {
        var regexp = /^[0-9a-zA-Z\s]+$/g;
        if ($(this).val().match(regexp)) {
            $(this).val($(this).val().replace(regexp, ''));
        }
    });
    $(".charactor_only").css("text-transform", "uppercase");


}

function number_only() {
    $('.number_only,.jq-dte-day,.jq-dte-month,.jq-dte-year').on("input", function () {
        var regexp = /[^0-9]/g;
        if ($(this).val().match(regexp)) {
            $(this).val($(this).val().replace(regexp, ''));
        }
    });
}

function ClearSession() {
    ExtendedAjaxCall('Home.aspx/ClearSession', null, 'POST', null, null, beforeSendCall, null);
}

$('.home-link').click(function () {
    ClearSession();
});