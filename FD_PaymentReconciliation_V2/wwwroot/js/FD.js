

var objInvstDtlBo = {
        "CPCode": "4545", "ApplNo": "5266",
        "CPName": "CPName", "UserName": "UserName", "AppCode": "AppCode", "CPTransRefNo": "CPTransRefNo", "MFSysRefno": "MFSysRefno",
        "Category": "Category", "Scheme": "Scheme", "SchemeCode": "sd", "IntRate": "233", "IntFreq": "33", "Tenure": "Tenure", "IsAutoRenewal": "true",
        "Amount": "23", "PaymentMode": "pm", "PaymentInstruction": "sdas", "CreatedBy": "443", "CreatedByUName": "asd", "CreatedIP": "::1", "SessionId": "322",
        "CpLocationCode": "code", "IsTnCAccepted": "true", "IsDecAccepted": "true"
    }

var objInvestorBankDtlBo = {
       CP_Code: '111',
       CP_Name: "mmfsl",
       User_Name: "raj",
       App_Code: "112233",
       CP_Trans_Ref_No: "1234",
       MF_Sys_Ref_no: "4321",
       Cp_Location_Code: "619",
       MICRCode: "22255588",
       NEFTCode: "998877",
       BankName: "SBI",
       BranchName: "JUHU",
       BankAccountNo: "021548796",
       CreatedBy: "XYZ",
       CreatedByUName: "abc",
       CreatedIP: "12345698",
       SessionId: "5241698"
}

var objKycdetailsBo = [];

