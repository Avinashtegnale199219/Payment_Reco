using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FD_PaymentReconciliation_V2.App_Code.BusinessObject
{
    public class BankRevalidationRaiseRequestBO
    {
        public string P_Dep_NO { get; set; }
        public string P_Folio_NO { get; set; }
        public string P_War_NO { get; set; }
        public float P_War_Amount { get; set; }
        public string P_Dep_Name { get; set; }
        public float P_Dep_Amount { get; set; }
        public string P_IsChg_BankDtl_ReqRcd_ONL { get; set; }

        public string P_Chg_BankDtl_ReqRcd_On_BVL { get; set; }
        public string P_IsChg_BankDtl_ReqRcd_Offln { get; set; }
        public string P_Chg_BankDtl_ReqRcd_On_Offln { get; set; }
        public string P_DMS_Upload { get; set; }
        public string P_Rvld_Status { get; set; }
        public string P_SubStatus { get; set; }
        public string P_Status_Code { get; set; }
        public string P_Status_Reason { get; set; }

        public string P_UTR_Sequence_No { get; set; }
        public string P_Payment_Date { get; set; }
        public string P_Bank_Name { get; set; }
        public string P_Bank_Account_No { get; set; }
        public string P_IFSC_Code { get; set; }

        public string P_Payment_Int_Type { get; set; }
        public string P_Old_OFAS_TC { get; set; }
        public string P_Old_OFAS_Voucher_No { get; set; }
        public string P_Stale_Status { get; set; }
        public string P_OFAS_Cheque_No { get; set; }
        public string P_Phy_Cheque_DD_War_No { get; set; }
        public string P_Stop_Payment_Request { get; set; }
        public string P_Selected_TC { get; set; }
        public string P_FormCode { get; set; }
        public string P_Session { get; set; }
        public string P_Created_By { get; set; }

        public string P_Pan { get; set; }
        public string Rev_Req_No { get; set; }

        public string P_REMARKS { get; set; }

        public int P_APPROVAL_STAGE { get; set; }
        public string P_APPROVAL_STATUS { get; set; }

        public string First_CheckerRemark { get; set; }

        public string Second_CheckerRemark { get; set; }
        public string P_USER_ID { get; set; }
    }
}
