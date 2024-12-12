$('#tableRtgsUtrUpload').DataTable({
    destroy: true,
    initComplete: function () {
    }
});

$(document).ready(function () {
    $('#lblPageName').text('E-SARATHI UTR RTGS REPORT');
    bindData();
});

function bindData() {
    MessageCenter('', '');

    ExtendedAjaxCall('/ESAT_UTR_RTGS_UploadReport/GetUTR_RTGS_UploadData', null, 'GET', function (result) {
        let html = '';
        if (!$.isEmptyObject(result)) {
            if (result.status != null && result.status != "0" && result.data != null && result.data.length > 0) {
                $('#btnExporttoExcel').show();
                $.each(result.data, function (i, row) {
                    html += '<tr>';
                    //html += '<td style="display:none">' + row['f_ID'] + '</td>';
                    html += '<td>' + row['f_APPL_NO'] + '</td>';
                    html += '<td>' + row['f_INVESTOR_NAME'] + '</td>';
                    html += '<td>' + row['f_AMOUNT'] + '</td>';
                    html += '<td>' + row['f_TRANS_DATE'] + '</td>';
                    html += '<td>' + row['f_UPLOADED_DATE'] + '</td>';
                    if (row['FILE_PATH'] != '') {
                        html += '<td class="text-center"><a class="vertical_top" href="javascript:DownloadReconUploadedData(\'' + row['f_ID'] + '\',\'' + row['f_FileName'] + '\');"  ><img src="img/icon_download.png" height="20px" /></a></td>';
                    } else {
                        html += '<td></td>';
                    }
                    html += '</tr>'
                });
            }
            else {
                $('#btnExporttoExcel').hide();
            }
        }
        else {
            $('#btnExporttoExcel').hide();
        }
        $('#tableRtgsUtrUpload').DataTable().destroy();
        $('#tableRtgsUtrUpload tbody').html(html);
        $('#tableRtgsUtrUpload').DataTable({
            destroy: true,
            initComplete: function () {
            }
        });

    }, null, null, false, false);
}

function DownloadReconUploadedData(hdrSeq, FileName) {

    var extValue = [];
    var filePath = FileName;
    extValue = filePath.split('.');
    var ext = extValue.pop();

    if (filePath == null && filePath == "") {
        MessageCenter('No File Found', 'error');
    }
    else {
        ExtendedAjaxCall('/ESAT_UTR_RTGS_UploadReport/ViewFile?HdrSeq=' + hdrSeq, null, 'POST', function (result) {
            $('#iframeViewDoc').attr('src', "");
            $('#imgViewDocument').attr('src', "");
            if (result != null) {
                if (result.status == "1") {
                    if (ext == "pdf") {
                        $('#iframeViewDoc').show();
                        $('#imgViewDocument').hide();
                        $('#iframeViewDoc').attr('src', "data:application/pdf;base64," + result.data);
                    }
                    else {
                        $('#iframeViewDoc').hide();
                        $('#imgViewDocument').show();
                        $('#imgViewDocument').attr('src', "data:image/jpg;base64," + result.data);
                    }
                    $('#dvViewDocument').modal();
                }
                else {
                    MessageCenter(result.data, 'error');
                }
            }
            $('#preloader').hide();
        }, null, null, false, false);
    }
}

$("#btnExporttoExcel").click(function () {
    $("#tableRtgsUtrUpload").table2excel({
        filename: "ESAT_UTR_RTGS_UPLOAD.xls"
    });
});    