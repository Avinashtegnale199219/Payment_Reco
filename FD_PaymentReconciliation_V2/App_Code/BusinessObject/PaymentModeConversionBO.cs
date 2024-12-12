using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FD_PaymentReconciliation_V2.App_Code.BusinessObject
{
    public class PaymentModeConversionBO
    {
        public string Appl_No { get; set; }
        public string Old_PaymentMode { get; set; }
        public string New_PaymentMode { get; set; }
        public string Amount { get; set; }
    }
}