var ObjKYCDataDetail1 = {
    Ckyc_Queueid: 'A',
    Source_Type: 'A',
    Source_Sub_Type: 'A',
    Holder_Type: 'A',
    Appl_No: 'A',
    CP_Trans_Ref_No: 'A',
    Mf_Sys_Ref_no: '',
    Mcp_Code: 'A',
    Mcp_Busi_Code: 'A',
    Cp_Location_Code: '1',
    Scp_Code: '1',
    Cp_Api_User_Name: 'A',
    Gateway_Entitycode: 'A',
    Gateway_TransactionID: '12',
    Ckyc_Resp_Tran_Status: 'A',
    Cyc_Resp_Rej_Code: 'A',
    Ckyc_Resp_Rej_Desc: 'A',
    BranchCode: 'A',
    RecordIdentifier: '1',
    ApplicationFormNo: '1',
    Kyc_ConstiType: '1',
    Kyc_AccType: '1',
    Kyc_Number: '1',
    Kyc_NamePrefix: 'A',
    Kyc_FirstName: 'AA',
    Kyc_MiddleName: 'BB',
    Kyc_LastName: 'CC',
    Kyc_FullName: 'DD',
    Kyc_MaidenNamePrefix: 'A',
    Kyc_MaidenFirstName: 'AA',
    Kyc_MaidenMiddleName: '',
    Kyc_MaidenLastName: 'A',
    Kyc_MaidenFullName: 'A',
    Kyc_FatherNamePrefix: 'A',
    Kyc_FatherFirstName: 'A',
    Kyc_FatherMiddleName: 'A',
    Kyc_FatherLastName: 'A',
    Kyc_FatherFullName: 'A',
    Kyc_SpouseNamePrefix: 'A',
    Kyc_SpouseFirstName: 'A',
    Kyc_SpouseMiddleName: 'A',
    Kyc_SpouseLastName: 'A',
    Kyc_SpouseFullName: 'A',
    Kyc_MotherNamePrefix: 'A',
    Kyc_MotherFirstName: 'A',
    Kyc_MotherMiddletName: 'A',
    Kyc_MotherLastName: 'A',
    Kyc_MotherFullName: 'A',
    Kyc_Gender: 'A',
    Kyc_MaritalStatus: 'A',
    Kyc_Nationality_Code: 'A',
    Kyc_Nationality_Desc: 'A',
    Kyc_Occupation_Code: 'A',
    Kyc_Occupation_Desc: 'A',
    Kyc_DOB: '2019-01-01',
    Kyc_ResidentialStatus_Code: '1',
    Kyc_ResidentialStatus_Desc: 'A',
    Kyc_TaxResidencyOutsideIndia_Code: '1',
    Kyc_TaxResidencyOutsideIndia_Mst: 'A',
    Kyc_JurisdictionofRes_Code: '1',
    Kyc_JurisdictionofRes_Desc: 'A',
    Kyc_TIN: '1',
    Kyc_CountryOfBirth: 'A',
    Kyc_PlaceOfBirth: 'A',
    Kyc_Per_AddType_Code: '1',
    Kyc_Per_AddType_Desc: '1',
    Kyc_Per_Add1: $('#txtaddress1').val(),
    Kyc_Per_Add2: $('#txtaddress2').val(),
    Kyc_Per_Add3: $('#txtaddress3').val(),
    Kyc_Per_AddCity_Desc: 'A',
    Mf_Kyc_Per_AddCity_Desc: 'A',
    Kyc_Per_AddDistrict_Desc: 'A',
    Mf_Kyc_Per_AddDistrict_Desc: 'A',
    Kyc_Per_AddState_Code: '1',
    Mf_Per_AddState_Code: '1',
    Kyc_Per_AddState_Desc: 'A',
    Mf_Kyc_Per_AddState_Desc: 'A',
    Kyc_Per_AddCountry_Code: '1',
    Kyc_Per_AddCountry_Desc: 'A',
    Kyc_Per_AddPin: $('#txtPIN').val(),
    Kyc_Per_AddPOA: 'A',
    Kyc_Per_AddPOAOthers: 'A',
    Kyc_Per_AddSameAsCor_Add: 'A',
    Kyc_Cor_Add1: 'A',
    Kyc_Cor_Add2: 'A',
    Kyc_Cor_Add3: 'A',
    Kyc_Cor_AddCity: '',
    Kyc_Cor_AddDistrict: $('#ddlDistrict option:selected').text(),
    Kyc_Cor_AddState_Code: 'A',
    Kyc_Cor_AddState_desc: 'A',
    Mf_Kyc_Cor_AddState_Code: $('#ddlState').val(),
    Mf_Kyc_Cor_AddState_Desc: $('#ddlState option:selected').text(),
    Kyc_Cor_AddCountry_Code: 'A',
    Kyc_Cor_AddCountry_Desc: 'A',
    Kyc_Cor_AddPin: 'A',
    Kyc_PerAddSameAsJurAdd: 'A',
    Kyc_Jur_Add1: 'A',
    Kyc_Jur_Add2: 'A',
    Kyc_Jur_Add3: 'A',
    Kyc_Jur_AddCity_Desc: 'A',
    Kyc_Jur_AddState_Desc: 'A',
    Mf_Kyc_Jur_AddState_Code: '1',
    Mf_Kyc_Jur_AddState_Desc: 'A',
    Kyc_Jur_AddCountry_Code: 'A',
    Kyc_Jur_AddCountry_Desc: 'A',
    Kyc_Jur_AddPin: '1',
    Kyc_ResTelSTD: '1',
    Kyc_ResTelNumber: '1',
    Kyc_OffTelSTD: '1',
    Kyc_OffTelNumber: '1',
    Kyc_MobileISD: 'A',
    Kyc_MobileNumber: '1',
    Kyc_FAXSTD: 'A',
    Kyc_FaxNumber: 'A',
    Kyc_EmailAdd: 'A',
    Kyc_Remarks: 'A',
    Kyc_DateofDeclaration: '2019-01-01',
    Kyc_PlaceofDeclaration: 'A',
    Kyc_VerificationDate: '2019-01-01',
    Kyc_TypeofDocSubmitted: 'A',
    Kyc_VerificationName: 'A',
    Kyc_VerificationDesg: 'A',
    Kyc_VerificationBranch: 'A',
    Kyc_VerificationEmpcode: 'A',
    Kyc_NumberofIds: 'A',
    Kyc_NumberofRelatedPersons: 'A',
    Kyc_NumberofLocalAdds: 'A',
    Kyc_NumberofImages: 'A',
    Kyc_NameUpdated: 'A',
    Kyc_PersonalorEntityDetailsUpdated: 'A',
    Kyc_AddressDetailsUpdated: 'A',
    Kyc_ContactDetailsUpdated: 'A',
    Kyc_RemarksUpdated: 'A',
    Kyc_VerificationUpdated: 'A',
    Kyc_ID_DetailsUpdated: 'A',
    Kyc_RelatedPersonsUpdated: 'A',
    Kyc_ImageDetailsUpdated: 'A'


    //objKycdetailsBo.Kyc_Per_Add1 = $('#txtaddress1').val();// user address1
    //objKycdetailsBo.Kyc_Per_Add2 = $('#txtaddress2').val();//user address2
    //objKycdetailsBo.Kyc_Per_Add3 = $('#txtaddress3').val();//user address3
    //objKycdetailsBo.Kyc_Per_AddPin = $('#txtPIN').val(); //PIN CODE
    //objKycdetailsBo.Mf_Kyc_Per_AddState_Desc = $('#ddlState option:selected').text();//STATE DESC  
    //objKycdetailsBo.Mf_Per_AddState_Code = $('#ddlState').val();//STATE CODE
    //objKycdetailsBo.Mf_Kyc_Per_AddDistrict_Desc = $('#ddlDistrict option:selected').text();//DISTRICT DESC 
    //objKycdetailsBo.DISTRICTCODE = $('#ddlDistrict').val();//DISTRICT CODE
    //objKycdetailsBo.pan = $('#txtPAN').val();//PAN 
    //objKycdetailsBo.addressproof = $('#ddladdressproof').val();//ADDRESS PRO0F
    //objKycdetailsBo.DepositPayableTo = $('#ddlDeposit').val();//DEPOSOT PAYABLE TO


    
}

objKycdetailsBo.push(ObjKYCDataDetail1);

