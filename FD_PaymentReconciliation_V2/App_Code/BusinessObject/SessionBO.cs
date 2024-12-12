namespace FD_PaymentReconciliation_V2.BusinessObject
{
    public class SessionBO
    {
        public SessionBO()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        long _Pk_Session_ID;

        public long Pk_Session_ID
        {
            get { return _Pk_Session_ID; }
            set { _Pk_Session_ID = value; }
        }
        string _Agency_Clustered_ID;

        public string Agency_Clustered_ID
        {
            get { return _Agency_Clustered_ID; }
            set { _Agency_Clustered_ID = value; }
        }
        string _Agency_Usr_Clustered_ID;

        public string Agency_Usr_Clustered_ID
        {
            get { return _Agency_Usr_Clustered_ID; }
            set { _Agency_Usr_Clustered_ID = value; }
        }
        string _Agency_Cd;

        public string Agency_Cd
        {
            get { return _Agency_Cd; }
            set { _Agency_Cd = value; }
        }
        string _Agency_Type;

        public string Agency_Type
        {
            get { return _Agency_Type; }
            set { _Agency_Type = value; }
        }
        string _Agency_Sub_Type;

        public string Agency_Sub_Type
        {
            get { return _Agency_Sub_Type; }
            set { _Agency_Sub_Type = value; }
        }
        string _Agency_Name;

        public string Agency_Name
        {
            get { return _Agency_Name; }
            set { _Agency_Name = value; }
        }
        string _Agency_Usr_Name;

        public string Agency_Usr_Name
        {
            get { return _Agency_Usr_Name; }
            set { _Agency_Usr_Name = value; }
        }
        string _Agency_Usr_EmailID;

        public string Agency_Usr_EmailID
        {
            get { return _Agency_Usr_EmailID; }
            set { _Agency_Usr_EmailID = value; }
        }
        string _Agency_Usr_MobileNo;

        public string Agency_Usr_MobileNo
        {
            get { return _Agency_Usr_MobileNo; }
            set { _Agency_Usr_MobileNo = value; }
        }
        string _Agency_Usr_Base_Loc_cd;

        public string Agency_Usr_Base_Loc_cd
        {
            get { return _Agency_Usr_Base_Loc_cd; }
            set { _Agency_Usr_Base_Loc_cd = value; }
        }
        string _Agency_Usr_Base_Loc_Desc;

        public string Agency_Usr_Base_Loc_Desc
        {
            get { return _Agency_Usr_Base_Loc_Desc; }
            set { _Agency_Usr_Base_Loc_Desc = value; }
        }
        string _Agency_Usr_Base_Role_cd;

        public string Agency_Usr_Base_Role_cd
        {
            get { return _Agency_Usr_Base_Role_cd; }
            set { _Agency_Usr_Base_Role_cd = value; }
        }
        string _Agency_Usr_Base_Role_Desc;

        public string Agency_Usr_Base_Role_Desc
        {
            get { return _Agency_Usr_Base_Role_Desc; }
            set { _Agency_Usr_Base_Role_Desc = value; }
        }
        string _Server_IP;

        public string Server_IP
        {
            get { return _Server_IP; }
            set { _Server_IP = value; }
        }
        string _Server_Domain;

        public string Server_Domain
        {
            get { return _Server_Domain; }
            set { _Server_Domain = value; }
        }
        string _DataBase_Access;

        public string DataBase_Access
        {
            get { return _DataBase_Access; }
            set { _DataBase_Access = value; }
        }
        string _ServerInstanceName;

        public string ServerInstanceName
        {
            get { return _ServerInstanceName; }
            set { _ServerInstanceName = value; }
        }
        string _ClientIP_Address;

        public string ClientIP_Address
        {
            get { return _ClientIP_Address; }
            set { _ClientIP_Address = value; }
        }
        string _Client_Mac_Address;

        public string Client_Mac_Address
        {
            get { return _Client_Mac_Address; }
            set { _Client_Mac_Address = value; }
        }
        string _BrowserType;

        public string BrowserType
        {
            get { return _BrowserType; }
            set { _BrowserType = value; }
        }
        string _BrowserVersion;

        public string BrowserVersion
        {
            get { return _BrowserVersion; }
            set { _BrowserVersion = value; }
        }
        string _BrowserMajorVersion;

        public string BrowserMajorVersion
        {
            get { return _BrowserMajorVersion; }
            set { _BrowserMajorVersion = value; }
        }
        string _BrowserMinorVersion;

        public string BrowserMinorVersion
        {
            get { return _BrowserMinorVersion; }
            set { _BrowserMinorVersion = value; }
        }
        string _UserAgent;

        public string UserAgent
        {
            get { return _UserAgent; }
            set { _UserAgent = value; }
        }
        string _UserMAC;

        public string UserMAC
        {
            get { return _UserMAC; }
            set { _UserMAC = value; }
        }
        string _Active;

        public string Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        string _SSO_AuthKey;

        public string SSO_AuthKey
        {
            get { return _SSO_AuthKey; }
            set { _SSO_AuthKey = value; }
        }
        string _IIS_Session_ID;

        public string IIS_Session_ID
        {
            get { return _IIS_Session_ID; }
            set { _IIS_Session_ID = value; }
        }
        string _FormCode;

        public string FormCode
        {
            get { return _FormCode; }
            set { _FormCode = value; }
        }

        public string F_Agency_Cont_User_Cd
        {
            get
            {
                return _f_Agency_Cont_User_Cd;
            }

            set
            {
                _f_Agency_Cont_User_Cd = value;
            }
        }

        public string F_Agency_Cont_User_EmailID
        {
            get
            {
                return _f_Agency_Cont_User_EmailID;
            }

            set
            {
                _f_Agency_Cont_User_EmailID = value;
            }
        }

        public string F_Agency_cont_User_MobileNo
        {
            get
            {
                return _f_Agency_cont_User_MobileNo;
            }

            set
            {
                _f_Agency_cont_User_MobileNo = value;
            }
        }

        public string F_Agency_Cont_User_Name
        {
            get
            {
                return _f_Agency_Cont_User_Name;
            }

            set
            {
                _f_Agency_Cont_User_Name = value;
            }
        }

        string _f_Agency_Cont_User_Cd;
        string _f_Agency_Cont_User_EmailID;
        string _f_Agency_cont_User_MobileNo;
        string _f_Agency_Cont_User_Name;

        public string Entity_Type_Code { get; set; }
        public string Entity_Id { get; set; }
        public string Entity_Name { get; set; }
        public string CreatedIP { get; set; }
        public string CreatedByUName { get; set; }
        public string CreatedType { get; set; }
        public long Session_ID { get; set; }
        public string CreatedBy { get; set; }

        public string Source { get; set; } = "CPTP";

        public string Busi_Broker_Cd { get; set; }
        public long Pk_Session_Ref_ID { get; set; }



    }
}
