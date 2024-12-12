//function getcategory() {

//    var SchemeTypeCode = "C";
//    var CategoryCode = "PU";


//    ExtendedAjaxCall(Api_settings.url + 'CategoryMaster?Category_Code=' + SchemeTypeCode + 'Category_Name=' + CategoryCode + '', null, 'GET', function (result) {
//        ;
//        if (result !== null && typeof (result) !== 'undefined') {
//            if (result.length > 0) {
//                $('#ddlcategory').empty().append('<option value="0">All</option>');
//                //$('#ddlBranch').empty();
//                $.each(result, function (i, row) {
//                    $('#ddlcategory').append("<option value='" + row['Category_Code'] + "' data-row='" + JSON.stringify(row) + "'>" + row['Category_Name'] + "</option>");
//                });

//            } else {
//                $('#lblLookUpMessage').text('No company found').addClass('red-text');
//            }
//        } else {
//            $('#lblLookUpMessage').text('something went wrong while binding company').addClass('red-text');
//        }
//    }, null, null, false, true);

//}


//Dashboard
$('#btnInvest').click(function () {

    var ddlcategory = $('#ddlcategory').val()
    var txtFDAmount = $('#txtFDAmount').val();
    $('#lblErrormsg').text('').addClass('red-text');

    if (ddlcategory == "0" || ddlcategory == "Select" || ddlcategory == null) {
        $('#lblErrormsg').text('Please select category.').addClass('red-text');
        return;
    }

    if (txtFDAmount == "" || txtFDAmount == null) {
        $('#lblErrormsg').text('Please enter valid no.').addClass('red-text');
        return;
    }


    $.ajax({
        type: "GET",
        ///wa_fd_esarathi_query_window
        url: "/DashboardQuery/GetDashboardData",
        data: { ddlSelectValue: ddlcategory, txtCP_Ref_No: txtFDAmount },
        success: function (result) {

            if (result.validResponce) {
                if (result !== null && typeof (result) !== 'undefined') {
                    if (result.result.length > 0) {
                        $('#btn_show').empty();
                        $('#btn_show').append(result.result);
                        //alert(result.result);
                        $("#btn_show").css({ display: "block" });
                    } else {
                        $('#btn_show').empty();
                        $('#lblErrormsg').text('something went wrong while binding company').addClass('red-text');
                        $("#btn_show").css({ display: "none" });
                    }
                } else {
                    $('#btn_show').empty();
                    $('#lblErrormsg').text('Data not found.').addClass('red-text');
                    $("#btn_show").css({ display: "none" });
                }
            }
            else {
                $('#btn_show').empty();
                $('#lblErrormsg').text('Data not found.').addClass('red-text');
                $("#btn_show").css({ display: "none" });
            }
        }

    });
});


//API Call View=> First Tab
//GetCallViewData
$('#btnCallView').click(function () {
    APICallDetaits();
    GetAPICallViewLeadInformation();
});

//GetCallViewData
$('#btnBasicTransactionDetails').click(function () {
    GetAPICallViewLeadInformation();
});

function GetAPICallViewLeadInformation() {

    var ddlcategory = $('#ddlcategory').val()
    var txtFDAmount = $('#txtFDAmount').val();

    $('#lblErrormsg').text('').addClass('red-text');

    $.ajax({
        type: "GET",
        url: "/DashboardQuery/BasicTransactionDetails",
        data: { ddlSelectValue: ddlcategory, txtCP_Ref_No: txtFDAmount },
        dataType: 'json',
        success: function (result) {


            if (result !== null && typeof (result) !== 'undefined') {
                if (result.result.length > 0) {

                    $('#tblCallView thead').empty();

                    var th = '<tr> <th style="text-align:left">Root Transaction</th>';
                    th += '<th style="text-align:left">Element Transaction</th> ';
                    th += '<th style="text-align:left">Attribute Name</th>';
                    th += '<th style="text-align:left">Attribute Value</th> ';
                    th += '<th style="text-align:left">Attribute Status</th> ';
                    th += '<th style="text-align:left">Attribute TransactedOn</th> <tr>';

                    $('#tblCallView thead').append(th);

                    $('#tblCallView tbody').empty();
                    var data1 = JSON.parse(result.result);
                    $.each(data1, function (j, e) {
                        var row = '<tr>';
                        row += '<td>' + e["Root Transaction"] + '</td>';
                        row += '<td>' + e["Element Transaction"] + '</td>';
                        row += '<td>' + e["Attribute Name"] + '</td>';
                        row += '<td>' + e["Attribute Value"] + '</td>';
                        row += '<td>' + e["Attribute Status"] + '</td>';
                        row += '<td>' + e["Attribute TransactedOn"] + '</td>';
                        row += '</tr>';
                        $('#tblCallView tbody').append(row);
                    });

                    var HeaderVal = $('#btnAPIcallQueueDetail label')
                    //$("#linkHeader").val(HeaderVal);

                } else {
                    $('#tblAPIDataView tbody').empty();
                    $('#lblErrormsg').text('something went wrong while binding company').addClass('red-text');
                }
            } else {
                $('#tblAPIDataView tbody').empty();
                $('#lblErrormsg').text('Data not found.').addClass('red-text');
            }
        }

    });
}


