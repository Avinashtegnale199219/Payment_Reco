using DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FD_PaymentReconciliation_V2.App_Code.DataAccessLayer
{
    public class EP_UTR_RTGS_UploadReportDAL
    {
        private readonly string strConn;
        public EP_UTR_RTGS_UploadReportDAL()
        {
            strConn = Startup.Configuration["ConnectionString:ConStr_PaymentReco"].ToString();
        }

        public DataSet GetReconciliationData()
        {
            return SqlHelper.ExecuteDataSet(strConn, CommandType.StoredProcedure, "Usp_FD_EP_UTR_RTGS_Document_Dtls_V1");
        }

        public DataTable GetReconUploadData(string HdrSeq)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@HdrSeq", HdrSeq);
            DataSet ds = SqlHelper.ExecuteDataSet(strConn, CommandType.StoredProcedure, "Usp_FD_EP_UTR_RTGS_Document_Dtls_V1", param);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;

        }
    }
}
