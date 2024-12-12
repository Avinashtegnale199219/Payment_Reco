$(document).ready(function () {
    $('#lblPageName').text('Online Payment Reconciliation List');
    bindData();
    
    //$('#tblReconDetails').on('click', '.IsException', function (e) {
    //    console.log($(this));
    //    var HdrNo = $(this).attr('data-IsException');
    //    console.log(HdrNo);
        
    //});
    $('#tblReconDetails').on('click', '.TotalFileCountDownload', function (e) {
        console.log($(this));
         var HdrNo = $(this).attr('data-TotalFileCountDownload');
        console.log(HdrNo);
        GetTotalFileCountDownloadByHdrSeq(HdrNo)
    });
    $('#tblReconDetails').on('click', '.IsRecordAlreadyPresent', function (e) {
        console.log($(this));
        var HdrNo = $(this).attr('data-IsRecordAlreadyPresent');
        console.log(HdrNo);
        GetIsRecordAlreadyPresentDownloadByHdrSeq(HdrNo)
    });
    $('#tblReconDetails').on('click', '.IsException', function (e) {
        console.log($(this));
        var HdrNo = $(this).attr('data-IsException');
        console.log(HdrNo);
        GetRecordExceptionDownloadByHdrSeq(HdrNo)
    });

    $('#tblReconDetails').on('click', '.IsSuccess', function (e) {
        
        var filepath = $(this).attr('data-FilePath');
        var filename = $(this).attr('data-FileName');

        GetSuccessDownloadByHdrSeq(filepath, filename)
    });
    $('#tblReconDetails').on('click', '.IsRectification', function (e) {
        console.log($(this));
        var HdrNo = $(this).attr('data-IsRectification');
        console.log(HdrNo);
        GetRecordRectificationByHdrSeq(HdrNo)
    });
    $('#tblReconDetails').on('click', '.isProcessVisible', function (e) {
        if (confirm("Are you sure you want to Process this document ?") == true) {

            var HdrNo = $(this).attr('data-IsProcessVisible');
            ConfirmProcessOption(HdrNo)
        }
    });
    $('#tblReconDetails').on('click', '.isCancelProcessing', function (e) {
        if (confirm("Are you sure you want to Cancel this document?") == true) {

            var HdrNo = $(this).attr('data-IsCancelProcessing');
            ConfirmFileCancelOption(HdrNo)
        }
    });
});

