$(document).ready(function () {
    GetRectificationList();
});

function GetRectificationList() {
    
    try {

        var req = "";
        req = localStorage.getItem("HdrSeq");
        var objBO = {
            HdrReq: req
        }
        ExtendedAjaxCall('/RectificationRequestList/GetRectificationList', objBO, 'POST', function (result) {
            $("#tbody_RectificationList").html('');
            if (result != null && result != '' && result != '0' && result != '-1') {
                if (result.success) {
                    if (result.headers.length > 0) {
                        var _html = "";
                       
                        $.each(result.headers, function (key, item) {

                            _html += "<tr>";
                            _html += "<td><input type='checkbox' class='chk' value='" + item.applNo + "' /></td>";
                            _html += "<td>" + item.applNo + "</td>";
                            _html += "<td>" + item.custName + "</td>";
                            _html += "<td>" + item.fdAmount + "</td>";
                            _html += "<td>" + item.transId + "</td>";
                            _html += "<td>" + item.trasn_dt + "</td>";
                            _html += "<td>" + item.status + "</td>";
                            _html += "<td>" + item.paymtAmnt + "</td>";
                            _html += "<td>" + item.paymtTransId + "</td>";
                            _html += "<td>" + item.paymtDate + "</td>";
                            _html += "<td>" + item.paymtStatus + "</td>";
                            _html += "</tr>";
                        })

                        $("#tbody_RectificationList").html(_html);
                    }
                }

            } else if (result!= '0' && result != '-1') {

                $("#tbody_RectificationList").html('');
                $("#tbody_RectificationList").html('<tr><td colspan="11" ><center>No Data Found !!</center></td></tr>');
            }
        }, null, null, false, false);
        $(".chk").on("click", function () {

            $("#div_Rectify").removeClass('hidden');
        });
    }
    catch (e) { }

}


$("#btn_rectify").on("click", function () {
    try {
        var ApplNo = "";
        var req = localStorage.getItem("HdrSeq");
        var IsChecked = false;
        $("#tbody_RectificationList input[type=checkbox]:checked").map(function () {
            var Value = $(this).val();
            ApplNo = ApplNo + Value + ",";
            IsChecked = true;
        });

        var Remarks = $("#txt_remarks").val();


        if (IsChecked == true) {
            if (Remarks == "") {
                $('#lblErrormsg').css("color", "red");
                $('#lblErrormsg').text("Please Enter Remarks");
                $("#txt_remarks").focus();
                return false;
            }
            var objBO = {
                HdrReq: req,
                Applist: ApplNo,
                Remarks: Remarks
            };
            ExtendedAjaxCall('/RectificationRequestList/InsertRectificationRequest', objBO, 'POST', function (result) {
                if (result != null && result != '' && result != '0' && result != '-1') {

                    $('#lblErrormsg').css("color", "green");
                    $("#txt_remarks").val('');
                    $("#div_Rectify").addClass("hidden");
                    $(".chk").prop("checked", false);
                    GetRectificationList();
                    $('#lblErrormsg').text("Payment Rectification Request Submitted Successfully.");
                    $(window).scrollTop(0);
                    $(".SearchAll").prop("checked", false);

                } else if (result != '0' && result != '-1') {
                    $('#lblErrormsg').text(result == "0" || result == null ? "No Data Found !!" : (result == "-1") ? "Something went wrong !!" : "");
                }
            }, null,null, false, false);
        }
        else {
            $('#lblErrormsg').text("Please Select CheckBox to process Rectification.");
            $('#lblErrormsg').css("color", "red");
            return false;
        }
    }
    catch (e) { console.log(e.message); }
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