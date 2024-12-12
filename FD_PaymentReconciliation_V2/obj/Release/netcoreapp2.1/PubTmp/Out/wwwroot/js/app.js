var reload = null;
var IsAjaxTokenRequired = false;

$(document).on('contextmenu', function () {
    return false;
});

$(document).ready(function () {
    DisableBackButton();
    window.onload = DisableBackButton;
    window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
    window.onunload = function () { void (0) }

    $('.submod').click(function () {
        var id = '';
        var submodcode = $(this).attr('submodcode');

        if (typeof (submodcode) != "undefined" && submodcode != null && submodcode.trim() != '') {
            id = submodcode;
        }
        else {
            id = this.id;
        }
        ExtendedAjaxCall('/Default/SaveMenuLog/' + id, null, 'GET', null, null, null, false, null, null, 'text');
    });

    $('#preloader').hide();
});

function SessionExpireAlert(timeout) {
    var seconds = timeout / 1000;
    setTimeout(function () {
        $('#sessionAlertModal>.modal-dialog>.modal-content>.modal-body>p').html("Your Session will Expire.Do you want to continue ?");
        $('#sessionAlertModal').modal('show');
    }, timeout - 30 * 1000);

    reload = setTimeout(function () {
        sessionStorage.clear();
        ExtendedAjaxCall('Default.aspx/Logout', null, 'GET', null, null, null, null);
        $('#sessionAlertModal').modal('hide');
        window.location = "../Error/Expired.aspx";
    }, timeout);
};

$('#btnModalYes').click(function () {
    clearTimeout(reload);
    reload = null;

    ExtendedAjaxCall('Default.aspx/Timout', null, 'GET',
        function (result) {
            var timeout = parseInt(result.d);
            var seconds = timeout / 1000;
            setTimeout(function () {
                $('#sessionAlertModal>.modal-dialog>.modal-content>.modal-body>p').html("Your Session will Expire.Do you want to continue ?");
                $('#sessionAlertModal').modal('show');
            }, timeout - 30 * 1000);

            reload = setTimeout(function () {
                sessionStorage.clear();
                ExtendedAjaxCall('Default.aspx/Logout', null, 'GET', null, null, null, null);
                $('#sessionAlertModal').modal('hide');
                window.location = "../Error/Expired.aspx";
            }, timeout);
        }, null, null, OnComplete);
});

$('#btnModalNo').click(function () {
    $('#sessionAlertModal').modal('hide');
    $('#lnkLogout').click();
    ExtendedAjaxCall('Default.aspx/Logout', null, 'GET', null, null, null, null, false);
    ResetSession();
});

function ResetSession() {
    window.location = window.location.href;
}

function ExceptionLog(errormsg) {
    var sUrLWcfMethod = '/Error/ExceptionLog';
    $.ajax({
        type: "POST",
        beforeSend: function (request) {
            request.setRequestHeader("X-CSRF-TOKEN", $('[name="__RequestVerificationToken"]').val());
        },
        url: sUrLWcfMethod,
        data: { 'ErrorMsg': errormsg }
    });
}

window.onerror = function (errorMsg, url, lineNumber, column, errorObj) {

    if (errorMsg == "'console' is undefined") {
        $('#preloader').hide();
        return;
    }

    $('#preloader').hide();

    ExceptionLog('Error Type : NonAjaxError; Error: ' + errorMsg + ' Script: ' + url + ' Line: ' + lineNumber
        + ' Column: ' + column + ' StackTrace: ' + errorObj);

    console.log('Error Type : NonAjaxError; Error: ' + errorMsg + ' Script: ' + url + ' Line: ' + lineNumber
        + ' Column: ' + column + ' StackTrace: ' + errorObj);
}

$('.mini').click(function () {
    var symbol = $(this).text();
    var target = $(this).attr('icr-height');
    var href = $(this).attr('href');
    if (symbol === '+') {
        $(this).text('-');
    } else {
        $(this).text('+');
    }

    if (typeof (target) !== 'undefined' && target !== '') {
        var hrefheight = $(href).height();
        var targetheight = $(target).parent('div').height();
        var incrheight = Number($(target).attr('tblheight'));

        if (symbol === '-') {
            $(target).parent('div').css('maxHeight', (targetheight + hrefheight + 30) + 'px');
            $(target).attr('tblheight', hrefheight);
        } else {
            $(target).parent('div').css('maxHeight', (targetheight - incrheight - 30) + 'px');
            $(target).attr('tblheight', 0);
        }
    }
});

