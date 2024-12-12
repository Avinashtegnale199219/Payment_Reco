
$(document).ready(function () {
    $('#lblPageName').text('Offline Reconciliation Dashboard');
    bindData();

});

function bindData() {
    MessageCenter('', '');

    ExtendedAjaxCall('OfflineReconciliationDashBoard/GetReconciliationData', null, 'GET', function (result) {
        console.log(result);
        $("#tbody_OfflineRecoDashboardList").html('');
        if (result.data != '' && result.data != null) {
            
            if (result.status != null && result.status != "0" && result.data != null && result.data.length > 0) {
            //if ($.fn.DataTable.isDataTable('#tblReconDetails')) {
            //        $('#tblReconDetails').dataTable().fnDestroy();
            //    }
                $('#tblReconDetails > tbody').empty();
                $.each(result.Data, function (i, row) {
                    var tr = $("<tr/>");
                    tr.append("<td style='display:none'>" + row['id'] + "</td>");
                    tr.append("<td>" + row['hdrSeq'] + "</td>");
                    tr.append("<td>" + row['createdBy'] + "</td>");
                    tr.append("<td>" + row['createdDate'] + "</td>");
                    if (row['totalFileUploadedCount'] > 0) {

                        tr.append('<td class="text-center"><a class="vertical_top" href="javascript:DownloadReconUploadedData(\'' + row['hdrSeq'] + '\',\'' + row['downloadUploadedFileName'] + '\')"  ><img src="img/icon_download.png" height="20px" /></a> (' + row['totalFileUploadedCount'] + ')</td>');
                    } else {
                        tr.append("<td></td>");
                    }
                    if (row['exceptionCount'] > 0) {
                        tr.append('<td class="text-center" ><a class="vertical_top" href="javascript:DownloadReconExceptionData(\'' + row['hdrSeq'] + '\',\'' + row['downloadExceptionFileName'] + '\')"  ><img src="img/icon_download.png"  height="20px" /></a> (' + row['exceptionCount'] + ')</td>');
                    } else {
                        tr.append("<td></td>");
                    }
                    //if (row['RectificationCount'] > 0) {
                    //    tr.append('<td class="text-center"><a class="vertical_top" href="javascript:DownloadReconRectificationData(\'' + row['HdrSeq'] + '\',\'' + row['DownloadRectFileName'] + '\')" ><img src="/WA_FD_Payment_Reco/img/icon_download.png" height="20px"  /></a> (' + row['RectificationCount'] + ')</td>');
                    //} else {
                    //    tr.append("<td></td>");
                    //}
                    if (row['successFileCount'] > 0) {
                        tr.append('<td class="text-center"><a class="vertical_top" href="javascript:DownloadReconSuccessData(\'' + row['hdrSeq'] + '\',\'' + row['downloadSuccessFileName'] + '\')"  ><img src="img/icon_download.png" height="20px"  /></a> (' + row['successFileCount'] + ')</td>');
                    } else {
                        tr.append("<td></td>");
                    }
                    if (row['isProcessVisible']) {
                        tr.append('<td class="text-center"><a  href="javascript:ProcessReconData(\'' + row['hdrSeq'] + '\')" >Process</a></td>');
                    } else {
                        tr.append("<td></td>");
                    }
                    if (row['isCancelProcessing']) {
                        tr.append('<td class="text-center"><a href="javascript:CancelProcess(\'' + row['hdrSeq'] + '\')">Cancel</a></td>');
                    } else {
                        tr.append("<td></td>");
                    }
                    tr.append("<td>" + row['recoRemarks'] + "</td>");
                    $('#tblReconDetails').append(tr);
                });
                $('#tblReconDetails').DataTable({
                    initComplete: function () {
                        $("#preloader").hide();
                    },
                    //'columnDefs': [
                    //    //hide the second & fourth column
                    //    { 'visible': false, 'targets': [0] }
                    //],
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
    }, null, null, false, true);
} 

function DownloadReconUploadedData(hdrSeq, FileName){
   if (FileName != null && FileName != "") {
        DownloadFile(FileName);
    }

    else { 
        var objBO = { HdrReq: hdrSeq };
        ExtendedAjaxCall('OfflineReconciliationDashBoard/GetReconUploadDataFileByHdrSeq', objBO, 'POST', function (result) {
            if (result.data != null && result.data != null) {
               
                if (result.status == "1") {
                    var objectURL = InitFunction();
                    window.location.href = objectURL.uploadedData;
                    bindData();
                }
                else {
                    MessageCenter(result.data, "error");
                }
            }
        }, null, null, false, true);
    }
}  

function DownloadReconExceptionData(hdrSeq, FileName) {


    if (FileName != null && FileName.trim() != "") {
        DownloadFile(FileName);
    }
    else {
        var objBO = { HdrReq: hdrSeq };
        ExtendedAjaxCall('OfflineReconciliationDashBoard/GetReconExceptionDataFileByHdrSeq', objBO, 'POST', function (result) {
            if (result.data != null && result.data != null) {
               
                if (result.status == "1") {
                    var objectURL = InitFunction();
                    window.location.href = objectURL.uploadedException;
                    bindData();
                }
                else {
                    MessageCenter(result.data, "error");
                }
            }
        }, null, null, false, true);
    }
} 

function DownloadReconSuccessData(hdrSeq, FileName){
    if (FileName != null && FileName.trim() != "") {
        DownloadFile(FileName);
    }
    else {
        var objBO = { HdrReq: hdrSeq };
        ExtendedAjaxCall('/OfflineReconciliationDashBoard/GetReconSuccessDataFileByHdrSeq ', objBO, 'POST', function (result) {
            if (result.data != null && result.data != '') {
                // var dataResult = JSON.parse(result.d);

                if (result.status == "1") {
                    var objectURL = InitFunction();
                    window.location.href = objectURL.uploadedSuccess;
                    bindData();
                }
                else {
                    MessageCenter(result.data, "error");
                }
            }

        }, null, null, false, true);
    }
} 

function CancelProcess(hdrSeq) {

    if (confirm("Do you want to Cancel process?")) {
        
        var objBO = { HdrSequence: hdrSeq };
        ExtendedAjaxCall('OfflineReconciliationDashBoard/CancelReconciliationProcess', objBO, 'POST', function (result) {
            
            if (result.data != null && result.data != null) {
                // var dataResult = JSON.parse(result.d);
                if (result.status == "1") {
                    bindData();
                    MessageCenter(result.data, "success");

                }
                else {
                    MessageCenter(result.data, "error");
                }
            }
            else {
            }

        }, null, null, false, true);

    }

}

function ProcessReconData(hdrSeq) {

    if (confirm("Do you want to process the reconciliation data?")) {
        
        var objBO = { HdrSequence: hdrSeq };

        ExtendedAjaxCall('OfflineReconciliationDashBoard /ProcessReconciliationData', objBO, 'POST', function (result) {
            console.log(hdrSeq);
            
            if (result.data != null && result.data != '') {
                //    var dataResult = JSON.parse(result.d);
                if (result.status == "1") {

                    bindData();

                    MessageCenter(result.data, "success");
                }
                else {
                    MessageCenter(result.data, "error");
                }
            }
            else {
            }

        }, null, null, false, true);

    }

}  

function DownloadFile(FileName) {
    var objBO = { FileName: FileName };
    ExtendedAjaxCall('/CMSReconciliationDashBoard/SetFileName', objBO, 'POST', function (result) {
        console.log(result);


        if (result.status == "1") {
            var objectURL = InitFunction();
            window.location.href = objectURL.uploadedData;
            bindData();
        }
        else {
            MessageCenter(result.data, "error");
        }

    }, null, null, false, true);
}



