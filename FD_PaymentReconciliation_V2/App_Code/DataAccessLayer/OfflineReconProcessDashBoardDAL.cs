using DBHelper;
using FD_PaymentReconciliation_V2;
using FD_PaymentReconciliation_V2.BusinessObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FD_PaymentReconciliation_V2.App_Code.DataAccessLayer
{
    public class OfflineReconProcessDashBoardDAL
    {
        string ConStr_PaymentReco = string.Empty;
        public OfflineReconProcessDashBoardDAL()
        {
            ConStr_PaymentReco = Startup.Configuration["ConnectionString:ConStr_PaymentReco"].ToString();
        }
        public DataSet GetReconciliationData()
        {
            return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Offline_RecoUpload_Data");
        } 
        public DataSet GetReconRectificationData(string HdrSeq)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@HdrSeq", HdrSeq);
            return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Offline_Reco_Rectification_Data", param);
        } 
        public DataSet GetReconSuccessData(string HdrSeq)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@HdrSeq", HdrSeq);
            return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Offline_Reco_Success_Data", param);
        }
        public DataSet GetReconUploadData(string HdrSeq)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@HdrSeq", HdrSeq);
            return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Offline_Reco_Upload_Data", param);
        }
        public DataSet GetReconExceptionData(string HdrSeq)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@HdrSeq", HdrSeq);
            return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Offline_Reco_Exception_Data", param);
        }
        public Int32 UpdateReconHdrUploadedFileDetails(string HdrSeq, string FileName, string FilePath, SessionBO obj)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@HdrSequence", HdrSeq);
            param[1] = new SqlParameter("@TotalUploadFileName", FileName);
            param[2] = new SqlParameter("@TotalUploadFilePath", FilePath);
            param[3] = new SqlParameter("@UpdatedBy", obj.CreatedBy);
            param[4] = new SqlParameter("@UpdatedByUName", obj.Entity_Name);
            param[5] = new SqlParameter("@UpdatedIP", obj.CreatedIP);
            return SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Offline_Update_Hdr_Total_Upload_File_Details", param);
        }
        public Int32 UpdateReconHdrSuccessFileDetails(string HdrSeq, string FileName, string FilePath, SessionBO obj)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@HdrSequence", HdrSeq);
            param[1] = new SqlParameter("@SuccessFileName", FileName);
            param[2] = new SqlParameter("@SuccessFilePath", FilePath);
            param[3] = new SqlParameter("@UpdatedBy", obj.CreatedBy);
            param[4] = new SqlParameter("@UpdatedByUName", obj.Entity_Name);
            param[5] = new SqlParameter("@UpdatedIP", obj.CreatedIP);
            return SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Offline_Update_Hdr_Success_File_Details", param);
        }

        public Int32 UpdateReconHdrExceptionFileDetails(string HdrSeq, string FileName, string FilePath, SessionBO obj)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@HdrSequence", HdrSeq);
            param[1] = new SqlParameter("@ExceptionFileName", FileName);
            param[2] = new SqlParameter("@ExceptionFilePath", FilePath);
            param[3] = new SqlParameter("@UpdatedBy", obj.CreatedBy);
            param[4] = new SqlParameter("@UpdatedByUName", obj.Entity_Name);
            param[5] = new SqlParameter("@UpdatedIP", obj.CreatedIP);
            return SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Offline_Update_Hdr_Exception_File_Details", param);
        }

        public Int32 UpdateReconHdrRectificationFileDetails(string HdrSeq, string FileName, string FilePath, SessionBO obj)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@HdrSequence", HdrSeq);
            param[1] = new SqlParameter("@RectExceptionFileName", FileName);
            param[2] = new SqlParameter("@RectFilePath", FilePath);
            param[3] = new SqlParameter("@UpdatedBy", obj.CreatedBy);
            param[4] = new SqlParameter("@UpdatedByUName", obj.Entity_Name);
            param[5] = new SqlParameter("@UpdatedIP", obj.CreatedIP);
            return SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Offline_Update_Hdr_Rect_File_Details", param);
        }



        public int CancelReconciliationProcess(string HdrSeq, SessionBO obj)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@HdrSequence", HdrSeq);
            param[1] = new SqlParameter("@UpdatedBy", obj.CreatedBy);
            param[2] = new SqlParameter("@UpdatedByUName", obj.Entity_Name);
            param[3] = new SqlParameter("@UpdatedIP", obj.CreatedIP);
            return SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Offline_Cancel_Reconciliation_Process", param);
        }

        public int ProcessReconciliationData(string HdrSeq, SessionBO obj)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@HdrSequence", HdrSeq);
            param[1] = new SqlParameter("@UpdatedBy", obj.CreatedBy);
            param[2] = new SqlParameter("@UpdatedByUName", obj.Entity_Name);
            param[3] = new SqlParameter("@UpdatedIP", obj.CreatedIP);
            return SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Offline_Process_Reconciliation_Data", param);
        }

    }
}