var objNBo = {
        "Source_Type": "CP",
        "Source_Sub_Type": "CP",
        "Holder_Type": "H",
        "Appl_No": "1",
        "CP_Trans_Ref_No": "",
        "Mf_Sys_Ref_no": "",
        "Mcp_Code": "1",
        "Mcp_Busi_Code": "1",
        "Cp_Location_Code": "1",
        "Scp_Code": "1",
        "Cp_Api_User_Name": "S",
        "Gateway_Entitycode": "1",
        "Gateway_TransactionID": "S",
        "Ckyc_Resp_Tran_Status": "A",
        "Cyc_Resp_Rej_Code": "1",
        "Ckyc_Resp_Rej_Desc": "A",
        "BranchCode": "1",
        "RecordIdentifier": "A",
        "ApplicationFormNo": "12",
        "Kyc_ConstiType": "A",
        "Kyc_AccType": "A",
        "Kyc_Number": "A",
        "Kyc_NamePrefix": "A",
        "Kyc_FirstName": "A",
        "Kyc_MiddleName": "A",
        "Kyc_LastName": "A",
        "Kyc_FullName": "A",
        "Kyc_MaidenNamePrefix": "A",
        "Kyc_MaidenFirstName": "A",
        "Kyc_MaidenMiddleName": "A",
        "Kyc_MaidenLastName": "A",
        "Kyc_MaidenFullName": "A",
        "Kyc_FatherNamePrefix": "A",
        "Kyc_FatherFirstName": "A",
        "Kyc_FatherMiddleName": "A",
        "Kyc_FatherLastName": "A",
        "Kyc_FatherFullName": "A",
        "Kyc_SpouseNamePrefix": "A",
        "Kyc_SpouseFirstName": "A",
        "Kyc_SpouseMiddleName": "A",
        "Kyc_SpouseLastName": "A",
        "Kyc_SpouseFullName": "A",
        "Kyc_MotherNamePrefix": "A",
        "Kyc_MotherFirstName": "A",
        "Kyc_MotherMiddletName": "A",
        "Kyc_MotherLastName": "A",
        "Kyc_MotherFullName": "A",
        "Kyc_Gender": "M",
        "Kyc_MaritalStatus": "A",
        "Kyc_Nationality_Code": "S",
        "Kyc_Nationality_Desc": "S",
        "Kyc_Occupation_Code": "S",
        "Kyc_Occupation_Desc": "S",
        "Kyc_DOB": "2019/01/01",
        "Kyc_ResidentialStatus_Code": "1",
        "Kyc_ResidentialStatus_Desc": "1",
        "Kyc_TaxResidencyOutsideIndia_Code": "1",
        "Kyc_TaxResidencyOutsideIndia_Mst": "1",
        "Kyc_JurisdictionofRes_Code": "1",
        "Kyc_JurisdictionofRes_Desc": "1",
        "Kyc_TIN": "1",
        "Kyc_CountryOfBirth": "A",
        "Kyc_PlaceOfBirth": "A",
        "Kyc_Per_AddType_Code": "1",
        "Kyc_Per_AddType_Desc": "A",
        "Kyc_Per_Add1": "",
        "Kyc_Per_Add2": "",
        "Kyc_Per_Add3": "",
        "Kyc_Per_AddCity_Desc": "A",
        "Kyc_Per_AddDistrict_Desc": "A",
        "Kyc_Per_AddState_Code": "1",
        "Mf_Per_AddState_Code": "1",
        "Kyc_Per_AddState_Desc": "A",
        "Mf_Kyc_Per_AddState_Desc": "A",
        "Kyc_Per_AddCountry_Code": "1",
        "Kyc_Per_AddCountry_Desc": "A",
        "Kyc_Per_AddPin": "123456",
        "Kyc_Per_AddPOA": "1",
        "Kyc_Per_AddPOAOthers": "1",
        "Kyc_Per_AddSameAsCor_Add": "1",
        "Kyc_Cor_Add1": "A",
        "Kyc_Cor_Add2": "A",
        "Kyc_Cor_Add3": "A",
        "Kyc_Cor_AddCity": "A",
        "Kyc_Cor_AddDistrict": "",
        "Kyc_Cor_AddState_Code": "A",
        "Kyc_Cor_AddState_desc": "A",
        "Mf_Kyc_Cor_AddState_Code": "1",
        "Mf_Kyc_Cor_AddState_Desc": "A",
        "Kyc_Cor_AddCountry_Code": "1",
        "Kyc_Cor_AddCountry_Desc": "A",
        "Kyc_Cor_AddPin": "1",
        "Kyc_PerAddSameAsJurAdd": "A",
        "Kyc_Jur_Add1": "A",
        "Kyc_Jur_Add2": "A",
        "Kyc_Jur_Add3": "A",
        "Kyc_Jur_AddCity_Desc": "A",
        "Kyc_Jur_AddState_Desc": "A",
        "Mf_Kyc_Jur_AddState_Code": "1",
        "Mf_Kyc_Jur_AddState_Desc": "A",
        "Kyc_Jur_AddCountry_Code": "1",
        "Kyc_Jur_AddCountry_Desc": "A",
        "Kyc_Jur_AddPin": "1",
        "Kyc_ResTelSTD": "1",
        "Kyc_ResTelNumber": "1",
        "Kyc_OffTelSTD": "1",
        "Kyc_OffTelNumber": "1",
        "Kyc_MobileISD": "1",
        "Kyc_MobileNumber": "1",
        "Kyc_FAXSTD": "1",
        "Kyc_FaxNumber": "1",
        "Kyc_EmailAdd": "1",
        "Kyc_Remarks": "A",
        "Kyc_DateofDeclaration": "01-01-2012",
        "Kyc_PlaceofDeclaration": "1",
    "Kyc_VerificationDate": "01-01-2012",
        "Kyc_TypeofDocSubmitted": "A",
        "Kyc_VerificationName": "A",
        "Kyc_VerificationDesg": "A",
        "Kyc_VerificationBranch": "A",
        "Kyc_VerificationEmpcode": "1",
        "Kyc_NumberofIds": "2",
        "Kyc_NumberofRelatedPersons": "2",
        "Kyc_NumberofLocalAdds": "A",
        "Kyc_NumberofImages": "2",
        "Kyc_NameUpdated": "AAA",
        "Kyc_PersonalorEntityDetailsUpdated": "AAA",
        "Kyc_AddressDetailsUpdated": "AA",
        "Kyc_ContactDetailsUpdated": "BB",
        "Kyc_RemarksUpdated": "AA",
        "Kyc_VerificationUpdated": "SS",
        "Kyc_ID_DetailsUpdated": "S",
        "Kyc_RelatedPersonsUpdated": "A",
        "Kyc_ImageDetailsUpdated": "A",
        "CreatedIP": "",
        "CreatedBy": "",
        "CreatedType": "",
        "SessionID": "",
        "CreatedByUName": ""
    }