function bindData() {
    MessageCenter('', '');

    ExtendedAjaxCall('/OnlinePaymentReconciliationList_RW/BindTechProcessGrid', null, 'GET', function (result) {
        console.log("result : ", result);
        //$("#tbody_onlinepaymentrecoList").html('');
        
        if (result.data != null && result.data != '') {
            //var dataResult = JSON.parse(result.d);

            if (result.status != null && result.status != "0" && result.data != null && result.data.length > 0) {
                console.log("Data : ", result.data);

                if ($.fn.DataTable.isDataTable('#tblReconDetails')) {
                    $('#tblReconDetails').dataTable().fnDestroy();
                }

                $('#tblReconDetails > tbody').html('');

                $.each(result.data, function (i, row) {
                    
                    var tr = $("<tr/>");
                    tr.append("<td class='text-center'>" + row['hdrSeq'] + "</td>");
                    tr.append("<td class='text-center'>" + row['createdBy'] + "</td>");
                    tr.append("<td class='text-center'>" + row['createdDate'] + "</td>");
                   
                    if (row['totalFileUploadedCount'] > 0) {
                        var TotalFileUploadedCount = row["totalFileUploadedCount"] == "0" ? "NA" : row["totalFileUploadedCount"];
                        tr.append("<td class='text-center'><a class='totalFileUploadedCount'><img src='img/icon_download.png' height='20px' class='TotalFileCountDownload' data-TotalFileCountDownload=" + row["hdrSeq"] + " /><label style='padding-left: 5px; font-weight: normal; font-size: 14px;'> (" + TotalFileUploadedCount + ") </label> </td>");
                    } else {
                        tr.append("<td class='text-center'>NA</td>");
                    }
                    if (row['isRecordAlreadyPresent'] > 0) {
                        var IsRecordAlreadyPresent = row["isRecordAlreadyPresent"] == "0" ? "NA" : row["isRecordAlreadyPresent"];
                        tr.append("<td class='text-center' ><a class='isRecordAlreadyPresent'><img src='img/icon_download.png'  height='20px' class='IsRecordAlreadyPresent' data-IsRecordAlreadyPresent=" + row["hdrSeq"] + " /><label style='padding-left: 5px; font-weight: normal; font-size: 14px;'> (" + IsRecordAlreadyPresent + ") </label></td>");
                    } else {
                        tr.append("<td class='text-center'>NA</td>");
                    }

                    if (row['isException'] > 0) {
                        var IsException = row["isException"] == "0" ? "NA" : row["isException"];
                        tr.append("<td class='text-center'><a class='isException'><img src='img/icon_download.png' height='20px' class='IsException' data-IsException=" + row["hdrSeq"] + " /><label style='padding-left: 5px; font-weight: normal; font-size: 14px;'> (" + IsException + ") </label></td>");
                    } else {
                        tr.append("<td class='text-center'>NA</td>");
                    }
                    if (row['isRectification'] > 0) {
                        var IsRectification = row["isRectification"] == "0" ? "NA" : row["isRectification"];
                        tr.append("<td class='text-center'><a class='isRectification'><img src='img/icon_download.png' height='20px' class='IsRectification'  data-IsRectification=" + row["hdrSeq"] + " /><label  style='padding-left: 5px; font-weight: normal; font-size: 14px;'> (" + IsRectification + ") </label> /></td>");
                    } else {
                        tr.append("<td class='text-center'>NA</td>");
                    }
                    if (row['isSuccessVisible'] > 0) {
                        var IsSuccessVisible = row["isSuccessVisible"] == "0" ? "NA" : row["isSuccessVisible"];
                        tr.append("<td class='text-center'><a class='isSuccessVisible'><img src='img/icon_download.png' height='20px' class='IsSuccess'  data-FilePath=" + row["downloadFilePath"] + "   data-FileName=" + row["downloadFileName"] + "><label style='padding-left: 5px; font-weight: normal; font-size: 14px;'> (" + IsSuccessVisible + ")</label></td>");
                    } else {
                        tr.append("<td class='text-center'>NA</td>");
                    }
                    if (row['isProcessVisible'] == true) {
                        tr.append("<td class='text-center'><a class='isProcessVisible'  data-IsProcessVisible=" + row["hdrSeq"] + " >Process</a></td>");
                    } else {
                        tr.append("<td></td>");
                    }
                    if (row['isCancelProcessing'] == true) {
                        tr.append("<td class='text-center'><a class='isCancelProcessing'  data-IsCancelProcessing=" + row["hdrSeq"] + ">Cancel</a></td>");
                    } else {
                        tr.append("<td></td>");
                    }
                    
                    $("#tbody_onlinepaymentrecoList").append(tr);
                });
                $('#tblReconDetails').DataTable({
                    initComplete: function () {
                        //$("#preloader").hide();
                    },

                    "order": [[0, "desc"]]
                });
            }
            else {
                $('#tblReconDetails > tbody').empty();
                $('#tblReconDetails  > tbody').append('<tr><td colspan="15"><center>Data Not found</center></td></tr>');

            }
        }
        else {
            $('#tblReconDetails > tbody').empty();
            $('#tblReconDetails  > tbody').append('<tr><td colspan="15"><center>Data Not found</center></td></tr>');

        }
    }, null, null, false, false);
}


function GetTotalFileCountDownloadByHdrSeq(HdrNo) {
   
    console.log(HdrNo);
    var objbo = {
        HdrSeq:HdrNo
    } 

    ExtendedAjaxCall('/OnlinePaymentReconciliationList_RW/GetUploadDataFileByHdrSeq', objbo, 'POST', function (result) {
        if (result.data != null && result.data != '') {
            if (result.status == "1") {
                var objectURL = InitFunction();
                window.location.href = objectURL.uploadedData;
                bindData();
            }
            else {
                MessageCenter(result.data, "error");
            }
        }
    }, null, null, false, false);

}

