$(document).ready(function () {
    GetContact();  
});

function GetContact() {
    //
    $.ajax({
  
        url: "/wa_fd_esarthi_query_window/Default/GET_AGENCY_USER_STANDARD_APP",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        success: function (data) {
            var row = "";
            $.each(data, function (index, item) {
                $("div#rpt").append('<a id="tab1" class="btn btn-app auto"><i  code="' + item.code + '" class="demo-icon icon-cog-alt" onclick="Click(this);"></i>' + item.name + '</a>');
            });
        },
        error: function (result) {
            alert("Error");
        }
    });
}

function Click(code) {

    var rowid = $(code).attr('code');
    $.ajax({
        url: "/wa_fd_esarthi_query_window/Default/lnkBtnStandardApp_Click",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: { code: rowid },
        dataType: "json",
        success: function (data,result) {
            if (result != undefined && result != null) {
                if (result.validRespose) {
                    document.location.href = result.msg;

                } else {
                }

            }
        },
    });
}