var LeadDetails= {
    CPCode :'',
    CPName :'',
    UserName: '',   
    AppCode: '',     
    Salutation: '',         
    Name : '',
    Gender: '',  
    Mobile : '',  
    EmailId : '',  
    PAN  : '',
    Amount : '',
    DOB : '',
    LeadType: '', 
    Source: '', 
    CreatedBy: '', 
    CreatedByUName: '', 
    CreatedIP: '', 
    SessionId : '',
    TransRefNo : '',
    SysRefno: '', 
    LocationCode:''
}

var MFSysRefno = "";

$(document).ready(function () {
     
    //Page Setup
    // MainBtnHandler(false, false, false, false, false, false, false);//Set Buttons
    $('#pnlQuickBook,#fdInvestmentDetails,#pnlKYC, #pnlfdhome, #pnlkyc,#pnlSECONDHOLDER,#pnlthirdHOLDER,#pnlbankdetails, #pnlpersonal, #pnlholder, #pnlnominee, #pnlcalulate').show();//Show Buttons
    //$('#lblPageName').text('First Level Authorization');//Set Page Name
    $('#pnlfdhome1,#pnlholder').hide();

      
    GetUserDtl();
    //Defined in AppConfig.js in CDN
    
    //gettenure();
    getcategory();
    getScheme();
    getState();
     
    getAddressProof();
    $('#txtDOB,#txtDOB1,#txtshDOB,#txtthDOB').datepicker({ autoclose: true, format: 'dd/mm/yyyy' });

});

$('#ddlScheme').on('change', function () {
  
    if ($(this).val() != 0) {
        var SchemeTypeCode = $(this).val();
        var CategoryCode = $('#ddlcategory').val();
         
        
        //Bind Dropdown
        //---------

        //GetDepartment

        ExtendedAjaxCall(Api_settings.url + 'InterestFrequencyMaster?SchemeTypeCode=' + SchemeTypeCode + '&CategoryCode=' + CategoryCode + '', null, 'GET', function (result) {
           
            if (result != null && typeof (result) != 'undefined') {
                if (result.length > 0) {
                    $('#ddlinterest').empty().append('<option value="0">Select Branch</option>');
                    $.each(result, function (i, row) {
                        $('#ddlinterest').append("<option value='" + row['Branch'] + "' data-row='" + JSON.stringify(row) + "'>" + row['BRNDES'] + "</option>");
                    });

                } else {
                    $('#lblLookUpMessage').text('No department found').addClass('red-text');
                }
            } else {
                $('#lblLookUpMessage').text('something went wrong while binding State').addClass('red-text');
            }
        }, null, null, null, true);
    }
});

$('#ddlcategory').on('change', function () {
   
    if ($(this).val() != 0) {
        var CategoryCode = $(this).val();
        var SchemeTypeCode = $('#ddlScheme').val();
        
        var InterestFreq = "Y";
        //Bind Dropdown
        //---------

        //GetDepartment

        ExtendedAjaxCall(Api_settings.url + 'TenureMaster?SchemeTypeCode=' + SchemeTypeCode + '&CategoryCode=' + CategoryCode + '&InterestFreq=' + InterestFreq + '', null, 'GET', function (result) {
           
            if (result != null && typeof (result) != 'undefined') {
                if (result.length > 0) {
                    $('#ddlTenure').empty().append('<option value="0">Select Tenure</option>');
                    $.each(result, function (i, row) {
                        $('#ddlTenure').append("<option value='" + row['Tenure'] + "' data-row='" + JSON.stringify(row) + "'>" + row['Tenure'] + "" + "months--" + row['Interest_Rate'] + "</option>");
                    });

                } else {
                    $('#lblLookUpMessage').text('No department found').addClass('red-text');
                }
            } else {
                $('#lblLookUpMessage').text('something went wrong while binding State').addClass('red-text');
            }
        }, null, null, null, true);
    }
});

