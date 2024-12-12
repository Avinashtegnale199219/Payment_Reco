$(document).ready(function () {
    
    $('#lblPageName').text('Bank Revalidation Reconciliation Dashboard');
     bindData();
});

function bindData() {
    MessageCenter('', '');

    ExtendedAjaxCall('BankRevalidationRequestDashboard_L1/Get_BankRevalidation_PendingApproval_Dashboard', null, 'GET', function (result) {
        if (result != null) {
            var dataResult = result;//JSON.parse(result.d);

            if (dataResult.status != null && dataResult.status != "0" && dataResult.data != null && dataResult.data.length > 0) {


                if ($.fn.DataTable.isDataTable('#tblBankRevalidationRequestDashboard')) {
                    $('#tblBankRevalidationRequestDashboard').dataTable().fnDestroy();
                }
                $('#tblBankRevalidationRequestDashboard > tbody').empty();
                $.each(dataResult.data, function (i, row) {
                    
                    var tr = $("<tr/>");
                    tr.append("<td style='display:none'>" + row['f_DEP_NO'] + "</td>");
//                    tr.append("<td><button type='button' class='btn btn-red btnAPRReq' id=" + row['f_DEP_NO'] + "_" + row['f_WAR_NO'] + ">View Request</button></td>");
                    tr.append("<td><button type='button' class='btn btn-red btnAPRReq' id=" + row['f_REVLD_REQDTLS_REQ_NO'] +">View Request</button></td>");

                    tr.append("<td>" + row['f_WAR_NO'] + "</td>");
                    tr.append("<td>" + row['f_DEP_NO'] + "</td>");
                    tr.append("<td>" + row['f_FOLIO_NO'] + "</td>");
                    tr.append("<td>" + row['f_DEP_NAME'] + "</td>");
                    tr.append("<td>" + row['f_WAR_AMOUNT'] + "</td>");
                    if (row['f_PAN'] == null || row['f_PAN'] == "") {
                        tr.append("<td>" + "" + "</td>");
                    }
                    else {
                        tr.append("<td>" + row['f_PAN'] + "</td>");
                    }

                    tr.append("<td>" + row['f_OLD_OFAS_VOUCHER_NO'] + "</td>");
                    tr.append("<td>" + row['f_SUBSTATUS'] + "</td>");

                    tr.append("<td>" + row['f_CREATED_BY'] + "</td>");
                    tr.append("<td>" + row['f_CREATED_DATE'] + "</td>");
                    
                   // tr.append("<td>" + row['f_REVLD_REQDTLS_REQ_NO'] + "</td>");

                    $('#tblBankRevalidationRequestDashboard').append(tr);

                });
                $('#tblBankRevalidationRequestDashboard').DataTable({
                    initComplete: function () {
                        $("#preloader").hide();
                    },
                    "order": [[0, "desc"]]
                });

              
                //$('.btnAPRReq').click(function () {
                    $(document).on("click", ".btnAPRReq", function (e) {
                    
                    var Rev_Req_No = $(this).attr('id');
                    var DEP_No = $(this).closest('tr').children('td:eq(3)').text();
                    var IW_No = $(this).closest('tr').children('td:eq(2)').text();
                    var objBO = {
                        P_Dep_NO: DEP_No,
                        P_War_NO: IW_No,
                        Rev_Req_No: Rev_Req_No
                    }
                  
                    
                    //alert("hidden value :" + $("#Hdn_REQDTLS_REQ_NO").val())
                    ExtendedAjaxCall('BankRevalidationRequestDashboard_L1/SendData', objBO, 'POST', function (result) {
                        if (result.data.P_Dep_NO != "" || result.data.p_War_NO) {
                            window.location.href = 'BankRevalidationRequest_FirstApproval/Index/'; //UAT

                            //window.location.href = '/BankRevalidationRequest_FirstApproval/Index/'; //LOCAL

                            var objectURL = InitFunction1();
                            window.location.href = objectURL.uploadedData;
                        }
                        else {
                            $('#lblErrormsg').css('color', 'red').text("Error occurs while Fetching data.");
                            $('#dvErrorMsg').html(Response);
                            $("#ModelError").modal('show');
                        }
                        //p_War_NO
                    });
                    //LOCATIIN.HREF
                });
                
            }
            else {
                $('#tblBankRevalidationRequestDashboard > tbody').empty();
                $('#tblBankRevalidationRequestDashboard  > tbody').append('<tr><td colspan="15"><center>Data Not Found</center></td></tr>');

            }
        }
        else {
            $('#tblBankRevalidationRequestDashboard > tbody').empty();
            $('#tblBankRevalidationRequestDashboard  > tbody').append('<tr><td colspan="15"><center>Data Not Found</center></td></tr>');

        }
    }, null, null, false, true);
}