function APICallDetaits() {

    var ddlcategory = $('#ddlcategory').val()
    var txtFDAmount = $('#txtFDAmount').val();
    $('#lblErrormsg').text('').addClass('red-text');

    $.ajax({
        type: "GET",
        url: "/DashboardQuery/APICalledView",
        data: { ddlSelectValue: ddlcategory, txtCP_Ref_No: txtFDAmount },
        dataType: 'json',
        success: function (result) {


            if (result.validResponce) {

                if (result !== null && typeof (result) !== 'undefined') {

                    if (result.result.length > 0) {
                        var resultParce = JSON.parse(result.result);
                        //alert(resultParce);
                        //$('#tblCallView thead').empty();
                        //var th = '<tr><th style="text-align:left">API</th> <tr>';
                        //$('#tblCallView thead').append(th);

                        $('#tblCallView thead').empty();
                        $('#tblCallView tbody').empty();

                        $('#btnAPILinkClick').empty();
                        $.each(resultParce, function (j, e) {

                            //var row = "<a href=" + e["CpApiCalled"] + ">" + e["CpApiCalled"] + "</a>";
                            var row = e["CpApiCalled"];
                            $('#btnAPILinkClick').append(row);
                        });

                    } else {
                        $('#tblCallView tbody').empty();
                        $('#lblErrormsg').text('Something went wrong while binding data.').addClass('red-text');
                    }
                } else {
                    $('#tblCallView tbody').empty();
                    $('#lblErrormsg').text('Something went wrong while binding data.').addClass('red-text');
                }
            }
            else {
                $('#tblCallView tbody').empty();
                $('#lblErrormsg').text('Data not found.').addClass('red-text');
            }

        }

    });
}

//Get API Called View
$('#btnAPIcallPANDetail').click(function () {



});


//Get API Selected Data
$('#btnAPILink').click(function () {

    var ddlcategory = $('#ddlcategory').val();
    var txtFDAmount = $('#txtFDAmount').val();
    var apiPath = $('#btnAPILinkClick').text();

    $('#lblErrormsg').text('').addClass('red-text');

    $.ajax({
        type: "GET",
        url: "/DashboardQuery/APISelectedDataView",
        data: { ddlSelectValue: ddlcategory, txtCP_Ref_No: txtFDAmount, ApiPath: apiPath },
        dataType: 'json',
        success: function (result) {


            if (result.validResponce) {

                if (result !== null && typeof (result) !== 'undefined') {

                    if (result.result.length > 0) {
                        var resultParce = JSON.parse(result.result);
                        //alert(resultParce);
                        $('#tblCallView thead').empty();

                        var th = '<tr> <th style="text-align:left">Root Transaction</th>';
                        th += '<th style="text-align:left">Element Transaction</th> ';
                        th += '<th style="text-align:left">Attribute Name</th>';
                        th += '<th style="text-align:left">Attribute Value</th> ';
                        th += '<th style="text-align:left">Attribute Status</th> ';
                        th += '<th style="text-align:left">Attribute TransactedOn</th> <tr>';

                        $('#tblCallView thead').append(th);

                        $('#tblCallView tbody').empty();
                        $.each(resultParce, function (j, e) {
                            var row = '<tr>';
                            row += '<td>' + e["Root Transaction"] + '</td>';
                            row += '<td>' + e["Element Transaction"] + '</td>';
                            row += '<td>' + e["Attribute Name"] + '</td>';
                            row += '<td>' + e["Attribute Value"] + '</td>';
                            row += '<td>' + e["Attribute Status"] + '</td>';
                            row += '<td>' + e["Attribute TransactedOn"] + '</td>';
                            row += '</tr>';
                            $('#tblCallView tbody').append(row);
                        });

                    } else {
                        $('#lblErrormsg').text('Something went wrong while binding data.').addClass('red-text');
                        $('#tblCallView tbody').empty();
                    }
                } else {
                    $('#lblErrormsg').text('Something went wrong while binding data.').addClass('red-text');
                    $('#tblCallView tbody').empty();
                }
            }
            else {
                //$('#tblCallView thead').append(th);
                $('#lblErrormsg').text('Data not found.').addClass('red-text');
                $('#tblCallView tbody').empty();
            }

        }

    });
});

