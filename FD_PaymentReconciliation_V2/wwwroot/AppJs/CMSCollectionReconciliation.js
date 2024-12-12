$(document).ready(function () {
    $('#lblPageName').text('CMS Collection Reconciliation');
    $('#lblRecoUpload').text('');
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
                    url: WA_FD_PAYMENT_RECO +"CMSCollectionReconciliation/Upload_File",
                    contentType: false,
                    processData: false,
                    data: data,
                    async: false,
                    beforeSend: function () {
                    },
                    success: function (result) {
                        console.log(result);
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

$('#btnCancel').click(function (e) {
    $("#flvRecoUpload,#txtRemarks").val('');
    $('#lblRecoUpload').text('');
    MessageCenter('', '');
})

$('#btnProcess').click(function (e) {

    if (IsValid()) {

        var objBO = {
            Mode: "CMS_Collection",
            FileName: $('#lblRecoUpload').text(),
            Remarks: $('#txtRemarks').val()
        }

        ExtendedAjaxCall('/CMSCollectionReconciliation/Process_Reco'
            , objBO
            , 'POST'
            , function (result) {
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
        MessageCenter('Please upload file..!', 'error')
    }

    return isvalid
}