function GetIsRecordAlreadyPresentDownloadByHdrSeq(HdrNo) {

    console.log(HdrNo);
    var objbo = {
        HdrSeq: HdrNo
    }

    ExtendedAjaxCall('/OnlinePaymentReconciliationList_RW/GetAlreadyPresentDataFileByHdrSeq', objbo, 'POST', function (result) {
        if (result.data != null && result.data != '') {
            if (result.status == "1") {
                var objectURL = InitFunction();
                window.location.href = objectURL.alreadyUploaded;
                bindData();
            }
            else {
                MessageCenter(result.data, "error");
            }
        }
    }, null, null, false, false);

}

function GetRecordExceptionDownloadByHdrSeq(HdrNo) {
    console.log(HdrNo);
    var objbo = {
        HdrSeq: HdrNo
    }

    ExtendedAjaxCall('/OnlinePaymentReconciliationList_RW/GetReconExceptionDataFileByHdrSeq', objbo, 'POST', function (result) {
        
        if (result.data != null && result.data != '') {
            if (result.status == "1") {
                var objectURL = InitFunction();
                window.location.href = objectURL.uploadedException;
                bindData();
            }
            else {
                MessageCenter(result.data, "error");
            }
        }
    }, null, null, false, false);
}

function GetRecordRectificationByHdrSeq(HdrNo) {

    console.log(HdrNo);
    var objbo = {
        HdrSeq: HdrNo
    }

    ExtendedAjaxCall('/OnlinePaymentReconciliationList_RW/GetRecordRectificationByHdrSeq', objbo, 'POST', function (result) {
        if (result.data != null && result.data != '') {
            if (result.status == "1") {
                var objectURL = InitFunction();
                window.location.href = objectURL.uploadedRectification;
                bindData();
            }
            else {
                MessageCenter(result.data, "error");
            }
        }
    }, null, null, false, false);

}

function ConfirmProcessOption(HdrNo) {
    console.log(HdrNo);
    var objbo = {
        HdrSeq: HdrNo
    }

    ExtendedAjaxCall('/OnlinePaymentReconciliationList_RW/ProcessReconciliationData', objbo, 'POST', function (result) {
        if (result.data != null && result.data != '') {
            if (result.status == "1") {
                //var objectURL = InitFunction();
                //window.location.href = objectURL.uploadedRectification;
                bindData();
            }
            else {
                MessageCenter(result.data, "error");
            }
        }
    }, null, null, false, false);

}

function ConfirmFileCancelOption(HdrNo) {
    console.log(HdrNo);
    var objbo = {
        HdrSeq: HdrNo
    }

    ExtendedAjaxCall('/OnlinePaymentReconciliationList_RW/CancelReconciliationProcess', objbo, 'POST', function (result) {
        if (result.data != null && result.data != '') {
            if (result.status == "1") {
                //var objectURL = InitFunction();
                //window.location.href = objectURL.uploadedRectification;
                bindData();
            }
            else {
                MessageCenter(result.data, "error");
            }
        }
    }, null, null, false, false);

}

function GetSuccessDownloadByHdrSeq(filepath, filename) {
    
    console.log(filepath);
    console.log(filename);
    var objbo = {
        FileName: filename,
        FilePath: filepath
    }
    ExtendedAjaxCall('/OnlinePaymentReconciliationList_RW/GetReconSuccessDataFileByHdrSeq', objbo, 'POST', function (result) {
        
        console.log(result);

        if (result.data != null && result.data != '') {
            if (result.status == "1") {
                var objectURL = InitFunction();
                window.location.href = objectURL.uploadedSuccess;
                bindData();
            }
            else {
                MessageCenter(result.data, "error");
            }
        }
    }, null, null, false, false);
}