//API Data => Second Tab

//GetAPIDataView
$('#btnAPIDataView').click(function () {

    GetAPIDataLeadInformation();
    //
    //var ddlcategory = $('#ddlcategory').val()
    //var txtFDAmount = $('#txtFDAmount').val();

    //
    //$.ajax({
    //    type: "GET",
    //    url: "/DashboardQuery/GetAPIDataView",
    //    data: { ddlSelectValue: ddlcategory, txtCP_Ref_No: txtFDAmount },
    //    dataType: 'json',
    //    success: function (result) {
    //        

    //        if (result.validResponce) {
    //            if (result.result.length > 0) {
    //                var arr = result.result.split("|");

    //                var row = '<tr>';
    //                if (arr.length > 0) {
    //                    $('#tblAPIDataView tbody').empty();
    //                    $.each(arr, function (j, e) {
    //                        row += '<td>' + e + '</td>';
    //                    });
    //                    row += '</tr>';
    //                    $('#tblAPIDataView tbody').append(row);
    //                    //alert(row);
    //                }
    //                else {
    //                    $('#lblLookUpMessage').text('something went wrong while binding data.').addClass('red-text');

    //                }

    //            } else {
    //                $('#lblLookUpMessage').text('something went wrong while binding data.').addClass('red-text');
    //            }
    //        }
    //        else {
    //            $('#lblLookUpMessage').text('Data not found.').addClass('red-text');
    //        }
    //        //if (result !== null && typeof (result) !== 'undefined') {

    //        //    if (result.result.length > 0) {

    //        //    } else {
    //        //        $('#lblLookUpMessage').text('No company found').addClass('red-text');
    //        //    }
    //        //} else {
    //        //    $('#lblLookUpMessage').text('something went wrong while binding company').addClass('red-text');
    //        //}
    //    }

    //});
});


$('#btnLeadInformation').click(function () {
    GetAPIDataLeadInformation();
});


function GetAPIDataLeadInformation() {

    var ddlcategory = $('#ddlcategory').val()
    var txtFDAmount = $('#txtFDAmount').val();
    $('#lblErrormsg').text('').addClass('red-text');

    $.ajax({
        type: "GET",
        url: "/DashboardQuery/LeadInformation",
        data: { ddlSelectValue: ddlcategory, txtCP_Ref_No: txtFDAmount },
        dataType: 'json',
        success: function (result) {


            if (result.validResponce) {
                if (result.result.length > 0) {

                    $('#tblAPIDataView thead').empty();
                    var row = '<tr> <td> <b>Name</b></td>'
                    row += ' <td><b>Value</b></td> </tr>'
                    $('#tblAPIDataView thead').append(row);

                    $('#tblAPIDataView tbody').empty();

                    var resultParce = JSON.parse(result.result);
                    var data = resultParce.split("|");

                    var tLength = data.length;

                    for (var item = 0; item < tLength; item++) {

                        var data1 = data[item].split(":");

                        var row = '<tr> <td>' + data1[0] + '</td>'
                        row += ' <td>' + data1[1] + '</td> </tr>'

                        $('#tblAPIDataView tbody').append(row);
                    }

                } else {
                    $('#tblAPIDataView tbody').empty();
                    $('#lblErrormsg').text('something went wrong while binding data.').addClass('red-text');
                }
            }
            else {
                $('#tblAPIDataView tbody').empty();
                $('#lblErrormsg').text('Data not found.').addClass('red-text');
            }
        }

    });
}


