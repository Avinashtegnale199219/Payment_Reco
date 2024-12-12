using DBHelper;
using FD_PaymentReconciliation_V2.App_Code.BusinessObject;
using FD_PaymentReconciliation_V2.BusinessObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FD_PaymentReconciliation_V2.App_Code.DataAccessLayer
{
    public class BankRevalidationRequestDAL
    {
        string ConStr_PaymentReco = string.Empty;
        string ConStr_ORA = string.Empty;
        //string ConStr_Common = string.Empty;
        public BankRevalidationRequestDAL()
        {
            try
            {
                ConStr_PaymentReco = Startup.Configuration["ConnectionString:ConStr_PaymentReco"].ToString();
                ConStr_ORA = Startup.Configuration["ConnectionString:ConStr_ORA"].ToString();
                //ConStr_Common = Startup.Configuration["ConnectionString:ConStr_Common"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Search Condition 

        public DataTable Get_RevalidationData(string SearchType, string SearchValue)
        {
            DataTable dt = new DataTable();
            try
            {
                OracleConnection objConn = new OracleConnection(ConStr_ORA);
                OracleCommand cursCmd = new OracleCommand("USP_FD_REVLD_SEARCH_DTLS", objConn);
                cursCmd.CommandType = CommandType.StoredProcedure;
                cursCmd.Parameters.Add("P_SEARCH_TYPE", OracleType.VarChar, 2000).Value = SearchType;
                cursCmd.Parameters["P_SEARCH_TYPE"].Direction = ParameterDirection.Input;
                cursCmd.Parameters.Add("P_SEARCH_VALUE", OracleType.VarChar, 2000).Value = SearchValue;
                cursCmd.Parameters["P_SEARCH_VALUE"].Direction = ParameterDirection.Input;

                cursCmd.Parameters.Add("P_SEARCH_DTLS", OracleType.Cursor).Direction = ParameterDirection.Output;
                cursCmd.Parameters.Add("P_OUTMSG", OracleType.VarChar, 2000).Direction = ParameterDirection.Output;

                OracleDataAdapter OradataAD = new OracleDataAdapter(cursCmd);
                OradataAD.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        public DataTable Get_BankRevalidationDepDtl(string deP_NO, string iW_NO)
        {

            DataTable ds = new DataTable();
            try
            {
                OracleConnection objConn = new OracleConnection(ConStr_ORA);

                OracleCommand cursCmd = new OracleCommand("usp_fd_revld_dep_dtls", objConn);
                cursCmd.CommandType = CommandType.StoredProcedure;
                cursCmd.Parameters.Add("P_DEP_NO", OracleType.VarChar, 2000).Value = deP_NO;
                cursCmd.Parameters["P_DEP_NO"].Direction = ParameterDirection.Input;

                cursCmd.Parameters.Add("P_WARNO", OracleType.VarChar, 2000).Value = iW_NO;
                cursCmd.Parameters["P_WARNO"].Direction = ParameterDirection.Input;

                cursCmd.Parameters.Add("P_OUTMSG", OracleType.Cursor).Direction = ParameterDirection.Output;

                OracleDataAdapter OradataAD = new OracleDataAdapter(cursCmd);
                OradataAD.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        public DataSet Get_BankRevalidationData()
        {
            DataSet dt = new DataSet();
            try
            {
                OracleConnection objConn = new OracleConnection(ConStr_ORA);

                OracleCommand cursCmd = new OracleCommand("", objConn);
                cursCmd.CommandType = CommandType.StoredProcedure;
                cursCmd.Parameters.Add("P_SEARCH_DTLS", OracleType.Cursor).Direction = ParameterDirection.Output;
                cursCmd.Parameters.Add("P_OUTMSG", OracleType.VarChar, 2000).Direction = ParameterDirection.Output;

                OracleDataAdapter OradataAD = new OracleDataAdapter(cursCmd);
                OradataAD.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        //avinash added
        public string SAVE_Revalidation_RaiseRequest(BankRevalidationRaiseRequestBO bankRevalidationRaiseRequestBO, SessionBO session)
        {
          
            try
            {
                OracleConnection objConn = new OracleConnection(ConStr_ORA);
                objConn.Open();
                OracleCommand cursCmd = new OracleCommand("USP_FD_REVLD_REQDTLS_INS", objConn);
                cursCmd.CommandType = CommandType.StoredProcedure;

                cursCmd.Parameters.Add("P_Dep_NO", bankRevalidationRaiseRequestBO.P_Dep_NO);
                cursCmd.Parameters.Add("P_Folio_NO", bankRevalidationRaiseRequestBO.P_Folio_NO);
                cursCmd.Parameters.Add("P_War_NO", bankRevalidationRaiseRequestBO.P_War_NO);
                cursCmd.Parameters.Add("P_War_Amount", bankRevalidationRaiseRequestBO.P_War_Amount);
                cursCmd.Parameters.Add("P_Pan", bankRevalidationRaiseRequestBO.P_Pan);
                cursCmd.Parameters.Add("P_Dep_Name", bankRevalidationRaiseRequestBO.P_Dep_Name);
                cursCmd.Parameters.Add("P_Dep_Amount", bankRevalidationRaiseRequestBO.P_Dep_Amount);
                cursCmd.Parameters.Add("P_IsChg_BankDtl_ReqRcd_ONL", bankRevalidationRaiseRequestBO.P_IsChg_BankDtl_ReqRcd_ONL);

                if (bankRevalidationRaiseRequestBO.P_Chg_BankDtl_ReqRcd_On_BVL == null)
                {
                    cursCmd.Parameters.Add("P_Chg_BankDtl_ReqRcd_On_ONL", DBNull.Value);
                }
                else
                {
                    string st=Convert.ToDateTime(bankRevalidationRaiseRequestBO.P_Chg_BankDtl_ReqRcd_On_BVL).ToString("dd/MMM/yyyy");
                    cursCmd.Parameters.Add("P_Chg_BankDtl_ReqRcd_On_ONL", OracleType.VarChar,100).Value = st;
                }

                cursCmd.Parameters.Add("P_IsChg_BankDtl_ReqRcd_Offln", bankRevalidationRaiseRequestBO.P_IsChg_BankDtl_ReqRcd_Offln);

                if (bankRevalidationRaiseRequestBO.P_Chg_BankDtl_ReqRcd_On_Offln == null || bankRevalidationRaiseRequestBO.P_Chg_BankDtl_ReqRcd_On_Offln.ToString() == "1/1/0001 12:00:00 AM")
                {
                    cursCmd.Parameters.Add("P_Chg_BankDtl_ReqRcd_On_Offln", DBNull.Value);
                }
                else
                {
                    string st1 = Convert.ToDateTime(bankRevalidationRaiseRequestBO.P_Chg_BankDtl_ReqRcd_On_Offln).ToString("dd/MMM/yyyy");
                    cursCmd.Parameters.Add("P_Chg_BankDtl_ReqRcd_On_Offln", OracleType.VarChar, 100).Value = st1;
                }

                cursCmd.Parameters.Add("P_DMS_Upload", bankRevalidationRaiseRequestBO.P_DMS_Upload);
                cursCmd.Parameters.Add("P_Rvld_Status", bankRevalidationRaiseRequestBO.P_Rvld_Status);
                cursCmd.Parameters.Add("P_SubStatus", bankRevalidationRaiseRequestBO.P_SubStatus);
                cursCmd.Parameters.Add("P_Status_Code", bankRevalidationRaiseRequestBO.P_Status_Code);
                cursCmd.Parameters.Add("P_Status_Reason", bankRevalidationRaiseRequestBO.P_Status_Reason);
                cursCmd.Parameters.Add("P_UTR_Sequence_No", bankRevalidationRaiseRequestBO.P_UTR_Sequence_No);
                string PaymentDate = Convert.ToDateTime(bankRevalidationRaiseRequestBO.P_Payment_Date).ToString("dd/MMM/yyyy");
                cursCmd.Parameters.Add("P_Payment_Date", OracleType.VarChar, 100).Value = PaymentDate;
                cursCmd.Parameters.Add("P_Bank_Name", bankRevalidationRaiseRequestBO.P_Bank_Name);
                cursCmd.Parameters.Add("P_Bank_Account_No", bankRevalidationRaiseRequestBO.P_Bank_Account_No);
                cursCmd.Parameters.Add("P_IFSC_Code", bankRevalidationRaiseRequestBO.P_IFSC_Code);
                cursCmd.Parameters.Add("P_Payment_Int_Type", bankRevalidationRaiseRequestBO.P_Payment_Int_Type);
                cursCmd.Parameters.Add("P_Old_OFAS_TC", bankRevalidationRaiseRequestBO.P_Old_OFAS_TC);
                cursCmd.Parameters.Add("P_Old_OFAS_Voucher_No", bankRevalidationRaiseRequestBO.P_Old_OFAS_Voucher_No);
                cursCmd.Parameters.Add("P_Stale_Status", bankRevalidationRaiseRequestBO.P_Stale_Status);
                cursCmd.Parameters.Add("P_OFAS_Cheque_No", bankRevalidationRaiseRequestBO.P_OFAS_Cheque_No);
                cursCmd.Parameters.Add("P_Phy_Cheque_DD_War_No", bankRevalidationRaiseRequestBO.P_Phy_Cheque_DD_War_No);
                cursCmd.Parameters.Add("P_Stop_Payment_Request", bankRevalidationRaiseRequestBO.P_Stop_Payment_Request);
                cursCmd.Parameters.Add("P_Selected_TC", bankRevalidationRaiseRequestBO.P_Selected_TC);
                cursCmd.Parameters.Add("P_Remarks", bankRevalidationRaiseRequestBO.P_REMARKS);

                cursCmd.Parameters.Add("P_FormCode", session.FormCode);
                cursCmd.Parameters.Add("P_Session", session.Session_ID);
                cursCmd.Parameters.Add("P_Created_By", session.CreatedBy);
                cursCmd.Parameters.Add("P_OUTMSG", OracleType.VarChar,500).Direction = ParameterDirection.Output;
                cursCmd.ExecuteNonQuery();
                string Outvalue = Convert.ToString(cursCmd.Parameters["P_OUTMSG"].Value);
                objConn.Close();

                return Outvalue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Get_BankRevalidation_PendingApproval_Dashboard(int Approval_Stage, SessionBO sessionBO)
        {
            DataSet ds = new DataSet();
            try
            {
                OracleConnection objConn = new OracleConnection(ConStr_ORA);

                OracleCommand cursCmd = new OracleCommand("USP_FD_REVLD_REQDTLS_PEN", objConn);
                cursCmd.CommandType = CommandType.StoredProcedure;

                cursCmd.Parameters.Add("P_APPROVAL_STAGE",Approval_Stage);
                cursCmd.Parameters.Add("P_USER_ID", sessionBO.CreatedBy);
                cursCmd.Parameters.Add("P_REQDTLS_PENDING", OracleType.Cursor).Direction = ParameterDirection.Output;
                cursCmd.Parameters.Add("P_OUTMSG", OracleType.VarChar, 2000).Direction = ParameterDirection.Output;

                OracleDataAdapter OradataAD = new OracleDataAdapter(cursCmd);
                OradataAD.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        public string FirstApproval_Rejection(BankRevalidationRaiseRequestBO bankRevalidationRaiseRequestBO, SessionBO sessionBO)
        {
            //string result = "";
            DataTable ds = new DataTable();
            try
            {
                OracleConnection objConn = new OracleConnection(ConStr_ORA);
                objConn.Open();
                OracleCommand cursCmd = new OracleCommand("USP_FD_REVLD_REQDTLS_FSTAPR", objConn);
                cursCmd.CommandType = CommandType.StoredProcedure;
                cursCmd.Parameters.Add("P_USER_ID", sessionBO.CreatedBy);
                cursCmd.Parameters.Add("P_REVLD_REQDTLS_REQ_NO", bankRevalidationRaiseRequestBO.Rev_Req_No);
                cursCmd.Parameters.Add("P_DEP_NO", bankRevalidationRaiseRequestBO.P_Dep_NO);
                cursCmd.Parameters.Add("P_WAR_NO", bankRevalidationRaiseRequestBO.P_War_NO);
                cursCmd.Parameters.Add("P_FOLIO_NO", bankRevalidationRaiseRequestBO.P_Folio_NO);
                cursCmd.Parameters.Add("P_APPROVAL_STAGE", bankRevalidationRaiseRequestBO.P_APPROVAL_STAGE);
                cursCmd.Parameters.Add("P_APPROVAL_STATUS", bankRevalidationRaiseRequestBO.P_APPROVAL_STATUS);
                cursCmd.Parameters.Add("P_REMARKS", bankRevalidationRaiseRequestBO.First_CheckerRemark);
                cursCmd.Parameters.Add("P_OUTMSG", OracleType.VarChar, 500).Direction = ParameterDirection.Output;
                cursCmd.ExecuteNonQuery();
                string Outvalue = Convert.ToString(cursCmd.Parameters["P_OUTMSG"].Value);

                objConn.Close();

                return Outvalue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        public string SecondApproval_Rejection(BankRevalidationRaiseRequestBO bankRevalidationRaiseRequestBO, SessionBO sessionBO)
        {
            //string result = "";
            DataTable ds = new DataTable();
            try
            {
                OracleConnection objConn = new OracleConnection(ConStr_ORA);
                objConn.Open();
                OracleCommand cursCmd = new OracleCommand("USP_FD_REVLD_REQDTLS_SECAPR", objConn);
                cursCmd.CommandType = CommandType.StoredProcedure;
                cursCmd.Parameters.Add("P_USER_ID", sessionBO.CreatedBy);
                cursCmd.Parameters.Add("P_REVLD_REQDTLS_REQ_NO", bankRevalidationRaiseRequestBO.Rev_Req_No);
                cursCmd.Parameters.Add("P_DEP_NO", bankRevalidationRaiseRequestBO.P_Dep_NO);
                cursCmd.Parameters.Add("P_WAR_NO", bankRevalidationRaiseRequestBO.P_War_NO);
                cursCmd.Parameters.Add("P_FOLIO_NO", bankRevalidationRaiseRequestBO.P_Folio_NO);
                cursCmd.Parameters.Add("P_APPROVAL_STAGE", bankRevalidationRaiseRequestBO.P_APPROVAL_STAGE);
                cursCmd.Parameters.Add("P_APPROVAL_STATUS", bankRevalidationRaiseRequestBO.P_APPROVAL_STATUS);
                cursCmd.Parameters.Add("P_REMARKS", bankRevalidationRaiseRequestBO.Second_CheckerRemark);
                cursCmd.Parameters.Add("P_OUTMSG", OracleType.VarChar, 500).Direction = ParameterDirection.Output;
                //cursCmd.Parameters.Add("P_OUTMSG", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
                cursCmd.ExecuteNonQuery();
                string Outvalue = Convert.ToString(cursCmd.Parameters["P_OUTMSG"].Value);
                objConn.Close();

                return Outvalue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

    }
}