function getQuerystring(key) {
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(window.location.href);
    return qs[1];
}

function ExtendedAjaxCall(sUrLWcfMethod, sParam, type, successCallBack, beforeSendCall, completeCall, IsAsync, IsTokenRequired, errorCallBack, dataType) {

    if (dataType == null) {
        dataType = "json";
    }

    if (IsAsync == null) {
        IsAsync = true;
    }

    if (beforeSendCall == null) {
        beforeSendCall = OnBeforeSendCall;
    }

    if (completeCall == null) {
        completeCall = OnComplete;
    }

    if (IsTokenRequired == null) {
        IsTokenRequired = false;
    }

    IsAjaxTokenRequired = IsTokenRequired;

    if (sParam != null && $.trim(sParam) != '') {
        $.ajax({
            url: sUrLWcfMethod,
            dataType: dataType,
            async: IsAsync,
            type: type,
            cache: false,
            data: JSON.stringify(sParam),
            beforeSend: beforeSendCall,
            contentType: "application/json; charset=utf-8",
            success: successCallBack,
            error: errorCallBack,
            complete: completeCall
        });
    } else {
        $.ajax({
            url: sUrLWcfMethod,
            dataType: dataType,
            type: type,
            cache: false,
            async: IsAsync,
            beforeSend: beforeSendCall,
            contentType: "application/json; charset=utf-8",
            success: successCallBack,
            error: errorCallBack,
            complete: completeCall
        });
    }
}
function beforeSendCall() { }
function OnError(jqXHR, textStatus, err) {
    $('#preloader').hide();
    if (jqXHR.status == 401) {
        jqXHR.abort();
        location = '../Error/Authorization';
    }
    console.error(jqXHR);
    console.error('Error Status : ' + textStatus);
    console.error('Error : ' + err);
}

function OnComplete() {
    $('#preloader').hide();
}

function OnBeforeSendCall(request) {

    if (IsAjaxTokenRequired) {

        var tokens = GetToken();

        if (tokens != null) {
            //request.setRequestHeader("Authorization", 'Bearer ' + tokens);
            request.setRequestHeader("Authorization", tokens);
        }
    }
    else {
        request.setRequestHeader("X-CSRF-TOKEN", $('[name="__RequestVerificationToken"]').val());
    }
    $('#preloader').show();
}

$('#lnkLogout,#lnkHome').click(function () {
    sessionStorage.clear();
});

function MessageCenter(message, type) {
    if ($('#lblErrormsg').hasClass('basic-text')) {
        $('#lblErrormsg').removeClass('basic-text');
    } else if ($('#lblErrormsg').hasClass('red-text')) {
        $('#lblErrormsg').removeClass('red-text');
    } else if ($('#lblErrormsg').hasClass('green-text')) {
        $('#lblErrormsg').removeClass('green-text');
    }
    if (type == 'success') {
        $('#lblErrormsg').addClass('green-text');
    } else if (type == 'error') {
        $('#lblErrormsg').addClass('red-text');
    } else {
        $('#lblErrormsg').addClass('basic-text');
    }
    $('#lblErrormsg').text(message);
}

function MainBtnHandler(add, edit, save, report, search, cancel, help) {
    if (!add) {
        DisableButton($("#btnAdd"));
    } else if ($("#btnAdd").hasClass('disabled')) {
        EnableButton($("#btnAdd"));
    }
    if (!edit) {
        DisableButton($("#btnEdit"));
    } else if ($("#btnEdit").hasClass('disabled')) {
        EnableButton($("#btnEdit"));
    }
    if (!save) {
        DisableButton($("#btnSave"));
    } else if ($("#btnSave").hasClass('disabled')) {
        EnableButton($("#btnSave"));
    }
    if (!report) {
        DisableButton($("#btnReport"));
    } else if ($("#btnReport").hasClass('disabled')) {
        EnableButton($("#btnReport"));
    }
    if (!search) {
        DisableButton($("#btnSearch"));
    } else if ($("#btnSearch").hasClass('disabled')) {
        EnableButton($("#btnSearch"));
    }
    if (!cancel) {
        DisableButton($("#btnCancel"));
    } else if ($("#btnCancel").hasClass('disabled')) {
        EnableButton($("#btnCancel"));
    }
    if (!help) {
        DisableButton($("#btnHelp"));
    } else if ($("#btnHelp").hasClass('disabled')) {
        EnableButton($("#btnHelp"));
    }
}