//GetAPIDataView
$('#btnInvestorInformation').click(function () {

    var ddlcategory = $('#ddlcategory').val()
    var txtFDAmount = $('#txtFDAmount').val();
    $('#lblErrormsg').text('').addClass('red-text');
    $.ajax({
        type: "GET",
        url: "/DashboardQuery/InvestorInformation",
        data: { ddlSelectValue: ddlcategory, txtCP_Ref_No: txtFDAmount },
        dataType: 'json',
        success: function (result) {


            if (result.validResponce) {
                if (result.result.length > 0) {

                    $('#tblAPIDataView thead').empty();
                    var row = '<tr> <td> <b>Name</b></td>'
                    row += ' <td><b>Value</b></td> </tr>'
                    $('#tblAPIDataView thead').append(row);

                    $('#tblAPIDataView tbody').empty();

                    var resultParce = JSON.parse(result.result);
                    var data = resultParce.split("|");

                    var tLength = data.length;

                    for (var item = 0; item < tLength; item++) {

                        var data1 = data[item].split(":");

                        var row = '<tr> <td>' + data1[0] + '</td>'
                        row += ' <td>' + data1[1] + '</td> </tr>'

                        $('#tblAPIDataView tbody').append(row);
                    }
                    //$.each(arr, function (j, e) {
                    //    var row = '<tr>';
                    //    row += '<td>' + e["Key"] + '</td>';
                    //    row += '<td>' + e["Value"] + '</td>';
                    //});
                    //row += '</tr>';
                    //$('#tblAPIDataView tbody').append(row);
                } else {
                    $('#tblAPIDataView tbody').empty();
                    $('#lblErrormsg').text('something went wrong while data binding.').addClass('red-text');
                }
            }
            else {
                $('#tblAPIDataView tbody').empty();
                $('#lblErrormsg').text('Data not found.').addClass('red-text');
            }
        }

    });
});

//
$('#btnInvestmentDetails').click(function () {

    var ddlcategory = $('#ddlcategory').val()
    var txtFDAmount = $('#txtFDAmount').val();
    $('#lblErrormsg').text('').addClass('red-text');
    $.ajax({
        type: "GET",
        url: "/DashboardQuery/InvestmentDetails",
        data: { ddlSelectValue: ddlcategory, txtCP_Ref_No: txtFDAmount },
        dataType: 'json',
        success: function (result) {


            if (result.validResponce) {
                if (result.result.length > 0) {

                    $('#tblAPIDataView thead').empty();
                    var row = '<tr> <td> <b>Name</b></td>'
                    row += ' <td><b>Value</b></td> </tr>'
                    $('#tblAPIDataView thead').append(row);

                    $('#tblAPIDataView tbody').empty();

                    var resultParce = JSON.parse(result.result);
                    var data = resultParce.split("|");

                    var tLength = data.length;

                    for (var item = 0; item < tLength; item++) {

                        var data1 = data[item].split(":");

                        var row = '<tr> <td>' + data1[0] + '</td>'
                        row += ' <td>' + data1[1] + '</td> </tr>'

                        $('#tblAPIDataView tbody').append(row);
                    }


                    //$.each(arr, function (j, e) {
                    //    var row = '<tr>';
                    //    row += '<td>' + e["Key"] + '</td>';
                    //    row += '<td>' + e["Value"] + '</td>';
                    //});
                    //row += '</tr>';
                    //$('#tblAPIDataView tbody').append(row);
                } else {
                    $('#tblAPIDataView tbody').empty();
                    $('#lblErrormsg').text('something went wrong while data binding.').addClass('red-text');
                }
            }
            else {
                $('#tblAPIDataView tbody').empty();
                $('#lblErrormsg').text('Data not found.').addClass('red-text');
            }
        }

    });
});


$('#btnCKYCInventorInformation').click(function () {

    var ddlcategory = $('#ddlcategory').val()
    var txtFDAmount = $('#txtFDAmount').val();
    $('#lblErrormsg').text('').addClass('red-text');

    $.ajax({
        type: "GET",
        url: "/DashboardQuery/CKYCInventorInformation",
        data: { ddlSelectValue: ddlcategory, txtCP_Ref_No: txtFDAmount },
        dataType: 'json',
        success: function (result) {


            if (result.validResponce) {
                if (result.result.length > 0) {

                    $('#tblAPIDataView thead').empty();
                    var row = '<tr> <td> <b>Name</b></td>'
                    row += ' <td><b>Value</b></td> </tr>'
                    $('#tblAPIDataView thead').append(row);


                    $('#tblAPIDataView tbody').empty();

                    var resultParce = JSON.parse(result.result);
                    var data = resultParce.split("|");

                    var tLength = data.length;

                    for (var item = 0; item < tLength; item++) {

                        var data1 = data[item].split(":");

                        var row = '<tr> <td>' + data1[0] + '</td>'
                        row += ' <td>' + data1[1] + '</td> </tr>'

                        $('#tblAPIDataView tbody').append(row);
                    }

                } else {
                    $('#tblAPIDataView tbody').empty();
                    $('#lblErrormsg').text('something went wrong while data binding.').addClass('red-text');
                }
            }
            else {
                $('#tblAPIDataView tbody').empty();
                $('#lblErrormsg').text('Data not found.').addClass('red-text');
            }
        }

    });
});


