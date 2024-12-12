using Extension;
using FD_PaymentReconciliation_V2.BusinessObject;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using WA_FD_CP_AUTHENTICATION_MODEL;

namespace FD_CP_BTP
{
    public class AppLayoutModel
    {
        public AppLayoutModel()
        {
            EntityName = DepartmentName = LastLoginDate = LastLoginDateTime = LastLoginTime = SapCode = Oprn_Category = BranchCode = Company = string.Empty;
        }

        public string AppCode { get; set; } = string.Empty;

        public string AppName { get; set; } = string.Empty;

        public string EntityName { get; set; }

        public string DepartmentName { get; set; }

        public string LastLoginDateTime { get; set; }

        public string LastLoginDate { get; set; }

        public string LastLoginTime { get; set; }

        public string SapCode { get; set; }

        public string Oprn_Category { get; set; }

        public string BranchCode { get; set; }

        public string Company { get; set; }

        public string SubModuleName { get; set; }

        public IEnumerable<UserMenuItem> AppMenu { get; set; }

        public AppLayoutModel Get(HttpContext ctx)
        {
            SessionBO dtl = ctx.Session.GetObject<SessionBO>("UserSessionDetails");

            if (dtl != null)
            {

                EntityName = dtl.Agency_Usr_Name;
                SapCode = dtl.Agency_Usr_Clustered_ID;
                Oprn_Category = dtl.Agency_Usr_EmailID;
                BranchCode = dtl.Agency_Usr_Base_Loc_Desc;                
            }

            this.AppMenu = ctx.Session.GetObject<IList<UserMenuItem>>("UserAppMenus");

            if (this.AppMenu != null && this.AppMenu != null && this.AppMenu.Count() > 0)
            {
                this.AppName = Convert.ToString(this.AppMenu.FirstOrDefault().AppName);

                if (string.IsNullOrEmpty(this.AppName))
                {
                    this.AppName = string.Empty;
                }
            
                this.SubModuleName = Convert.ToString(this.AppMenu.FirstOrDefault().SubModName);
                if (string.IsNullOrEmpty(SubModuleName))
                {
                    this.SubModuleName = string.Empty;
                }
                else
                {
                    this.SubModuleName = string.Empty;
                }
            }

            return this;
        }
    }
}
