
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
    public class RectificationApprDashboardDAL
    {
        string ConStr_PaymentReco = string.Empty;
        public RectificationApprDashboardDAL()
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
        public DataSet GetRectification_ApprovalDashboard(string SapCode)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@CreatedBy", SapCode.Trim());

                return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_Get_Rectification_Approval_Dashboard", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Get_RectificationApproverList(string SeqNo, string RectReqNo)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@ReqHdr", SeqNo.Trim());
                param[1] = new SqlParameter("@ReqRectHdr", RectReqNo.Trim());
                return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_Get_Rect_Appr_List", param);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        //public DataSet Update_RectificationApproverData(string RecthdrSeq, string Req, string Applist, string Remarks, SessionBO SBO, bool IsAllRequestApproved)
        //{
        //    try
        //    {
        //        SqlParameter[] param = new SqlParameter[10];


        //        param[0] = new SqlParameter("@hdr", Req.Trim());
        //        param[1] = new SqlParameter("@Remarks", Remarks.Trim());
        //        param[2] = new SqlParameter("@AppList", Applist.Trim());
        //        param[3] = new SqlParameter("@IsAllRequestApproved", IsAllRequestApproved);
        //        param[4] = new SqlParameter("@CreatedBy", SBO.Entity_Id);
        //        param[5] = new SqlParameter("@CreatedByUName", SBO.Entity_Name);
        //        param[6] = new SqlParameter("@CreatedIP", SBO.CreatedIP);
        //        param[7] = new SqlParameter("@SessionId", SBO.Session_ID);
        //        param[8] = new SqlParameter("@FormCode", SBO.FormCode);
        //        param[9] = new SqlParameter("@RecthdrSeq", RecthdrSeq.Trim());

        //        return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_Update_Rect_Appr_Data", param);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public int Update_RectificationApproverData(string RecthdrSeq, string Req, string Applist, string Remarks, SessionBO SBO, bool IsAllRequestApproved)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[10];

                param[0] = new SqlParameter("@hdr", Req.Trim());
                param[1] = new SqlParameter("@Remarks", Remarks.Trim());
                param[2] = new SqlParameter("@AppList", Applist.Trim());
                param[3] = new SqlParameter("@IsAllRequestApproved", IsAllRequestApproved);
                param[4] = new SqlParameter("@CreatedBy", SBO.Entity_Id);
                param[5] = new SqlParameter("@CreatedByUName", SBO.Entity_Name);
                param[6] = new SqlParameter("@CreatedIP", SBO.CreatedIP);
                param[7] = new SqlParameter("@SessionId", SBO.Session_ID);
                param[8] = new SqlParameter("@FormCode", SBO.FormCode);
                param[9] = new SqlParameter("@RecthdrSeq", RecthdrSeq.Trim());

                return SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_Update_Rect_Appr_Data", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update_LAFDRectificationApproverData(string RecthdrSeq, string Req, string Applist, string Remarks, SessionBO SBO, bool IsAllRequestApproved)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[10];

                param[0] = new SqlParameter("@hdr", Req.Trim());
                param[1] = new SqlParameter("@Remarks", Remarks.Trim());
                param[2] = new SqlParameter("@AppList", Applist.Trim());
                param[3] = new SqlParameter("@IsAllRequestApproved", IsAllRequestApproved);
                param[4] = new SqlParameter("@CreatedBy", SBO.Entity_Id);
                param[5] = new SqlParameter("@CreatedByUName", SBO.Entity_Name);
                param[6] = new SqlParameter("@CreatedIP", SBO.CreatedIP);
                param[7] = new SqlParameter("@SessionId", SBO.Session_ID);
                param[8] = new SqlParameter("@FormCode", SBO.FormCode);
                param[9] = new SqlParameter("@RecthdrSeq", RecthdrSeq.Trim());

                return SqlHelper.ExecuteNonQuery(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_LAFD_Update_Rect_Appr_Data", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Get_LAFD_RectificationApproverList(string SeqNo, string RectReqNo)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@ReqHdr", SeqNo.Trim());
                param[1] = new SqlParameter("@ReqRectHdr", RectReqNo.Trim());
                return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_LAFD_Get_Rect_Appr_List", param);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }



    }
}
