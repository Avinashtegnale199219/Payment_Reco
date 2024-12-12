
$(document).ready(function () {
    $("#divGetResult").hide();
    $("#BankRevalidationDIV").hide();
});

$('#btnAdvSearch').click(function (e) {
    $("#divGetResult").show();
    
    var objBO = {
        searchtype: $('#ddlSearchType').val(),
        searchvalue: $('#txtSearchText').val()
    }

    MessageCenter('', '');

    ExtendedAjaxCall('BankRevalidationRequest/GetApplicationDetailsForConversion', objBO, 'POST', function (result) {
        
        if (result.status == "1") {
            $("#divGetResult").show();
            var dataResult = result;//JSON.parse(result.d);
            var filename = dataResult.data;



            if ($.fn.DataTable.isDataTable('#tblBankRevalidationDetails')) {
                $('#tblBankRevalidationDetails').dataTable().fnDestroy();
            }
            $('#tblBankRevalidationDetails > tbody').empty();
            $.each(dataResult.data, function (i, row) {
                var tr = $("<tr/>");
                tr.append("<td style='display:none'>" + row['deP_NO'] + "</td>");
                tr.append("<td><button type='button' class='btn btn-red btnRaiseReq' id=" + row['deP_NO'] + "_" + row['iW_NO'] + ">Raise Request</button></td>");

                tr.append("<td>" + row['iW_NO'] + "</td>");
                tr.append("<td>" + row['deP_NO'] + "</td>");
                tr.append("<td>" + row['foliO_NO'] + "</td>");
                tr.append("<td>" + row['depositoR_NAME'] + "</td>");
                tr.append("<td>" + row['waR_AMOUNT'] + "</td>");
                tr.append("<td>" + row['pan'] + "</td>");
                tr.append("<td>" + row['ofaS_VOUCHER_NO'] + "</td>");
                tr.append("<td>" + row['paymenT_STATUS'] + "</td>");


                $('#tblBankRevalidationDetails').append(tr);
                $("#hdn_pan").val(row['pan'])
            });

            RaiseReqClick();
        }
        else {
            $("#divGetResult").hide();
            $("#txtSearchText").val('');
            $("#ddlSearchType").val($("#ddlSearchType option:first").val());
            $('#lblErrormsg').css('color', 'red').text(result.msg);
            $("#dvErrorMsg").html(result.msg);
            $("#ModelError").modal('show');
        }
    }, null, null, false, false);
});


function RaiseReqClick() {
    $('.btnRaiseReq').on('click', function () {
        var deP_NO = this.id.split('_')[0];
        $("#hdndeP_NO").val(deP_NO);

        var iW_NO = this.id.split('_')[0];
        $("#hdniW_NO").val(iW_NO);
        $("#BankRevalidationDIV").show();
        RaiseRequestDetails(this.id);
    });

}

