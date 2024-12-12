$(document).ready(function () {
    GetRectificationApprList();
    $("#divSearch").css("display", "block");
    $("#divApproval").css("display", "none");
    $("#hdnHdrReq").val("");
    MessageCenter("", "");
});


function GetRectificationApprList() {
   
    MessageCenter("", "");
    
    try {
        ExtendedAjaxCall('/RectificationApprDashboard/GetRectificationApprovalList', null, 'GET', function (result) {
            
            $("#tbody_RectificationList").html('');
            if (result != null && result != '' &&result != '0' && result != '-1') {
               
                if (result.headers.length > 0) {
                    var _html = "";
                   
                    $.each(result.headers, function (key, item) {

                        _html += "<tr>";
                        _html += "<td>" + item.hdrSeq + "</td>";
                        _html += "<td>" + item.hdrRectSeq + "</td>";
                        _html += "<td>" + item.createdBy + "</td>";
                        _html += "<td>" + item.createdDate + "</td>";
                        _html += "<td><div onclick=javascript:ProcessforRectification('" + item.hdrSeq + "','" + item.hdrRectSeq + "') class='btn btn-red'>Process</div></td>";
                        _html += "</tr>";
                    })

                    $("#tbody_RectificationList").append(_html);
                   
                   
                } else {

                    $("#tbody_RectificationList").html('');
                    $("#tbody_RectificationList").append("<tr><td colspan='6'  style='text-align:left;'><center>No Rectification List Found !!</center></td></tr>");
                   
                }
            } else if (result != '0' && result != '-1') {

                $("#tbody_RectificationList").html('');
                $("#tbody_RectificationList").append("<tr><td colspan='6' style='text-align:left;'><center>No Rectification List Found !!</center></td></tr>");
               
            }
           
        }, null, null,false, false);


        $(".chk").on("click", function () {
            $("#div_Rectify").removeClass('hidden');
        });
    }
    catch (e) {
       
    }

}


function ProcessforRectification(hdrseq, HdrRectSeq) {
   
    GetRectificationApproverList(hdrseq, HdrRectSeq);
    $("#divSearch").css("display", "none");
    $("#divApproval").css("display", "block");
    $("#hdnHdrReq").val("");
    $("#hdnHdrReq").val(hdrseq);
    $("#hdnHdrRectReq").val("");
    $("#hdnHdrRectReq").val(HdrRectSeq);
}

function Reset() {
    GetRectificationApprList();
    $("#divSearch").css("display", "block");
    $("#divApproval").css("display", "none");
    $("#hdnHdrReq").val("");
    $("#hdnHdrRectReq").val("");
    Recthdrseq = "";
}


var TotalRequestCount = 0;
var Recthdrseq = "";



function GetRectificationApproverList(HdrSeq, HdrRectSeq) {
 
    try {

        var req = HdrSeq; //localStorage.getItem("HdrSeq");

        var objBO = {
            HdrReq: req,
            HdrRectReq: HdrRectSeq
        }
        TotalRequestCount = 0;
        ExtendedAjaxCall('/RectificationApprDashboard/GetRectificationApproverList', objBO, 'POST', function (result) {
           
            if (result != null && result != '' && result != '0' && result != '-1') {
               
                if (result.headers.length > 0) {
                    var _html = ""; var Remarks = "";
                    if ($.fn.DataTable.isDataTable('#tbl_RectList')) {
                        $('#tbl_RectList').dataTable().fnDestroy();
                    }
                   
                    $("#tbody_RectList").html('');
                    $.each(result.headers, function (key, item) {
                        TotalRequestCount = TotalRequestCount + 1;
                        Recthdrseq = item.RecthdrSeq;
                        var CustName= (item.custName == null ? "" : item.custName);
                        _html += "<tr>";
                        _html += "<td><input type='checkbox' class='chk' value='" + item.applNo + "' /></td>";
                        _html += "<td>" + item.applNo + "</td>";
                        _html += "<td>" + CustName+ "</td>";
                        _html += "<td>" + item.fdAmount + "</td>";
                        _html += "<td>" + item.transId + "</td>";
                        _html += "<td>" + item.trasn_dt + "</td>";
                        _html += "<td>" + item.status + "</td>";
                        _html += "<td>" + item.paymtAmnt + "</td>";
                        _html += "<td>" + item.paymtTransId + "</td>";
                        _html += "<td>" + item.paymtDate + "</td>";
                        _html += "<td>" + item.paymtStatus + "</td>";
                        _html += "</tr>";
                        Remarks = item.requestorRemarks;
                    })
                    $("#div_Remarks").removeClass('hidden');
                    $("#Remarks").text(Remarks);
                    $("#tbody_RectList").html(_html);
                    $('#tbl_RectList').DataTable({
                        "order": [[2, "asc"]],
                        initComplete: function () {
                        }
                    });
                }
                else {
                    
                    $("#tbody_RectList").html('');
                    $("#tbody_RectList").html("<tr><td colspan='11' style='text-align:left;'><center>No Rectification List Found !!</center></td></tr>");
                }
            } else if (result.d != '0' && result.d != '-1') {
               
                $("#tbody_RectList").html('');
                $("#tbody_RectList").html("<tr><td colspan='11' style='text-align:left;'><center>No Rectification List Found !!</center></td></tr>");
                // $('#lblErrormsg').text(result.d == "0" || result.d == null ? "No Data Found !!" : (result.d == "-1") ? "Something went wrong !!" : "");

            }
           
        }, null, null, false, false);


        $(".chk").on("click", function () {
            $("#div_Rectify").removeClass('hidden');
        });
    }
    catch (e) {
        
    }

}