$('#btnCKYCSecondHolderInformation').click(function () {

    var ddlcategory = $('#ddlcategory').val()
    var txtFDAmount = $('#txtFDAmount').val();
    $('#lblErrormsg').text('').addClass('red-text');

    $.ajax({
        type: "GET",
        url: "/DashboardQuery/CKYCSecondHolderInformation",
        data: { ddlSelectValue: ddlcategory, txtCP_Ref_No: txtFDAmount },
        dataType: 'json',
        success: function (result) {


            if (result.validResponce) {
                if (result.result.length > 0) {

                    $('#tblAPIDataView thead').empty();
                    var row = '<tr> <td> <b>Name</b></td>'
                    row += ' <td><b>Value</b></td> </tr>'
                    $('#tblAPIDataView thead').append(row);


                    $('#tblAPIDataView tbody').empty();

                    var resultParce = JSON.parse(result.result);
                    var data = resultParce.split("|");

                    var tLength = data.length;

                    for (var item = 0; item < tLength; item++) {

                        var data1 = data[item].split(":");

                        var row = '<tr> <td>' + data1[0] + '</td>'
                        row += ' <td>' + data1[1] + '</td> </tr>'

                        $('#tblAPIDataView tbody').append(row);
                    }

                } else {
                    $('#tblAPIDataView tbody').empty();
                    $('#lblErrormsg').text('something went wrong while data binding.').addClass('red-text');
                }
            }
            else {
                $('#tblAPIDataView tbody').empty();
                $('#lblErrormsg').text('Data not found.').addClass('red-text');
            }
        }

    });
});


$('#btnCKYCThirdHolderInformation').click(function () {

    
    var ddlcategory = $('#ddlcategory').val()
    var txtFDAmount = $('#txtFDAmount').val();
    $('#lblErrormsg').text('').addClass('red-text');

    $.ajax({
        type: "GET",
        url: "/DashboardQuery/CKYCThirdHolderInformation",
        data: { ddlSelectValue: ddlcategory, txtCP_Ref_No: txtFDAmount },
        dataType: 'json',
        success: function (result) {


            if (result.validResponce) {
                if (result.result.length > 0) {

                    $('#tblAPIDataView thead').empty();
                    var row = '<tr> <td> <b>Name</b></td>'
                    row += ' <td><b>Value</b></td> </tr>'
                    $('#tblAPIDataView thead').append(row);


                    $('#tblAPIDataView tbody').empty();

                    var resultParce = JSON.parse(result.result);
                    var data = resultParce.split("|");

                    var tLength = data.length;

                    for (var item = 0; item < tLength; item++) {

                        var data1 = data[item].split(":");

                        var row = '<tr> <td>' + data1[0] + '</td>'
                        row += ' <td>' + data1[1] + '</td> </tr>'

                        $('#tblAPIDataView tbody').append(row);
                    }

                } else {
                    $('#tblAPIDataView tbody').empty();
                    $('#lblErrormsg').text('something went wrong while data binding.').addClass('red-text');
                }
            }
            else {
                $('#tblAPIDataView tbody').empty();
                $('#lblErrormsg').text('Data not found.').addClass('red-text');
            }
        }

    });
});


$('#btnCKYCSecondHolderInformation').click(function () {

    var ddlcategory = $('#ddlcategory').val()
    var txtFDAmount = $('#txtFDAmount').val();
    $('#lblErrormsg').text('').addClass('red-text');

    $.ajax({
        type: "GET",
        url: "/DashboardQuery/CKCInformation_Investor",
        data: { ddlSelectValue: ddlcategory, txtCP_Ref_No: txtFDAmount },
        dataType: 'json',
        success: function (result) {


            if (result.validResponce) {
                if (result.result.length > 0) {

                    $('#tblAPIDataView thead').empty();
                    var row = '<tr> <td> <b>Name</b></td>'
                    row += ' <td><b>Value</b></td> </tr>'
                    $('#tblAPIDataView thead').append(row);


                    $('#tblAPIDataView tbody').empty();

                    var resultParce = JSON.parse(result.result);
                    var data = resultParce.split("|");

                    var tLength = data.length;

                    for (var item = 0; item < tLength; item++) {

                        var data1 = data[item].split(":");

                        var row = '<tr> <td>' + data1[0] + '</td>'
                        row += ' <td>' + data1[1] + '</td> </tr>'

                        $('#tblAPIDataView tbody').append(row);
                    }

                } else {
                    $('#lblErrormsg').text('something went wrong while data binding.').addClass('red-text');
                    $('#tblAPIDataView tbody').empty();
                }
            }
            else {
                $('#lblErrormsg').text('Data not found.').addClass('red-text');
                $('#tblAPIDataView tbody').empty();
            }
        }

    });
});