function DisableButton(obj) {
    obj.unbind('click');
    obj.bind("click", function (ev) {
        ev.stopImmediatePropagation();
    });
    obj.addClass('disabled');
}

function EnableButton(obj) {
    obj.unbind('click');
    obj.removeClass('disabled');
}

function ValidateCtrl(obj) {
    MessageCenter('', '');
    var IsValid = true;
    var _body = $(obj).attr('ctrl-body');

    $(_body + ' input.mandatory:text').each(function () {
        if ($(this).val().trim() == '') {
            if (typeof ($(this).attr('attr-error-msg')) === "undefined" || $(this).attr('attr-error-msg').trim() === '') {
                MessageCenter('Please enter a value', 'error');
            } else {
                MessageCenter($(this).attr('attr-error-msg').trim(), 'error');
            }
            IsValid = false;
            $(this).focus();
            $(this).addClass('red-border');
            return false;
        }
        else {
            $(this).removeClass('red-border');
        }
    });
    if (IsValid == true) {
        $(_body + ' select.mandatory').each(function () {
            var val = $(this).val();

            if (val != null) {
                val = $.trim(val);
            }
            var error_val = $(this).attr('error_val');

            if (error_val != null) {
                error_val = $.trim(error_val);
            }

            if ((val === null || val === '' || val === error_val) && typeof (error_val) !== "undefined") {
                if (typeof ($(this).attr('attr-error-msg')) === "undefined" || $(this).attr('attr-error-msg').trim() === '') {
                    MessageCenter('Please select something.', 'error');
                } else {
                    MessageCenter($(this).attr('attr-error-msg').trim(), 'error');
                }
                $(this).focus();
                $(this).addClass('red-border');
                IsValid = false;
                return false;
            }
            else {
                $(this).removeClass('red-border');
            }
        });
    }
    if (IsValid == true) {
        $(_body + ' textarea.mandatory').each(function () {
            if ($(this).val().trim() == '') {
                if (typeof ($(this).attr('attr-error-msg')) === "undefined" || $(this).attr('attr-error-msg').trim() === '') {
                    MessageCenter('Please enter a value', 'error');
                } else {
                    MessageCenter($(this).attr('attr-error-msg').trim(), 'error');
                }
                IsValid = false;
                $(this).focus();
                $(this).addClass('red-border');
                return false;
            }
            else {
                $(this).removeClass('red-border');
            }
        });
    }
    return IsValid;
}

//Loader
$('input[type=submit]').click(function () {
    $('#preloader').show();
});

function DisableCutCopyPaste() {
    if (!window.jQuery) {
        var inputElements = document.getElementsByTagName('input');
        for (var i = 0; i < inputElements.length; i++) {
            if (inputElements[i].type == 'text' || inputElements[i].type == 'password') {
                inputElements[i].oncopy = function () { return false; };
                inputElements[i].oncut = function () { return false; };
                inputElements[i].onpaste = function () { return false; };
            }
        }
    } else {
        $('input[type=text],input[type=password]').on("cut copy paste", function (e) {
            e.preventDefault();
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

function DisableBackButton() {
    window.history.forward()
}

function GetToken() {
    var token = null;
    try {

        var sUrLWcfMethod = '/Default/GetToken';

        $.ajax({
            type: "GET",
            beforeSend: function (request) {
                request.setRequestHeader("X-CSRF-TOKEN", $('[name="__RequestVerificationToken"]').val());
            },
            async: false,
            url: sUrLWcfMethod,
            success: function (result) {

                if (result != null && result.token != null && result.token != '') {

                    token = result.token;

                } else {
                    window.location.href = "Home/Expired";
                }

            }, error: function (rx) {
            }
        });
    } catch (e) {

    }
    return token;
}
