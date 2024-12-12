$(document).ready(function () {
    $('#lblPageName').text('Reconciliation - HDFC Soft Feed');
    $('#lblRecoUpload').text('');
   // bindDDL();
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
                    url: WA_FD_PAYMENT_RECO + "HDFCSoftFeedReconciliation/Upload_File",
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

//$("#ddlTempType").change(function () {
//    ChangeDLL();
//});

$('#btnCancel').click(function (e) {
    // $("#ddlPortal,#ddlMode").val('select');
    //$("#ddlTempType").val('0');
    $("#flvRecoUpload,#txtRemarks").val('');
    $('#lblRecoUpload').text('');
    //$('#dvfiletemplate').hide();
    //$("#ddlTempType").focus();
    MessageCenter('', '');
})

$('#btnProcess').click(function (e) {

    if (IsValid()) {

        var objBO = {
            TemplateType: "HDFCSoftFeed",
            FileName: $('#lblRecoUpload').text(),
            Remarks: $('#txtRemarks').val()
        }

        ExtendedAjaxCall('HDFCSoftFeedReconciliation/Process_Reco'
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
//function changeTemplate(TemplateType) {
    
//    var templatePath = "";
//    if (TemplateType == 'HDFCSoftFeed') {
//        templatePath = "../Content/Templates/HDFC_EXCEL_UPLOAD.xlsx";
//    }
//    //else if (TemplateType == 'BILL DESK') {
//    //    templatePath = "../Content/Templates/BillDesk.xlsx";
//    //}   
//    else {
//        $('#dvfiletemplate').hide();
//        return;
//    }
//    $("#atemplate").attr("href", templatePath);
//    $('#dvfiletemplate').show();
//}

//function bindDDL() {
    
//    BindDLL_AjaxCall('ddlTempType', 'HDFCSoftFeedReconciliation.aspx/Get_TemplateType', null, 'GET', null, null, beforeSendCall, OnComplete);
//}

//function validDDL() {
//    $(".help-block").text('');
//    var ddlTempType = $('#ddlTempType').val();
    
//    if (ddlTempType == 'select' || ddlTempType == null || ddlTempType == undefined) {
//        isvalid = false;
//        $('#ddlTempType').siblings(".help-block").text('Required field.!');
//    }
//    return isvalid
//}

//function ChangeDLL() {
//    $(".help-block").text('');

//    var ddlTempType = $('#ddlTempType').val();
   
//    if (ddlTempType == 'select' || ddlTempType == null || ddlTempType == undefined) {
//        $('#dvfiletemplate').hide();
//        return;
//    }
    
//    changeTemplate(ddlTempType);
//}

function IsValid() {
    $(".help-block").text('');
    MessageCenter('', '')
   
    var lblRecoUpload = $('#lblRecoUpload').text();
    var txtRemarks = $('#txtRemarks').val();

    isvalid = true;
  
    
    if (txtRemarks == '' || txtRemarks == null || txtRemarks == undefined) {
        isvalid = false;
         $('#txtRemarks').siblings(".help-block").text('Required field.!');
    }
    if (lblRecoUpload == '' || lblRecoUpload == null || lblRecoUpload == undefined) {
        isvalid = false;
        MessageCenter('Please upload file..!', 'error');
    }
    
    return isvalid
}



