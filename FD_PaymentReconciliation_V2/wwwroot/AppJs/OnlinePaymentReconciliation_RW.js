$(document).ready(function () {
   // $("#preloader").hide();
  //  Validation();
})
//File Upload Validation
$('input[type=file').on('change', function (e) {

    var ext = $(this).val().split('.').pop().toLowerCase();
    if ($.inArray(ext, ['.csv', 'csv']) == -1) {
        $('.help-block').text('File should be csv format');
        $(this).val('');
      
        return;
    } else {
        $('.help-block').text('');
    }

    $('.help-block').css("color", "green");
    $('.help-block').text($(this)[0].files[0].name);
});

function FileSizeValidation(file, t) {
    var filesize = Math.round((file[0].size / 1024 / 1024), 0);
    if (filesize > 5) {
        $(t).parent().siblings('.help-block').text('Selected file could not be uploaded.The file is ' + filesize + ' MB exceeding the maximum file size of 5MB !!');
        $(t).val('');
        return false;
    }
    return true;
}

function CharactersCount(id, spanId) {
    try {

        var CharLength = 500;
        //var CharLength = lengthCount;
        var txtMsg = document.getElementById(id);
        var lblCount = document.getElementById(spanId.id);

        var colorwidth = txtMsg.value.length;
        //var divcolor = document.getElementById('Colordiv');
        if (txtMsg.value.length > CharLength) {
            txtMsg.value = txtMsg.value.substring(0, CharLength);
        }
        lblCount.innerHTML = CharLength - txtMsg.value.length;

    } catch (e) {
    }
}


$('#btn_UploadFile').click(function () {

    if (Validation()) {

        var file = $('#FileUpload1').get(0).files;
        console.log(file[0].name);
        // var file = e.target.files[0];
        // var fileExtension = $("#FileUpload1").val().split('.').pop().toLowerCase();
        var fileExtension = file[0].name.split('.');
        console.log(fileExtension);
        // var extension = ['xls', 'xlsx', 'XLS', 'XLSX'];
        var extension = ['csv', 'CSV'];
        if (jQuery.inArray(fileExtension[1], extension) != -1) {
            if (file[0].size > 0) {

                console.log(file[0].size);
                if (window.FormData !== undefined) {
                    var data = new FormData();
                    data.append(file[0].name, file[0]);
                    $.ajax({
                        url: WA_FD_PAYMENT_RECO +'OnlinePaymentReconciliation_RW/Upload_File',
                        type: "POST",
                        data: data,
                        async: false,
                        contentType: false,
                        processData: false,
                        beforeSend: function () {
                        },
                        success: function (result) {
                           // console.log(result);
                            // var res = JSON.parse(result);
                            if (result != null) {

                                if (result.status == "1") {
                                    $('.help-block').text(result.filename);
                                    MessageCenter(result.data, 'error')
                                }
                                else {
                                    $('.help-block').text('');
                                    MessageCenter(result.data, 'error')
                                }
                            } else {
                                MessageCenter('Something went wrong..!', 'error')
                            }
                            $('#FileUpload1').val('');
                        },
                        error: function (result) {
                            
                            // MessageCenter(result.data, 'error')
                            MessageCenter(result.data, 'error')
                            $('#FileUpload1').val('');
                        },
                        complete: OnComplete
                    });
                }
                else {
                    MessageCenter('Please Upload Valid File..!', 'error')
                }
            } else {
                MessageCenter('Please Upload Fileeee..!', 'error')
            }
        } else {
            // MessageCenter('File is Not Valid.(Valid:.xls,.xlsx)..!', 'error')
            MessageCenter('File is Not Valid.(Valid:.csv,.CSV)..!', 'error')
        }
    }
 })

function Validation() {
    
    if ($('#FileUpload1').val() == '') {
        MessageCenter("Please Upload File..!");
        return false;
    }
    else if ($('#txt_remarks').val() == '') {
        MessageCenter("Remark field Required..!");
        return false;
    }
    else {
        return true;
    }
        
   
}


//function IsValid() {
    
//    $(".help-block").text('');
//    MessageCenter('', '')
   
//    var upload = $('.help-block').text();
//    var txtRemarks = $('#txt_remarks').val();
//    isvalid = true;
   
//    if (txtRemarks == '' || txtRemarks == null || txtRemarks == undefined) {
//          isvalid = false;
//        MessageCenter('Required field.!', 'error')
//       // $('#txt_remarks').siblings(".help-block").text('Required field.!');
        
//    }
//    else if (upload == '' || upload == null || upload == undefined) {
//        isvalid = false;
//       // $('#txt_remarks').text('Required field.!');
//        MessageCenter('Please upload file..!', 'error')
//    }
//    return isvalid
//} 


//$('#btn_UploadFile').click(function () {
//    if (IsValid()) {
//        var file = $('#FileUpload1').get(0).files;
//        console.log(file[0].name);
//        // var file = e.target.files[0];
//        // var fileExtension = $("#FileUpload1").val().split('.').pop().toLowerCase();
//        var fileExtension = file[0].name.split('.');
//        console.log(fileExtension);
//        // var extension = ['xls', 'xlsx', 'XLS', 'XLSX'];
//        var extension = ['csv', 'CSV'];
//        if (jQuery.inArray(fileExtension[1], extension) != -1) {
//            if (file[0].size > 0) {

//                console.log(file[0].size);
//                if (window.FormData !== undefined) {
//                    var data = new FormData();
//                    data.append(file[0].name, file[0]);
//                    $.ajax({
//                        url: '/OnlinePaymentReconciliation_RW/Upload_File',
//                        type: "POST",
//                        data: data,
//                        async: false,
//                        contentType: false,
//                        processData: false,
//                        beforeSend: function () {
//                        },
//                        success: function (result) {
//                            console.log(result);
//                            // var res = JSON.parse(result);
//                            if (result != null) {

//                                if (result.status == "1") {
//                                    $('.help-block').text(result.data);
//                                    MessageCenter('', '')
//                                }
//                                else {
//                                    $('.help-block').text('');
//                                    MessageCenter(result.data, 'error')
//                                }
//                            } else {
//                                MessageCenter('Something went wrong..!', 'error')
//                            }
//                            $('#FileUpload1').val('');
//                        },
//                        error: function (result) {
//                            
//                            MessageCenter(result.data, 'error')
//                            $('#FileUpload1').val('');
//                        },
//                        complete: OnComplete
//                    });
//                }
//                else {
//                    MessageCenter('Please Upload Valid File..!', 'error')
//                }
//            } else {
//                MessageCenter('Please Upload Fileeee..!', 'error')
//            }
//        } else {
//            // MessageCenter('File is Not Valid.(Valid:.xls,.xlsx)..!', 'error')
//            MessageCenter('File is Not Valid.(Valid:.csv,.CSV)..!', 'error')
//        }
//    } 
//})

