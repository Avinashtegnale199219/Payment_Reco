$(document).ready(function () {
    $('#lblPageName').text('Reconciliation - Offline');
    $('#lblRecoUpload').text('');
    $('#dvfiletemplate').hide();
    bindDDL1();
});

//$('#flvRecoUpload').change(function (e) {

//    $('#lblRecoUpload').text('');
//    MessageCenter('', '')
//    var file = e.target.files[0];
//    var fileExtension = file.name.split('.');
//    var extension = ['xls', 'xlsx', 'XLS', 'XLSX'];
//    if (jQuery.inArray(fileExtension[1], extension) != -1) {
//        if (file.size > 0) {
//            if (window.FormData !== undefined) {
//                var data = new FormData();
//                data.append(file.name, file);
//                $.ajax({
//                    url: "Reconciliation_Handler.ashx",
//                    type: "POST",
//                    data: data,
//                    async: false,
//                    contentType: false,
//                    processData: false,
//                    beforeSend: beforeSendCall,
//                    success: function (result) {
//                        var res = JSON.parse(result);
//                        if (res != null) {

//                            if (res.Status == "1") {
//                                $('#lblRecoUpload').text(res.Data);
//                                MessageCenter('', '')
//                            }
//                            else {
//                                $('#lblRecoUpload').text('');
//                                MessageCenter(res.Data, 'error')
//                            }
//                        } else {
//                            MessageCenter('Something went wrong..!', 'error')
//                        }
//                        $('#flvRecoUpload').val('');
//                    },
//                    error: function () {
//                        MessageCenter(res.Data, 'error')
//                        $('#flvRecoUpload').val('');
//                    },
//                    complete: OnComplete
//                });
//            }
//            else {
//                MessageCenter('Please Upload Valid File..!', 'error')
//            }
//        } else {
//            MessageCenter('Please Upload Valid File..!', 'error')
//        }
//    } else {
//        MessageCenter('File is Not Valid.(Valid:.xls,.xlsx)..!', 'error')
//    }

//});

$("#ddlTempType").change(function () {
 ChangeDLL();
});

//old js function
function bindDDL() {
    ExtendedAjaxCall('/offlineReconciliation/Get_TemplateType', null, 'GET', function (result) {
    console.log(result);
    });
}

$('#btnProcess').click(function (e) {

    if (IsValid()) {
        console.log($('#ddlTempType').val());
        console.log($('#lblRecoUpload').text());
        console.log($('#txtRemarks').val());
        var objBO = {
            TemplateType: $('#ddlTempType').val(),
            //Portal: $('#ddlPortal').val(),
            //Mode: $('#ddlMode').val(),
            FileName: $('#lblRecoUpload').text(),
            Remarks: $('#txtRemarks').val()
        }

        ExtendedAjaxCall('/OfflineReconciliation/Process_Reco', objBO, 'POST', function (result) {

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
        $('#txtRemarks').siblings(".help-block").text('Required field.!');
    }
    if (lblRecoUpload == '' || lblRecoUpload == null || lblRecoUpload == undefined) {
        isvalid = false;
        MessageCenter('Please upload file..!', 'error')
    }

    return isvalid
}

$('#btnCancel').click(function (e) {
    // $("#ddlPortal,#ddlMode").val('select');
    $("#ddlTempType").val('select');
    $("#flvRecoUpload,#txtRemarks").val('');
    $('#lblRecoUpload').text('');
    $('#dvfiletemplate').hide();
    $("#ddlTempType").focus();
    MessageCenter('', '');
})


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

function changeTemplate(TemplateType) {

        var templatePath = "";
        if (TemplateType == 'ICICI') {
            templatePath = "~/Templates/HDFC.xlsx";
        }
        else if (TemplateType == 'HDFC') {
            templatePath = "~/Templates/HDFC.xlsx";
        }
        else if (TemplateType == 'SBI') {
            templatePath = "~/Templates/HDFC.xlsx";
        }
        else {
            $('#dvfiletemplate').hide();
            return;
        }
        $("#atemplate").attr("href", templatePath);
        $('#dvfiletemplate').show();
    }

function bindDDL1() {
            if ($("#ddlTempType").click()) {
            $('#ddlTempType').html('');
            $('#ddlTempType').append($("<option/>", {
                value: '0',
                text: '--Select--'
            }));
            ExtendedAjaxCall('/offlineReconciliation/Get_TemplateType', null, 'GET', function(result) {
               console.log(result.data);
                if (result.data) {
                    $.each(result.data, function (i, row) {
                        $('#ddlTempType').append($("<option/>", {
                            value: row.code,
                            text: row.dec
                        }));
                    });
                }
            }, null, null, function () {
                OnComplete();
            });
            }
        else {
            $('#ddlTempType').html('');
            $('#ddlTempType').append($("<option/>", {
                value: '0',
                text: '--Select--'
            }));
        }
        $("#dvfiletemplate").hide();
}

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
                    url: WA_FD_PAYMENT_RECO +"offlineReconciliation/Upload_File",
                    type: "POST",
                    data: data,
                    async: false,
                    contentType: false,
                    processData: false,
                    beforeSend: function () {
                    },
                    success: function (result) {
                        console.log(result);
                        //var res = JSON.parse(result);
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
        MessageCenter('File is Not Valid.(Valid:.xls,.xlsx)..!', 'error')
    }

});


//$('#flvRecoUpload').change(function (e) {
//    
//    $('#lblRecoUpload').text('');
//    MessageCenter('', '')
//    var file = e.target.files[0];
//    var fileExtension = file.name.split('.');
//    var extension = ['xls', 'xlsx', 'XLS', 'XLSX'];
//    if (jQuery.inArray(fileExtension[1], extension) != -1) {
//        if (file.size > 0) {
//            if (window.FormData !== undefined) {
//                var data = new FormData();
//                data.append(file.name, file);
//                $.ajax({
//                    url: "/offlineReconciliation/Upload_File",
//                    type: "POST",
//                    data: data,
//                    async: false,
//                    contentType: false,
//                    processData: false,
//                    beforeSend: function () {

//                    },
//                    success: function (data) {
//                        console.log(data);
//                        // var res = JSON.parse(result);
//                        if (data != null) {

//                            if (data == 1) {
//                                $('#lblRecoUpload').text(data);
//                                MessageCenter('', '')
//                            }
//                            else {
//                                $('#lblRecoUpload').text('');
//                                MessageCenter(data, 'error')
//                            }
//                        } else {
//                            MessageCenter('Something went wrong..!', 'error')
//                        }
//                        $('#flvRecoUpload').val('');
//                    },
//                    error: function () {
//                        MessageCenter(result.data, 'error')
//                        $('#flvRecoUpload').val('');
//                    },
//                    complete: OnComplete
//                });
//            }
//            else {
//                MessageCenter('Please Upload Valid File..!', 'error')
//            }
//        } else {
//            MessageCenter('Please Upload Valid File..!', 'error')
//        }
//    } else {
//        MessageCenter('File is Not Valid.(Valid:.xls,.xlsx)..!', 'error')
//    }

//});