function RaiseRequestDetails(deP_NO) {

    ExtendedAjaxCall('BankRevalidationRequest/GetRaiseRequest', deP_NO, 'POST', function (result) {
        
        if (result != null) {
            //Investor Details
            $('#lblFDR').text(result.data[0].deposiT_NO);
            $('#lblInvName').text(result.data[0].depositoR_NAME);
            $('#lblFolioNo').text(result.data[0].foliO_NUMBER);
            $('#lblDepAmount').text(result.data[0].deposiT_AMOUNT);
            $('#lblbankDtl').text(result.data[0].banK_DTLS_REQ_RECD_BVL);
            $('#lblbankDtlOffline').text(result.data[0].banK_DTLS_REQ_RECD_OFFLINE);
            $('#lblDMSDownload').text(result.data[0].dmS_DOCID1);
            $('#lblDMSUpload').text(result.data[0].dmS_DOCID2);
            $('#lblStatus').text(result.data[0].status);
            $('#lblSubStatus').text(result.data[0].suB_STATUS);
            $('#lblStatusCode').text(result.data[0].statuS_CODE);
            $('#lblStatusReason').text(result.data[0].statuS_REASON);
            $('#lblsequenceNo').text(result.data[0].utR_SEQUENCE_NO);
            $('#lblPaymentDate').text(result.data[0].paymenT_DATE);

            //Bank Details
            $('#lblBankName').text(result.data[0].banK_NAME);
            $('#lblBankAccount').text(result.data[0].banK_ACCOUNT_NO);
            $('#lblIFSCcode').text(result.data[0].ifsC_CODE);
            $('#lblWARAmt').text(result.data[0].waR_AMOUNT);
            $('#lblPayType').text(result.data[0].inT_PAY_TYPE);
            $('#lblOFAS_TC').text(result.data[0].ofaS_TC);
            $('#lblOFAS_VOU_NO').text(result.data[0].ofaS_VOU_NO);
           // $('#chkStaleStatus').text();
            $('#lblWAR_NO').text(result.data[0].waR_NO);
            $('#lblWAR_DT').text(result.data[0].waR_DT);
            $('#lblOFAS_CHQ_NO').text(result.data[0].chequE_NO);
            $('#lblPHY_CHQ').text(result.data[0].phY_CHQ_DD_NO);
            //$('#chkStopPayment').text();
            //$('#ddlOFAS_TC').text();

            if (result.data[0].stalE_STATUS=='Y') {
                $('#chkStaleStatus').prop('checked', true);
            }
            else {
                $('#chkStaleStatus').prop('checked', false);
            }

            if (result.data[0].f_STOP_PAYMENT_REQUEST == 'Y') {
                $('#chkStopPayment').prop('checked', true);
            }
            else {
                $('#chkStopPayment').prop('checked', false);
            }

            if ($('#ddlOFAS_TC').val() != 0 && !$.isEmptyObject(result.data[0].selecteD_TC)) {
                /* $('#ddlOFAS_TC').find('option:selected').text(result.data[0].selecteD_TC);*/
                $('#ddlOFAS_TC').val(result.data[0].selecteD_TC);
            }
            else {
                $("#ddlOFAS_TC").val($("#ddlOFAS_TC option:eq(2)").val());
            }

            //REQUEST_TYPE	OLD PAYMENT MODE	PAYMENT MODE	  TC to be available
            //Offline		Soft			    Soft(S)			    HFI
            //Offline		Soft			    Warrant(W)		    HBA
            //Offline		Warrant			    Soft(S)			    HFI
            //Offline		Warrant			    Warrant(W)		    HBA
            if ((result.data[0].requesT_TYPE.toLowerCase() == "offline" || result.data[0].requesT_TYPE.toLowerCase() == "online") && result.data[0].f_PAYMENT_MODE.toLowerCase() == "s" && result.data[0].olD_PAYMENT_MODE.toLowerCase() == "soft") {
                //$("#ddlOFAS_TC").prop("disabled", true);
                //$('#ddlOFAS_TC').find('option:selected').text(result.data[0].selecteD_TC);
                /* $('#ddlOFAS_TC').find('option:selected').text("HFI");*/
                $('#ddlOFAS_TC').val("HFI");
            }
            else if ((result.data[0].requesT_TYPE.toLowerCase() == "offline" || result.data[0].requesT_TYPE.toLowerCase() == "online") && result.data[0].f_PAYMENT_MODE.toLowerCase() == "w" && result.data[0].olD_PAYMENT_MODE.toLowerCase() == "soft") {
                //$("#ddlOFAS_TC").prop("disabled", true);
                //$('#ddlOFAS_TC').find('option:selected').text(result.data[0].selecteD_TC);
                /*$('#ddlOFAS_TC').find('option:selected').text("HBA");*/
                $('#ddlOFAS_TC').val("HBA");
            }
            else if ((result.data[0].requesT_TYPE.toLowerCase() == "offline" || result.data[0].requesT_TYPE.toLowerCase() == "online") && result.data[0].f_PAYMENT_MODE.toLowerCase() == "s" && result.data[0].olD_PAYMENT_MODE.toLowerCase() == "warrant") {
                //$("#ddlOFAS_TC").prop("disabled", true);
            //    $('#ddlOFAS_TC').find('option:selected').text(result.data[0].selecteD_TC);
                /* $('#ddlOFAS_TC').find('option:selected').text("HFI");*/
                $('#ddlOFAS_TC').val("HFI");

            }
            else if ((result.data[0].requesT_TYPE.toLowerCase() == "offline" || result.data[0].requesT_TYPE.toLowerCase() == "online") && result.data[0].f_PAYMENT_MODE.toLowerCase() == "w" && result.data[0].olD_PAYMENT_MODE.toLowerCase() == "warrant") {
                //$("#ddlOFAS_TC").prop("disabled", true);
                //$('#ddlOFAS_TC').find('option:selected').text(result.data[0].selecteD_TC);
                /*  $('#ddlOFAS_TC').find('option:selected').text("HBA");*/
                $('#ddlOFAS_TC').val("HBA");
            }
            else {
                //$("#ddlOFAS_TC").prop("disabled", false);
                //$('#ddlOFAS_TC').find('option:selected').text(result.data[0].selecteD_TC);
            }
         
        }
    }, null, null, false, false);
}


