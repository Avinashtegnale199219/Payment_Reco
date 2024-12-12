$(document).ready(function () {
    if (deP_NO == "" || deP_NO == null) {
        ExtendedAjaxCall('/BankRevalidationRequest_SecondApproval/GetdeP_NO', null, 'POST', function (result) {
            
            if (result != null) {
                BindApprovalDetails(result.data);
            }
        });
    }

    else if (deP_NO != "" || deP_NO != null) {
        BindApprovalDetails(deP_NO);
    }

});




function BindApprovalDetails(deP_NO) {
    debugger
    ExtendedAjaxCall('/BankRevalidationRequest/GetRaiseRequest', deP_NO, 'POST', function (result) {
        
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
            //  $('#chkStaleStatus').text();
            $('#lblWAR_NO').text(result.data[0].waR_NO);
            $('#lblWAR_DT').text(result.data[0].waR_DT);
            $('#lblOFAS_CHQ_NO').text(result.data[0].chequE_NO);
            $('#lblPHY_CHQ').text(result.data[0].phY_CHQ_DD_NO);
            //$('#chkStopPayment').text();
            //$('#ddlOFAS_TC').text();

            if (result.data[0].stalE_STATUS == 'Y') {
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

            if (result.data[0].selecteD_TC == 0) {
                $('#ddlOFAS_TC option[value="0"]').attr("selected", "selected");
            }
            else {
                $('#ddlOFAS_TC').val(result.data[0].selecteD_TC);
                //$('#ddlOFAS_TC').find('option:selected').text(result.data[0].selecteD_TC);
                //$("#ddlOFAS_TC").prop("disabled", true);
            }
            $("#Rev_Req_No_ID1").text(result.data[0].f_REVLD_REQDTLS_REQ_NO);

            $("#ddlOFAS_TC").prop("disabled", false);

            //REQUEST_TYPE	OLD PAYMENT MODE	PAYMENT MODE	  TC to be available
            //Offline		Soft			    Soft(S)			    HFI
            //Offline		Soft			    Warrant(W)		    HBA
            //Offline		Warrant			    Soft(S)			    HFI
            //Offline		Warrant			    Warrant(W)		    HBA
            //if ((result.data[0].requesT_TYPE.toLowerCase() == "offline" || result.data[0].requesT_TYPE.toLowerCase() == "online") && result.data[0].f_PAYMENT_MODE.toLowerCase() == "s" && result.data[0].olD_PAYMENT_MODE.toLowerCase() == "soft") {
            //    $("#ddlOFAS_TC").prop("disabled", true);
            //    $('#ddlOFAS_TC').find('option:selected').text(result.data[0].selecteD_TC);
            //}
            //else if ((result.data[0].requesT_TYPE.toLowerCase() == "offline" || result.data[0].requesT_TYPE.toLowerCase() == "online") && result.data[0].f_PAYMENT_MODE.toLowerCase() == "w" && result.data[0].olD_PAYMENT_MODE.toLowerCase() == "soft") {
            //    $("#ddlOFAS_TC").prop("disabled", true);
            //    $('#ddlOFAS_TC').find('option:selected').text(result.data[0].selecteD_TC);
            //}
            //else if ((result.data[0].requesT_TYPE.toLowerCase() == "offline" || result.data[0].requesT_TYPE.toLowerCase() == "online") && result.data[0].f_PAYMENT_MODE.toLowerCase() == "s" && result.data[0].olD_PAYMENT_MODE.toLowerCase() == "warrant") {
            //    $("#ddlOFAS_TC").prop("disabled", true);
            //    $('#ddlOFAS_TC').find('option:selected').text(result.data[0].selecteD_TC);
            //}
            //else if ((result.data[0].requesT_TYPE.toLowerCase() == "offline" || result.data[0].requesT_TYPE.toLowerCase() == "online") && result.data[0].f_PAYMENT_MODE.toLowerCase() == "w" && result.data[0].olD_PAYMENT_MODE.toLowerCase() == "warrant") {
            //    $("#ddlOFAS_TC").prop("disabled", true);
            //    $('#ddlOFAS_TC').find('option:selected').text(result.data[0].selecteD_TC);
            //}
            //else {
            //    $('#ddlOFAS_TC').find('option:selected').text(result.data[0].selecteD_TC);
            //    $("#ddlOFAS_TC").prop("disabled", true);
            //}
        }
    }, null, null, false, false);
}



