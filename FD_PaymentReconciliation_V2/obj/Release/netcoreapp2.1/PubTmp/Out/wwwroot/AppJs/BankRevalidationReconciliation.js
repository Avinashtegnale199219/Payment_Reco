


$(document).ready(function () {
    $('#lblPageName').text('Reconciliation - Bank Revalidation');
    $('#lblRecoUpload').text('');
    $('#dvfiletemplate').hide();
    bindDDL();
});

$('#flvRecoUpload').change(function (e) {

    $('#lblRecoUpload').text('');
    MessageCenter('', '')
    var file = e.target.files[0];
    var fileExtension = file.name.split('.');
    var extension = ['xls', 'xlsx', 'XLS', 'XLSX'];
    if (jQuery.inArray(fileExtension[1], extension) != -1) {
        if (file.size > 0) {
            if (window.FormData !== undefined) {
                var data = new FormData();
                data.append(file.name, file);
                $.ajax({
                    type: "POST",
                    url: WA_FD_PAYMENT_RECO + "BankRevalidationReconciliation/Upload_File",
                    contentType: false,
                    processData: false,
                    data: data,
                    async: false,
                    beforeSend: function () {
                    },
                    success: function (result) {

                        if (result.status == "1") {
                            $('#lblRecoUpload').text(result.data);
                            MessageCenter('', '');
                        }
                        else {
                            $('#lblRecoUpload').text('');
                            MessageCenter(result.data, 'error');
                        }
                    },
                    error: function () {
                        MessageCenter("Something went wrong.", "Red");
                    },
                    complete: function () {

                    }

                });
            }
            else {
                MessageCenter('Please Upload Valid File..!', 'error')
            }
        } else {
            MessageCenter('Please Upload Valid File..!', 'error')
        }
    } else {
        MessageCenter('File is Not Valid.(Valid:.xls,.xlsx)..!', 'error')
    }

});

$("#ddlTempType").change(function () {
    ChangeDLL();
});

$('#btnCancel').click(function (e) {
    // $("#ddlPortal,#ddlMode").val('select');
    $("#ddlTempType").val('0');
    $("#flvRecoUpload,#txtRemarks").val('');
    $('#lblRecoUpload').text('');
    $('#dvfiletemplate').hide();
    $("#ddlTempType").focus();
    MessageCenter('', '');
})

$('#btnProcess').click(function (e) {

    if (IsValid()) {

        var objBO = {
            TemplateType: $('#ddlTempType').val(),
            FileName: $('#lblRecoUpload').text(),
            Remarks: $('#txtRemarks').val()
        }

        ExtendedAjaxCall('BankRevalidationReconciliation/Proccess_Reco'
            , objBO
            , 'POST'
            , function (result) {

                var res = JSON.parse(result);
                if (res.Status == "1") {
                    $('#btnCancel').click();
                    MessageCenter(res.Data, 'success');
                }
                else {
                    $('#btnCancel').click();
                    MessageCenter(res.Data, 'error');
                }

            }
            , null
            , null
            , OnComplete
            , false
        );
    }
})

$('#btnBulkProcess').click(function (e) {

    if (IsValid()) {

        var objBO = {
            TemplateType: $('#ddlTempType').val(),
            FileName: $('#lblRecoUpload').text(),
            Remarks: $('#txtRemarks').val()
        }

        ExtendedAjaxCall('BankRevalidationReconciliation/ProcessBulkFile'
            , objBO
            , 'POST'
            , function (result) {

                var res = JSON.parse(result);
                if (res.Status == "1") {
                    $('#btnCancel').click();
                    MessageCenter(res.Data, 'success');
                }
                else {
                    $('#btnCancel').click();
                    MessageCenter(res.Data, 'error');
                }

            }
            , null
            , null
            , OnComplete
            , false
        );
    }
})
function changeTemplate(TemplateType) {

    var templatePath = "";
    if (TemplateType == 'DD_PAID_UNPAID_STATUS') {
        templatePath = "Templates/DD_PAID_UNPAID_STATUS.xls";
    }
    else if (TemplateType == 'NACH_SOFT_PAYMENT') {
        templatePath = "Templates/NACH_SOFT_PAYMENT.xls";
    }
    else if (TemplateType == 'WARRANT_PAID_UNPAID_STATUS') {
        templatePath = "Templates/WARRANT_PAID_UNPAID_STATUS.xls";
    }
    else if (TemplateType == 'NEFT_REJECTION') {
        templatePath = "Templates/NEFT_REJECTION.xls";
    }
    else if (TemplateType == 'DD_ISSUED_FOR_ACH_REJECT') {
        templatePath = "Templates/DD_ISSUED_FOR_ACH_REJECT.xls";
    }

    else {
        $('#dvfiletemplate').hide();
        return;
    }
    $("#atemplate").attr("href", templatePath);
    $('#dvfiletemplate').show();
}

function bindDDL() {

    ExtendedAjaxCall('BankRevalidationReconciliation/Get_TemplateType', null, 'GET', function (result) {
       
        if (result != null && result != '' && result != '0' && result != '-1') {
            if (result.status == "1") {
                if (result.data.length > 0) {

                    $.each(result.data, function (key, item) {

                        $("#ddlTempType").append($("<option></option>").val(item.code).html(item.dec));

                    });
                }
            }

        }
    }, null, null, false, false);
}
function validDDL() {
    $(".help-block").text('');
    var ddlTempType = $('#ddlTempType').val();

    if (ddlTempType == 'select' || ddlTempType == null || ddlTempType == undefined) {
        isvalid = false;
        $('#ddlTempType').siblings(".help-block").text('Required field.!');
    }
    return isvalid
}

function ChangeDLL() {
    $(".help-block").text('');

    var ddlTempType = $('#ddlTempType').val();

    if (ddlTempType == 'select' || ddlTempType == null || ddlTempType == undefined) {
        $('#dvfiletemplate').hide();
        return;
    }

    changeTemplate(ddlTempType);

}

function IsValid() {
    $(".help-block").text('');
    MessageCenter('', '')
    var ddlTempType = $('#ddlTempType').val();

    var lblRecoUpload = $('#lblRecoUpload').text();
    var txtRemarks = $('#txtRemarks').val();

    isvalid = true;
    if (ddlTempType == 'select' || ddlTempType == null || ddlTempType == undefined) {
        isvalid = false;
        $('#ddlTempType').siblings(".help-block").text('Required field.!');
    }

    if (txtRemarks == '' || txtRemarks == null || txtRemarks == undefined) {
        isvalid = false;
        //MessageCenter('Please Enter Remarks..!', 'error');
        $('#txtRemarks').siblings(".help-block").text('Required field.!');
    }
    if (lblRecoUpload == '' || lblRecoUpload == null || lblRecoUpload == undefined) {
        isvalid = false;
        MessageCenter('Please upload file..!', 'error');
    }

    return isvalid
}