$('#ddlState').on('change', function () {
  
    if ($(this).val() != 0) {
        var GeoMFStateCode = $(this).val();
        


        //Bind Dropdown
        //---------

        //GetDepartment

        ExtendedAjaxCall(Api_settings.url + 'MFDistrict?GeoMFStateCode=' + GeoMFStateCode + '', null, 'GET', function (result) {
            
            if (result != null && typeof (result) != 'undefined') {
                if (result.length > 0) {
                    $('#ddlDistrict').empty().append('');
                    $.each(result, function (i, row) {
                        $('#ddlDistrict').append("<option value='" + row['MF_State_Code'] + "' data-row='" + JSON.stringify(row) + "'>" + row['MF_District_Name'] + "</option>");
                    });

                } else {
                    $('#lblLookUpMessage').text('No department found').addClass('red-text');
                }
            } else {
                $('#lblLookUpMessage').text('something went wrong while binding State').addClass('red-text');
            }
        }, null, null, null, true);
    }
});

function getcategory() {

    var SchemeTypeCode = "C";
    var CategoryCode = "PU";
  
  
    ExtendedAjaxCall(Api_settings.url + 'CategoryMaster?', null, 'GET', function (result) {
       
        if (result !== null && typeof (result) !== 'undefined') {
            if (result.length > 0) {
                $('#ddlcategory').empty().append('<option value="0">All</option>');
                //$('#ddlBranch').empty();
                $.each(result, function (i, row) {
                    $('#ddlcategory').append("<option value='" + row['Category_Code'] + "' data-row='" + JSON.stringify(row) + "'>" + row['Category_Name'] + "</option>");
                });

            } else {
                $('#lblLookUpMessage').text('No company found').addClass('red-text');
            }
        } else {
            $('#lblLookUpMessage').text('something went wrong while binding company').addClass('red-text');
        }
    }, null, null, false, true);

}

function getScheme() {

   
    
    ExtendedAjaxCall(Api_settings.url + 'SchemeMaster?', null, 'GET', function (result) {
        
        if (result !== null && typeof (result) !== 'undefined') {
            if (result.length > 0) {
                $('#ddlScheme').empty().append('<option value="0">All</option>');
                //$('#ddlBranch').empty();
                $.each(result, function (i, row) {
                    $('#ddlScheme').append("<option value='" + row['Scheme_Code'] + "' data-row='" + JSON.stringify(row) + "'>" + row['Scheme_Name'] + "</option>");
                });

            } else {
                $('#lblLookUpMessage').text('No company found').addClass('red-text');
            }
        } else {
            $('#lblLookUpMessage').text('something went wrong while binding company').addClass('red-text');
        }
    }, null, null, false, true);

}

function getState() {

    
    ExtendedAjaxCall(Api_settings.url + 'MFState?', null, 'GET', function (result) {
       
        if (result !== null && typeof (result) !== 'undefined') {
            if (result.length > 0) {
                $('#ddlState').empty().append('<option value="0">All</option>');
                //$('#ddlBranch').empty();
                $.each(result, function (i, row) {
                    $('#ddlState').append("<option value='" + row['MF_State_Code'] + "' data-row='" + JSON.stringify(row) + "'>" + row['MF_State_Name'] + "</option>");
                });

            } else {
                $('#lblLookUpMessage').text('No company found').addClass('red-text');
            }
        } else {
            $('#lblLookUpMessage').text('something went wrong while binding company').addClass('red-text');
        }
    }, null, null, false, true);

}

function getAddressProof() {
  
    ExtendedAjaxCall(Api_settings.url + 'CKYCOvdMaster?', null, 'GET', function (result) {
       
        if (result !== null && typeof (result) !== 'undefined') {
            if (result.length > 0) {
                $('#ddladdressproof').empty().append('<option value="0">All</option>');
                //$('#ddlBranch').empty();
                $.each(result, function (i, row) {
                    $('#ddladdressproof').append("<option value='" + row['CKYC_OVD_Id'] + "' data-row='" + JSON.stringify(row) + "'>" + row['CKYC_OVD_Desc'] + "</option>");
                });

            } else {
                $('#lblLookUpMessage').text('No company found').addClass('red-text');
            }
        } else {
            $('#lblLookUpMessage').text('something went wrong while binding company').addClass('red-text');
        }
    }, null, null, false, true);

}

$('#chkActive').on('click', function () {
   
    if ($('#chkActive').prop('checked')) {

        $('#txtNMaddress1, #txtNMaddress2, #txtNMaddress3, #txtNMCity, #txtNMPinCode').attr('disabled', true);

        $('#txtNMaddress1').val($('#txtSHaddress1').val());
        $('#txtNMaddress2').val($('#txtSHaddress2').val());
        $('#txtNMaddress3').val($('#txtSHaddress3').val());
        $('#txtNMCity').val($('#txtSHCity').val());
        $('#txtNMPinCode').val($('#txtSHPinCode').val());



    }
    else {

        $('#txtNMaddress1, #txtNMaddress2, #txtNMaddress3, #txtNMCity, #txtNMPinCode').attr('disabled', false);
        $('#txtNMaddress2,#txtNMaddress1,#txtNMaddress3,#txtNMCity,#txtNMPinCode').val('');

    }
  
});

