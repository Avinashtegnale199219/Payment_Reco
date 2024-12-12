using System;

namespace FD_PaymentReconciliation_V2.AppCode.BusinessObject
{
    public class SearchBO
    {
        public string BROKER_CD { get; set; }
        public DateTime DEP_DATE { get; set; }
        public string START_DATE { get; set; }
        public string END_DATE { get; set; }
        public string VERIFIED_STATUS { get; set; }
        public string START_FOLIO { get; set; }
        public string END_FOLIO { get; set; }
        public string FIN_YEAR { get; set; }
        public string SEARCH_TYPE { get; set; }
        public string SEARCH_VALUE { get; set; }
    }

}