$('#btnSubmit').on('click', function () {
    
    if ($('#chkStaleStatus').is(":checked")) {
        var chkStaleStatus = 'Y';
    }
    else {
        var chkStaleStatus = 'N';
    }

    if ($('#chkStopPayment').is(":checked")) {
        var chkStopPayment = 'Y';
    }
    else {
        var chkStopPayment = 'N';
    }
    var PAN = $('#hdn_pan').val();
    var DepAmount = $('#lblDepAmount').text(), WarAmt = $('#lblWARAmt').text();
    
    if (DepAmount == "" || DepAmount == null) {
        DepAmount = 0;
    }
    else {
        DepAmount = parseFloat($('#lblDepAmount').text());
    }

    if (WarAmt == "" || WarAmt == null) {
        WarAmt = 0;
    }
    else {
        WarAmt = parseFloat($('#lblWARAmt').text());
    }

    //DepAmount = parseFloat($('#lblDepAmount').text());
    //WarAmt = parseFloat($('#lblWARAmt').text());

    var objBO = {
        P_Dep_NO: $('#lblFDR').text(),
        P_Folio_NO: $('#lblFolioNo').text(),
        P_War_NO: $('#lblWAR_NO').text(),
        //P_War_Amount: $('#lblWARAmt').text(),
        P_War_Amount: WarAmt,

        P_Dep_Name: $('#lblInvName').text(),
        //P_Dep_Amount: $('#lblDepAmount').text(),
        P_Dep_Amount: DepAmount,
        P_IsChg_BankDtl_ReqRcd_ONL: $('#lblbankDtl').text(),
        // P_Chg_BankDtl_ReqRcd_On_BVL: $('#lblbankDtl').text(),
        P_IsChg_BankDtl_ReqRcd_Offln: $('#lblbankDtlOffline').text(),
        //P_Chg_BankDtl_ReqRcd_Offln: $('#lblbankDtlOffline').text(),

        P_DMS_Upload: $('#lblDMSUpload').text(),
        P_Rvld_Status: $('#lblStatus').text(),
        P_SubStatus: $('#lblSubStatus').text(),
        P_Status_Code: $('#lblStatusCode').text(),
        P_Status_Reason: $('#lblStatusReason').text(),
        P_UTR_Sequence_No: $('#lblsequenceNo').text(),
        P_Payment_Date: $('#lblPaymentDate').text(),
        P_Bank_Name: $('#lblBankName').text(),
        P_Bank_Account_No: $('#lblBankAccount').text(),
        P_IFSC_Code: $('#lblIFSCcode').text(),
        P_Payment_Int_Type: $('#lblPayType').text(),
        P_Old_OFAS_TC: $('#lblOFAS_TC').text(),
        P_Old_OFAS_Voucher_No: $('#lblOFAS_VOU_NO').text(),
        P_Stale_Status: chkStaleStatus,
        P_OFAS_Cheque_No: $('#lblOFAS_CHQ_NO').text(),
        P_Phy_Cheque_DD_War_No: $('#lblPHY_CHQ').text(),
        P_Stop_Payment_Request: chkStopPayment,
        //P_Selected_TC: $('#ddlOFAS_TC').val(),
        P_Selected_TC: $("#ddlOFAS_TC :selected").text(),
        P_Pan: PAN,
        P_Remarks: $('#RemarkID').val()
    }

    MessageCenter('', '');
    var Remarks = $("#RemarkID").val();
    var NewOFAS_TC= $('#ddlOFAS_TC').val();

    if (NewOFAS_TC == "0" || (NewOFAS_TC == null || NewOFAS_TC=="")) {
        var htmlMsg = "Select New OFAS TC...!";
        $('#dvErrorMsg').html(htmlMsg);
        $("#ModelError").modal('show');
    }
    else if (Remarks == "") {
        var htmlMsg = "Enter Remarks...!";
        $('#dvErrorMsg').html(htmlMsg);
        $("#ModelError").modal('show');
    }
    else {
        ExtendedAjaxCall('BankRevalidationRequest/SaveRevalidation_RaiseRequest', objBO, 'POST', function (result) {
            
            if (result != null) {
                var dataResult = result;//JSON.parse(result.d);
                var ResponseMsg = dataResult.data;

                if (ResponseMsg == "Request Generated Successfully") {
                    $('#lblErrormsg').css('color', '#047904').text("Maker :" + ResponseMsg);
                    $('#dvModalMsg').html(ResponseMsg);
                    $("#ModelMessage").modal('show');

                    $("#OkButton").click(function () {
                        window.location.href = 'BankRevalidationRequest';   //OR LOCAL
                    });
                }
                else {
                    $('#lblErrormsg').css('color', 'red').text("Maker :" + ResponseMsg);
                    $('#dvErrorMsg').html(ResponseMsg);
                    $("#ModelError").modal('show');
                }
            }
        }, null, null, false, false);
    }
});