$('#chkActive1').on('click', function () {
   
    if ($('#chkActive1').prop('checked')) {

        $('#txttHaddress1, #txttHaddress2, #txttHaddress3, #txttHCity, #txttHPinCode').attr('disabled', true);

        $('#txttHaddress1').val($('#txtSHaddress1').val());
        $('#txttHaddress2').val($('#txtSHaddress2').val());
        $('#txttHaddress3').val($('#txtSHaddress3').val());
        $('#txttHCity').val($('#txtSHCity').val());
        $('#txttHPinCode').val($('#txtSHPinCode').val());



    }
    else {

        $('#txttHaddress1, #txttHaddress2, #txttHaddress3, #txttHCity, #txtNMPinCode').attr('disabled', false);
        $('#txttHaddress1,#txttHaddress2,#txttHaddress3,#txttHCity,#txttHPinCode').val('');

    }

});

$('#btnContinue').on('click', function () {
   
    var FDChannel = {

        Name: '',
        DOB: '',
        FDAMOUNT: '',
        Mobile: '',
        Email: ''
    };


    LeadDetails.Name = $('#txtName1').val();
    LeadDetails.DOB = convertMMDDYYYY($('#txtDOB1').val());
   // LeadDetails.DOB = "2019-01-01";
    LeadDetails.TransRefNo = "111";
    LeadDetails.LocationCode = "ho";
    LeadDetails.Amount = $('#txtFDAmount1').val();
    LeadDetails.Mobile = $('#txtMobileNumber1').val();
    LeadDetails.EmailId = $('#txtEmail1').val();
    LeadDetails.CreatedBy = UserMst.SapCode;
    LeadDetails.CreatedByUName = UserMst.UpdatedByUname;
    LeadDetails.SessionId = UserMst.SessionId;
    LeadDetails.CreatedIP = UserMst.CreatedIP;

   
   
    $('#txtName').val(LeadDetails["Name"]);
    $('#txtDOB').val(LeadDetails["DOB"]);
    $('#txtFDAmount').val(LeadDetails["Amount"]);
    $('#txtMobileNumber').val(LeadDetails["Mobile"]);
    $('#txtEmail').val(LeadDetails["Email"]);
    //$('#pnlQuickBook').show();
    //$('#pnlfdhome,#fdInvestmentDetails,#pnlKYC').hide();
   
    

     ExtendedAjaxCall(Api_settings.url + 'SaveLeadDetails', LeadDetails,  'POST', function (result) {
        
            $('#pnlQuickBook,#fdInvestmentDetails,#pnlKYC, #pnlfdhome, #pnlkyc, #pnlbankdetails, #pnlpersonal, #pnlholder, #pnlnominee, #pnlcalulate').show();//Show Buttons
        if (result != null && typeof (result) != 'undefined') {
           

            MFSysRefno = result["res"];
                //MessageCenter(result.msg, "success");
                //$('#compiledata').hide();
            $('#pnlQuickBook,#fdInvestmentDetails,#pnlKYC, #pnlfdhome, #pnlkyc, #pnlbankdetails, #pnlpersonal, #pnlholder, #pnlnominee, #pnlcalulate').show();//Show Buttons
            $('#pnlfdhome1,#pnlholder').hide();


        }
         
        else {
            MessageCenter("Something went wrong", "error");
        }

    }, null, null, false, true);



});

$('#txtSearch').on('blur', function () {
   
    var bankname = $('#txtSearch').val();
    //$('#pnlQuickBook').show();
    //$('#pnlfdhome,#fdInvestmentDetails,#pnlKYC').hide();
   
    ExtendedAjaxCall("http://172.30.21.230:2019/WA_EP_CORE_FD_CP_API/api/" + 'GetBankDetails?SearchText=' + bankname , null, 'GET', function (result) {
       
        $('#txtmicrcode').val(result[0]["F_MICR_CODE"]);
        $('#txtNEFT').val(result[0]["F_NEFT_CODE"]);
        $('#txtbankname').val(result[0]["F_BANK_NAME"]);
        $('#txtbankbranch').val(result[0]["f_BANK_BRANCH"]);
       // $('#txtEmail').val(result["Email"]);
       

    }, null, null, false, true);


});