$('#btnApproval').on('click', function () {
    var DEP_No = $("#lblFDR").text();
    var IW_No = $("#lblWAR_NO").text();
    var Folio_No = $('#lblFolioNo').text();
    var Remarks = $("#RemarkID").val();
    var Rev_Req_No = $("#Rev_Req_No_ID1").text();

    var objBO1 = {
        P_Dep_NO: DEP_No,
        P_War_NO: IW_No,
        P_Folio_NO: Folio_No,
        P_APPROVAL_STAGE: 2,
        P_APPROVAL_STATUS: "APR",
        Second_CheckerRemark: Remarks,
        Rev_Req_No: Rev_Req_No
    }
    var NewOFAS_TC = $('#ddlOFAS_TC').val();

    if (NewOFAS_TC == "0" || (NewOFAS_TC == null || NewOFAS_TC == "")) {
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
        ExtendedAjaxCall('/BankRevalidationRequest_SecondApproval/btnApproval_Click', objBO1, 'POST', function (result) {
            
            if (result != null) {
                var Response = result.data;
                if (Response == "Record Saved Succesfully") {
                    $('#lblErrormsg').css('color', '#047904').text("Second Approval :" + Response);
                    $("#RemarkID").attr("disabled", false);
                    $("#btnApproval").attr("disabled", false);
                    $("#btnReject").attr("disabled", false);
                    $("#btnCancel").attr("disabled", false);
                    $('#dvModalMsg').html(Response);
                    $("#ModelMessage").modal('show');

                    $("#OkButton").click(function () {
                        //window.location.href = WA_FD_PAYMENT_RECO + 'BankRevalidationRequestDashboard_L2';   //FOR UAT
                        //window.location.href = '/BankRevalidationRequestDashboard_L2';   //OR LOCAL

                        var objectURL = InitFunction2();
                        window.location.href = objectURL.uploadedData;
                    });
                }
                else if (Response == "" || Response == null) {
                    $('#lblErrormsg').css('color', 'red').text("Second Approval :" + 'Error Occurs : Something went wrong..!');
                    $('#dvErrorMsg').html('Error Occurs : Something went wrong..!');
                    $("#ModelError").modal('show');
                    $("#RemarkID").attr("disabled", "disabled");
                    $("#btnApproval").attr("disabled", "disabled");
                    $("#btnReject").attr("disabled", "disabled");
                    $("#btnCancel").attr("disabled", "disabled");
                }
                else if (Response != "" || Response != null) {
                    $('#lblErrormsg').css('color', 'red').text("Second Approval :" + Response);
                    $('#dvErrorMsg').html(Response);
                    $("#ModelError").modal('show');
                    $("#RemarkID").attr("disabled", "disabled"); 
                    $("#btnApproval").attr("disabled", "disabled");
                    $("#btnReject").attr("disabled", "disabled");
                    $("#btnCancel").attr("disabled", "disabled");
                }
                else {
                    $('#lblErrormsg').css('color', 'red').text("Second Approval :" + Response);
                    $('#dvErrorMsg').html(Response);
                    $("#ModelError").modal('show');
                }
            }
        }, null, null, false, false);
    }

});

$('#btnReject').on('click', function () {
    
    var DEP_No = $("#lblFDR").text();
    var IW_No = $("#lblWAR_NO").text();
    var Folio_No = $('#lblFolioNo').text();
    var Remarks = $("#RemarkID").val();
    var Rev_Req_No = $("#Rev_Req_No_ID1").text();

    var objBO1 = {
        P_Dep_NO: DEP_No,
        P_War_NO: IW_No,
        P_Folio_NO: Folio_No,
        P_APPROVAL_STAGE: 2,
        P_APPROVAL_STATUS: "REJ",
        Second_CheckerRemark: Remarks,
        Rev_Req_No: Rev_Req_No

    }
    var NewOFAS_TC = $('#ddlOFAS_TC').val();

    if (NewOFAS_TC == "0" || (NewOFAS_TC == null || NewOFAS_TC == "")) {
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
        ExtendedAjaxCall('/BankRevalidationRequest_SecondApproval/btnReject_Click', objBO1, 'POST', function (result) {
            
            if (result != null) {
                var Response = result.data;
                if (Response == "Record Saved Succesfully") {
                    $('#lblErrormsg').css('color', '#047904').text("Second Rejection :" + Response);
                    $("#RemarkID").attr("disabled", false);
                    $("#btnApproval").attr("disabled", false);
                    $("#btnReject").attr("disabled", false);
                    $("#btnCancel").attr("disabled", false);

                    $('#dvModalMsg').html(Response);
                    $("#ModelMessage").modal('show');

                    $("#OkButton").click(function () {
                        //  window.location.href = WA_FD_PAYMENT_RECO + 'BankRevalidationRequestDashboard_L2';   //FOR UAT
                        //window.location.href = '/BankRevalidationRequestDashboard_L2';   //OR LOCAL

                        var objectURL = InitFunction2();
                        window.location.href = objectURL.uploadedData;
                    });
                }
                else if (Response == "" || Response == null) {
                    $('#lblErrormsg').css('color', 'red').text("Second Rejection :" + 'Error Occurs : Something went wrong..!');
                    $('#dvErrorMsg').html('Error Occurs : Something went wrong..!');
                    $("#ModelError").modal('show');
                    $("#RemarkID").attr("disabled", "disabled");
                    $("#btnApproval").attr("disabled", "disabled");
                    $("#btnReject").attr("disabled", "disabled");
                    $("#btnCancel").attr("disabled", "disabled");
                }
                else if (Response != "" || Response != null) {
                    $('#lblErrormsg').css('color', 'red').text("Second Rejection :" + Response);
                    $('#dvErrorMsg').html(Response);
                    $("#ModelError").modal('show');
                    $("#RemarkID").attr("disabled", "disabled");
                    $("#btnApproval").attr("disabled", "disabled");
                    $("#btnReject").attr("disabled", "disabled");
                    $("#btnCancel").attr("disabled", "disabled");
                }
                else {
                    $('#lblErrormsg').css('color', 'red').text("Second Rejection :" + Response);
                    $('#dvErrorMsg').html(Response);
                    $("#ModelError").modal('show');
                }
            }
        }, null, null, false, false);
    }

});

$("#btnCancel").on('click', function () {
    //window.location.href = WA_FD_PAYMENT_RECO +  'BankRevalidationRequestDashboard_L2';

    var objectURL = InitFunction2();
    window.location.href = objectURL.uploadedData;
});