$("#btnAdvReset").click(function () {
    $("#ddlSearchType").val($("#ddlSearchType option:first").val());
    $("#txtSearchText").val('');
    $("#tblBankRevalidationDetails td").remove();

    //Inverstor details clear
    $('#lblFDR').text('');
    $('#lblInvName').text('');
    $('#lblFolioNo').text('');
    $('#lblDepAmount').text('');
    $('#lblbankDtl').text('');
    $('#lblbankDtlOffline').text('');
    $('#lblDMSDownload').text('');
    $('#lblDMSUpload').text('');
    $('#lblStatus').text('');
    $('#lblSubStatus').text('');
    $('#lblStatusCode').text('');
    $('#lblStatusReason').text('');
    $('#lblsequenceNo').text('');
    $('#lblPaymentDate').text('');

    //Bank Details
    $('#lblBankName').text('');
    $('#lblBankAccount').text('');
    $('#lblIFSCcode').text('');
    $('#lblWARAmt').text('');
    $('#lblPayType').text('');
    $('#lblOFAS_TC').text('');
    $('#lblOFAS_VOU_NO').text('');
    $('#chkStaleStatus').removeAttr('checked');
    $('#lblWAR_NO').text('');
    $('#lblWAR_DT').text('');
    $('#lblOFAS_CHQ_NO').text('');
    $('#lblPHY_CHQ').text('');
    $('#chkStopPayment').removeAttr('checked');
    $('#RemarkID').val('');
    $("#ddlOFAS_TC").val($("#ddlOFAS_TC option:first").val());

    $("#divGetResult").hide();
    $("#BankRevalidationDIV").hide();
});

$("#lblDMSDownload").click(function () {
    var link = $("#lblDMSDownload").text();
    $("#lblDMSDownload").attr("href", link)
});