$(document).ready(function () {
    $('.alert').show();
    $("#tblApplData").hide();
});
$('#btnSearchApplicationForm').click(function (e) {

    MessageCenter('', '');
    BindGrid();
});

function BindGrid() {
    var dropdownhtml = '';
    ExtendedAjaxCall('/PaymentModeConversion/GetPaymentMode' , null, 'GET', function (result) {
        if (result != null && typeof (result) != 'undefined' && result.length > 0) {
            dropdownhtml = '<select class="form-control PaymentMode" ><option value="0">Select</option>';
            for (var i = 0; i < result.length; i++) {
                dropdownhtml += '<option value="' + result[i].code + '">' + result[i].desc + '</option>';
            }
            dropdownhtml += "</select>";
        }
    }, null, null, false, false);

    var ApplNo = $("#txtAppSearch").val().trim();
    if (ApplNo != "") {
        objBO = { Appl_No: ApplNo }
        ExtendedAjaxCall('/PaymentModeConversion/GetApplicationDetailsForConversion', objBO , 'POST', function (result) {
                       
            console.log(result);
            $("#tblApplData").show();

            if (result != null && typeof (result) != 'undefined') {


                if (result.status = "1") {
                    if ($.fn.DataTable.isDataTable('#tblApplData')) {
                        $('#tblApplData').dataTable().fnDestroy();
                    }
                    $('#tblApplData tbody').html('');
                    $("#tblrow").show();
                    var htmlstr = "";
                    $.each(result.data, function (i, row) {
                        htmlstr += '<tr class="text-center">';
                        htmlstr += '<td><button type="button" class="btn btn-red"  onclick="ConvertPaymentMode(this)"  value="' + $.trim(row.applNo) + '">Convert</button></td>';
                        htmlstr += '<td>' + dropdownhtml + '</td>';
                        //htmlstr += '<td> <select  class="form-control PaymentMode" ><option value="0">Select</option><option value="RTGS/NEFT">RTGS/NEFT</option>'
                        //    + '<option value="UPI">UPI</option><option value="1">Online</option></select></td>';
                        htmlstr += '<td>' + row.paymentmode + '<input type="hidden" value="' + row.paymentmode_code+'"/></td>';
                        htmlstr += '<td>' + row.applNo + '</td>';
                        htmlstr += '<td>' + row.applDate + '</td>';
                        htmlstr += '<td>' + row.amount + '</td>';
                        htmlstr += '<td>' + row.fullname + '</td>';
                        htmlstr += '<td>' + row.pan + '</td>';
                        htmlstr += '<td>' + row.mobile + '</td>';
                        htmlstr += '<td>' + row.email + '</td>';
                        htmlstr += '<td>' + row.scheme + '</td>';
                        htmlstr += '<td>' + row.category + '</td>';
                        htmlstr += '<td>' + row.tenure + '</td>';
                        htmlstr += '<td>' + row.int_Rate + '</td>';
                        htmlstr += '<td>' + row.created_By + '</td>';
                        htmlstr += '<td>' + row.created_Date + '</td>';
                        


                        htmlstr += '</tr>';
                    });

                    $('#tblApplData tbody').append(htmlstr);
                    $('#tblApplData').DataTable({
                        destroy: true,
                        "order": [[3, "asc"]],
                        initComplete: function () {

                        }
                    });
                }
                else {
                    if (result.status = "0") {
                        $('#tblApplData tbody').html('<tr><td colspan="5">No Record Found..!!!</td></tr>');
                    }
                    else {
                        MessageCenter(result.msg, 'error')
                    }
                }
            }
            else {
                $('#tblApplData tbody').html('<tr><td colspan="5">No Record Found..!!!</td></tr>');
            }
        }, null, null, false, false);
    }
    else {

        MessageCenter('Please provide Application Number ..!', 'error')

    }

}

function ConvertPaymentMode(e) {

    MessageCenter('', '');

    var ApplNo = $(e).attr('value');
    var Oldpayment_mode = $($($($(e).closest('tr').find('td')[2])[0]).find('input')[0]).val();
    var InvAmount = $($(e).closest('tr').find('td')[5]).text();
    var Newpayment_mode = $($(e).closest('tr').find('select')).val();
    if (Newpayment_mode == "0") {
        MessageCenter('Please select Conversion Payment Mode', 'error');
    } else {

        var objBO = {

            Appl_No: ApplNo,
            Amount: InvAmount,
            Old_PaymentMode: Oldpayment_mode,
            New_PaymentMode: Newpayment_mode

        }
        
        if (Oldpayment_mode.toUpperCase() != Newpayment_mode.toUpperCase()) {
            ExtendedAjaxCall('/PaymentModeConversion/ConvertPaymentMode',
                objBO, 'POST', function (result) {
                   
                    if (result != null) {

                        if (result.status == "1") {
                            MessageCenter("Payment Mode Conversion done successfully for Application Number " + ApplNo, 'success');
                            $('#lblErrormsg').css('color', 'green');

                        }
                        else {
                            MessageCenter(result.msg, 'error');
                        }
                    }
                    else {
                        MessageCenter("Something Went Wrong", 'error');
                    }

                    BindGrid();

                }, null, null, false, false);
        }
        else {
            MessageCenter('Existing Payment Mode and Conversion Payment Mode is same', 'error');
        }
    }





}
