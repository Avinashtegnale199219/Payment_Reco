$(document).ready(function () {
    $('#lblPageName').text('HDFC Soft Feed Reconciliation Dashboard');
    bindData();
});

function bindData() {
    MessageCenter('', '');

    ExtendedAjaxCall('HDFCSoftFeedReconciliationDashBoard/GetReconciliationData', null, 'GET', function (result) {
        if (result != null) {
            var dataResult = result;//JSON.parse(result);

            if (dataResult.status != null && dataResult.Status != "0" && dataResult.data != null && dataResult.data.length > 0) {


                if ($.fn.DataTable.isDataTable('#tblReconDetails')) {
                    $('#tblReconDetails').dataTable().fnDestroy();
                }
                $('#tblReconDetails > tbody').empty();
                $.each(dataResult.data, function (i, row) {
                    console.log(row);
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
    }, null, null, false, false);
}



function DownloadReconUploadedData(HdrSeq, FileName) {


    if (FileName != null && FileName != "") {
        DownloadFile(FileName);
    }
    else {
        ExtendedAjaxCall('HDFCSoftFeedReconciliationDashBoard/GetReconUploadDataFileByHdrSeq', HdrSeq, 'POST', function (result) {
            if (result != null) {
                var dataResult = result;//JSON.parse(result);
                var filename = dataResult.data;
                if (dataResult.status == "1") {
                    DownloadFile(filename);
                    bindData();
                }
                else {
                    MessageCenter(dataResult.data, "error");
                }
            }
        }, null, null, false, false);
    }
}

function DownloadReconExceptionData(HdrSeq, FileName) {


    if (FileName != null && FileName.trim() != "") {
        DownloadFile(FileName);
    }
    else {
        ExtendedAjaxCall('HDFCSoftFeedReconciliationDashBoard/GetReconExceptionDataFileByHdrSeq', HdrSeq, 'POST', function (result) {
            if (result != null) {
                var dataResult = result;//JSON.parse(result);
                var filename = dataResult.data;
                if (dataResult.status == "1") {
                    DownloadFile(filename);
                    bindData();
                }
                else {
                    MessageCenter(dataResult.data, "error");
                }
            }
        }, null, null, false, false);
    }
}

function DownloadReconSuccessData(HdrSeq, FileName) {


    if (FileName != null && FileName.trim() != "") {
        DownloadFile(FileName);
    }
    else {
        ExtendedAjaxCall('HDFCSoftFeedReconciliationDashBoard/GetReconSuccessDataFileByHdrSeq', HdrSeq, 'POST', function (result) {
            if (result != null) {
                var dataResult = result;//JSON.parse(result);
                var filename = dataResult.data;
                if (dataResult.status == "1") {
                    DownloadFile(filename);
                    bindData();
                }
                else {
                    MessageCenter(dataResult.data, "error");
                }
            }

        }, null, null, false, false);
    }
}

function CancelProcess(HdrSequence) {

    if (confirm("Do you want to Cancel process?")) {
       
        //var data = new FormData();
        //data.append("HdrSequence", hdrSeq);
        ExtendedAjaxCall('HDFCSoftFeedReconciliationDashBoard/CancelReconciliationProcess',
            HdrSequence, 'POST', function (result) {
               
                if (result != null) {
                    var dataResult = result;//JSON.parse(result);
                    if (dataResult.status == "1") {
                        bindData();
                        MessageCenter(dataResult.data, "success");

                    }
                    else {
                        MessageCenter(dataResult.data, "error");
                    }
                }
                else {
                }

            }, null, null, false, false);

    }

}

function ProcessReconData(HdrSequence) {

    if (confirm("Do you want to process the reconciliation data?")) {
       
        //var data = new FormData();
        //data.append("HdrSequence", hdrSeq);
        ExtendedAjaxCall('HDFCSoftFeedReconciliationDashBoard/ProcessReconciliationData',
            HdrSequence, 'POST', function (result) {
               
                if (result != null) {
                    var dataResult = result;//JSON.parse(result);
                    if (dataResult.status == "1") {

                        bindData();

                        MessageCenter(dataResult.data, "success");
                    }
                    else {
                        MessageCenter(dataResult.data, "error");
                    }
                }
                else {
                }

            }, null, null, false, false);

    }

}

function DownloadFile(FileName) {
    ExtendedAjaxCall('HDFCSoftFeedReconciliationDashBoard/SetFileName', FileName, 'POST', function (result) {
        console.log(result);

        if (result.status == "1") {
            var objectURL = InitFunction();
            window.location.href = objectURL.uploadedData;
            bindData();
        }
        else {
            MessageCenter(result.data, "error");
        }

    }, null, null, false, false);
}
