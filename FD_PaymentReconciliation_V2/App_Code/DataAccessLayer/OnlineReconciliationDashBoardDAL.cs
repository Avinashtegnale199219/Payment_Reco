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
    public class OnlineReconciliationDashBoardDAL
    {
        string ConStr_PaymentReco = string.Empty;
        public OnlineReconciliationDashBoardDAL()
        {           
            try
            {
                ConStr_PaymentReco = Startup.Configuration["ConnectionString:ConStr_PaymentReco"].ToString();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetReconciliationData()
        {
            return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_RecoUpload_Data");
        }
        public DataSet GetReconRectificationData(string HdrSeq)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@HdrSeq", HdrSeq);
            return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_Reco_Rectification_Data", param);
        }

        public DataSet GetReconSuccessData(string HdrSeq)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@HdrSeq", HdrSeq);
            return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_Reco_Success_Data", param);
        }
        public DataSet GetReconUploadData(string HdrSeq)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@HdrSeq", HdrSeq);
            return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_Reco_Upload_Data", param);
        }
        public DataSet GetReconExceptionData(string HdrSeq)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@HdrSeq", HdrSeq);
            return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_Reco_Exception_Data", param);
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
            return SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_Update_Hdr_Total_Upload_File_Details", param);
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
            return SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_Update_Hdr_Success_File_Details", param);
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
            return SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_Update_Hdr_Exception_File_Details", param);
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
            return SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_Update_Hdr_Rect_File_Details", param);
        }
        
        public int CancelReconciliationProcess(string HdrSeq, SessionBO obj)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@HdrSequence", HdrSeq);
            param[1] = new SqlParameter("@UpdatedBy", obj.CreatedBy);
            param[2] = new SqlParameter("@UpdatedByUName", obj.Entity_Name);
            param[3] = new SqlParameter("@UpdatedIP", obj.CreatedIP);
            return SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_Cancel_Reconciliation_Process", param);
        }
        
        public int ProcessReconciliationData(string HdrSeq, SessionBO obj)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@HdrSequence", HdrSeq);
            param[1] = new SqlParameter("@UpdatedBy", obj.CreatedBy);
            param[2] = new SqlParameter("@UpdatedByUName", obj.Entity_Name);
            param[3] = new SqlParameter("@UpdatedIP", obj.CreatedIP);
            return SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_Process_Reconciliation_Data", param);
        }

        //Added By Satish Pawar on 14 Nov 2022
        public DataSet GetReconLAFDUploadData(string HdrSeq)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@HdrSeq", HdrSeq);
            return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_LAFD_Reco_Upload_Data", param);
        }
        public DataSet GetReconLAFDExceptionData(string HdrSeq)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@HdrSeq", HdrSeq);
            return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_LAFD_Reco_Exception_Data", param);
        }
        public DataSet GetReconLAFDSuccessData(string HdrSeq)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@HdrSeq", HdrSeq);
            return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_LAFD_Reco_Success_Data", param);
        }
        public DataSet GetReconLAFDRectificationData(string HdrSeq)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@HdrSeq", HdrSeq);
            return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_LAFD_Reco_Rectification_Data", param);
        }
        public int ProcessLAFDReconciliationData(string HdrSeq, SessionBO obj)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@HdrSequence", HdrSeq);
            param[1] = new SqlParameter("@UpdatedBy", obj.CreatedBy);
            param[2] = new SqlParameter("@UpdatedByUName", obj.Entity_Name);
            param[3] = new SqlParameter("@UpdatedIP", obj.CreatedIP);
            return SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_LAFD_Process_Reconciliation_Data", param);
        }
        //Added By Satish Pawar on 14 Nov 2022
        public Int32 UpdateLAFDReconHdrExceptionFileDetails(string HdrSeq, string FileName, string FilePath, SessionBO obj)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@HdrSequence", HdrSeq);
            param[1] = new SqlParameter("@ExceptionFileName", FileName);
            param[2] = new SqlParameter("@ExceptionFilePath", FilePath);
            param[3] = new SqlParameter("@UpdatedBy", obj.CreatedBy);
            param[4] = new SqlParameter("@UpdatedByUName", obj.Entity_Name);
            param[5] = new SqlParameter("@UpdatedIP", obj.CreatedIP);
            return SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_LAFD_Update_Hdr_Exception_File_Details", param);
        }

        public Int32 UpdateLAFDReconHdrUploadedFileDetails(string HdrSeq, string FileName, string FilePath, SessionBO obj)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@HdrSequence", HdrSeq);
            param[1] = new SqlParameter("@TotalUploadFileName", FileName);
            param[2] = new SqlParameter("@TotalUploadFilePath", FilePath);
            param[3] = new SqlParameter("@UpdatedBy", obj.CreatedBy);
            param[4] = new SqlParameter("@UpdatedByUName", obj.Entity_Name);
            param[5] = new SqlParameter("@UpdatedIP", obj.CreatedIP);
            return SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_LAFD_Update_Hdr_Total_Upload_File_Details", param);
        }

        public Int32 UpdateLAFDReconHdrSuccessFileDetails(string HdrSeq, string FileName, string FilePath, SessionBO obj)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@HdrSequence", HdrSeq);
            param[1] = new SqlParameter("@SuccessFileName", FileName);
            param[2] = new SqlParameter("@SuccessFilePath", FilePath);
            param[3] = new SqlParameter("@UpdatedBy", obj.CreatedBy);
            param[4] = new SqlParameter("@UpdatedByUName", obj.Entity_Name);
            param[5] = new SqlParameter("@UpdatedIP", obj.CreatedIP);
            return SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_LAFD_Update_Hdr_Success_File_Details", param);
        }
        public Int32 UpdateLAFDReconHdrRectificationFileDetails(string HdrSeq, string FileName, string FilePath, SessionBO obj)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@HdrSequence", HdrSeq);
            param[1] = new SqlParameter("@RectExceptionFileName", FileName);
            param[2] = new SqlParameter("@RectFilePath", FilePath);
            param[3] = new SqlParameter("@UpdatedBy", obj.CreatedBy);
            param[4] = new SqlParameter("@UpdatedByUName", obj.Entity_Name);
            param[5] = new SqlParameter("@UpdatedIP", obj.CreatedIP);
            return SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_LAFD_Update_Hdr_Rect_File_Details", param);
        }
        public int CancelLAFDReconciliationProcess(string HdrSeq, SessionBO obj)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@HdrSequence", HdrSeq);
            param[1] = new SqlParameter("@UpdatedBy", obj.CreatedBy);
            param[2] = new SqlParameter("@UpdatedByUName", obj.Entity_Name);
            param[3] = new SqlParameter("@UpdatedIP", obj.CreatedIP);
            return SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_LAFD_Cancel_Reconciliation_Process", param);
        }
       
    }
}
