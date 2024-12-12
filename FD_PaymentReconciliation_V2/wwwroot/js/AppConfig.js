var UserMst = {};
UserMst.SapCode = '';
UserMst.Email = '';
UserMst.Mobile = '';
UserMst.BranchCode = '';
UserMst.DepartmentCode = '';
UserMst.DepartmentName = '';
UserMst.CompanyCode = '';
UserMst.CompanyDesc = '';
UserMst.CreatedIP = '';
UserMst.CreatedOn = null;
UserMst.CreatedBy = '';
UserMst.CreatedID = '';
UserMst.CreatedType = '';
UserMst.UpdatedIP = '';
UserMst.UpdatedBy = '';
UserMst.UpdatedOn = null;
UserMst.SessionId = '';
UserMst.FormId = 0;
UserMst.CreatedByUname = '';
UserMst.UpdatedByUname = '';
UserMst.FormIdCode = '';
UserMst.UpdatedID = '';
UserMst.UpdatedType = '';
UserMst.StateCode = '';
UserMst.UserName = '';

function GetUserDtl() {

UserMst.SapCode = UserMst.Email = UserMst.Mobile = UserMst.DepartmentCode = UserMst.DepartmentName = UserMst.CompanyCode = UserMst.CompanyDesc = UserMst.UserName = '';
	UserMst.CreatedOn = UserMst.UpdatedOn = null;
	UserMst.CreatedIP = UserMst.CreatedBy = UserMst.CreatedID = UserMst.CreatedType = UserMst.UpdatedIP = UserMst.UpdatedBy = UserMst.SessionId = '';
	UserMst.FormId = 0;
	UserMst.CreatedByUname = UserMst.UpdatedByUname = UserMst.FormIdCode = UserMst.UpdatedID = UserMst.UpdatedType = UserMst.StateCode = '';

    ExtendedAjaxCall('/Default/GetSessionInfo', null, 'GET', function (result) {
        UserMst.SapCode = UserMst.UpdatedBy = UserMst.UpdatedID = UserMst.CreatedID = UserMst.CreatedBy = result.session.entity_SAPCode;
        UserMst.UpdatedType = UserMst.CreatedType = result.session.entity_Type_Code;
        UserMst.SessionId = result.session.sessionId;
        UserMst.UpdatedIP = UserMst.CreatedIP = result.ip;
        UserMst.UpdatedByUname = UserMst.CreatedByUname = result.session.entity_Name;
        UserMst.FormIdCode = result.formCode;
        UserMst.DepartmentCode = result.session.entity_DepartmentCode;
        UserMst.DepartmentName = result.session.entity_DepartmentName;
        UserMst.Email = result.session.entity_Email;
        UserMst.Mobile = result.session.entity_MobileNo;
        UserMst.CompanyCode = result.session.entity_Company_Code;
        UserMst.CompanyDesc = result.session.entity_Company_Desc;
	UserMst.BranchCode = result.session.entity_Branch_Code;
	UserMst.UserName = result.session.userName;
	if(typeof(result.stateCode)!='undefined'){
		UserMst.StateCode = result.stateCode;
	}

    }, null, null, false, false, ErrorFunction);
};