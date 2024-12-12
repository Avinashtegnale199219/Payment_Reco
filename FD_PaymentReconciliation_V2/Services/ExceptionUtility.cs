using DBHelper;
using FD_PaymentReconciliation_V2.BusinessObject;
using Microsoft.AspNetCore.Http;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FD_PaymentReconciliation_V2.Services
{

    public class ExceptionUtility
    {
        #region Properties
        string AppName { get; set; }

        string ControllerName { get; set; }

        string ActionName { get; set; }

        string PageUrl { get; set; }

        string ExceptionType { get; set; }

        string Exception { get; set; }

        string ExceptionSource { get; set; }

        int LineNo { get; set; }
        long SESSIONID { get; set; }
        string CreatedIp { get; set; }
        string CreatedByUName { get; set; }
        string FormCode { get; set; }

        string ExceptionStackTrace { get; set; }

        public static SessionBO Session { get; set; }

        #endregion

        public async static void LogExceptionAsync(Exception ex, Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor controller = null)
        {
            try
            {
                string ControllerName = string.Empty, ActionName = string.Empty, DisplayName = string.Empty;

                ExceptionUtility ext = SetValue(ex);

                if (controller != null)
                {
                    ext.ControllerName = controller.ControllerName;
                    ext.ActionName = controller.ActionName;
                    ext.AppName = controller.ControllerTypeInfo.ToString();

                }


                await Task.Run(() =>
                {
                    DBErrorLog(ext);

                    //SendErrorMailAsync(ext);

                });

                FileErrorLogAsync(ext);
            }
            catch (Exception)
            {
            }
        }

        public static void DBErrorLog(ExceptionUtility _eul)
        {
            try
            {
                string Conn = Startup.Configuration["ExceptionSettings:DB_Nc_Error_con"];

                using (SqlConnection con = new SqlConnection(Conn))
                {
                    SqlParameter[] sqlparam = new SqlParameter[10];
                    
                    sqlparam[0] = new SqlParameter("@F_App_Name", _eul.AppName);
                    sqlparam[1] = new SqlParameter("@F_Module","FD Payment Reco");
                    sqlparam[2] = new SqlParameter("@F_Submodule", _eul.ControllerName);
                    sqlparam[3] = new SqlParameter("@F_Err_Type", _eul.AppName);
                    sqlparam[4] = new SqlParameter("@F_Err_Execp", _eul.Exception);
                    sqlparam[5] = new SqlParameter("@F_Err_Source", _eul.ExceptionSource);
                    sqlparam[6] = new SqlParameter("@F_CREATEDIP", _eul.CreatedIp);
                    sqlparam[7] = new SqlParameter("@F_CREATEDBY", _eul.CreatedByUName);
                    sqlparam[8] = new SqlParameter("@F_CREATEDBYUNAME", _eul.CreatedByUName);
                    sqlparam[9] = new SqlParameter("@F_SESSIONID", _eul.SESSIONID);

                    SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "usp_Insert_ErrorLog", sqlparam);
                }
            }
            catch (Exception )
            {
            }
        }

        public async static void SendErrorMailAsync(ExceptionUtility _eul)
        {
            StringBuilder sbhtml = new StringBuilder();

            try
            {
                #region Create Html

                sbhtml.Append("<table width=\"90%\" style=\"border: 1px solid #df4a40; background:#f7eaea;\" rules=\"all\">");

                sbhtml.Append("<tr><td style=\"height: 30px;width:20%;border: 1px solid #df4a40;\"> Error occured on :</td><td style=\"height: 30px;border: 1px solid #df4a40\"\">" + DateTime.Now.ToString() + "</td></tr>");

                sbhtml.Append("<tr><td style=\"height: 30px;width:20%;border: 1px solid #df4a40;\"> Application Name :</td><td style=\"height: 30px;border: 1px solid #df4a40\"\">" + _eul.AppName + " </td></tr>");

                sbhtml.Append("<tr><td style=\"height: 30px;width:20%;border: 1px solid #df4a40;\"> Request Url :</td><td style=\"height: 30px;border: 1px solid #df4a40\"\">" + _eul.PageUrl + "</td></tr>");

                sbhtml.Append("<tr><td style=\"height: 30px;border: 1px solid #df4a40\">Error Message : </td>  <td style=\"height: 30px;border: 1px solid #df4a40\">" + _eul.Exception + "</td></tr>");

                sbhtml.Append("<tr><td style=\"height: 30px;border: 1px solid #df4a40\"> Host IP  : </td> <td style=\"height: 30px;border: 1px solid #df4a40\"\">" + _eul.CreatedIp + "</td></tr>");

                sbhtml.Append("<tr><td style=\"height: 30px;border: 1px solid #df4a40\"> Error Info: </td><td style=\"height: 30px;border: 1px solid #df4a40\"\">  " + _eul.ExceptionSource + "</td></tr>");

                sbhtml.Append("<tr><td style=\"height: 30px;border: 1px solid #df4a40\">" +
                      "Error Stack Trace : </td><td style=\"height: 30px;border: 1px solid #df4a40\">" + _eul.ExceptionStackTrace + "</td></tr>");

                sbhtml.Append("</table>");


                #endregion

                using (System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage())
                {
                    string ToEmailId = Convert.ToString(Startup.Configuration["ExceptionSettings:MailSettings:ToEmailId"]);
                    string FromEmailId = Convert.ToString(Startup.Configuration["ExceptionSettings:MailSettings:FromEmailId"]);
                    string Host = Convert.ToString(Startup.Configuration["ExceptionSettings:MailSettings:Host"]);
                    string Port = Convert.ToString(Startup.Configuration["ExceptionSettings:MailSettings:Port"]);
                    string DisplayName = Convert.ToString(Startup.Configuration["ExceptionSettings:MailSettings:DisplayName"]);
                    string Password = Convert.ToString(Startup.Configuration["ExceptionSettings:MailSettings:Password"]);
                    string Subject = Convert.ToString(Startup.Configuration["ExceptionSettings:MailSettings:Subject"]);
                    string UserName = Convert.ToString(Startup.Configuration["ExceptionSettings:MailSettings:UserName"]);

                    if (string.IsNullOrEmpty(ToEmailId) || string.IsNullOrEmpty(FromEmailId))
                    {
                        return;
                    }

                    string[] maildids = ToEmailId.Split(';');
                    foreach (string Mailid in maildids)
                    {
                        msg.To.Add(Mailid);
                    }

                    msg.From = new System.Net.Mail.MailAddress(FromEmailId, DisplayName);
                    msg.Subject = Subject;
                    msg.IsBodyHtml = true;
                    msg.Body = sbhtml.ToString();
                    msg.Priority = System.Net.Mail.MailPriority.High;
                    using (System.Net.Mail.SmtpClient SmtpMail = new System.Net.Mail.SmtpClient())
                    {
                        SmtpMail.Host = Host;
                        SmtpMail.Port = Convert.ToInt32(Port);
                        SmtpMail.UseDefaultCredentials = false;
                        SmtpMail.Credentials = new NetworkCredential(UserName, Password);
                        await SmtpMail.SendMailAsync(msg);
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                sbhtml = null;
            }
        }

        public static void FileErrorLogAsync(ExceptionUtility _eul)
        {
            string ErrorLogFilePath = Convert.ToString(Startup.Configuration["ExceptionSettings:ErrorLogFilePath"]);

            if (string.IsNullOrEmpty(ErrorLogFilePath))
            {
                return;
            }

            if (!Directory.Exists(ErrorLogFilePath.Trim()))
            {
                return;
            }

            FileStream file = null;
            StreamWriter sw = null;
            StringBuilder sbLogMessage = new StringBuilder();
            try
            {
                #region Log Error in Text File                

                string logFile = ErrorLogFilePath + @"\" + System.DateTime.Now.ToString("dd_MM_yyyy") + ".txt";

                sbLogMessage.AppendLine("******************");
                sbLogMessage.AppendLine("Exception Date: ");
                sbLogMessage.AppendLine(DateTime.Now.ToString());
                sbLogMessage.AppendLine("Application Name: ");
                sbLogMessage.AppendLine(_eul.AppName);
                sbLogMessage.AppendLine("Module Name: ");
                sbLogMessage.AppendLine(_eul.ControllerName);
                sbLogMessage.AppendLine("Sub Module Name: ");
                sbLogMessage.AppendLine(_eul.ActionName);
                sbLogMessage.AppendLine("Request Url: ");
                sbLogMessage.AppendLine(_eul.PageUrl);
                sbLogMessage.AppendLine("Created Type: ");
                sbLogMessage.AppendLine("EMP");
                sbLogMessage.AppendLine("Created IP: ");
                sbLogMessage.AppendLine(_eul.CreatedIp);
                sbLogMessage.AppendLine("Exception Type: ");
                sbLogMessage.AppendLine(_eul.ExceptionType);
                sbLogMessage.AppendLine("Exception: ");
                sbLogMessage.AppendLine(_eul.Exception);
                sbLogMessage.AppendLine("Source: ");
                sbLogMessage.AppendLine(_eul.ExceptionSource);
                sbLogMessage.AppendLine("Stack Trace: ");
                sbLogMessage.AppendLine(_eul.ExceptionStackTrace);

                using (file = new System.IO.FileStream(logFile, FileMode.Append, FileAccess.Write, FileShare.Read))
                {
                    using (sw = new StreamWriter(file, Encoding.Unicode))
                    {
                        sw.Write(sbLogMessage.ToString());
                    }
                }
                #endregion
            }
            catch (IOException)
            {
                if (sw != null)
                {
                    sw.Close(); sw.Dispose();
                }
                if (file != null)
                {
                    file.Close(); file.Dispose();
                }
                if (sbLogMessage != null)
                {
                    sbLogMessage.Clear(); sbLogMessage = null;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close(); sw.Dispose();
                }
                if (file != null)
                {
                    file.Close(); file.Dispose();
                }
                if (sbLogMessage != null)
                {
                    sbLogMessage.Clear();
                }
            }
        }

        public static void ClientSideErrorLog(string ErrorMessage)
        {
            try
            {
                string ErrorLogFilePath = Convert.ToString(Startup.Configuration["ExceptionSettings:ErrorLogFilePath"]);

                if (string.IsNullOrEmpty(ErrorLogFilePath))
                {
                    return;
                }

                if (!Directory.Exists(ErrorLogFilePath.Trim()))
                {
                    return;
                }

                string logFile = ErrorLogFilePath + @"\" + System.DateTime.Now.ToString("dd_MM_yyyy") + ".txt";

                using (FileStream file = new FileStream(logFile, FileMode.Append, FileAccess.Write, FileShare.Read))
                {
                    using (StreamWriter sw = new StreamWriter(file, Encoding.Unicode))
                    {
                        sw.Write(ErrorMessage);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        static ExceptionUtility SetValue(Exception ex)
        {
            ExceptionUtility extut = new ExceptionUtility();
            Clear(extut);
            try
            {
                if (ex != null)
                {
                    try
                    {
                        extut.Exception = (string.IsNullOrEmpty(ex.Message) ? ex.InnerException.Message : ex.Message);
                        extut.ExceptionSource = (string.IsNullOrEmpty(ex.Source) ? ex.InnerException.Source : ex.Source);
                        extut.ExceptionStackTrace = (string.IsNullOrEmpty(ex.StackTrace) ? (ex.InnerException.StackTrace) : ex.StackTrace);
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        StackTrace stk = new StackTrace(ex, true);
                        if (stk != null && stk.FrameCount > 0)
                        {
                            extut.Exception += " Line Number: " + stk.GetFrame(0).GetFileLineNumber().ToString();
                            extut.ExceptionSource += " Method Name: " + stk.GetFrame(0).GetMethod().ToString();
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                extut.CreatedByUName = Session.CreatedByUName;
                extut.CreatedIp = Session.CreatedIP;
                extut.FormCode = Session.FormCode;
                extut.SESSIONID = Session.Session_ID;

                HttpContextAccessor ctx = new HttpContextAccessor();

                if (ctx != null && ctx.HttpContext != null)
                {

                    HttpRequest Request = ctx.HttpContext.Request;

                    UserApplicationDtls.UserApplicationDtls _app = new UserApplicationDtls.UserApplicationDtls(ctx.HttpContext);
                    extut.CreatedIp = _app.CreatedIP;
                    extut.PageUrl = _app.RequestUrl;
                }

            }
            catch (Exception)
            {
            }

            return extut;
        }

        static void Clear(ExceptionUtility _eul)
        {
            _eul.AppName = _eul.Exception = _eul.ExceptionSource = _eul.ExceptionType = _eul.PageUrl = _eul.ExceptionStackTrace = _eul.CreatedIp = string.Empty;
        }
    }

    
}
