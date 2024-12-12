/*Form Validator*/
function formValidator($form) {
    var isInvalidAny = false;
    $('' + $form + ' [val-type]').each(function () {

        var element = $(this);

        if (!ElementValidation(element, $(element).attr('val-type'))) {
            isInvalidAny = true;
        }

    });
    if (isInvalidAny)
        return false;
    return true;
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

        case 'radio':
            isValid = VerifyRadio(element);
            break;

        case 'file':
            isValid = VerifyFile(element);
            break;

        default:
            break;
    }

    return isValid;
}
function VerifyText(element) {
    var isValid = true;
    if ($(element).val() == '' && $(element).attr('val-type') != undefined) {
        ShowError(element, $(element).attr('name') + ' is required', $(element).attr('with-parent'))
        isValid = false;
    }
    else {
        if ($(element).attr('regex') != undefined) {
            if (!verifyInput($(element).val(), $(element).attr('regex'))) {
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
function VerifyCheckbox(element) {
    var isValid = true;
    if (!$(element).prop('checked')) {
        ShowError(element, 'Please check ' + $(element).attr('name'), $(element).attr('with-parent'))
        isValid = false;
    }
    else {
        RemoveError(element, $(element).attr('with-parent'));
    }
    return isValid;
}
function VerifyDropdown(element) {
    var isValid = true;

    if ($(element).val() == "0") {
        ShowError(element, 'select ' + $(element).attr('name'), $(element).attr('with-parent'));
        isValid = false;
    }
    else {
        RemoveError(element, $(element).attr('with-parent'));
    }
    return isValid;
}
function VerifyRadio(element) {
    var isValid = true;
    var isSelectedAny = false;

    $('input[type=radio][name=' + element[0].name + ']').each(function (key, ele) {
        if ($(ele).prop('checked')) {
            isSelectedAny = true;
            return;
        }
    });

    if (!isSelectedAny) {
        ShowError(element, 'Please select ' + $(element).attr('name'), $(element).attr('with-parent'))
        isValid = false;
    }
    else {
        RemoveError(element, $(element).attr('with-parent'));
    }

    return isValid;
}
function VerifyFile(element) {
    var isValid = true;
    if ($(element).val() == '') {
        ShowError(element, $(element).attr('name') + ' is required', $(element).attr('with-parent'))
        isValid = false;
    }
    else {
        if ($(element).attr('regex') != undefined) {
            //var extension = file.substr((file.lastIndexOf('.') + 1));
            if (!verifyInput($(element).val(), $(element).attr('regex'))) {
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