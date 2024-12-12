using DBHelper;
using FD_PaymentReconciliation_V2;
using FD_PaymentReconciliation_V2.BusinessObject;
using FD_PaymentReconciliation_V2.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FD_CP_MIS_VIEW.App_Code.DataAccessLayer
{
    public class OnlinePaymentReconciliation_RW_DAL
    {
       // string conStr = string.Empty;

        private readonly string conStr;
        public OnlinePaymentReconciliation_RW_DAL()
        {
            conStr = Startup.Configuration["ConnectionString:ConnectionString_Reward"].ToString();
        }
        //public OnlinePaymentReconciliation_RW_DAL()
        //{
        //    conStr = Startup.Configuration["ConnectionString:ConnectionString_Reward"].ToString();
        //}

        public string SAVE_REQUEST_QUEUE_HDR(string AggrCode, string AggrName, string fileLocation, string FileName, SessionBO SBO, string Remarks)
        {
            string result = string.Empty;
            try
            {
                int hdrseq = 1;
                SqlConnection conn = new SqlConnection(conStr);

                SqlParameter[] param = new SqlParameter[11];
                param[0] = new SqlParameter("@f_Request_HdrSequence", hdrseq);
                param[1] = new SqlParameter("@RWCode", AggrCode.Trim());
                param[2] = new SqlParameter("@RWName", AggrName.Trim());
                param[3] = new SqlParameter("@UploadFileName", FileName.Trim());
                param[4] = new SqlParameter("@UploadFilePath", fileLocation.Trim());
                param[5] = new SqlParameter("@CreatedBy", SBO.Entity_Id);
                param[6] = new SqlParameter("@CreatedByUName", SBO.Entity_Name);
                param[7] = new SqlParameter("@CreatedIP", SBO.CreatedIP);
                param[8] = new SqlParameter("@SessionID", SBO.Session_ID);
                param[9] = new SqlParameter("@FormCode", SBO.FormCode);
                param[10] = new SqlParameter("@Remarks", Remarks);


                DataSet ds = SqlHelper.ExecuteDataSet(conn, CommandType.StoredProcedure, "Usp_FD_Insert_RW_TransUpload_Req_Hdr", param);

                if (ds.Tables[0].Rows[0][0] != null)
                {
                    DataSet ds2 = new DataSet();
                    SqlConnection conn1 = new SqlConnection(conStr);
                    SqlParameter[] param1 = new SqlParameter[1];
                    param1[0] = new SqlParameter("@HDR_SEQ", SqlDbType.NVarChar);
                    param1[0].Value = ds.Tables[0].Rows[0][0].ToString();


                    ds2 = SqlHelper.ExecuteDataSet(conn1, CommandType.StoredProcedure, "Usp_FD_Update_RW_TransUpload_Req_Hdr_SEQ", param1);
                    result = ds2.Tables[0].Rows[0][0].ToString();
                    //result = SqlHelper.ExecuteDataset(sqltrn, CommandType.StoredProcedure, "", param);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
               // ExceptionUtility.LogException(ex, ex.Source);
               // return null;
            }
        }

        public void SaveOriginalFileUpload(DataTable dt, string hdrseq, SessionBO SBO, ref string strErr)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection conn = new SqlConnection(conStr);
                SqlParameter[] param = new SqlParameter[8];

                param[0] = new SqlParameter("@Details", SqlDbType.Structured);
                param[0].TypeName = "dbo.t_FD_RW_TransUpload";
                param[0].Value = dt;


                param[1] = new SqlParameter("@hdr", SqlDbType.NVarChar);
                param[1].Value = hdrseq;

                param[2] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar);
                param[2].Value = SBO.Entity_Id;

                param[3] = new SqlParameter("@CreatedByUName", SqlDbType.NVarChar);
                param[3].Value = SBO.Entity_Name;

                param[4] = new SqlParameter("@CreatedIP", SqlDbType.NVarChar);
                param[4].Value = SBO.CreatedIP;

                param[5] = new SqlParameter("@SessionID", SqlDbType.NVarChar);
                param[5].Value = SBO.Session_ID;

                param[6] = new SqlParameter("@FormCode", SqlDbType.NVarChar);
                param[6].Value = SBO.FormCode;

                param[7] = new SqlParameter("@Message", SqlDbType.NVarChar);
                param[7].Direction = ParameterDirection.Output;
                param[7].Size = int.MaxValue;

                //Insert data into Header and Detail Table 
                ds = SqlHelper.ExecuteDataSet(conn, CommandType.StoredProcedure, "usp_FD_TransUploadDetails_Upload", param);
                strErr = Convert.ToString(param[7].Value);
            }
            catch (Exception ex)
            {
                throw ex;
               // ExceptionUtility.LogException(ex, ex.Source);
            }

        }

        public DataTable SaveDetails(DataTable dt, string hdrseq, SessionBO SBO, ref string strErr, DataTable DuplicateDt)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection conn = new SqlConnection(conStr);
                SqlParameter[] param = new SqlParameter[8];

                param[0] = new SqlParameter("@Details", SqlDbType.Structured);
                param[0].TypeName = "dbo.t_FD_RW_TransUpload";
                param[0].Value = dt;


                param[1] = new SqlParameter("@hdr", SqlDbType.NVarChar);
                param[1].Value = hdrseq;

                param[2] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar);
                param[2].Value = SBO.Entity_Id;

                param[3] = new SqlParameter("@CreatedByUName", SqlDbType.NVarChar);
                param[3].Value = SBO.Entity_Name;

                param[4] = new SqlParameter("@CreatedIP", SqlDbType.NVarChar);
                param[4].Value = SBO.CreatedIP;

                param[5] = new SqlParameter("@SessionID", SqlDbType.NVarChar);
                param[5].Value = SBO.Session_ID;

                param[6] = new SqlParameter("@FormCode", SqlDbType.NVarChar);
                param[6].Value = SBO.FormCode;

                param[7] = new SqlParameter("@Message", SqlDbType.NVarChar);
                param[7].Direction = ParameterDirection.Output;
                param[7].Size = int.MaxValue;

                //Insert data into Header and Detail Table 
                ds = SqlHelper.ExecuteDataSet(conn, CommandType.StoredProcedure, "usp_FD_TransUploadDetails", param);//sp has some change
                strErr = Convert.ToString(param[7].Value);

                // Insert Duplicate Record into Table 
                if (DuplicateDt != null && DuplicateDt.Rows.Count > 0)
                {
                    SqlConnection conn1 = new SqlConnection(conStr);
                    param[0] = new SqlParameter("@Details", SqlDbType.Structured);
                    param[0].TypeName = "dbo.t_FD_RW_TransUpload_Exec";
                    param[0].Value = DuplicateDt;
                    SqlHelper.ExecuteDataSet(conn1, CommandType.StoredProcedure, "usp_FD_Insert_TransUploadDetails_EXEC", param);
                }

                // Fetch Data For Preparing Excel 
                DataSet ds2 = new DataSet();
                SqlConnection conn2 = new SqlConnection(conStr);
                SqlParameter[] param1 = new SqlParameter[1];

                //param1[0] = new SqlParameter("@Details", SqlDbType.Structured);
                //param1[0].TypeName = "dbo.t_FD_Aggr_TransUpload";
                //param1[0].Value = dt;

                param1[0] = new SqlParameter("@hdr", SqlDbType.NVarChar);
                param1[0].Value = hdrseq;

                ds2 = SqlHelper.ExecuteDataSet(conn2, CommandType.StoredProcedure, "usp_FD_GetTransactionUpload_DetailsDownload", param1);
                dt = ds2.Tables[0];
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
              //  ExceptionUtility.LogException(ex);
              //  return null;
            }
        }
        public void UpdateDownloadFilePath(string hdrseq, string filepath, string filename, int SucessRowCount, ref string strErr)
        {
            SqlConnection conn = new SqlConnection(conStr);

            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@HDR_SEQ", SqlDbType.NVarChar);
                param[0].Value = hdrseq;

                param[1] = new SqlParameter("@FILEPATH", SqlDbType.NVarChar);
                param[1].Value = filepath;

                param[2] = new SqlParameter("@FILENAME", SqlDbType.NVarChar);
                param[2].Value = filename;

                param[3] = new SqlParameter("@SucessRowCount", SqlDbType.BigInt);
                param[3].Value = SucessRowCount;


                int result = SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "Usp_FD_RW_Update_TransUploadDownloadPath_Status", param);



            }
            catch (Exception ex)
            {
                strErr = ex.Message.ToString();

            }

        }



    }
}
