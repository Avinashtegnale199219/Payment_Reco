using DBHelper;
using FD_PaymentReconciliation_V2;
using FD_PaymentReconciliation_V2.App_Code.BusinessObject;
using FD_PaymentReconciliation_V2.BusinessObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FD_PaymentReconciliation_V2.App_Code.DataAccessLayer
{
    public class PaymentModeConversionDAL
    {
        string ConStr_PaymentReco = string.Empty;
        string ConStr_ORA = string.Empty;
        string ConStr_Common = string.Empty;
        public PaymentModeConversionDAL()
        {
            try
            {
                ConStr_PaymentReco = Startup.Configuration["ConnectionString:ConStr_PaymentReco"].ToString();
                ConStr_ORA = Startup.Configuration["ConnectionString:ConStr_ORA"].ToString();
                ConStr_Common = Startup.Configuration["ConnectionString:ConStr_Common"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetApplicationDetailsForConversion(string ApplNo)
        {
            DataSet res = new DataSet();
            try
            {
                SqlParameter[] sqlparam = new SqlParameter[1];
                sqlparam[0] = new SqlParameter("@ApplNo", ApplNo);
                res = SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_RECO_Get_PaymentModeConversionDtls", sqlparam);
                return res.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        public DataTable GetPaymentMode()
        {
            DataSet res = new DataSet();
            try
            {
                res = SqlHelper.ExecuteDataSet(ConStr_Common, CommandType.StoredProcedure, "USP_FD_CMN_KYC_GET_Payment_Mode_Mst");
                return res.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        public int InsertPaymentModeConvLog(SessionBO session, PaymentModeConversionBO obj)

        {
            int res = 0;
            try
            {
                SqlParameter[] sqlparam = new SqlParameter[10];

                sqlparam[0] = new SqlParameter("@Appl_No", obj.Appl_No);
                sqlparam[1] = new SqlParameter("@Amount", obj.Amount);
                sqlparam[2] = new SqlParameter("@New_Payment_Mode", obj.New_PaymentMode);
                sqlparam[3] = new SqlParameter("@Old_Payment_Mode", obj.Old_PaymentMode);
                sqlparam[4] = new SqlParameter("@User_Name", session.Entity_Name);
                sqlparam[5] = new SqlParameter("@CreatedBy", session.CreatedBy);
                sqlparam[6] = new SqlParameter("@CreatedByUName", session.Entity_Name);
                sqlparam[7] = new SqlParameter("@CreatedIP", session.CreatedIP);
                sqlparam[8] = new SqlParameter("@SessionId", session.Session_ID);
                sqlparam[9] = new SqlParameter("@Form_Code", session.FormCode);

                res = SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Reco_Insert_PaymentMode_Conv_Log", sqlparam);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }


        public int ConvertPaymentMode(SessionBO session, PaymentModeConversionBO obj)
        {
            int res = 0;
            try
            {
                SqlParameter[] sqlparam = new SqlParameter[10];

                sqlparam[0] = new SqlParameter("@Appl_No", obj.Appl_No);
                sqlparam[1] = new SqlParameter("@Amount", obj.Amount);
                sqlparam[2] = new SqlParameter("@New_Payment_Mode", obj.New_PaymentMode);
                sqlparam[3] = new SqlParameter("@Old_Payment_Mode", obj.Old_PaymentMode);
                sqlparam[4] = new SqlParameter("@User_Name", session.Entity_Name);
                sqlparam[5] = new SqlParameter("@CreatedBy", session.CreatedBy);
                sqlparam[6] = new SqlParameter("@CreatedByUName", session.Entity_Name);
                sqlparam[7] = new SqlParameter("@CreatedIP", session.CreatedIP);
                sqlparam[8] = new SqlParameter("@SessionId", session.Session_ID);
                sqlparam[9] = new SqlParameter("@Form_Code", session.FormCode);
                res = SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Reco_Convert_PaymentMode_V1", sqlparam);


                OracleConnection objConn = new OracleConnection(ConStr_ORA);
                objConn.Open();
                OracleCommand cursCmd = new OracleCommand("USP_FD_RECO_PAYMENT_CONV", objConn);
                cursCmd.CommandType = CommandType.StoredProcedure;
                cursCmd.CommandTimeout = 0;
                cursCmd.Parameters.Add("P_APPL_No", obj.Appl_No);
                cursCmd.Parameters.Add("P_NEW_PAYMENTMODE", obj.New_PaymentMode);
                //cursCmd.Parameters.Add("P_OLD_PAYMENTMODE", obj.Old_PaymentMode);
                cursCmd.Parameters.Add("P_UPDATEDBY", session.CreatedBy);
                cursCmd.Parameters.Add("P_UPDATEDBYUNAME", session.Entity_Name);
                cursCmd.Parameters.Add("P_UPDATEDIP", session.CreatedIP);

                cursCmd.Parameters.Add("P_RES", OracleType.Cursor).Direction = ParameterDirection.Output;
                cursCmd.ExecuteNonQuery();
                objConn.Close();

                return res;
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
