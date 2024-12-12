using DBHelper;
using FD_PaymentReconciliation_V2.BusinessObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FD_PaymentReconciliation_V2.App_Code.DataAccessLayer
{
    public class OnlinePaymentReconciliationList_RW_DAL
    {
        private readonly string strConn;
        public OnlinePaymentReconciliationList_RW_DAL()
        {
            strConn = Startup.Configuration["ConnectionString:ConnectionString_Reward"].ToString();
        }

        public DataSet GetTechProcessData(string SapCode)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConn);

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@SapCode", SapCode.Trim());

                return SqlHelper.ExecuteDataSet(conn, CommandType.StoredProcedure, "usp_FD_RW_Get_RECO_Upload", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetTechProcess_ExceptionData(string SeqNo)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConn);

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Reqhdr", SeqNo.Trim());

                return SqlHelper.ExecuteDataSet(conn, CommandType.StoredProcedure, "usp_FD_Get_RECO_ExceptionData", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetTechProcess_AlreadyExistsData(string SeqNo)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConn);

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Reqhdr", SeqNo.Trim());

                return SqlHelper.ExecuteDataSet(conn, CommandType.StoredProcedure, "usp_FD_Get_RECO_AlreadyExists", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet updateIsProcesshdrSeq(string SeqNo, SessionBO SBO)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConn);

                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@hdr", SeqNo.Trim());
                param[1] = new SqlParameter("@CreatedBy", SBO.Entity_Id);
                param[2] = new SqlParameter("@CreatedByUName", SBO.Entity_Name);
                param[3] = new SqlParameter("@CreatedIP", SBO.CreatedIP);

                return SqlHelper.ExecuteDataSet(conn, CommandType.StoredProcedure, "usp_FD_RW_Updatehdr_Seq", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetTechprocessUploadedFileDownload(string SeqNo)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConn);

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Reqhdr", SeqNo.Trim());

                return SqlHelper.ExecuteDataSet(conn, CommandType.StoredProcedure, "usp_FD_Get_RECO_UploadedFileDownload", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet CancelFileUploadHdrSeq(string SeqNo, SessionBO SBO)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConn);

                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@Reqhdr", SeqNo.Trim());
                param[1] = new SqlParameter("@CreatedBy", SBO.Entity_Id);
                param[2] = new SqlParameter("@CreatedByUName", SBO.Entity_Name);
                param[3] = new SqlParameter("@CreatedIP", SBO.CreatedIP);

                return SqlHelper.ExecuteDataSet(conn, CommandType.StoredProcedure, "usp_FD_RECO_CancelFileUploadHdrSeq", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetTechProcess_RectificationData(string SeqNo)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConn);

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Reqhdr", SeqNo.Trim());

                return SqlHelper.ExecuteDataSet(conn, CommandType.StoredProcedure, "usp_FD_Get_RECO_RectificationData", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
