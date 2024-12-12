$(document).ready(function () {
    $('#lblPageName').text('CMS Reconciliation');
    $('#lblRecoUpload').text('');

    // bindDDL();
});

$('#btnCancel').click(function (e) {
    // $("#ddlPortal,#ddlMode").val('select');
    //$("#ddlTempType").val('select');
    $("#flvRecoUpload,#txtRemarks").val('');
    $('#lblRecoUpload').text('');
    //$('#dvfiletemplate').hide();
    //$("#ddlTempType").focus();
    MessageCenter('', '');
})  

//$('#btnProcess').click(function (e) {

//    if (IsValid()) {

//        var ReconciliationProcessBO = {
//            //TemplateType: $('#ddlTempType').val(),
//            //Portal: $('#ddlPortal').val(),
//            Mode: "CMS",
//            FileName: $('#lblRecoUpload').text(),
//            Remarks: $('#txtRemarks').val()
//        }

//        ExtendedAjaxCall('/CMSReconciliation/Process_Reco'
//            , { _ReconciliationProcessBO: ReconciliationProcessBO }
//            , 'POST'
//            , function (result) {

//                var res = JSON.parse(result.d);
//                if (res.Status == "1") {
//                    $('#btnCancel').click();
//                    MessageCenter(res.Data, 'success');
//                }
//                else {
//                    $('#btnCancel').click();
//                    MessageCenter(res.Data, 'error');
//                }

//            }
//            , null
//            , null
//            , OnComplete
//            , false
//        );
//    }
//})     

function IsValid() {
    $(".help-block").text('');
    MessageCenter('', '')
    //var ddlTempType = $('#ddlTempType').val();
    var lblRecoUpload = $('#lblRecoUpload').text();
    var txtRemarks = $('#txtRemarks').val();

    isvalid = true;
    //if (ddlTempType == 'select' || ddlTempType == null || ddlTempType == undefined) {
    //    isvalid = false;
    //    $('#ddlTempType').siblings(".help-block").text('Required field.!');
    //}
    if (txtRemarks == '' || txtRemarks == null || txtRemarks == undefined) {
        isvalid = false;
        $('#txtRemarks').siblings(".help-block").text('Required field.!');
    }
    if (lblRecoUpload == '' || lblRecoUpload == null || lblRecoUpload == undefined) {
        isvalid = false;
        MessageCenter('Please upload file..!', 'error')
    }

    return isvalid
} 

$('#flvRecoUpload').change(function (e) {
    
    $('#lblRecoUpload').text('');
    MessageCenter('', '');
    var file = e.target.files[0];
    var fileExtension = file.name.split('.');
    //var extension = ['xls', 'xlsx', 'XLS', 'XLSX'];
    var extension = ['csv', 'CSV'];
    if (jQuery.inArray(fileExtension[1], extension) != -1) {
        if (file.size > 0) {
            if (window.FormData !== undefined) {
                var data = new FormData();
                data.append(file.name, file);
                $.ajax({
                    url: WA_FD_PAYMENT_RECO +"CMSReconciliation/Upload_File",
                    type: "POST",
                    data: data,
                    async: false,
                    contentType: false,
                    processData: false,
                    beforeSend: function () {
                    },
                    success: function (result) {
                        console.log(result);
                        // var res = JSON.parse(result);
                        if (result != null) {

                            if (result.status == "1") {
                                $('#lblRecoUpload').text(result.data);
                                MessageCenter('', '')
                            }
                            else {
                                $('#lblRecoUpload').text('');
                                MessageCenter(result.data, 'error')
                            }
                        } else {
                            MessageCenter('Something went wrong..!', 'error')
                        }
                        $('#flvRecoUpload').val('');
                    },
                    error: function () {
                        MessageCenter(result.data, 'error')
                        $('#flvRecoUpload').val('');
                    },
                    complete: OnComplete
                });
            }
            else {
                MessageCenter('Please Upload Valid File..!', 'error')
            }
        } else {
            MessageCenter('Please Upload Valid File..!', 'error')
        }
    } else {
       
        MessageCenter('File is Not Valid.(Valid:.csv)..!', 'error')
    }

});

$('#btnProcess').click(function (e) {

    if (IsValid()) {
       // console.log($('#ddlTempType').val());
        console.log($('#lblRecoUpload').text());
        console.log($('#txtRemarks').val());
        var objBO = {
           // TemplateType: $('#ddlTempType').val(),
            //Portal: $('#ddlPortal').val(),
            //Mode: $('#ddlMode').val(),
            FileName: $('#lblRecoUpload').text(),
            Remarks: $('#txtRemarks').val()
        }

        ExtendedAjaxCall('/CMSReconciliation/Process_Reco', objBO, 'POST', function (result) {
            console.log(result);
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
            , false
            , false
        );
    }
})