$('#btnPaymentOnlineLog').click(function () {

    var ddlcategory = $('#ddlcategory').val()
    var txtFDAmount = $('#txtFDAmount').val();
    $('#lblErrormsg').text('').addClass('red-text');

    $.ajax({
        type: "GET",
        url: "/DashboardQuery/PaymentOnlineLog",
        data: { ddlSelectValue: ddlcategory, txtCP_Ref_No: txtFDAmount },
        dataType: 'json',
        success: function (result) {


            if (result.validResponce) {
                if (result.result.length > 0) {

                    $('#tblAPIDataView thead').empty();

                    var row = '<tr> <td> <b>Name</b></td>'
                    row += ' <td><b>Value</b></td> </tr>'
                    $('#tblAPIDataView thead').append(row);


                    $('#tblAPIDataView tbody').empty();

                    var resultParce = JSON.parse(result.result);
                    var data = resultParce.split("|");

                    var tLength = data.length;

                    for (var item = 0; item < tLength; item++) {

                        var data1 = data[item].split(":");

                        var row = '<tr> <td>' + data1[0] + '</td>'
                        row += ' <td>' + data1[1] + '</td> </tr>'

                        $('#tblAPIDataView tbody').append(row);
                    }

                } else {
                    $('#lblErrormsg').text('something went wrong while data binding.').addClass('red-text');
                    $('#tblAPIDataView tbody').empty();
                }
            }
            else {
                $('#lblErrormsg').text('Data not found.').addClass('red-text');
                $('#tblAPIDataView tbody').empty();
            }
        }

    });
});


$('#btnPaymentOfflineLog').click(function () {

    var ddlcategory = $('#ddlcategory').val()
    var txtFDAmount = $('#txtFDAmount').val();
    $('#lblErrormsg').text('').addClass('red-text');

    $.ajax({
        type: "GET",
        url: "/DashboardQuery/PaymentOfflineLog",
        data: { ddlSelectValue: ddlcategory, txtCP_Ref_No: txtFDAmount },
        dataType: 'json',
        success: function (result) {


            if (result.validResponce) {
                if (result.result.length > 0) {

                    $('#tblAPIDataView thead').empty();
                    var row = '<tr> <td> <b>Name</b></td>'
                    row += ' <td><b>Value</b></td> </tr>'
                    $('#tblAPIDataView thead').append(row);


                    $('#tblAPIDataView tbody').empty();

                    var resultParce = JSON.parse(result.result);
                    var data = resultParce.split("|");

                    var tLength = data.length;

                    for (var item = 0; item < tLength; item++) {

                        var data1 = data[item].split(":");

                        var row = '<tr> <td>' + data1[0] + '</td>'
                        row += ' <td>' + data1[1] + '</td> </tr>'

                        $('#tblAPIDataView tbody').append(row);
                    }

                } else {
                    $('#lblErrormsg').text('something went wrong while data binding.').addClass('red-text');
                    $('#tblAPIDataView tbody').empty();
                }
            }
            else {
                $('#lblErrormsg').text('Data not found.').addClass('red-text');
                $('#tblAPIDataView tbody').empty();
            }
        }

    });
});



function SplitLeaderInformation(splitLI) {

    var arr = splitLI.split("|");

    if (arr.length > 0) {

        for (var item in arr) {
            var arr1 = item.split(":");

            var row = '<tr>';
            row += '<td>' + arr1[0] + '</td>';
            row += '<td>' + arr1[1] + '</td>';
            row += '</tr>';

        }

        //var row = '<tr>';
        //$.each(arr, function (j, e) {
        //    row += '<td>' + e + '</td>';
        //});
        //row += '</tr>';

        return row;
        //$('#tblAPIDataView tbody').append(row);
        //alert(row);
    }
    else {
        //$('#lblLookUpMessage').text('something went wrong while data binding.').addClass('red-text');

    }
}



