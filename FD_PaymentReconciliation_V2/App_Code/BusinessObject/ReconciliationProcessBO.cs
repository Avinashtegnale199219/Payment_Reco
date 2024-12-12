using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FD_PaymentReconciliation_V2.App_Code.BusinessObject
{
    public class ReconciliationProcessBO
    {      
        public string Portal { get; set; }        

        public string Mode { get; set; }

        public string FileName { get; set; }
        public string FilePath { get; set; }

        public string Remarks { get; set; }
        public string TemplateType { get; set; }
    }
    public class HdrClass
    {       
        public string HdrSeq { get; set; }
    }
}
