﻿using DBHelper;
using FD_PaymentReconciliation_V2;
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
    public class RTGSRecoProcessDashBoardDAL
    {
        private readonly string strConn;
        private readonly string strConn1;

        public RTGSRecoProcessDashBoardDAL()
        {
            strConn = Startup.Configuration["ConnectionString:ConStr_PaymentReco"].ToString();
            strConn1= Startup.Configuration["ConnectionString:ConStr_FD"].ToString();
        }
        RTGSReconciliationDashBoardBO objbo = new RTGSReconciliationDashBoardBO();
        public DataSet GetReconciliationData()
        {
            return SqlHelper.ExecuteDataSet(strConn, CommandType.StoredProcedure, "usp_FD_RTGS_RecoUpload_Data");
        }
        public DataSet GetReconUploadData(string HdrSeq)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@HdrSeq", HdrSeq);
            return SqlHelper.ExecuteDataSet(strConn, CommandType.StoredProcedure, "usp_FD_RTGS_Reco_Upload_Data", param);
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
            return SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "usp_FD_RTGS_Update_Hdr_Total_Upload_File_Details", param);
        }

        public DataSet GetReconExceptionData(string HdrSeq)
        {
            
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@HdrSeq", HdrSeq);
            return SqlHelper.ExecuteDataSet(strConn, CommandType.StoredProcedure, "usp_FD_RTGS_Reco_Exception_Data", param);
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
            return SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "usp_FD_RTGS_Update_Hdr_Exception_File_Details", param);
        }

        public DataSet GetReconSuccessData(string HdrSeq)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@HdrSeq", HdrSeq);
            return SqlHelper.ExecuteDataSet(strConn, CommandType.StoredProcedure, "usp_FD_RTGS_Reco_Success_Data", param);
        }

        public DataTable GetProcessedReconciliationData(string HdrSeq)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@HdrSequence", HdrSeq);
            DataSet ds = SqlHelper.ExecuteDataSet(strConn, CommandType.StoredProcedure, "usp_FD_RTGS_Processed_Data", param);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else return null;
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
            return SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "usp_FD_RTGS_Update_Hdr_Success_File_Details", param);
        }

        public int CancelReconciliationProcess(string HdrSeq, SessionBO obj)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@HdrSequence", HdrSeq);
            param[1] = new SqlParameter("@UpdatedBy", obj.CreatedBy);
            param[2] = new SqlParameter("@UpdatedByUName", obj.Entity_Name);
            param[3] = new SqlParameter("@UpdatedIP", obj.CreatedIP);
            return SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "usp_FD_RTGS_Cancel_Reconciliation_Process", param);
        }

        public int ProcessReconciliationData(string HdrSeq, SessionBO obj)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@HdrSequence", HdrSeq);
            param[1] = new SqlParameter("@UpdatedBy", obj.CreatedBy);
            param[2] = new SqlParameter("@UpdatedByUName", obj.Entity_Name);
            param[3] = new SqlParameter("@UpdatedIP", obj.CreatedIP);
            return SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "usp_FD_RTGS_Process_Reconciliation_Data", param);
        }


        public int UpdateFDNewPaid(string ApplNo, DateTime PaymentDate)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ApplNo", ApplNo);
            param[1] = new SqlParameter("@PaymentDate", PaymentDate);
            return SqlHelper.ExecuteNonQuery(strConn1, CommandType.StoredProcedure, "usp_FD_RTGS_Update_New_Paid_Flag", param);
        }
    }
}
