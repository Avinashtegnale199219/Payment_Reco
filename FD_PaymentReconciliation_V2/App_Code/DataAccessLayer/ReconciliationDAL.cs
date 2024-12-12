using DBHelper;
using FD_PaymentReconciliation_V2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FD_PaymentReconciliation_V2.App_Code.DataAccessLayer
{
    public class ReconciliationDAL
    {
        string ConStr_PaymentReco = string.Empty;
       
        public ReconciliationDAL()
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

        public DataSet Get_Template_Type(string PaymentMode)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@PaymentMode", PaymentMode);
            return SqlHelper.ExecuteDataSet(ConStr_PaymentReco, CommandType.StoredProcedure, "Usp_FD_Reco_Get_Template_Type_Dtl", param);
        }

    }
}
