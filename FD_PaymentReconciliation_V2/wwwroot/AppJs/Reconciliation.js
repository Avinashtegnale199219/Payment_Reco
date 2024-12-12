
$(document).ready(function () {
    $('#lblPageName').text('Reconciliation - Online');
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
                    url: WA_FD_PAYMENT_RECO+"Reconciliation/Upload_File",
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
            //Portal: $('#ddlPortal').val(),
            //Mode: $('#ddlMode').val(),
            FileName: $('#lblRecoUpload').text(),
            Remarks: $('#txtRemarks').val()
        }
        
        ExtendedAjaxCall('/Reconciliation/Proccess_Reco'
            , objBO
            , 'POST'
            , function (result) {
                console.log(result);
                var res = JSON.parse(result);
                console.log(res);
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
            , false
            , false
        );
    }
})
function changeTemplate(TemplateType) {
    //function changeTemplate(portal, mode) {

    var templatePath = "";
    if (TemplateType == 'TPSL') {
        templatePath = "Templates/TPSL.xlsx";
    }
    else if (TemplateType == 'BILL DESK') {
        templatePath = "Templates/BillDesk.xlsx";
    }
    else if (TemplateType == 'PAYU') {
        templatePath = "Templates/PAYU.xlsx";
    }
    else if (TemplateType == 'CAMS') {
        //templatePath = "Templates/CAMSPay.xlsx";
        //Above Code Commented by Satish Pawar on 01 July 2022 
        //Below Code Added By Satish Pawar on 01 July 2022 (Created a template)
        templatePath = "Templates/CAMS.xlsx";
    }
    //Added By Satish Pawar on 11 Nov 2022
    else if (TemplateType == 'CAMS LAFD') {
        templatePath = "Templates/CAMS_LAFD.xlsx";
    }
    //if (TemplateType == 'Reward' && mode == 'Online') {
    //    templatePath = "../Content/Templates/Reward_Online.xlsx";
    //}
    //else if (portal == 'Reward' && mode == 'Offline') {
    //    templatePath = "../Content/Templates/Reward_Offline.xlsx";
    //}
    //else if (portal == 'Enterprise' && mode == 'Online') {
    //    templatePath = "../Content/Templates/Enterprise_Online.xlsx";
    //}
    //else if (portal == 'Enterprise' && mode == 'Offline') {
    //    templatePath = "../Content/Templates/Enterprise_Offline.xlsx";
    //}
    //else if (portal == 'QB' && mode == 'Online') {
    //    templatePath = "../Content/Templates/QB_Online.xlsx";
    //}
    //else if (portal == 'QB' && mode == 'Offline') {
    //    templatePath = "../Content/Templates/QB_Offline.xlsx";
    //}
    //else if (portal == 'BOTC' && mode == 'Online') {
    //    templatePath = "../Content/Templates/BOTC_Online.xlsx";
    //}
    //else if (portal == 'BOTC' && mode == 'Offline') {
    //    templatePath = "../Content/Templates/BOTC_Offline.xlsx";
    //}
    //else if (portal == 'BTP' && mode == 'Online') {
    //    templatePath = "../Content/Templates/BTP_Online.xlsx";
    //}
    //else if (portal == 'BTP' && mode == 'Offline') {
    //    templatePath = "../Content/Templates/BTP_Offline.xlsx";
    //}
    //else if (portal == 'BTP-CP' && mode == 'Online') {

    //    templatePath = "../Content/Templates/BTPCP_Online.xlsx";
    //}
    //else if (portal == 'BTP-CP' && mode == 'Offline') {
    //    templatePath = "../Content/Templates/BTPCP_Offline.xlsx";
    //}
    //else if (portal == 'CP' && mode == 'Online') {
    //    templatePath = "../Content/Templates/CP_Online.xlsx";
    //}
    //else if (portal == 'CP' && mode == 'Offline') {
    //    templatePath = "../Content/Templates/CP_Offline.xlsx";
    //}
    //else if (portal == 'CP-LCA' && mode == 'Online') {
    //    templatePath = "../Content/Templates/CPLCA_Online.xlsx";
    //}
    //else if (portal == 'CP-LCA' && mode == 'Offline') {
    //    templatePath = "../Content/Templates/CPLCA_Offline.xlsx";
    //}
    else {
        $('#dvfiletemplate').hide();
        return;
    }
    $("#atemplate").attr("href", templatePath);
    $('#dvfiletemplate').show();
}

function bindDDL() {
   
    //BindDLL_AjaxCall('ddlTempType', 'Reconciliation/Get_TemplateType', null, 'GET', null, null, false, false);
    ExtendedAjaxCall('/Reconciliation/Get_TemplateType', null, 'GET', function (result) {
        console.log(result);
        if (result != null && result != '' && result != '0' && result != '-1') {
            if (result.status=="1") {
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
    //var ddlPortal = $('#ddlPortal').val();
    //var ddlMode = $('#ddlMode').val();

    //isvalid = true;
    //if (ddlPortal == 'select' || ddlPortal == null || ddlPortal == undefined) {
    //    isvalid = false;
    //    $('#ddlPortal').siblings(".help-block").text('Required field.!');
    //}
    //if (ddlMode == 'select' || ddlMode == null || ddlMode == undefined) {
    //    isvalid = false;
    //    $('#ddlMode').siblings(".help-block").text('Required field.!');
    //}
    if (ddlTempType == 'select' || ddlTempType == null || ddlTempType == undefined) {
        isvalid = false;
        $('#ddlTempType').siblings(".help-block").text('Required field.!');
    }
    return isvalid
}

function ChangeDLL() {
    $(".help-block").text('');

    var ddlTempType = $('#ddlTempType').val();
    //var ddlPortal = $('#ddlPortal').val();
    //var ddlMode = $('#ddlMode').val();

    if (ddlTempType == 'select' || ddlTempType == null || ddlTempType == undefined) {
        $('#dvfiletemplate').hide();
        return;
    }
    //if (ddlPortal == 'select' || ddlPortal == null || ddlPortal == undefined) {
    //    $('#dvfiletemplate').hide();
    //    return;
    //}
    //if (ddlMode == 'select' || ddlMode == null || ddlMode == undefined) {
    //    $('#dvfiletemplate').hide();
    //    return;
    //}

    changeTemplate(ddlTempType);
    //changeTemplate(ddlPortal, ddlMode);
}

function IsValid() {
    $(".help-block").text('');
    MessageCenter('', '')
    var ddlTempType = $('#ddlTempType').val();
    //var ddlPortal = $('#ddlPortal').val();
    //var ddlMode = $('#ddlMode').val();
    var lblRecoUpload = $('#lblRecoUpload').text();
    var txtRemarks = $('#txtRemarks').val();

    isvalid = true;
    if (ddlTempType == '0' || ddlTempType == null || ddlTempType == undefined) {
        isvalid = false;
        $('#ddlTempType').siblings(".help-block").text('Required field.!');
    }
    //if (ddlPortal == 'select' || ddlPortal == null || ddlPortal == undefined) {
    //    isvalid = false;
    //    $('#ddlPortal').siblings(".help-block").text('Required field.!');
    //}
    //if (ddlMode == 'select' || ddlMode == null || ddlMode == undefined) {
    //    isvalid = false;
    //    $('#ddlMode').siblings(".help-block").text('Required field.!');
    //}
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