$("#btn_Approve").on("click", function () {
    try {
        MessageCenter("", "");
        var ApplNo = "";
        var req = $("#hdnHdrReq").val();
        var reqRect = $("#hdnHdrRectReq").val();
        var IsChecked = false;

        var RequestApporovedCount = 0;
        $("#tbody_RectList input[type=checkbox]:checked").map(function () {
            var Value = $(this).val();
            ApplNo = ApplNo + Value + ",";
            IsChecked = true;
            RequestApporovedCount = RequestApporovedCount + 1;
        });

        var Remarks = $("#txt_remarks").val();

        var AllRequest = false;
        if (TotalRequestCount == RequestApporovedCount) { AllRequest = true; }
        if (IsChecked == true) {
            if (Remarks == "") {
                $('#lblErrormsg').css("color", "red");
                $('#lblErrormsg').text("Please Enter Remarks");
                $("#txt_remarks").focus();
                return false;
            }
            
            var objBO = {
                HdrRectReq: reqRect,
                HdrReq: req,
                Applist: ApplNo,
                Remarks: Remarks,
                IsAllRequestApproved: AllRequest
            };
            ExtendedAjaxCall('/RectificationApprDashboard/UpdateRectificationApprovalData', objBO, 'POST', function (result) {
                if (result != null && result != '' && result != '0' && result != '-1') {

                    $('#lblErrormsg').css("color", "green");
                    $("#txt_remarks").val('');
                    $("#div_Rectify").addClass("hidden");
                    $(".chk").prop("checked", false);
                    GetRectificationApproverList(req, reqRect);
                    $(window).scrollTop(0);
                    $('#lblErrormsg').text("Payment Rectification Request Submitted Successfully.");
                    $(".SearchAll").prop("checked", false);
                   
                    $("#div_Remarks").addClass('hidden');

                } else if (result != '0' && result != '-1') {
                    $('#lblErrormsg').text(result == "0" || result == null ? "Something went wrong !!" : "");
                   
                }
            }, null, null, false, false);
        }
        else {
            $('#lblErrormsg').text("Please Select CheckBox to process Rectification.");
            $('#lblErrormsg').css("color", "red");
            //$("#preloader").hide();
            return false;

        }
    }
    catch (e) { $("#preloader").hide(); }
})

$("#btn_Reject").on("click", function () {
    try {

        var ApplNo = "";
        var req = $("#hdnHdrReq").val();
        var reqRect = $("#hdnHdrRectReq").val();
        var IsChecked = false;

        var RequestApporovedCount = 0;
        $("#tbody_RectList input[type=checkbox]:checked").map(function () {
            var Value = $(this).val();
            ApplNo = ApplNo + Value + ",";
            IsChecked = true;
            RequestApporovedCount = RequestApporovedCount + 1;
        });

        var Remarks = $("#txt_remarks").val();

        var AllRequest = false;
        if (TotalRequestCount == RequestApporovedCount) { AllRequest = true; }
        if (IsChecked == true) {
            if (Remarks == "") {
                $('#lblErrormsg').css("color", "red");
                $('#lblErrormsg').text("Please Enter Remarks");
                $("#txt_remarks").focus();
                return false;
            }
           
            var objBO = {
                HdrRectReq: reqRect,
                HdrReq: req,
                Applist: ApplNo,
                Remarks: Remarks,
                IsAllRequestApproved: AllRequest
            };
            ExtendedAjaxCall('RectificationApprDashboard/UpdateRectificationRejectionData', objBO, 'POST', function (result) {
                if (result != null && result != '' && result != '0' && result != '-1') {

                    $('#lblErrormsg').css("color", "green");
                    $("#txt_remarks").val('');
                    $("#div_Rectify").addClass("hidden");
                    $(".chk").prop("checked", false);
                    GetRectificationApproverList(req, reqRect);
                    $(window).scrollTop(0);
                    $('#lblErrormsg').text("Payment Rectification Request Submitted Successfully.");
                    $(".SearchAll").prop("checked", false);
                   
                    $("#div_Remarks").addClass

                } else if (result != '0' && result != '-1') {
                    $('#lblErrormsg').text(result == "0" || result == null ? "Something went wrong !!" : "");
                    
                }
            }, null, null, false, false);
        }
        else {
            $('#lblErrormsg').text("Please Select CheckBox to process Rectification.");
            $('#lblErrormsg').css("color", "red");
           
            return false;

        }
    }
    catch (e) {
        
    }
})

$(".SearchAll").on("click", function () {
    var IsChecked = $(this)[0].checked;
    if (IsChecked == true) {
        $(".chk").prop("checked", true);
        $("#div_Rectify").removeClass('hidden');
    }
    else {
        $(".chk").prop("checked", false);
        $("#txt_remarks").val('');
        $("#div_Rectify").addClass('hidden');
    }
});


$("#btn_cancel").on("click", function () {
    $(".chk").prop("checked", false);
    $(".SearchAll").prop("checked", false);
    $("#txt_remarks").val('');
    $("#div_Rectify").addClass('hidden');
});

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