$('#btnInvest').on('click', function () {

    MessageCenter("");

    if ($('#ddlcategory').val() == "0") {
        //MessageCenter("Please select Category")
        MessageCenter("Please select Category", 'error');
        return false;
    }

    if ($('#ddlScheme').val() == "0") {
        //MessageCenter("Please select Tenure")
        MessageCenter("Please select Scheme", 'error');
        return false;

    }

    if ($('#ddlTenure').val() == "0") {  //!= 0
        MessageCenter("Please select Tenure", 'error');
        return false;
    }

    if ($('#txtbankaccno').val() == $('#txtrebankaccno').val()) {

        objInvestorBankDtlBo.BankAccountNo = $('#txtbankaccno').val();
    }
    else {

        MessageCenter(" Account Number MisMatch", 'error');
        return false;
    }

    objInvstDtlBo.TransRefNo = "111";
    objInvstDtlBo.LocationCode = "ho";

    objInvstDtlBo.Name = $('#txtName').val();
    objInvstDtlBo.DOB = convertMMDDYYYY($('#txtDOB').val());
    objInvstDtlBo.Amount = $('#txtFDAmount').val();
    objInvstDtlBo.Mobile = $('#txtMobileNumber').val();
    objInvstDtlBo.EmailId = $('#txtEmail').val();
    objInvstDtlBo.Tenure = $('#ddlTenure').val();
    objInvstDtlBo.SchemeCode = $('#ddlScheme option:selected').text();    
    objInvstDtlBo.Scheme = $('#ddlScheme').val();
    objInvstDtlBo.Category = $('#ddlcategory').val();

    //$('#pnlQuickBook').show();
    //$('#pnlfdhome,#fdInvestmentDetails,#pnlKYC').hide();

    ExtendedAjaxCall(Api_settings.url + 'SaveLeadDetails', objInvstDtlBo, 'POST', function (result) {
        
        $('#pnlQuickBook,#fdInvestmentDetails,#pnlKYC, #pnlfdhome, #pnlkyc, #pnlbankdetails, #pnlpersonal, #pnlholder, #pnlnominee, #pnlcalulate').show();//Show Buttons
        if (result != null && typeof (result) != 'undefined') {
           

            MFSysRefno = result["res"];
            //MessageCenter(result.msg, "success");
            //$('#compiledata').hide();
            $('#pnlQuickBook,#fdInvestmentDetails,#pnlKYC, #pnlfdhome, #pnlkyc, #pnlbankdetails, #pnlpersonal, #pnlholder, #pnlnominee, #pnlcalulate').show();//Show Buttons
            //$('#pnlfdhome1').hide();

        }

        else {
            MessageCenter("Something went wrong", "error");
        }

    }, null, null, false, true);

    objInvstDtlBo.MFSysRefno = MFSysRefno;
    //objInvstDtlBo.CreatedBy = UserMst.SapCode;
    //objInvstDtlBo.CreatedByUName = UserMst.UpdatedByUname;
    //objInvstDtlBo.SessionId = UserMst.SessionId;
    //objInvstDtlBo.CreatedIP = UserMst.CreatedIP;

    objInvestorBankDtlBo.SearchBank = $('#txtSearch').val();
    objInvestorBankDtlBo.MICRCode = $('#txtmicrcode').val();
    objInvestorBankDtlBo.NEFTCode = $('#txtNEFT').val();
    objInvestorBankDtlBo.BankName = $('#txtbankname').val();
    objInvestorBankDtlBo.BranchName = $('#txtbankbranch').val();
    objInvestorBankDtlBo.BankAccountNo = $('#txtbankaccno').val();

    //objKycdetailsBo.Kyc_Per_Add1 = $('#txtaddress1').val();// user address1
    //objKycdetailsBo.Kyc_Per_Add2 = $('#txtaddress2').val();//user address2
    //objKycdetailsBo.Kyc_Per_Add3 = $('#txtaddress3').val();//user address3
    //objKycdetailsBo.Kyc_Per_AddPin = $('#txtPIN').val(); //PIN CODE
    //objKycdetailsBo.Mf_Kyc_Per_AddState_Desc = $('#ddlState option:selected').text();//STATE DESC  
    //objKycdetailsBo.Mf_Per_AddState_Code = $('#ddlState').val();//STATE CODE
    //objKycdetailsBo.Mf_Kyc_Per_AddDistrict_Desc = $('#ddlDistrict option:selected').text();//DISTRICT DESC 
    //objKycdetailsBo.DISTRICTCODE = $('#ddlDistrict').val();//DISTRICT CODE
    //objKycdetailsBo.pan = $('#txtPAN').val();//PAN 
    //objKycdetailsBo.addressproof = $('#ddladdressproof').val();//ADDRESS PRO0F
   //objKycdetailsBo.DepositPayableTo = $('#ddlDeposit').val();//DEPOSOT PAYABLE TO
    //LeadDetails.secondholder = $('#txtsecondholdername').val();
    //LeadDetails.thirdholder = $('#txtthirdholdername').val();

    objNBo.Kyc_FullName = $('#txtNMName').val();//Nominee Name
    objNBo.Kyc_MobileNumber = $('#txtNMMobile').val();//Nominee Mobile number
    objNBo.Kyc_DOB = convertMMDDYYYY($('#txtDOB1').val());// DoB
    objNBo.NomineeRelationship = $('#ddlNMRel').val(); //Nominee Relationship
    objNBo.Kyc_Cor_Add1 = $('#txtNMaddress1').val(); //Nominee Add1
    objNBo.Kyc_Cor_Add2 = $('#txtNMaddress2').val();//Nominee Add2
    objNBo.Kyc_Cor_Add3 = $('#txtNMaddress3').val();//Nominee Add3
    objNBo.Kyc_Cor_AddCity = $('#txtNMCity').val();//Nominee city
    objNBo.Kyc_Cor_AddPin = $('#txtNMPinCode').val();//Nominee pin

    LeadDetails.secondname = $('#txtsecondholdername').val();//Second Name
    LeadDetails.SecondMobile = $('#txtSHMobile').val();//Second Mobile number
    LeadDetails.SecondDoB = convertMMDDYYYY($('#txtshDOB').val());//Second DoB
    LeadDetails.SecondRelationship = $('#ddlshRel').val(); //Second Relationship
    LeadDetails.SecondAdd1 = $('#txtSHaddress1').val(); //Second Add1
    LeadDetails.SecondAdd2 = $('#txtSHaddress2').val();//Second Add2
    LeadDetails.SecondAdd3 = $('#txtSHaddress3').val();//Second Add3
    LeadDetails.Secondcity = $('#txtSHCity').val();//Second city
    LeadDetails.Secondpin = $('#txtSHPinCode').val();//Second pin

    LeadDetails.thirdName = $('#txtthirdholdername').val();//thirdName
    LeadDetails.thirdMobile = $('#txttHMobile').val();//third Mobile number
    LeadDetails.thirdDoB = convertMMDDYYYY($('#txtthDOB').val());// third DoB
    LeadDetails.thirdRelationship = $('#ddlthRel').val(); //third Relationship
    LeadDetails.thirdAdd1 = $('#txttHaddress1').val(); //third Add1
    LeadDetails.thirdAdd2 = $('#txttHaddress2').val();//third Add2
    LeadDetails.thirdAdd3 = $('#txttHaddress3').val();//third Add3
    LeadDetails.thirdcity = $('#txttHCity').val();//third city
    LeadDetails.thirdpin = $('#txttHPinCode').val();//third pin
    

    var secondholder = {
        
        Holder_Type: 'H1',
       
        Kyc_FirstName: LeadDetails.secondname,
       
        Kyc_DOB: LeadDetails.SecondDoB,
        
        Kyc_Per_Add1: LeadDetails.SecondAdd1,
        Kyc_Per_Add2: LeadDetails.SecondAdd2,
        Kyc_Per_Add3: LeadDetails.SecondAdd3,
       
        Mf_Kyc_Per_AddCity_Desc: LeadDetails.Secondcity,
       
        Kyc_Per_AddPin: LeadDetails.Secondpin,
        
        Kyc_MobileISD: LeadDetails.SecondMobile,
        
        Kyc_DateofDeclaration: '2019-01-01',
       
        Kyc_VerificationDate: '2019-01-01',
       



        //secondname: LeadDetails.secondname,
        //SecondMobile: LeadDetails.SecondMobile,
        //SecondDoB: LeadDetails.SecondDoB,
        //SecondRelationship: LeadDetails.SecondRelationship,
        //SecondAdd1: LeadDetails.SecondAdd1,
        //SecondAdd2: LeadDetails.SecondAdd2,
        //SecondAdd3: LeadDetails.SecondAdd3,
        //Secondcity: LeadDetails.Secondcity,
        //Secondpin: LeadDetails.Secondpin
    };

    objKycdetailsBo.push(secondholder);

    var thirdhlder = {
        
        Holder_Type: 'H1',
        
        Kyc_FirstName: LeadDetails.thirdName,
       
        Kyc_DOB: '2019-01-01',
        
        Kyc_PlaceOfBirth: LeadDetails.thirdDoB,
       
        Kyc_Per_Add1: LeadDetails.thirdAdd1,
        Kyc_Per_Add2: LeadDetails.thirdAdd2,
        Kyc_Per_Add3: LeadDetails.thirdAdd3,
       
        Mf_Kyc_Per_AddCity_Desc: LeadDetails.thirdcity,
       
        Kyc_Per_AddPin: LeadDetails.thirdpin,
       
        Kyc_MobileISD: LeadDetails.thirdMobile,
       
        Kyc_DateofDeclaration: '2019-01-01',
       
        Kyc_VerificationDate: '2019-01-01',
        

        //thirdName: LeadDetails.thirdName,
        //thirdMobile: LeadDetails.thirdMobile,
        //thirdDoB: LeadDetails.thirdDoB,
        //thirdRelationship: LeadDetails.thirdRelationship,
        //thirdAdd1: LeadDetails.thirdAdd1,
        //thirdAdd2: LeadDetails.thirdAdd2,
        //thirdAdd3: LeadDetails.thirdAdd3,
        //thirdcity: LeadDetails.thirdcity,
        //thirdpin: LeadDetails.thirdpin
    };


    //Push to list 
    objKycdetailsBo.push(thirdhlder);

    var TransactionDetails = {

        objInvstDtlsBO: objInvstDtlBo,
       
        objInvestorBankDtlBO: objInvestorBankDtlBo,

        ObjKYCDataDetail: objKycdetailsBo,

        objNBO: objNBo
       
    };
    console.log(TransactionDetails);

    ExtendedAjaxCall(Api_settings.url + 'SaveTransactionDetails', TransactionDetails, 'POST', function (result) {
        console.log(result);
        
        if (result != null && typeof (result) != 'undefined') {

            if (result.validResponse) {

                MessageCenter(result.msg, "success");                             
            }
            else {
                MessageCenter(result.msg, "error");
            }
        }
        else {
            MessageCenter("Something went wrong", "error");
        }
    }, null, null, false, true);

    //FDChannel.SecondHolderName = $('#txtsecondholdername').val();
    //FDChannel.ThirdHolderName = $('#txtthirdholdername').val();
});

function convertMMDDYYYY(ddmmyyyy) {



    var date = ddmmyyyy.split('/');
    return (date[1] + "/" + date[0] + "/" + date[2]);
}

function convertDDMMYYYY(dateString) {
    var ds = dateString.split(' ');
    var date = ds[0].split('/');
    return (date[1] + "/" + date[0] + "/" + date[2]);
}