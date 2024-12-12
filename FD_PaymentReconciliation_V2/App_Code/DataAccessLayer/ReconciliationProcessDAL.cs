using DBHelper;
using FD_PaymentReconciliation_V2.App_Code.BusinessObject;
using FD_PaymentReconciliation_V2.BusinessObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FD_PaymentReconciliation_V2.App_Code.DataAccessLayer
{
    public class ReconciliationProcessDAL
    {
        string ConStr_PaymentReco = string.Empty;
        string ConStr_FD = string.Empty;
        public ReconciliationProcessDAL()
        {
            try
            {
                ConStr_PaymentReco = Startup.Configuration["ConnectionString:ConStr_PaymentReco"].ToString();
                ConStr_FD = Startup.Configuration["ConnectionString:ConStr_FD"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Get_Portal()
        {
            return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "Usp_FD_Reco_Get_Portal_dtl");
        }
        public DataSet Get_Mode()
        {
            return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "Usp_FD_Reco_Get_Mode_Dtl");
        }

        public DataSet Get_Template_Type(string PaymentMode)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@PaymentMode", PaymentMode);
            return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "Usp_FD_Reco_Get_Template_Type_Dtl", param);
        }

        public DataSet SaveData_Online(DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@UploadRecoFile", ds.GetXml().ToString());
            param[1] = new SqlParameter("@PaymentMode", "Online");
            param[2] = new SqlParameter("@ReconTemplateType", objBO.TemplateType);
            param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
            param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
            param[5] = new SqlParameter("@Remarks", objBO.Remarks);
            param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
            param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
            param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
            param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
            param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
            SqlConnection conn = new SqlConnection(ConStr_PaymentReco);
            conn.Open();
            SqlCommand sqlcomm = new SqlCommand("usp_FD_Online_UploadRecoFile");
            sqlcomm.CommandTimeout = 0;
            sqlcomm.Connection = conn;
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Parameters.AddRange(param);
            DataSet dsRes=new DataSet();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcomm);
            sqlda.Fill(dsRes);
            conn.Close();
            conn.Dispose();
            return dsRes;
           // return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_UploadRecoFile", param);
        }

        //Below Method Added By Satish Pawar On 11 Nov 2022
        public DataSet SaveData_LAFD_Online(DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@UploadRecoFile", ds.GetXml().ToString());
            param[1] = new SqlParameter("@PaymentMode", "Online");
            param[2] = new SqlParameter("@ReconTemplateType", objBO.TemplateType);
            param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
            param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
            param[5] = new SqlParameter("@Remarks", objBO.Remarks);
            param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
            param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
            param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
            param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
            param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
            SqlConnection conn = new SqlConnection(ConStr_PaymentReco);
            conn.Open();
            SqlCommand sqlcomm = new SqlCommand("usp_FD_Online_LAFD_UploadRecoFile");
            sqlcomm.CommandTimeout = 0;
            sqlcomm.Connection = conn;
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Parameters.AddRange(param);
            DataSet dsRes = new DataSet();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcomm);
            sqlda.Fill(dsRes);
            conn.Close();
            conn.Dispose();
            return dsRes;
            // return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_UploadRecoFile", param);
        }


        public DataSet SaveData_Offline(DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@UploadRecoFile", ds.GetXml().ToString());
            param[1] = new SqlParameter("@PaymentMode", "Offline");
            param[2] = new SqlParameter("@ReconTemplateType", objBO.TemplateType);
            param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
            param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
            param[5] = new SqlParameter("@Remarks", objBO.Remarks);
            param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
            param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
            param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
            param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
            param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
            SqlConnection conn = new SqlConnection(ConStr_PaymentReco);
            conn.Open();
            SqlCommand sqlcomm = new SqlCommand("usp_FD_Offline_UploadRecoFile");
            sqlcomm.CommandTimeout = 0;
            sqlcomm.Connection = conn;
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Parameters.AddRange(param);
            DataSet dsRes = new DataSet();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcomm);
            sqlda.Fill(dsRes);
            conn.Close();
            conn.Dispose();
            return dsRes;
            //return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Offline_UploadRecoFile", param);
        }


        public DataSet SaveData_CMS(DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@UploadRecoFile", ds.GetXml().ToString());
            param[1] = new SqlParameter("@PaymentMode", "CMS");
            param[2] = new SqlParameter("@ReconTemplateType", "CMS");
            param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
            param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
            param[5] = new SqlParameter("@Remarks", objBO.Remarks);
            param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
            param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
            param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
            param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
            param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
            SqlConnection conn = new SqlConnection(ConStr_PaymentReco);
            conn.Open();
            SqlCommand sqlcomm = new SqlCommand("usp_FD_CMS_UploadRecoFile");
            sqlcomm.CommandTimeout = 0;
            sqlcomm.Connection = conn;
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Parameters.AddRange(param);
            DataSet dsRes = new DataSet();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcomm);
            sqlda.Fill(dsRes);
            conn.Close();
            conn.Dispose();
            return dsRes;
           // return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_CMS_UploadRecoFile", param);
        }

        //CMS Collection Save
        public DataSet SaveData_CMS_Collection(DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@UploadRecoFile", ds.GetXml().ToString());
            param[1] = new SqlParameter("@PaymentMode", "CMS_Collection");
            param[2] = new SqlParameter("@ReconTemplateType", "CMS_Collection");
            param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
            param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
            param[5] = new SqlParameter("@Remarks", objBO.Remarks);
            param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
            param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
            param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
            param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
            param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
            SqlConnection conn = new SqlConnection(ConStr_PaymentReco);
            conn.Open();
            SqlCommand sqlcomm = new SqlCommand("usp_FD_CMS_Collection_UploadRecoFile");
            sqlcomm.CommandTimeout = 0;
            sqlcomm.Connection = conn;
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Parameters.AddRange(param);
            DataSet dsRes = new DataSet();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcomm);
            sqlda.Fill(dsRes);
            conn.Close();
            conn.Dispose();
            return dsRes;
            //return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_CMS_Collection_UploadRecoFile", param);
        }
        #region Not_to_use
        //Reward Portal
        //public DataSet SaveData_Reward_Online(DataTable dt, ReconciliationBO objBO, SessionBO objSBO)
        //{
        //    SqlParameter[] param = new SqlParameter[11];
        //    param[0] = new SqlParameter("@UploadRecoFile", dt);
        //    param[1] = new SqlParameter("@RewardCode", objBO.Portal);
        //    param[2] = new SqlParameter("@RewardName", objBO.Portal);
        //    param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
        //    param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
        //    param[5] = new SqlParameter("@Remarks", objBO.Remarks);
        //    param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
        //    param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
        //    param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
        //    param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
        //    param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
        //    return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_RW_Online_UploadRecoFile", param);
        //}
        //public DataSet SaveData_Reward_Offline(DataTable dt, ReconciliationBO objBO, SessionBO objSBO)
        //{

        //    SqlParameter[] param = new SqlParameter[11];
        //    param[0] = new SqlParameter("@UploadRecoFile", dt);
        //    param[1] = new SqlParameter("@RewardCode", objBO.Portal);
        //    param[2] = new SqlParameter("@RewardName", objBO.Portal);
        //    param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
        //    param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
        //    param[5] = new SqlParameter("@Remarks", objBO.Remarks);
        //    param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
        //    param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
        //    param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
        //    param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
        //    param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
        //    return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_RW_Offline_UploadRecoFile", param);
        //}

        //QB Portal
        //public DataSet SaveData_QB_Online(DataTable dt, ReconciliationBO objBO, SessionBO objSBO)
        //{
        //    SqlParameter[] param = new SqlParameter[11];
        //    param[0] = new SqlParameter("@UploadRecoFile", dt);
        //    param[1] = new SqlParameter("@QBCode", objBO.Portal);
        //    param[2] = new SqlParameter("@QBName", objBO.Portal);
        //    param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
        //    param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
        //    param[5] = new SqlParameter("@Remarks", objBO.Remarks);
        //    param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
        //    param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
        //    param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
        //    param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
        //    param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
        //    return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_QB_Online_UploadRecoFile", param);
        //}
        //public DataSet SaveData_QB_Offline(DataTable dt, ReconciliationBO objBO, SessionBO objSBO)
        //{

        //    SqlParameter[] param = new SqlParameter[11];
        //    param[0] = new SqlParameter("@UploadRecoFile", dt);
        //    param[1] = new SqlParameter("@QBCode", objBO.Portal);
        //    param[2] = new SqlParameter("@QBName", objBO.Portal);
        //    param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
        //    param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
        //    param[5] = new SqlParameter("@Remarks", objBO.Remarks);
        //    param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
        //    param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
        //    param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
        //    param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
        //    param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
        //    return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_QB_Offline_UploadRecoFile", param);
        //}

        //Enterprise Portal
        //public DataSet SaveData_Enterprise_Online(DataTable dt, ReconciliationBO objBO, SessionBO objSBO)
        //{
        //    SqlParameter[] param = new SqlParameter[11];
        //    param[0] = new SqlParameter("@UploadRecoFile", dt);
        //    param[1] = new SqlParameter("@EnterpriseCode", objBO.Portal);
        //    param[2] = new SqlParameter("@EnterpriseName", objBO.Portal);
        //    param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
        //    param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
        //    param[5] = new SqlParameter("@Remarks", objBO.Remarks);
        //    param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
        //    param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
        //    param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
        //    param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
        //    param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
        //    return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Enterprise_Online_UploadRecoFile", param);
        //}
        //public DataSet SaveData_Enterprise_Offline(DataTable dt, ReconciliationBO objBO, SessionBO objSBO)
        //{
        //    SqlParameter[] param = new SqlParameter[11];
        //    param[0] = new SqlParameter("@UploadRecoFile", dt);
        //    param[1] = new SqlParameter("@EnterpriseCode", objBO.Portal);
        //    param[2] = new SqlParameter("@EnterpriseName", objBO.Portal);
        //    param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
        //    param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
        //    param[5] = new SqlParameter("@Remarks", objBO.Remarks);
        //    param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
        //    param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
        //    param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
        //    param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
        //    param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
        //    return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Enterprise_Offline_UploadRecoFile", param);
        //}

        //BOTC Portal
        //public DataSet SaveData_BOTC_Online(DataTable dt, ReconciliationBO objBO, SessionBO objSBO)
        //{
        //    SqlParameter[] param = new SqlParameter[11];
        //    param[0] = new SqlParameter("@UploadRecoFile", dt);
        //    param[1] = new SqlParameter("@BOTCCode", objBO.Portal);
        //    param[2] = new SqlParameter("@BOTCName", objBO.Portal);
        //    param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
        //    param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
        //    param[5] = new SqlParameter("@Remarks", objBO.Remarks);
        //    param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
        //    param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
        //    param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
        //    param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
        //    param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
        //    return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_BOTC_Online_UploadRecoFile", param);
        //}
        //public DataSet SaveData_BOTC_Offline(DataTable dt, ReconciliationBO objBO, SessionBO objSBO)
        //{
        //    SqlParameter[] param = new SqlParameter[11];
        //    param[0] = new SqlParameter("@UploadRecoFile", dt);
        //    param[1] = new SqlParameter("@BOTCCode", objBO.Portal);
        //    param[2] = new SqlParameter("@BOTCName", objBO.Portal);
        //    param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
        //    param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
        //    param[5] = new SqlParameter("@Remarks", objBO.Remarks);
        //    param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
        //    param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
        //    param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
        //    param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
        //    param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
        //    return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_BOTC_Offline_UploadRecoFile", param);
        //}

        //BTP Portal
        //public DataSet SaveData_BTP_Online(DataTable dt, ReconciliationBO objBO, SessionBO objSBO)
        //{
        //    SqlParameter[] param = new SqlParameter[11];
        //    param[0] = new SqlParameter("@UploadRecoFile", dt);
        //    param[1] = new SqlParameter("@BTPCode", objBO.Portal);
        //    param[2] = new SqlParameter("@BTPName", objBO.Portal);
        //    param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
        //    param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
        //    param[5] = new SqlParameter("@Remarks", objBO.Remarks);
        //    param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
        //    param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
        //    param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
        //    param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
        //    param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
        //    return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_BTP_Online_UploadRecoFile", param);
        //}
        //public DataSet SaveData_BTP_Offline(DataTable dt, ReconciliationBO objBO, SessionBO objSBO)
        //{
        //    SqlParameter[] param = new SqlParameter[11];
        //    param[0] = new SqlParameter("@UploadRecoFile", dt);
        //    param[1] = new SqlParameter("@BTPCode", objBO.Portal);
        //    param[2] = new SqlParameter("@BTPName", objBO.Portal);
        //    param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
        //    param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
        //    param[5] = new SqlParameter("@Remarks", objBO.Remarks);
        //    param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
        //    param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
        //    param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
        //    param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
        //    param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
        //    return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_BTP_Offline_UploadRecoFile", param);
        //}

        //BTPCP Portal
        //public DataSet SaveData_BTPCP_Online(DataTable dt, ReconciliationBO objBO, SessionBO objSBO)
        //{
        //    SqlParameter[] param = new SqlParameter[11];
        //    param[0] = new SqlParameter("@UploadRecoFile", dt);
        //    param[1] = new SqlParameter("@BTPCPCode", objBO.Portal);
        //    param[2] = new SqlParameter("@BTPCPName", objBO.Portal);
        //    param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
        //    param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
        //    param[5] = new SqlParameter("@Remarks", objBO.Remarks);
        //    param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
        //    param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
        //    param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
        //    param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
        //    param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
        //    return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_BTPCP_Online_UploadRecoFile", param);
        //}
        //public DataSet SaveData_BTPCP_Offline(DataTable dt, ReconciliationBO objBO, SessionBO objSBO)
        //{
        //    SqlParameter[] param = new SqlParameter[11];
        //    param[0] = new SqlParameter("@UploadRecoFile", dt);
        //    param[1] = new SqlParameter("@BTPCPCode", objBO.Portal);
        //    param[2] = new SqlParameter("@BTPCPName", objBO.Portal);
        //    param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
        //    param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
        //    param[5] = new SqlParameter("@Remarks", objBO.Remarks);
        //    param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
        //    param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
        //    param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
        //    param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
        //    param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
        //    return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_BTPCP_Offline_UploadRecoFile", param);
        //}

        //CP Portal
        //public DataSet SaveData_CP_Online(DataTable dt, ReconciliationBO objBO, SessionBO objSBO)
        //{
        //    SqlParameter[] param = new SqlParameter[11];
        //    param[0] = new SqlParameter("@UploadRecoFile", dt);
        //    param[1] = new SqlParameter("@CPCode", objBO.Portal);
        //    param[2] = new SqlParameter("@CPName", objBO.Portal);
        //    param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
        //    param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
        //    param[5] = new SqlParameter("@Remarks", objBO.Remarks);
        //    param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
        //    param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
        //    param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
        //    param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
        //    param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
        //    return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_CP_Online_UploadRecoFile", param);
        //}
        //public DataSet SaveData_CP_Offline(DataTable dt, ReconciliationBO objBO, SessionBO objSBO)
        //{
        //    SqlParameter[] param = new SqlParameter[11];
        //    param[0] = new SqlParameter("@UploadRecoFile", dt);
        //    param[1] = new SqlParameter("@CPCode", objBO.Portal);
        //    param[2] = new SqlParameter("@CPName", objBO.Portal);
        //    param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
        //    param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
        //    param[5] = new SqlParameter("@Remarks", objBO.Remarks);
        //    param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
        //    param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
        //    param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
        //    param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
        //    param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
        //    return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_CP_Offline_UploadRecoFile", param);
        //}

        //CPLCA Portal
        //public DataSet SaveData_CPLCA_Online(DataTable dt, ReconciliationBO objBO, SessionBO objSBO)
        //{
        //    SqlParameter[] param = new SqlParameter[11];
        //    param[0] = new SqlParameter("@UploadRecoFile", dt);
        //    param[1] = new SqlParameter("@CP_LCACode", objBO.Portal);
        //    param[2] = new SqlParameter("@CP_LCAName", objBO.Portal);
        //    param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
        //    param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
        //    param[5] = new SqlParameter("@Remarks", objBO.Remarks);
        //    param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
        //    param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
        //    param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
        //    param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
        //    param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
        //    return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_CP_LCA_Online_UploadRecoFile", param);
        //}
        //public DataSet SaveData_CPLCA_Offline(DataTable dt, ReconciliationBO objBO, SessionBO objSBO)
        //{
        //    SqlParameter[] param = new SqlParameter[11];
        //    param[0] = new SqlParameter("@UploadRecoFile", dt);
        //    param[1] = new SqlParameter("@CP_LCACode", objBO.Portal);
        //    param[2] = new SqlParameter("@CP_LCAName", objBO.Portal);
        //    param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
        //    param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
        //    param[5] = new SqlParameter("@Remarks", objBO.Remarks);
        //    param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
        //    param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
        //    param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
        //    param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
        //    param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
        //    return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_CP_LCA_Offline_UploadRecoFile", param);
        //}
        #endregion

        public DataSet SaveData_UPI(DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@UploadRecoFile", ds.GetXml().ToString());
            param[1] = new SqlParameter("@PaymentMode", "UPI");
            param[2] = new SqlParameter("@ReconTemplateType", "UPI");
            param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
            param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
            param[5] = new SqlParameter("@Remarks", objBO.Remarks);
            param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
            param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
            param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
            param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
            param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
            SqlConnection conn = new SqlConnection(ConStr_PaymentReco);
            conn.Open();
            SqlCommand sqlcomm = new SqlCommand("usp_FD_UPI_UploadRecoFile");
            sqlcomm.CommandTimeout = 0;
            sqlcomm.Connection = conn;
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Parameters.AddRange(param);
            DataSet dsRes = new DataSet();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcomm);
            sqlda.Fill(dsRes);
            conn.Close();
            conn.Dispose();
            return dsRes;
            //return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_UPI_UploadRecoFile", param);
        }

        public DataSet SaveData_RTGS(DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@UploadRecoFile", ds.GetXml().ToString());
            param[1] = new SqlParameter("@PaymentMode", "RTGS");
            param[2] = new SqlParameter("@ReconTemplateType", "RTGS");
            param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
            param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
            param[5] = new SqlParameter("@Remarks", objBO.Remarks);
            param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
            param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
            param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
            param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
            param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
            SqlConnection conn = new SqlConnection(ConStr_PaymentReco);
            conn.Open();
            SqlCommand sqlcomm = new SqlCommand("usp_FD_RTGS_UploadRecoFile");
            sqlcomm.CommandTimeout = 0;
            sqlcomm.Connection = conn;
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Parameters.AddRange(param);
            DataSet dsRes = new DataSet();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcomm);
            sqlda.Fill(dsRes);
            conn.Close();
            conn.Dispose();
            return dsRes;
            //return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_RTGS_UploadRecoFile", param);
        }

        public DataSet SaveData_HDFCSoftFeed(DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@UploadRecoFile", ds.GetXml().ToString());
            param[1] = new SqlParameter("@PaymentMode", "HDFCSoftFeed");
            param[2] = new SqlParameter("@ReconTemplateType", objBO.TemplateType);
            param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
            param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
            param[5] = new SqlParameter("@Remarks", objBO.Remarks);
            param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
            param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
            param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
            param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
            param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
            SqlConnection conn = new SqlConnection(ConStr_PaymentReco);
            conn.Open();
            SqlCommand sqlcomm = new SqlCommand("usp_FD_HDFCSoftFeed_UploadRecoFile");
            sqlcomm.CommandTimeout = 0;
            sqlcomm.Connection = conn;
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Parameters.AddRange(param);
            DataSet dsRes = new DataSet();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcomm);
            sqlda.Fill(dsRes);
            conn.Close();
            conn.Dispose();
            return dsRes;
            //return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_HDFCSoftFeed_UploadRecoFile", param);
        }
        public DataSet SaveData_BankValidationNACHSOFTPayment(DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@UploadRecoFile", ds.GetXml().ToString());
            param[1] = new SqlParameter("@PaymentMode", "BankRevalidation");
            param[2] = new SqlParameter("@ReconTemplateType", objBO.TemplateType);
            param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
            param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
            param[5] = new SqlParameter("@Remarks", objBO.Remarks);
            param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
            param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
            param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
            param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
            param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
            SqlConnection conn = new SqlConnection(ConStr_PaymentReco);
            conn.Open();
            SqlCommand sqlcomm = new SqlCommand("usp_FD_BankValidation_NACHSOFTPayment_UploadRecoFile");
            sqlcomm.CommandTimeout = 0;
            sqlcomm.Connection = conn;
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Parameters.AddRange(param);
            DataSet dsRes = new DataSet();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcomm);
            sqlda.Fill(dsRes);
            conn.Close();
            conn.Dispose();
            return dsRes;
            //return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_BankValidation_NACHSOFTPayment_UploadRecoFile", param);
        }
        public DataSet SaveData_BankValidationDDPaidUnpaid(DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@UploadRecoFile", ds.GetXml().ToString());
            param[1] = new SqlParameter("@PaymentMode", "BankRevalidation");
            param[2] = new SqlParameter("@ReconTemplateType", objBO.TemplateType);
            param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
            param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
            param[5] = new SqlParameter("@Remarks", objBO.Remarks);
            param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
            param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
            param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
            param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
            param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
            SqlConnection conn = new SqlConnection(ConStr_PaymentReco);
            conn.Open();
            SqlCommand sqlcomm = new SqlCommand("usp_FD_BankValidation_DDPaidUnpaid_UploadRecoFile");
            sqlcomm.CommandTimeout = 0;
            sqlcomm.Connection = conn;
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Parameters.AddRange(param);
            DataSet dsRes = new DataSet();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcomm);
            sqlda.Fill(dsRes);
            conn.Close();
            conn.Dispose();
            return dsRes;
            //return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_BankValidation_DDPaidUnpaid_UploadRecoFile", param);
        }
        public DataSet SaveData_BankValidationWarrantPaidUnpaid(DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@UploadRecoFile", ds.GetXml().ToString());
            param[1] = new SqlParameter("@PaymentMode", "BankRevalidation");
            param[2] = new SqlParameter("@ReconTemplateType", objBO.TemplateType);
            param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
            param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
            param[5] = new SqlParameter("@Remarks", objBO.Remarks);
            param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
            param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
            param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
            param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
            param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
            SqlConnection conn = new SqlConnection(ConStr_PaymentReco);
            conn.Open();
            SqlCommand sqlcomm = new SqlCommand("usp_FD_BankValidation_WarrantPaidUnpaid_UploadRecoFile");
            sqlcomm.CommandTimeout = 0;
            sqlcomm.Connection = conn;
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Parameters.AddRange(param);
            DataSet dsRes = new DataSet();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcomm);
            sqlda.Fill(dsRes);
            conn.Close();
            conn.Dispose();
            return dsRes;
            //return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_BankValidation_WarrantPaidUnpaid_UploadRecoFile", param);
        }
        public DataSet SaveData_BankValidationNEFTRejection(DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@UploadRecoFile", ds.GetXml().ToString());
            param[1] = new SqlParameter("@PaymentMode", "BankRevalidation");
            param[2] = new SqlParameter("@ReconTemplateType", objBO.TemplateType);
            param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
            param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
            param[5] = new SqlParameter("@Remarks", objBO.Remarks);
            param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
            param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
            param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
            param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
            param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
            SqlConnection conn = new SqlConnection(ConStr_PaymentReco);
            conn.Open();
            SqlCommand sqlcomm = new SqlCommand("usp_FD_BankValidation_NEFTRejection_UploadRecoFile");
            sqlcomm.CommandTimeout = 0;
            sqlcomm.Connection = conn;
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Parameters.AddRange(param);
            DataSet dsRes = new DataSet();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcomm);
            sqlda.Fill(dsRes);
            conn.Close();
            conn.Dispose();
            return dsRes;
            //return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_BankValidation_NEFTRejection_UploadRecoFile", param);
        }

        public DataSet SaveData_BankValidationDD_ACH_REJECT(DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@UploadRecoFile", ds.GetXml().ToString());
            param[1] = new SqlParameter("@PaymentMode", "BankRevalidation");
            param[2] = new SqlParameter("@ReconTemplateType", objBO.TemplateType);
            param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
            param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
            param[5] = new SqlParameter("@Remarks", objBO.Remarks);
            param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
            param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
            param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
            param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
            param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
            SqlConnection conn = new SqlConnection(ConStr_PaymentReco);
            conn.Open();
            SqlCommand sqlcomm = new SqlCommand("usp_FD_BankValidation_DD_ACH_REJECT_UploadRecoFile");
            sqlcomm.CommandTimeout = 0;
            sqlcomm.Connection = conn;
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Parameters.AddRange(param);
            DataSet dsRes = new DataSet();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcomm);
            sqlda.Fill(dsRes);
            conn.Close();
            conn.Dispose();
            return dsRes;
            //return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_BankValidation_DD_ACH_REJECT_UploadRecoFile", param);
        }


        public DataSet SaveData_BankRevalWrappCommUpload(DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@XMLCommList", ds.GetXml().ToString());
            param[1] = new SqlParameter("@Mode", "BankRevalComm");
            param[2] = new SqlParameter("@TemplateType", "BankRevalComm");
            param[3] = new SqlParameter("@UploadFileName", objBO.FileName);
            param[4] = new SqlParameter("@UploadFilePath", objBO.FilePath);
            param[5] = new SqlParameter("@Remarks", objBO.Remarks);
            param[6] = new SqlParameter("@CreatedBy", objSBO.CreatedBy);
            param[7] = new SqlParameter("@CreatedByUName", objSBO.Entity_Name);
            param[8] = new SqlParameter("@CreatedIP", objSBO.CreatedIP);
            param[9] = new SqlParameter("@SessionID", objSBO.Session_ID);
            param[10] = new SqlParameter("@FormCode", objSBO.FormCode);
            SqlConnection conn = new SqlConnection(ConStr_FD);
            conn.Open();
            SqlCommand sqlcomm = new SqlCommand("USP_FD_BankRevalWrapp_SaveComm_Dtls_V6_5");
            sqlcomm.CommandTimeout = 0;
            sqlcomm.Connection = conn;
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Parameters.AddRange(param);
            DataSet dsRes = new DataSet();
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcomm);
            sqlda.Fill(dsRes);
            conn.Close();
            conn.Dispose();
            return dsRes;
            //return SqlHelper.ExecuteDataSet(ConStr_FD, CommandType.StoredProcedure, "USP_FD_BankRevalWrapp_SaveComm_Dtls_V6_5", param);
        }
    }
}
