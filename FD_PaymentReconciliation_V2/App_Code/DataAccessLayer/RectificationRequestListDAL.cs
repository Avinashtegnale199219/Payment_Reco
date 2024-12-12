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
    public class RectificationRequestListDAL
    {
        string ConStr_PaymentReco = string.Empty;
        public RectificationRequestListDAL()
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
        public DataSet Get_RectificationRequestList(string SeqNo)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ReqHdr", SeqNo.Trim());

                return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_Get_Rect_Req_List", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet InsertRectificationRequest_Hdr(string SeqNo, string Remarks, SessionBO SBO)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@ReqHdr", SeqNo.Trim());
                param[1] = new SqlParameter("@Remarks", Remarks.Trim());
                param[2] = new SqlParameter("@CreatedBy", SBO.Entity_Id);
                param[3] = new SqlParameter("@CreatedByUName", SBO.Entity_Name);
                param[4] = new SqlParameter("@CreatedIP", SBO.CreatedIP);
                param[5] = new SqlParameter("@SessionId", SBO.Session_ID);
                param[6] = new SqlParameter("@FormCode", SBO.FormCode);

                return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_Insert_Rect_Req_Hdr", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet InsertRectificationRequest_Dtl(string Recthdrseq, string reqHdr, string Applist, SessionBO SBO)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@RectHdr", Recthdrseq.Trim());
                param[1] = new SqlParameter("@ReqHdr", reqHdr.Trim());
                param[2] = new SqlParameter("@Applist", Applist.Trim());
                param[3] = new SqlParameter("@CreatedBy", SBO.Entity_Id);
                param[4] = new SqlParameter("@CreatedByUName", SBO.Entity_Name);
                param[5] = new SqlParameter("@CreatedIP", SBO.CreatedIP);
                param[6] = new SqlParameter("@SessionId", SBO.Session_ID);
                param[7] = new SqlParameter("@FormCode", SBO.FormCode);

                return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_Insert_Rect_Req_Dtl", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Added By Satish Pawar on 14 Nov 2022
        public DataSet Get_LAFDRectificationRequestList(string SeqNo)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ReqHdr", SeqNo.Trim());

                return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_LAFD_Get_Rect_Req_List", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet InsertLAFDRectificationRequest_Hdr(string SeqNo, string Remarks, SessionBO SBO)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@ReqHdr", SeqNo.Trim());
                param[1] = new SqlParameter("@Remarks", Remarks.Trim());
                param[2] = new SqlParameter("@CreatedBy", SBO.Entity_Id);
                param[3] = new SqlParameter("@CreatedByUName", SBO.Entity_Name);
                param[4] = new SqlParameter("@CreatedIP", SBO.CreatedIP);
                param[5] = new SqlParameter("@SessionId", SBO.Session_ID);
                param[6] = new SqlParameter("@FormCode", SBO.FormCode);

                return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_LAFD_Insert_Rect_Req_Hdr", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet InsertLAFDRectificationRequest_Dtl(string Recthdrseq, string reqHdr, string Applist, SessionBO SBO)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@RectHdr", Recthdrseq.Trim());
                param[1] = new SqlParameter("@ReqHdr", reqHdr.Trim());
                param[2] = new SqlParameter("@Applist", Applist.Trim());
                param[3] = new SqlParameter("@CreatedBy", SBO.Entity_Id);
                param[4] = new SqlParameter("@CreatedByUName", SBO.Entity_Name);
                param[5] = new SqlParameter("@CreatedIP", SBO.CreatedIP);
                param[6] = new SqlParameter("@SessionId", SBO.Session_ID);
                param[7] = new SqlParameter("@FormCode", SBO.FormCode);

                return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "usp_FD_Online_LAFD_Insert_Rect_Req_Dtl", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
