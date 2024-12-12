using FD_PaymentReconciliation_V2.App_Code.BusinessObject;
using FD_PaymentReconciliation_V2.App_Code.DataAccessLayer;
using FD_PaymentReconciliation_V2.BusinessObject;
using FD_PaymentReconciliation_V2.Services;
using Impersonate;
//using Impersonate;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading.Tasks;

namespace FD_PaymentReconciliation_V2.App_Code.BusinessLayer
{
    public class ReconciliationProcessBAL
    {
        ReconciliationProcessDAL objDAL = null;
        public string Proccess_Reco(ReconciliationProcessBO objBO, SessionBO objSBO, string Mode)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                string res = string.Empty;
                string fileLocation = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + objBO.FileName;
                objBO.FilePath = fileLocation;
                string Extn = Path.GetExtension(objBO.FileName);
                ImpersonateUser _imp = new ImpersonateUser();
                string nasUserId = Startup.Configuration["FS_key:DocUserID"];
                string nasPassword = Startup.Configuration["FS_key:DocPWD"];

                if (Extn.ToUpper() == ".CSV")
                {
                    var connString = string.Format(
                    @"Provider=Microsoft.ACE.OleDb.12.0; Data Source={0};Extended Properties=""Text;HDR=YES;FMT=Delimited""",
                    Path.GetDirectoryName(fileLocation)
                );
                    using (var conn = new OleDbConnection(connString))
                    {

                        dt.Locale = CultureInfo.CurrentCulture;
                        conn.Open();
                        OleDbCommand command = new OleDbCommand("SELECT * FROM [" + Path.GetFileName(fileLocation) + "]", conn);

                        OleDbDataReader dbReader = command.ExecuteReader();

                        for (int i = 0; i < dbReader.FieldCount; i++)
                        {

                            dt.Columns.Add(dbReader.GetName(i));
                        }
                        while (dbReader.Read())
                        {
                            DataRow dr = dt.NewRow();
                            for (int i = 0; i < dbReader.VisibleFieldCount; i++)
                            {
                                dr[i] = dbReader.GetValue(i).ToString().Trim();
                            }

                            dt.Rows.Add(dr);
                        }

                        //dt = dt.Rows
                        //    .Cast<DataRow>()
                        //    .Where(row => !row.ItemArray.All(f => f is DBNull))
                        //    .CopyToDataTable();
                        //using (Stream inputStream = File.OpenRead(fileLocation))
                        //{
                        //    using (ExcelEngine excelEngine = new ExcelEngine())
                        //    {
                        //        IApplication application = excelEngine.Excel;
                        //        IWorkbook workbook = application.Workbooks.Open(inputStream);
                        //        IWorksheet worksheet = workbook.Worksheets[0];

                        //        dt = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);

                        //    }
                        //}


                        if (dt != null && dt.Rows.Count > 0)
                        {
                            objBO.Mode = Mode;
                            if (Mode == "CMS")
                            {
                                res = CMS_RecoProcess(dt, objBO, objSBO);
                            }
                        }
                        else
                        {
                            res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No data found to upload..!" });
                        }
                    }
                }
                else
                {

                    OleDbConnection OleDbcnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'");
                    OleDbCommand oledbCmd;
                    OleDbcnn.Open();
                    objBO.FilePath = fileLocation;
                    System.Data.DataTable objSheetNames = OleDbcnn.GetSchema("Tables");
                    string SheetName = "";
                    String[] excelSheetNames = new String[objSheetNames.Rows.Count];
                    for (int i = 0; i < objSheetNames.Rows.Count; i++)
                    {
                        var row = objSheetNames.Rows[i];
                        excelSheetNames[i] = row["TABLE_NAME"].ToString();

                        if ((excelSheetNames[i].Contains("_FilterDatabase")))
                        {
                            objSheetNames.Rows[i].Delete();
                            objSheetNames.AcceptChanges();
                        }

                        i++;
                    }



                    if (objSheetNames != null && objSheetNames.Rows.Count > 0)
                    {
                        SheetName = Convert.ToString(objSheetNames.Rows[0][2]);
                    }
                    string ExcelSheetName = Convert.ToString(OleDbcnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0].ItemArray[2]).Contains("_FilterDatabase") ? Convert.ToString(OleDbcnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[1].ItemArray[2]).ToLower() : Convert.ToString(OleDbcnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0].ItemArray[2]).ToLower();
                    if (ExcelSheetName.ToLower() == SheetName.ToLower())
                    {
                        //System.Data.DataTable dt = new System.Data.DataTable();
                        System.Data.DataTable dtException = new System.Data.DataTable();
                        oledbCmd = new OleDbCommand("SELECT * FROM [" + SheetName + "]", OleDbcnn);
                        OleDbDataAdapter oledbDA = new OleDbDataAdapter(oledbCmd);
                        oledbDA.Fill(dt);

                        try
                        {
                            dt = dt.Rows.Cast<DataRow>().
        Where(row => !row.ItemArray.All(field => field is System.DBNull ||
              string.Compare((field as string).Trim(), string.Empty) == 0)).CopyToDataTable();
                        }
                        catch (Exception ex)
                        {

                        }
                        //var columns = dt.Columns.Cast<DataColumn>().ToArray();

                        //foreach (var col in columns)
                        //{
                        //    // check column values for null 
                        //    if (dt.AsEnumerable().All(dr => dr.IsNull(col)))
                        //    {
                        //        // remove all null value columns 
                        //        dt.Columns.Remove(col);
                        //    }

                        //}

                        //using (Stream inputStream = File.OpenRead(fileLocation))
                        //{
                        //    using (ExcelEngine excelEngine = new ExcelEngine())
                        //    {
                        //        IApplication application = excelEngine.Excel;
                        //        IWorkbook workbook = application.Workbooks.Open(inputStream);
                        //        IWorksheet worksheet = workbook.Worksheets[0];

                        //        dt = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);

                        //    }
                        //}

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            objBO.Mode = Mode;
                            if (Mode == "Online")
                            {
                                res = Online_RecoProcess(dt, objBO, objSBO);
                            }
                            else if (Mode == "Offline")
                            {
                                res = Offline_RecoProcess(dt, objBO, objSBO);
                            }
                            else if (Mode == "CMS")
                            {
                                res = CMS_RecoProcess(dt, objBO, objSBO);
                            }
                            else if (Mode == "CMS_Collection")
                            {
                                res = CMS_Collection_RecoProcess(dt, objBO, objSBO);
                            }
                            else if (Mode == "UPI")
                            {
                                res = UPI_RecoProcess(dt, objBO, objSBO);
                            }
                            else if (Mode == "RTGS")
                            {
                                res = RTGS_RecoProcess(dt, objBO, objSBO);
                            }
                            else if (Mode == "HDFCSoftFeed")
                            {
                                res = HDFC_Soft_Feed_RecoProcess(dt, objBO, objSBO);
                            }
                            else if (Mode == "BankRevalidation")
                            {
                                if (objBO.TemplateType == "DD_PAID_UNPAID_STATUS")
                                    res = BankRevalidation_DDPaidUnpaid_RecoProcess(dt, objBO, objSBO);
                                else if (objBO.TemplateType == "NACH_SOFT_PAYMENT")
                                    res = BankRevalidation_NACHSOFTPayment_RecoProcess(dt, objBO, objSBO);
                                else if (objBO.TemplateType == "WARRANT_PAID_UNPAID_STATUS")
                                    res = BankRevalidation_WarrantPaidUnpaid_RecoProcess(dt, objBO, objSBO);
                                else if (objBO.TemplateType == "NEFT_REJECTION")
                                    res = BankRevalidation_NEFTRejection_RecoProcess(dt, objBO, objSBO);
                                else if (objBO.TemplateType == "DD_ISSUED_FOR_ACH_REJECT")
                                    res = BankRevalidation_DD_ACH_REJECT_RecoProcess(dt, objBO, objSBO);
                            }
                            else if (Mode == "BankRevalComm")
                            {
                                res = BankRevalWrappCommProcess(dt, objBO, objSBO);
                            }
                        }
                        else
                        {
                            res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No data found to upload..!" });
                        }
                    }
                    else
                    {
                        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Sheet name is not valid. Please keep it as template file..!" });
                    }

                }
                //});
                return res;
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogExceptionAsync(ex);
                return JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Something went wrong!" });
                //throw ex;
            }

        }

        //public string ProcessBulkFile(ReconciliationProcessBO objBO, SessionBO objSBO, string Mode)
        //{
        //    try
        //    {
        //        System.Data.DataTable dt = new System.Data.DataTable();
        //        string res = string.Empty;
        //        string fileLocation = Startup.Configuration["AppSettings:RecoUploadPath"].ToString() + objBO.FileName;
        //        objBO.FilePath = fileLocation;

        //        res = BankRevalidation_Bulk_RecoProcess(dt, objBO, objSBO);



        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionUtility.LogExceptionAsync(ex);
        //        return JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Something went wrong!" });
        //        //throw ex;
        //    }

        //}

        #region Not_to_use
        //Reward Portal
        //private string Reward_Online_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        //{
        //    string res = "";

        //    string[] arr = new string[11];
        //    arr[0] = "SRNO";
        //    arr[1] = "BANK_ID";
        //    arr[2] = "BANK_NAME";
        //    arr[3] = "TRAN_ID";
        //    arr[4] = "SRC_PRN";
        //    arr[5] = "SRC_AMT";
        //    arr[6] = "TXN_DATE";
        //    arr[7] = "TXN_TIME";
        //    arr[8] = "PAYMENT_DATE";
        //    arr[9] = "SAPCODE";
        //    arr[10] = "EMPNAME";

        //    if (dt.Columns.Count == arr.Count())
        //    {

        //        for (int i = 0; i < arr.Count(); i++)
        //        {
        //            if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
        //            {
        //                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
        //    }

        //    if (res == "")
        //    {
        //        objDAL = new ReconciliationProcessDAL();
        //        using (DataSet ds = objDAL.SaveData_Reward_Online(dt, objBO, objSBO))
        //        {
        //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                string status = ds.Tables[0].Rows[0][0].ToString();
        //                string msg = ds.Tables[0].Rows[0][1].ToString();

        //                if (status == "1")
        //                {
        //                    res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
        //                }
        //                else
        //                {
        //                    throw new Exception(msg);
        //                }
        //            }
        //        }
        //    }

        //    return res;
        //}
        //private string Reward_Offline_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        //{
        //    string res = "";

        //    string[] arr = new string[11];
        //    arr[0] = "SRNO";
        //    arr[1] = "BANK_ID";
        //    arr[2] = "BANK_NAME";
        //    arr[3] = "TRAN_ID";
        //    arr[4] = "SRC_PRN";
        //    arr[5] = "SRC_AMT";
        //    arr[6] = "TXN_DATE";
        //    arr[7] = "TXN_TIME";
        //    arr[8] = "PAYMENT_DATE";
        //    arr[9] = "SAPCODE";
        //    arr[10] = "EMPNAME";

        //    if (dt.Columns.Count == arr.Count())
        //    {

        //        for (int i = 0; i < arr.Count(); i++)
        //        {
        //            if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
        //            {
        //                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
        //    }

        //    if (res == "")
        //    {
        //        objDAL = new ReconciliationProcessDAL();
        //        using (DataSet ds = objDAL.SaveData_Reward_Offline(dt, objBO, objSBO))
        //        {
        //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                string status = ds.Tables[0].Rows[0][0].ToString();
        //                string msg = ds.Tables[0].Rows[0][1].ToString();

        //                if (status == "1")
        //                {
        //                    res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
        //                }
        //                else
        //                {
        //                    throw new Exception(msg);
        //                }
        //            }
        //        }
        //    }

        //    return res;
        //}

        //QB Portal
        //private string QB_Online_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        //{
        //    string res = "";

        //    string[] arr = new string[16];
        //    arr[0] = "SRNO";
        //    arr[1] = "BANK_ID";
        //    arr[2] = "BANK_NAME";
        //    arr[3] = "TRAN_ID";
        //    arr[4] = "SRC_PRN";
        //    arr[5] = "SRC_AMT";
        //    arr[6] = "TOTCOMM";
        //    arr[7] = "SERVICE_TAX";
        //    arr[8] = "AMT_GIVE_TO_SUBMER";
        //    arr[9] = "TXN_DATE";
        //    arr[10] = "TXN_TIME";
        //    arr[11] = "PAYMENT_DATE";
        //    arr[12] = "SRC_ITC";
        //    arr[13] = "STATUS_DESC";
        //    arr[14] = "SAPCODE";
        //    arr[15] = "EMPNAME";

        //    if (dt.Columns.Count == arr.Count())
        //    {

        //        for (int i = 0; i < arr.Count(); i++)
        //        {
        //            if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
        //            {
        //                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
        //    }

        //    if (res == "")
        //    {
        //        objDAL = new ReconciliationProcessDAL();
        //        using (DataSet ds = objDAL.SaveData_QB_Online(dt, objBO, objSBO))
        //        {
        //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                string status = ds.Tables[0].Rows[0][0].ToString();
        //                string msg = ds.Tables[0].Rows[0][1].ToString();

        //                if (status == "1")
        //                {
        //                    res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
        //                }
        //                else
        //                {
        //                    throw new Exception(msg);
        //                }
        //            }
        //        }
        //    }

        //    return res;
        //}
        //private string QB_Offline_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        //{
        //    string res = "";

        //    string[] arr = new string[11];
        //    arr[0] = "SRNO";
        //    arr[1] = "BANK_ID";
        //    arr[2] = "BANK_NAME";
        //    arr[3] = "TRAN_ID";
        //    arr[4] = "SRC_PRN";
        //    arr[5] = "SRC_AMT";
        //    arr[6] = "TXN_DATE";
        //    arr[7] = "TXN_TIME";
        //    arr[8] = "PAYMENT_DATE";
        //    arr[9] = "SAPCODE";
        //    arr[10] = "EMPNAME";

        //    if (dt.Columns.Count == arr.Count())
        //    {

        //        for (int i = 0; i < arr.Count(); i++)
        //        {
        //            if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
        //            {
        //                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
        //    }

        //    if (res == "")
        //    {
        //        objDAL = new ReconciliationProcessDAL();
        //        using (DataSet ds = objDAL.SaveData_QB_Offline(dt, objBO, objSBO))
        //        {
        //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                string status = ds.Tables[0].Rows[0][0].ToString();
        //                string msg = ds.Tables[0].Rows[0][1].ToString();

        //                if (status == "1")
        //                {
        //                    res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
        //                }
        //                else
        //                {
        //                    throw new Exception(msg);
        //                }
        //            }
        //        }
        //    }

        //    return res;
        //}

        //Enterprise Portal
        //private string Enterprise_Online_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        //{
        //    string res = "";

        //    string[] arr = new string[14];
        //    arr[0] = "SRNO";
        //    arr[1] = "BANK_ID";
        //    arr[2] = "BANK_NAME";
        //    arr[3] = "TRAN_ID";
        //    arr[4] = "SRC_PRN";
        //    arr[5] = "SRC_AMT";
        //    arr[6] = "TOTCOMM";
        //    arr[7] = "SERVICE_TAX";
        //    arr[8] = "AMT_GIVE_TO_SUBMER";
        //    arr[9] = "TXN_DATE";
        //    arr[10] = "TXN_TIME";
        //    arr[11] = "PAYMENT_DATE";
        //    arr[12] = "SRC_ITC";
        //    arr[13] = "STATUS_DESC";

        //    if (dt.Columns.Count == arr.Count())
        //    {

        //        for (int i = 0; i < arr.Count(); i++)
        //        {
        //            if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
        //            {
        //                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
        //    }

        //    if (res == "")
        //    {
        //        objDAL = new ReconciliationProcessDAL();
        //        using (DataSet ds = objDAL.SaveData_Enterprise_Online(dt, objBO, objSBO))
        //        {
        //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                string status = ds.Tables[0].Rows[0][0].ToString();
        //                string msg = ds.Tables[0].Rows[0][1].ToString();

        //                if (status == "1")
        //                {
        //                    res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
        //                }
        //                else
        //                {
        //                    throw new Exception(msg);
        //                }
        //            }
        //        }
        //    }

        //    return res;
        //}
        //private string Enterprise_Offline_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        //{
        //    string res = "";

        //    string[] arr = new string[11];
        //    arr[0] = "SRNO";
        //    arr[1] = "BANK_ID";
        //    arr[2] = "BANK_NAME";
        //    arr[3] = "TRAN_ID";
        //    arr[4] = "SRC_PRN";
        //    arr[5] = "SRC_AMT";
        //    arr[6] = "TXN_DATE";
        //    arr[7] = "TXN_TIME";
        //    arr[8] = "PAYMENT_DATE";
        //    arr[9] = "SAPCODE";
        //    arr[10] = "EMPNAME";

        //    if (dt.Columns.Count == arr.Count())
        //    {

        //        for (int i = 0; i < arr.Count(); i++)
        //        {
        //            if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
        //            {
        //                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
        //    }

        //    if (res == "")
        //    {
        //        objDAL = new ReconciliationProcessDAL();
        //        using (DataSet ds = objDAL.SaveData_Enterprise_Offline(dt, objBO, objSBO))
        //        {
        //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                string status = ds.Tables[0].Rows[0][0].ToString();
        //                string msg = ds.Tables[0].Rows[0][1].ToString();

        //                if (status == "1")
        //                {
        //                    res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
        //                }
        //                else
        //                {
        //                    throw new Exception(msg);
        //                }
        //            }
        //        }
        //    }

        //    return res;
        //}

        //BOTC Portal
        //private string BOTC_Online_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        //{
        //    string res = "";

        //    string[] arr = new string[16];
        //    arr[0] = "SRNO";
        //    arr[1] = "BANK_ID";
        //    arr[2] = "BANK_NAME";
        //    arr[3] = "TRAN_ID";
        //    arr[4] = "SRC_PRN";
        //    arr[5] = "SRC_AMT";
        //    arr[6] = "TOTCOMM";
        //    arr[7] = "SERVICE_TAX";
        //    arr[8] = "AMT_GIVE_TO_SUBMER";
        //    arr[9] = "TXN_DATE";
        //    arr[10] = "TXN_TIME";
        //    arr[11] = "PAYMENT_DATE";
        //    arr[12] = "SRC_ITC";
        //    arr[13] = "STATUS_DESC";
        //    arr[14] = "SAPCODE";
        //    arr[15] = "EMPNAME";

        //    if (dt.Columns.Count == arr.Count())
        //    {

        //        for (int i = 0; i < arr.Count(); i++)
        //        {
        //            if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
        //            {
        //                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
        //    }

        //    if (res == "")
        //    {
        //        objDAL = new ReconciliationProcessDAL();
        //        using (DataSet ds = objDAL.SaveData_BOTC_Online(dt, objBO, objSBO))
        //        {
        //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                string status = ds.Tables[0].Rows[0][0].ToString();
        //                string msg = ds.Tables[0].Rows[0][1].ToString();

        //                if (status == "1")
        //                {
        //                    res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
        //                }
        //                else
        //                {
        //                    throw new Exception(msg);
        //                }
        //            }
        //        }
        //    }

        //    return res;
        //}
        //private string BOTC_Offline_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        //{
        //    string res = "";

        //    string[] arr = new string[11];
        //    arr[0] = "SRNO";
        //    arr[1] = "BANK_ID";
        //    arr[2] = "BANK_NAME";
        //    arr[3] = "TRAN_ID";
        //    arr[4] = "SRC_PRN";
        //    arr[5] = "SRC_AMT";
        //    arr[6] = "TXN_DATE";
        //    arr[7] = "TXN_TIME";
        //    arr[8] = "PAYMENT_DATE";
        //    arr[9] = "SAPCODE";
        //    arr[10] = "EMPNAME";

        //    if (dt.Columns.Count == arr.Count())
        //    {

        //        for (int i = 0; i < arr.Count(); i++)
        //        {
        //            if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
        //            {
        //                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
        //    }

        //    if (res == "")
        //    {
        //        objDAL = new ReconciliationProcessDAL();
        //        using (DataSet ds = objDAL.SaveData_BOTC_Offline(dt, objBO, objSBO))
        //        {
        //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                string status = ds.Tables[0].Rows[0][0].ToString();
        //                string msg = ds.Tables[0].Rows[0][1].ToString();

        //                if (status == "1")
        //                {
        //                    res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
        //                }
        //                else
        //                {
        //                    throw new Exception(msg);
        //                }
        //            }
        //        }
        //    }

        //    return res;
        //}

        //BTP Portal
        //private string BTP_Online_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        //{
        //    string res = "";

        //    string[] arr = new string[16];
        //    arr[0] = "SRNO";
        //    arr[1] = "BANK_ID";
        //    arr[2] = "BANK_NAME";
        //    arr[3] = "TRAN_ID";
        //    arr[4] = "SRC_PRN";
        //    arr[5] = "SRC_AMT";
        //    arr[6] = "TOTCOMM";
        //    arr[7] = "SERVICE_TAX";
        //    arr[8] = "AMT_GIVE_TO_SUBMER";
        //    arr[9] = "TXN_DATE";
        //    arr[10] = "TXN_TIME";
        //    arr[11] = "PAYMENT_DATE";
        //    arr[12] = "SRC_ITC";
        //    arr[13] = "STATUS_DESC";
        //    arr[14] = "SAPCODE";
        //    arr[15] = "EMPNAME";

        //    if (dt.Columns.Count == arr.Count())
        //    {

        //        for (int i = 0; i < arr.Count(); i++)
        //        {
        //            if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
        //            {
        //                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
        //    }

        //    if (res == "")
        //    {
        //        objDAL = new ReconciliationProcessDAL();
        //        using (DataSet ds = objDAL.SaveData_BTP_Online(dt, objBO, objSBO))
        //        {
        //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                string status = ds.Tables[0].Rows[0][0].ToString();
        //                string msg = ds.Tables[0].Rows[0][1].ToString();

        //                if (status == "1")
        //                {
        //                    res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
        //                }
        //                else
        //                {
        //                    throw new Exception(msg);
        //                }
        //            }
        //        }
        //    }

        //    return res;
        //}
        //private string BTP_Offline_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        //{
        //    string res = "";

        //    string[] arr = new string[11];
        //    arr[0] = "SRNO";
        //    arr[1] = "BANK_ID";
        //    arr[2] = "BANK_NAME";
        //    arr[3] = "TRAN_ID";
        //    arr[4] = "SRC_PRN";
        //    arr[5] = "SRC_AMT";
        //    arr[6] = "TXN_DATE";
        //    arr[7] = "TXN_TIME";
        //    arr[8] = "PAYMENT_DATE";
        //    arr[9] = "SAPCODE";
        //    arr[10] = "EMPNAME";

        //    if (dt.Columns.Count == arr.Count())
        //    {

        //        for (int i = 0; i < arr.Count(); i++)
        //        {
        //            if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
        //            {
        //                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
        //    }

        //    if (res == "")
        //    {
        //        objDAL = new ReconciliationProcessDAL();
        //        using (DataSet ds = objDAL.SaveData_BTP_Offline(dt, objBO, objSBO))
        //        {
        //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                string status = ds.Tables[0].Rows[0][0].ToString();
        //                string msg = ds.Tables[0].Rows[0][1].ToString();

        //                if (status == "1")
        //                {
        //                    res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
        //                }
        //                else
        //                {
        //                    throw new Exception(msg);
        //                }
        //            }
        //        }
        //    }

        //    return res;
        //}

        //BTP-CP Portal
        //private string BTPCP_Online_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        //{
        //    string res = "";

        //    string[] arr = new string[16];
        //    arr[0] = "SRNO";
        //    arr[1] = "BANK_ID";
        //    arr[2] = "BANK_NAME";
        //    arr[3] = "TRAN_ID";
        //    arr[4] = "SRC_PRN";
        //    arr[5] = "SRC_AMT";
        //    arr[6] = "TOTCOMM";
        //    arr[7] = "SERVICE_TAX";
        //    arr[8] = "AMT_GIVE_TO_SUBMER";
        //    arr[9] = "TXN_DATE";
        //    arr[10] = "TXN_TIME";
        //    arr[11] = "PAYMENT_DATE";
        //    arr[12] = "SRC_ITC";
        //    arr[13] = "STATUS_DESC";
        //    arr[14] = "SAPCODE";
        //    arr[15] = "EMPNAME";

        //    if (dt.Columns.Count == arr.Count())
        //    {

        //        for (int i = 0; i < arr.Count(); i++)
        //        {
        //            if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
        //            {
        //                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
        //    }

        //    if (res == "")
        //    {
        //        objDAL = new ReconciliationProcessDAL();
        //        using (DataSet ds = objDAL.SaveData_BTPCP_Online(dt, objBO, objSBO))
        //        {
        //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                string status = ds.Tables[0].Rows[0][0].ToString();
        //                string msg = ds.Tables[0].Rows[0][1].ToString();

        //                if (status == "1")
        //                {
        //                    res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
        //                }
        //                else
        //                {
        //                    throw new Exception(msg);
        //                }
        //            }
        //        }
        //    }

        //    return res;
        //}
        //private string BTPCP_Offline_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        //{
        //    string res = "";

        //    string[] arr = new string[11];
        //    arr[0] = "SRNO";
        //    arr[1] = "BANK_ID";
        //    arr[2] = "BANK_NAME";
        //    arr[3] = "TRAN_ID";
        //    arr[4] = "SRC_PRN";
        //    arr[5] = "SRC_AMT";
        //    arr[6] = "TXN_DATE";
        //    arr[7] = "TXN_TIME";
        //    arr[8] = "PAYMENT_DATE";
        //    arr[9] = "SAPCODE";
        //    arr[10] = "EMPNAME";

        //    if (dt.Columns.Count == arr.Count())
        //    {

        //        for (int i = 0; i < arr.Count(); i++)
        //        {
        //            if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
        //            {
        //                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
        //    }

        //    if (res == "")
        //    {
        //        objDAL = new ReconciliationProcessDAL();
        //        using (DataSet ds = objDAL.SaveData_BTPCP_Offline(dt, objBO, objSBO))
        //        {
        //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                string status = ds.Tables[0].Rows[0][0].ToString();
        //                string msg = ds.Tables[0].Rows[0][1].ToString();

        //                if (status == "1")
        //                {
        //                    res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
        //                }
        //                else
        //                {
        //                    throw new Exception(msg);
        //                }
        //            }
        //        }
        //    }

        //    return res;
        //}

        //CP Portal
        //private string CP_Online_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        //{
        //    string res = "";

        //    string[] arr = new string[16];
        //    arr[0] = "SRNO";
        //    arr[1] = "BANK_ID";
        //    arr[2] = "BANK_NAME";
        //    arr[3] = "TRAN_ID";
        //    arr[4] = "SRC_PRN";
        //    arr[5] = "SRC_AMT";
        //    arr[6] = "TOTCOMM";
        //    arr[7] = "SERVICE_TAX";
        //    arr[8] = "AMT_GIVE_TO_SUBMER";
        //    arr[9] = "TXN_DATE";
        //    arr[10] = "TXN_TIME";
        //    arr[11] = "PAYMENT_DATE";
        //    arr[12] = "SRC_ITC";
        //    arr[13] = "STATUS_DESC";
        //    arr[14] = "SAPCODE";
        //    arr[15] = "EMPNAME";

        //    if (dt.Columns.Count == arr.Count())
        //    {

        //        for (int i = 0; i < arr.Count(); i++)
        //        {
        //            if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
        //            {
        //                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
        //    }

        //    if (res == "")
        //    {
        //        objDAL = new ReconciliationProcessDAL();
        //        using (DataSet ds = objDAL.SaveData_CP_Online(dt, objBO, objSBO))
        //        {
        //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                string status = ds.Tables[0].Rows[0][0].ToString();
        //                string msg = ds.Tables[0].Rows[0][1].ToString();

        //                if (status == "1")
        //                {
        //                    res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
        //                }
        //                else
        //                {
        //                    throw new Exception(msg);
        //                }
        //            }
        //        }
        //    }

        //    return res;
        //}

        //private string CP_Offline_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        //{
        //    string res = "";

        //    string[] arr = new string[11];
        //    arr[0] = "SRNO";
        //    arr[1] = "BANK_ID";
        //    arr[2] = "BANK_NAME";
        //    arr[3] = "TRAN_ID";
        //    arr[4] = "SRC_PRN";
        //    arr[5] = "SRC_AMT";
        //    arr[6] = "TXN_DATE";
        //    arr[7] = "TXN_TIME";
        //    arr[8] = "PAYMENT_DATE";
        //    arr[9] = "SAPCODE";
        //    arr[10] = "EMPNAME";

        //    if (dt.Columns.Count == arr.Count())
        //    {

        //        for (int i = 0; i < arr.Count(); i++)
        //        {
        //            if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
        //            {
        //                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
        //    }

        //    if (res == "")
        //    {
        //        objDAL = new ReconciliationProcessDAL();
        //        using (DataSet ds = objDAL.SaveData_CP_Offline(dt, objBO, objSBO))
        //        {
        //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                string status = ds.Tables[0].Rows[0][0].ToString();
        //                string msg = ds.Tables[0].Rows[0][1].ToString();

        //                if (status == "1")
        //                {
        //                    res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
        //                }
        //                else
        //                {
        //                    throw new Exception(msg);
        //                }
        //            }
        //        }
        //    }

        //    return res;
        //}

        //CPLCA Portal
        //private string CPLCA_Online_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        //{
        //    string res = "";

        //    string[] arr = new string[16];
        //    arr[0] = "SRNO";
        //    arr[1] = "BANK_ID";
        //    arr[2] = "BANK_NAME";
        //    arr[3] = "TRAN_ID";
        //    arr[4] = "SRC_PRN";
        //    arr[5] = "SRC_AMT";
        //    arr[6] = "TOTCOMM";
        //    arr[7] = "SERVICE_TAX";
        //    arr[8] = "AMT_GIVE_TO_SUBMER";
        //    arr[9] = "TXN_DATE";
        //    arr[10] = "TXN_TIME";
        //    arr[11] = "PAYMENT_DATE";
        //    arr[12] = "SRC_ITC";
        //    arr[13] = "STATUS_DESC";
        //    arr[14] = "SAPCODE";
        //    arr[15] = "EMPNAME";

        //    if (dt.Columns.Count == arr.Count())
        //    {

        //        for (int i = 0; i < arr.Count(); i++)
        //        {
        //            if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
        //            {
        //                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
        //    }

        //    if (res == "")
        //    {
        //        objDAL = new ReconciliationProcessDAL();
        //        using (DataSet ds = objDAL.SaveData_CPLCA_Online(dt, objBO, objSBO))
        //        {
        //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                string status = ds.Tables[0].Rows[0][0].ToString();
        //                string msg = ds.Tables[0].Rows[0][1].ToString();

        //                if (status == "1")
        //                {
        //                    res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
        //                }
        //                else
        //                {
        //                    throw new Exception(msg);
        //                }
        //            }
        //        }
        //    }

        //    return res;
        //}
        //private string CPLCA_Offline_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        //{
        //    string res = "";

        //    string[] arr = new string[11];
        //    arr[0] = "SRNO";
        //    arr[1] = "BANK_ID";
        //    arr[2] = "BANK_NAME";
        //    arr[3] = "TRAN_ID";
        //    arr[4] = "SRC_PRN";
        //    arr[5] = "SRC_AMT";
        //    arr[6] = "TXN_DATE";
        //    arr[7] = "TXN_TIME";
        //    arr[8] = "PAYMENT_DATE";
        //    arr[9] = "SAPCODE";
        //    arr[10] = "EMPNAME";

        //    if (dt.Columns.Count == arr.Count())
        //    {

        //        for (int i = 0; i < arr.Count(); i++)
        //        {
        //            if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
        //            {
        //                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
        //    }

        //    if (res == "")
        //    {
        //        objDAL = new ReconciliationProcessDAL();
        //        using (DataSet ds = objDAL.SaveData_CPLCA_Offline(dt, objBO, objSBO))
        //        {
        //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                string status = ds.Tables[0].Rows[0][0].ToString();
        //                string msg = ds.Tables[0].Rows[0][1].ToString();

        //                if (status == "1")
        //                {
        //                    res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
        //                }
        //                else
        //                {
        //                    throw new Exception(msg);
        //                }
        //            }
        //        }
        //    }

        //    return res;
        //}
        #endregion
        private string Online_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            //CultureInfo myCI = new CultureInfo("en-US", false);
            //string data = DateTime.ParseExact("23/01/2019", "dd/MM/YYYY", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
            //return "hello";

            string res = "";
            string[] arr = null;
            if (objBO.TemplateType.ToUpper() == "BILL DESK")
            {

                arr = new string[19];
                arr[0] = "Biller Id";
                arr[1] = "Bank Id";
                arr[2] = "Bank Ref# No#";
                arr[3] = "PGI Ref# No#";
                arr[4] = "Ref# 1";
                arr[5] = "Ref# 2";
                arr[6] = "Ref# 3";
                arr[7] = "Ref# 4";
                arr[8] = "Ref# 5";
                arr[9] = "Ref# 6";
                arr[10] = "Ref# 7";
                arr[11] = "Ref# 8";
                arr[12] = "Filler";
                arr[13] = "Date of Txn";
                arr[14] = "Settlement Date";
                arr[15] = "Gross Amount(Rs#Ps)";
                arr[16] = "Charges (Rs#Ps)";
                arr[17] = "GST (Rs Ps)";
                arr[18] = "Net Amount(Rs#Ps)";
            }
            else if (objBO.TemplateType == "TPSL")
            {
                arr = new string[14];
                arr[0] = "SRNO";
                arr[1] = "BANK_ID";
                arr[2] = "BANK_NAME";
                arr[3] = "TRAN_ID";
                arr[4] = "SRC_PRN";
                arr[5] = "SRC_AMT";
                arr[6] = "TOTCOMM";
                arr[7] = "SERVICE_TAX";
                arr[8] = "AMT_GIVE_TO_SUBMER";
                arr[9] = "TXN_DATE";
                arr[10] = "TXN_TIME";
                arr[11] = "PAYMENT_DATE";
                arr[12] = "SRC_ITC";
                arr[13] = "STATUS_DESC";
                //arr[14] = "SAPCODE";
                //arr[15] = "EMPNAME";
            }
            else if (objBO.TemplateType == "PAYU")
            {
                arr = new string[14];
                arr[0] = "Merchant_Id";
                arr[1] = "Date of Transaction";
                arr[2] = "PayU ID";
                arr[3] = "Customer Name";
                arr[4] = "Amount";
                arr[5] = "Requested Action";
                arr[6] = "Status";
                arr[7] = "BankName";
                arr[8] = "Transaction Id";
                arr[9] = "Payment Gateway";
                arr[10] = "Bank Reference No";
                arr[11] = "Settlement Date";
                arr[12] = "Settlement UTR";
                arr[13] = "UDF 1";
            }
            else if (objBO.TemplateType.ToUpper() == "CAMS")
            {
                //Below Code is added by Satish Pawar on 01 July 2022
                //Template Check columns
                arr = new string[40];
                arr[0] = "CP Merchant ID";
                arr[1] = "Subbiller ID";
                arr[2] = "Company Name";
                arr[3] = "DBA Name";
                arr[4] = "Customer ID";
                arr[5] = "Customer Name";
                arr[6] = "Mobile 1";
                arr[7] = "Mobile 2";
                arr[8] = "SMS Status";
                arr[9] = "No# of Clicks";
                arr[10] = "Trans Ref no";
                arr[11] = "CP Ref no";
                arr[12] = "Customer Ref No";
                arr[13] = "Bank Ref No";
                arr[14] = "Payment Type";
                arr[15] = "Bank Name";
                arr[16] = "Status";
                arr[17] = "Status Description";
                arr[18] = "Trans Amount";
                arr[19] = "Transaction Date";
                arr[20] = "Transaction Remarks";
                arr[21] = "Npci Code";
                arr[22] = "Payer Virtual Address";
                arr[23] = "UMN";
                arr[24] = "Payer A/C No";
                arr[25] = "Payer A/C Name";
                arr[26] = "Payer IFSC Code";
                arr[27] = "Payee VPA";
                arr[28] = "Language";
                arr[29] = "Service Branch";
                arr[30] = "Region";
                arr[31] = "PRDCTY";
                arr[32] = "Reg A/C No";
                arr[33] = "Payout Status";
                arr[34] = "Payout Ref No";
                arr[35] = "Device Type";
                arr[36] = "Settlement Type";
                arr[37] = "Settlement Account";
                arr[38] = "Settlement Bank";
                arr[39] = "Mandate Ref No";

            }
            //Below Code is added by Satish Pawar on 11 Nov 2022
            else if (objBO.TemplateType.ToUpper() == "CAMS LAFD")
            {
                //Below Code is added by Satish Pawar on 01 July 2022
                //Template Check columns
                arr = new string[40];
                arr[0] = "CP Merchant ID";
                arr[1] = "Subbiller ID";
                arr[2] = "Company Name";
                arr[3] = "DBA Name";
                arr[4] = "Customer ID";
                arr[5] = "Customer Name";
                arr[6] = "Mobile 1";
                arr[7] = "Mobile 2";
                arr[8] = "SMS Status";
                arr[9] = "No# of Clicks";
                arr[10] = "Trans Ref no";
                arr[11] = "CP Ref no";
                arr[12] = "Customer Ref No";
                arr[13] = "Bank Ref No";
                arr[14] = "Payment Type";
                arr[15] = "Bank Name";
                arr[16] = "Status";
                arr[17] = "Status Description";
                arr[18] = "Trans Amount";
                arr[19] = "Transaction Date";
                arr[20] = "Transaction Remarks";
                arr[21] = "Npci Code";
                arr[22] = "Payer Virtual Address";
                arr[23] = "UMN";
                arr[24] = "Payer A/C No";
                arr[25] = "Payer A/C Name";
                arr[26] = "Payer IFSC Code";
                arr[27] = "Payee VPA";
                arr[28] = "Language";
                arr[29] = "Service Branch";
                arr[30] = "Region";
                arr[31] = "PRDCTY";
                arr[32] = "Reg A/C No";
                arr[33] = "Payout Status";
                arr[34] = "Payout Ref No";
                arr[35] = "Device Type";
                arr[36] = "Settlement Type";
                arr[37] = "Settlement Account";
                arr[38] = "Settlement Bank";
                arr[39] = "Mandate Ref No";

            }
            if (dt.Columns.Count == arr.Count())
            {

                for (int i = 0; i < arr.Count(); i++)
                {
                    if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
                    {
                        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
                        break;
                    }
                }
            }
            else
            {
                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
            }

            if (res == "")
            {
                objDAL = new ReconciliationProcessDAL();

                System.Data.DataTable dtMain = new System.Data.DataTable();

                dtMain.Columns.Add("SrNo");
                dtMain.Columns.Add("BANK_ID");
                dtMain.Columns.Add("BANK_NAME");
                dtMain.Columns.Add("TRAN_ID");
                dtMain.Columns.Add("SRC_PRN");
                dtMain.Columns.Add("SRC_AMT");
                dtMain.Columns.Add("TOTCOMM");
                dtMain.Columns.Add("SERVICE_TAX");
                dtMain.Columns.Add("AMT_GIVE_TO_SUBMER");
                dtMain.Columns.Add("TXN_DATE");
                dtMain.Columns.Add("TXN_TIME");
                dtMain.Columns.Add("PAYMENT_DATE");
                dtMain.Columns.Add("SRC_ITC");
                dtMain.Columns.Add("STATUS_DESC");
                dtMain.Columns.Add("SAPCODE");
                dtMain.Columns.Add("EMPNAME");
                dtMain.Columns.Add("BILLER_ID");
                dtMain.Columns.Add("PGI_REF_NO");
                dtMain.Columns.Add("REF_2");
                dtMain.Columns.Add("REF_3");
                dtMain.Columns.Add("REF_4");
                dtMain.Columns.Add("REF_5");
                dtMain.Columns.Add("REF_6");
                dtMain.Columns.Add("REF_7");
                dtMain.Columns.Add("REF_8");
                dtMain.Columns.Add("REF_9");
                dtMain.Columns.Add("REF_10");
                dtMain.Columns.Add("REF_11");
                dtMain.Columns.Add("FILLER");
                dtMain.Columns.Add("CHARGES");
                dtMain.Columns.Add("GST");
                dtMain.Columns.Add("NET_AMOUNT");
                dtMain.Columns.Add("Portal_Name");
                //Below Code added by Satish Pawar on 05 July 2022  
                //Code start
                dtMain.Columns.Add("SUB_BILLER_ID");
                dtMain.Columns.Add("COMPANY_NAME");
                dtMain.Columns.Add("DBA_NAME");
                dtMain.Columns.Add("CUST_REF_NO");
                dtMain.Columns.Add("PAYMENT_TYPE");
                dtMain.Columns.Add("STATUS");
                dtMain.Columns.Add("TRANS_REMARKS");
                dtMain.Columns.Add("PAYER_VIRTUAL_ADDRESS");
                dtMain.Columns.Add("PAYER_ACCOUNT_NO");
                dtMain.Columns.Add("PAYER_ACCOUNT_NAME");
                dtMain.Columns.Add("PAYER_IFSC_CODE");
                dtMain.Columns.Add("PAYEE_VPA");
                dtMain.Columns.Add("SERVICE_BRANCH");
                dtMain.Columns.Add("REGION");
                dtMain.Columns.Add("PRDCTY");
                dtMain.Columns.Add("REG_ACCOUNT_NO");
                dtMain.Columns.Add("PAYOUT_STATUS");
                dtMain.Columns.Add("PAYOUT_REF_NO");
                dtMain.Columns.Add("SETTLEMENT_TYPE");
                dtMain.Columns.Add("SETTLEMENT_ACCOUNT");
                dtMain.Columns.Add("SETTLEMENT_BANK");
                dtMain.Columns.Add("MANDATE_REF_NO");
                //Code End

                if (objBO.TemplateType.ToUpper() == "BILL DESK")
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow dtRow = dtMain.NewRow();
                        //string[] txnDate = Convert.ToString(dr["Date of Txn"]).Split(' ');
                        dtRow["SrNo"] = null;
                        dtRow["BANK_ID"] = dr["Bank Id"];
                        dtRow["BANK_NAME"] = null;
                        dtRow["TRAN_ID"] = dr["Bank Ref# No#"];
                        dtRow["SRC_PRN"] = dr["Ref# 1"];
                        dtRow["SRC_AMT"] = dr["Gross Amount(Rs#Ps)"];
                        dtRow["TOTCOMM"] = null;
                        dtRow["SERVICE_TAX"] = null;
                        dtRow["AMT_GIVE_TO_SUBMER"] = null;
                        //if (txnDate.Length > 0)
                        //{
                        //    dtRow["TXN_DATE"] = txnDate[0];
                        //}
                        //else
                        //{
                        //    dtRow["TXN_DATE"] = string.Empty;
                        //}
                        dtRow["TXN_DATE"] = dr["Date of Txn"];// Convert.ToDateTime(dr["TXN_DATE"]).ToString("dd-MMM-yyyy");
                        dtRow["TXN_TIME"] = null;

                        dtRow["PAYMENT_DATE"] = dr["Settlement Date"];// Convert.ToDateTime(dr["SETTLEMENT_DATE"]).ToString("dd-MMM-yyyy");
                        dtRow["SRC_ITC"] = null;
                        dtRow["STATUS_DESC"] = null;
                        dtRow["SAPCODE"] = null;
                        dtRow["EMPNAME"] = null;
                        dtRow["BILLER_ID"] = dr["Biller Id"];
                        dtRow["PGI_REF_NO"] = dr["PGI Ref# No#"];
                        dtRow["REF_2"] = dr["Ref# 2"];
                        dtRow["REF_3"] = dr["Ref# 3"];
                        dtRow["REF_4"] = dr["Ref# 4"];
                        dtRow["REF_5"] = dr["Ref# 5"];
                        dtRow["REF_6"] = dr["Ref# 6"];
                        dtRow["REF_7"] = dr["Ref# 7"];
                        dtRow["REF_8"] = dr["Ref# 8"];
                        dtRow["REF_9"] = string.Empty;
                        dtRow["REF_10"] = string.Empty;
                        dtRow["REF_11"] = string.Empty;
                        dtRow["FILLER"] = dr["Filler"];
                        dtRow["CHARGES"] = dr["Charges (Rs#Ps)"];
                        dtRow["GST"] = dr["GST (Rs Ps)"];
                        dtRow["NET_AMOUNT"] = dr["Net Amount(Rs#Ps)"];
                        dtRow["Portal_Name"] = string.Empty;

                        //Code added by Satish Pawar on 06 July 2022

                        dtRow["SUB_BILLER_ID"] = string.Empty;
                        dtRow["COMPANY_NAME"] = string.Empty;
                        dtRow["DBA_NAME"] = string.Empty;
                        dtRow["CUST_REF_NO"] = string.Empty;
                        dtRow["PAYMENT_TYPE"] = string.Empty;
                        dtRow["STATUS"] = string.Empty;
                        dtRow["TRANS_REMARKS"] = string.Empty;
                        dtRow["PAYER_VIRTUAL_ADDRESS"] = string.Empty;
                        dtRow["PAYER_ACCOUNT_NO"] = string.Empty;
                        dtRow["PAYER_ACCOUNT_NAME"] = string.Empty;
                        dtRow["PAYER_IFSC_CODE"] = string.Empty;
                        dtRow["PAYEE_VPA"] = string.Empty;
                        dtRow["SERVICE_BRANCH"] = string.Empty;
                        dtRow["REGION"] = string.Empty;
                        dtRow["PRDCTY"] = string.Empty;
                        dtRow["REG_ACCOUNT_NO"] = string.Empty;
                        dtRow["PAYOUT_STATUS"] = string.Empty;
                        dtRow["PAYOUT_REF_NO"] = string.Empty;
                        dtRow["SETTLEMENT_TYPE"] = string.Empty;
                        dtRow["SETTLEMENT_ACCOUNT"] = string.Empty;
                        dtRow["SETTLEMENT_BANK"] = string.Empty;
                        dtRow["MANDATE_REF_NO"] = string.Empty;

                        //Code End
                        dtMain.Rows.Add(dtRow);
                    }
                    dtMain.AcceptChanges();
                }
                else if (objBO.TemplateType.ToUpper() == "TPSL")
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        DataRow dtRow = dtMain.NewRow();

                        dtRow["SrNo"] = dr["SRNO"];
                        dtRow["BANK_ID"] = dr["BANK_ID"];
                        dtRow["BANK_NAME"] = dr["BANK_NAME"];
                        dtRow["TRAN_ID"] = dr["TRAN_ID"];
                        dtRow["SRC_PRN"] = dr["SRC_PRN"];
                        dtRow["SRC_AMT"] = dr["SRC_AMT"];
                        dtRow["TOTCOMM"] = dr["TOTCOMM"];
                        dtRow["SERVICE_TAX"] = dr["SERVICE_TAX"];
                        dtRow["AMT_GIVE_TO_SUBMER"] = dr["AMT_GIVE_TO_SUBMER"];

                        dtRow["TXN_DATE"] = dr["TXN_DATE"];
                        dtRow["TXN_TIME"] = dr["TXN_TIME"];

                        dtRow["PAYMENT_DATE"] = dr["PAYMENT_DATE"];
                        dtRow["SRC_ITC"] = dr["SRC_ITC"];
                        dtRow["STATUS_DESC"] = dr["STATUS_DESC"];
                        dtRow["SAPCODE"] = string.Empty;
                        dtRow["EMPNAME"] = string.Empty;
                        dtRow["BILLER_ID"] = string.Empty;
                        dtRow["PGI_REF_NO"] = string.Empty;
                        dtRow["REF_2"] = string.Empty;
                        dtRow["REF_3"] = string.Empty;
                        dtRow["REF_4"] = string.Empty;
                        dtRow["REF_5"] = string.Empty;
                        dtRow["REF_6"] = string.Empty;
                        dtRow["REF_7"] = string.Empty;
                        dtRow["REF_8"] = string.Empty;
                        dtRow["REF_9"] = string.Empty;
                        dtRow["REF_10"] = string.Empty;
                        dtRow["REF_11"] = string.Empty;
                        dtRow["FILLER"] = string.Empty;
                        dtRow["CHARGES"] = string.Empty;
                        dtRow["GST"] = string.Empty;
                        dtRow["NET_AMOUNT"] = string.Empty;
                        dtRow["Portal_Name"] = string.Empty;

                        //Code added by Satish Pawar on 06 July 2022

                        dtRow["SUB_BILLER_ID"] = string.Empty;
                        dtRow["COMPANY_NAME"] = string.Empty;
                        dtRow["DBA_NAME"] = string.Empty;
                        dtRow["CUST_REF_NO"] = string.Empty;
                        dtRow["PAYMENT_TYPE"] = string.Empty;
                        dtRow["STATUS"] = string.Empty;
                        dtRow["TRANS_REMARKS"] = string.Empty;
                        dtRow["PAYER_VIRTUAL_ADDRESS"] = string.Empty;
                        dtRow["PAYER_ACCOUNT_NO"] = string.Empty;
                        dtRow["PAYER_ACCOUNT_NAME"] = string.Empty;
                        dtRow["PAYER_IFSC_CODE"] = string.Empty;
                        dtRow["PAYEE_VPA"] = string.Empty;
                        dtRow["SERVICE_BRANCH"] = string.Empty;
                        dtRow["REGION"] = string.Empty;
                        dtRow["PRDCTY"] = string.Empty;
                        dtRow["REG_ACCOUNT_NO"] = string.Empty;
                        dtRow["PAYOUT_STATUS"] = string.Empty;
                        dtRow["PAYOUT_REF_NO"] = string.Empty;
                        dtRow["SETTLEMENT_TYPE"] = string.Empty;
                        dtRow["SETTLEMENT_ACCOUNT"] = string.Empty;
                        dtRow["SETTLEMENT_BANK"] = string.Empty;
                        dtRow["MANDATE_REF_NO"] = string.Empty;

                        //Code End
                        dtMain.Rows.Add(dtRow);


                    }
                    dtMain.AcceptChanges();
                }

                else if (objBO.TemplateType.ToUpper() == "PAYU")
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        DataRow dtRow = dtMain.NewRow();
                        //string[] txnDate = Convert.ToString(dr["Date of Transaction"]).Split(' ');
                        dtRow["SrNo"] = null;
                        dtRow["BANK_ID"] = dr["Payment Gateway"];
                        dtRow["BANK_NAME"] = dr["BankName"];
                        dtRow["TRAN_ID"] = dr["Transaction Id"];
                        dtRow["SRC_PRN"] = dr["UDF 1"];//UDF1 we require new format from manisha
                        dtRow["SRC_AMT"] = dr["Amount"];
                        dtRow["TOTCOMM"] = "0";
                        dtRow["SERVICE_TAX"] = "0";
                        dtRow["AMT_GIVE_TO_SUBMER"] = "0";
                        //if (txnDate.Length > 0)
                        //{
                        //    dtRow["TXN_DATE"] = txnDate[0];
                        //}
                        //else
                        //{
                        //    dtRow["TXN_DATE"] = string.Empty;
                        //}
                        // dtRow["TXN_DATE"] = dr["Date of Transaction"];
                        dtRow["TXN_DATE"] = dr["Date of Transaction"];
                        dtRow["TXN_TIME"] = "00:00:00";

                        dtRow["PAYMENT_DATE"] = dr["Settlement Date"];
                        dtRow["SRC_ITC"] = dr["Bank Reference No"];
                        dtRow["STATUS_DESC"] = dr["Status"];
                        dtRow["SAPCODE"] = string.Empty;
                        dtRow["EMPNAME"] = string.Empty;
                        dtRow["BILLER_ID"] = dr["Merchant_Id"];
                        dtRow["PGI_REF_NO"] = dr["Settlement UTR"];
                        dtRow["REF_2"] = dr["Customer Name"];
                        dtRow["REF_3"] = dr["PayU ID"];//PAYU_ID
                        dtRow["REF_4"] = string.Empty;
                        dtRow["REF_5"] = string.Empty;
                        dtRow["REF_6"] = string.Empty;
                        dtRow["REF_7"] = string.Empty;
                        dtRow["REF_8"] = string.Empty;
                        dtRow["REF_9"] = string.Empty;
                        dtRow["REF_10"] = string.Empty;
                        dtRow["REF_11"] = string.Empty;
                        dtRow["FILLER"] = string.Empty;
                        dtRow["CHARGES"] = string.Empty;
                        dtRow["GST"] = string.Empty;
                        dtRow["NET_AMOUNT"] = string.Empty;
                        dtRow["Portal_Name"] = string.Empty;

                        //Code added by Satish Pawar on 06 July 2022

                        dtRow["SUB_BILLER_ID"] = string.Empty;
                        dtRow["COMPANY_NAME"] = string.Empty;
                        dtRow["DBA_NAME"] = string.Empty;
                        dtRow["CUST_REF_NO"] = string.Empty;
                        dtRow["PAYMENT_TYPE"] = string.Empty;
                        dtRow["STATUS"] = string.Empty;
                        dtRow["TRANS_REMARKS"] = string.Empty;
                        dtRow["PAYER_VIRTUAL_ADDRESS"] = string.Empty;
                        dtRow["PAYER_ACCOUNT_NO"] = string.Empty;
                        dtRow["PAYER_ACCOUNT_NAME"] = string.Empty;
                        dtRow["PAYER_IFSC_CODE"] = string.Empty;
                        dtRow["PAYEE_VPA"] = string.Empty;
                        dtRow["SERVICE_BRANCH"] = string.Empty;
                        dtRow["REGION"] = string.Empty;
                        dtRow["PRDCTY"] = string.Empty;
                        dtRow["REG_ACCOUNT_NO"] = string.Empty;
                        dtRow["PAYOUT_STATUS"] = string.Empty;
                        dtRow["PAYOUT_REF_NO"] = string.Empty;
                        dtRow["SETTLEMENT_TYPE"] = string.Empty;
                        dtRow["SETTLEMENT_ACCOUNT"] = string.Empty;
                        dtRow["SETTLEMENT_BANK"] = string.Empty;
                        dtRow["MANDATE_REF_NO"] = string.Empty;

                        //Code End
                        dtMain.Rows.Add(dtRow);

                    }
                    dtMain.AcceptChanges();
                }

                else if (objBO.TemplateType.ToUpper() == "CAMS")
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        //Code By Satish Pawar on 07 Jul 2022
                        //Code Start
                        DataRow dtRow = dtMain.NewRow();

                        dtRow["SrNo"] = string.Empty;
                        dtRow["BANK_ID"] = string.Empty;
                        dtRow["BANK_NAME"] = dr["Bank Name"];
                        dtRow["TRAN_ID"] = dr["CP Ref no"];
                        dtRow["SRC_PRN"] = dr["Trans Ref no"];
                        dtRow["SRC_AMT"] = dr["Trans Amount"];
                        dtRow["TOTCOMM"] = "0";
                        dtRow["SERVICE_TAX"] = "0";
                        dtRow["AMT_GIVE_TO_SUBMER"] = "0";
                        dtRow["TXN_DATE"] = dr["Transaction Date"];
                        dtRow["TXN_TIME"] = string.Empty;
                        dtRow["PAYMENT_DATE"] = dr["Transaction Date"];
                        dtRow["SRC_ITC"] = string.Empty;
                        dtRow["STATUS_DESC"] = dr["Status Description"];
                        dtRow["SAPCODE"] = string.Empty;
                        dtRow["EMPNAME"] = string.Empty;
                        dtRow["BILLER_ID"] = dr["CP Merchant ID"];
                        dtRow["PGI_REF_NO"] = dr["Bank Ref No"];
                        dtRow["REF_2"] = dr["Customer Name"];
                        dtRow["REF_3"] = dr["Customer ID"];
                        dtRow["REF_4"] = dr["Mobile 1"];
                        dtRow["REF_5"] = dr["Mobile 2"];
                        dtRow["REF_6"] = dr["SMS Status"];
                        dtRow["REF_7"] = dr["No# of Clicks"];
                        dtRow["REF_8"] = dr["Npci Code"];
                        dtRow["REF_9"] = dr["UMN"];
                        dtRow["REF_10"] = dr["Language"];
                        dtRow["REF_11"] = dr["Device Type"];
                        dtRow["FILLER"] = string.Empty;
                        dtRow["CHARGES"] = string.Empty;
                        dtRow["GST"] = string.Empty;
                        dtRow["NET_AMOUNT"] = string.Empty;
                        dtRow["Portal_Name"] = string.Empty;
                        dtRow["SUB_BILLER_ID"] = dr["Subbiller ID"];
                        dtRow["COMPANY_NAME"] = dr["Company Name"];
                        dtRow["DBA_NAME"] = dr["DBA Name"];
                        dtRow["CUST_REF_NO"] = dr["Customer Ref No"];
                        dtRow["PAYMENT_TYPE"] = dr["Payment Type"];
                        dtRow["STATUS"] = dr["Status"];
                        dtRow["TRANS_REMARKS"] = dr["Transaction Remarks"];
                        dtRow["PAYER_VIRTUAL_ADDRESS"] = dr["Payer Virtual Address"];
                        dtRow["PAYER_ACCOUNT_NO"] = dr["Payer A/C No"];
                        dtRow["PAYER_ACCOUNT_NAME"] = dr["Payer A/C Name"];
                        dtRow["PAYER_IFSC_CODE"] = dr["Payer IFSC Code"];
                        dtRow["PAYEE_VPA"] = dr["Payee VPA"];
                        dtRow["SERVICE_BRANCH"] = dr["Service Branch"];
                        dtRow["REGION"] = dr["Region"];
                        dtRow["PRDCTY"] = dr["PRDCTY"];
                        dtRow["REG_ACCOUNT_NO"] = dr["Reg A/C No"];
                        dtRow["PAYOUT_STATUS"] = dr["Payout Status"];
                        dtRow["PAYOUT_REF_NO"] = dr["Payout Ref No"];
                        dtRow["SETTLEMENT_TYPE"] = dr["Settlement Type"];
                        dtRow["SETTLEMENT_ACCOUNT"] = dr["Settlement Account"];
                        dtRow["SETTLEMENT_BANK"] = dr["Settlement Bank"];
                        dtRow["MANDATE_REF_NO"] = dr["Mandate Ref No"];
                        dtMain.Rows.Add(dtRow);


                    }
                    dtMain.AcceptChanges();
                }

                //Below Code is Added By Satish Pawar On 11 Nov 2022
                else if (objBO.TemplateType.ToUpper() == "CAMS LAFD")
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        //Code By Satish Pawar on 07 Jul 2022
                        //Code Start
                        DataRow dtRow = dtMain.NewRow();

                        dtRow["SrNo"] = string.Empty;
                        dtRow["BANK_ID"] = string.Empty;
                        dtRow["BANK_NAME"] = dr["Bank Name"];
                        dtRow["TRAN_ID"] = dr["CP Ref no"];
                        dtRow["SRC_PRN"] = dr["Trans Ref no"];
                        dtRow["SRC_AMT"] = dr["Trans Amount"];
                        dtRow["TOTCOMM"] = "0";
                        dtRow["SERVICE_TAX"] = "0";
                        dtRow["AMT_GIVE_TO_SUBMER"] = "0";
                        dtRow["TXN_DATE"] = dr["Transaction Date"];
                        dtRow["TXN_TIME"] = string.Empty;
                        dtRow["PAYMENT_DATE"] = dr["Transaction Date"];
                        dtRow["SRC_ITC"] = string.Empty;
                        dtRow["STATUS_DESC"] = dr["Status Description"];
                        dtRow["SAPCODE"] = string.Empty;
                        dtRow["EMPNAME"] = string.Empty;
                        dtRow["BILLER_ID"] = dr["CP Merchant ID"];
                        dtRow["PGI_REF_NO"] = dr["Bank Ref No"];
                        dtRow["REF_2"] = dr["Customer Name"];
                        dtRow["REF_3"] = dr["Customer ID"];
                        dtRow["REF_4"] = dr["Mobile 1"];
                        dtRow["REF_5"] = dr["Mobile 2"];
                        dtRow["REF_6"] = dr["SMS Status"];
                        dtRow["REF_7"] = dr["No# of Clicks"];
                        dtRow["REF_8"] = dr["Npci Code"];
                        dtRow["REF_9"] = dr["UMN"];
                        dtRow["REF_10"] = dr["Language"];
                        dtRow["REF_11"] = dr["Device Type"];
                        dtRow["FILLER"] = string.Empty;
                        dtRow["CHARGES"] = string.Empty;
                        dtRow["GST"] = string.Empty;
                        dtRow["NET_AMOUNT"] = string.Empty;
                        dtRow["Portal_Name"] = string.Empty;
                        dtRow["SUB_BILLER_ID"] = dr["Subbiller ID"];
                        dtRow["COMPANY_NAME"] = dr["Company Name"];
                        dtRow["DBA_NAME"] = dr["DBA Name"];
                        dtRow["CUST_REF_NO"] = dr["Customer Ref No"];
                        dtRow["PAYMENT_TYPE"] = dr["Payment Type"];
                        dtRow["STATUS"] = dr["Status"];
                        dtRow["TRANS_REMARKS"] = dr["Transaction Remarks"];
                        dtRow["PAYER_VIRTUAL_ADDRESS"] = dr["Payer Virtual Address"];
                        dtRow["PAYER_ACCOUNT_NO"] = dr["Payer A/C No"];
                        dtRow["PAYER_ACCOUNT_NAME"] = dr["Payer A/C Name"];
                        dtRow["PAYER_IFSC_CODE"] = dr["Payer IFSC Code"];
                        dtRow["PAYEE_VPA"] = dr["Payee VPA"];
                        dtRow["SERVICE_BRANCH"] = dr["Service Branch"];
                        dtRow["REGION"] = dr["Region"];
                        dtRow["PRDCTY"] = dr["PRDCTY"];
                        dtRow["REG_ACCOUNT_NO"] = dr["Reg A/C No"];
                        dtRow["PAYOUT_STATUS"] = dr["Payout Status"];
                        dtRow["PAYOUT_REF_NO"] = dr["Payout Ref No"];
                        dtRow["SETTLEMENT_TYPE"] = dr["Settlement Type"];
                        dtRow["SETTLEMENT_ACCOUNT"] = dr["Settlement Account"];
                        dtRow["SETTLEMENT_BANK"] = dr["Settlement Bank"];
                        dtRow["MANDATE_REF_NO"] = dr["Mandate Ref No"];
                        dtMain.Rows.Add(dtRow);


                    }
                    dtMain.AcceptChanges();
                }
                
                //Below Code is added By Satish Pawar on 11 Nov 2022
                if(objBO.TemplateType.ToUpper() == "CAMS LAFD")
                {
                    using (DataSet ds = objDAL.SaveData_LAFD_Online(dtMain, objBO, objSBO))
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            string status = ds.Tables[0].Rows[0][0].ToString();
                            string msg = ds.Tables[0].Rows[0][1].ToString();

                            if (status == "1")
                            {
                                res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
                            }
                            else
                            {
                                throw new Exception(msg);
                            }
                        }
                    }
                }
                else
                {
                    using (DataSet ds = objDAL.SaveData_Online(dtMain, objBO, objSBO))
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            string status = ds.Tables[0].Rows[0][0].ToString();
                            string msg = ds.Tables[0].Rows[0][1].ToString();

                            if (status == "1")
                            {
                                res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
                            }
                            else
                            {
                                throw new Exception(msg);
                            }
                        }
                    }
                }


                //Below Code is commented By Satish Pawar on 11 Nov 2022
                //using (DataSet ds = objDAL.SaveData_Online(dtMain, objBO, objSBO))
                //{
                //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //    {
                //        string status = ds.Tables[0].Rows[0][0].ToString();
                //        string msg = ds.Tables[0].Rows[0][1].ToString();

                //        if (status == "1")
                //        {
                //            res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
                //        }
                //        else
                //        {
                //            throw new Exception(msg);
                //        }
                //    }
                //}
            }

            return res;
        }
        private string Offline_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            string res = "";

            string[] arr = new string[9];
            arr[0] = "BANKSRNO";
            arr[1] = "ENTRYDATE";
            arr[2] = "BRANCH";
            arr[3] = "APPLNO";
            arr[4] = "APPLNAME";
            arr[5] = "AMOUNT";
            arr[6] = "CHQNO";
            arr[7] = "BANK";
            arr[8] = "Remarks";

            if (dt.Columns.Count == arr.Count())
            {

                for (int i = 0; i < arr.Count(); i++)
                {
                    if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
                    {
                        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
                        break;
                    }
                }
            }
            else
            {
                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
            }

            if (res == "")
            {
                objDAL = new ReconciliationProcessDAL();


                System.Data.DataTable dtMain = new System.Data.DataTable();

                dtMain.Columns.Add("BANKSRNO");
                dtMain.Columns.Add("ENTRYDATE");
                dtMain.Columns.Add("BRANCH");
                dtMain.Columns.Add("APPLNO");
                dtMain.Columns.Add("APPLNAME");
                dtMain.Columns.Add("AMOUNT");
                dtMain.Columns.Add("CHQNO");
                dtMain.Columns.Add("BANK");
                dtMain.Columns.Add("Remarks");
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dtRow = dtMain.NewRow();
                    dtRow["BANKSRNO"] = dr["BANKSRNO"];
                    //try
                    //{
                    //    if (dr["ENTRYDATE"].ToString().Trim() != "")
                    //    {
                    //        double diffdays = ((TimeSpan)(DateTime.Now - DateTime.Parse(dr["ENTRYDATE"].ToString()))).TotalDays;

                    //        if (diffdays > 30)
                    //        {
                    //            return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "ENTRYDATE cannot be backended date more than 30 days" });
                    //        }
                    //        else if (diffdays < 0)
                    //        {
                    //            return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "ENTRYDATE cannot be future date" });
                    //        }

                    //    }
                    //    else
                    //    {
                    //        return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "ENTRYDATE is blank" });
                    //    }
                    //}
                    //catch
                    //{
                    //    return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "ENTRYDATE is not in correct format" });
                    //}

                    dtRow["ENTRYDATE"] = dr["ENTRYDATE"];// Convert.ToDateTime(dr["ENTRYDATE"]).ToString("dd-MMM-yyyy");
                                                         //dtRow["ENTRYDATE"] = dr["ENTRYDATE"];
                    dtRow["BRANCH"] = dr["BRANCH"];
                    dtRow["APPLNO"] = dr["APPLNO"];
                    dtRow["APPLNAME"] = dr["APPLNAME"];
                    dtRow["AMOUNT"] = dr["AMOUNT"];
                    dtRow["CHQNO"] = dr["CHQNO"];
                    dtRow["BANK"] = dr["BANK"];
                    dtRow["Remarks"] = dr["Remarks"];
                    dtMain.Rows.Add(dtRow);
                }
                dtMain.AcceptChanges();


                using (DataSet ds = objDAL.SaveData_Offline(dtMain, objBO, objSBO))
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string status = ds.Tables[0].Rows[0][0].ToString();
                        string msg = ds.Tables[0].Rows[0][1].ToString();

                        if (status == "1")
                        {
                            res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
                        }
                        else
                        {
                            throw new Exception(msg);
                        }
                    }
                }
            }

            return res;
        }

        private string CMS_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            string res = "";
            System.Data.DataTable dtMain = new System.Data.DataTable();
            string[] arr = new string[23];
            arr[0] = "Record Type";
            arr[1] = "Internal Dep No#";
            arr[2] = "Deposit Slip No";
            arr[3] = "Deposit Remarks";
            arr[4] = "Deposit Date";
            arr[5] = "Total Amount";
            arr[6] = "Total Instruments";
            arr[7] = "Pickup Date";
            arr[8] = "Pickup Location";
            arr[9] = "Pickup Point";
            arr[10] = "Arrangement Code";
            arr[11] = "Instrument No#";
            arr[12] = "Instrument Date";
            arr[13] = "Instrument Amount";
            arr[14] = "Clearing Location";
            arr[15] = "Drawer Name";
            arr[16] = "Drawee Bank";
            arr[17] = "Drawee Branch";
            arr[18] = "Product Code";
            arr[19] = "IE1";
            arr[20] = "IE2";
            arr[21] = "IE3";
            arr[22] = "IE4";



            if (dt.Columns.Count == arr.Count())
            {

                for (int i = 0; i < arr.Count(); i++)
                {
                    if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
                    {
                        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
                        break;
                    }
                }
            }
            else
            {
                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
            }

            if (res == "")
            {
                dtMain = dt.Copy();
                dtMain.Columns[0].ColumnName = "Record_Type";
                dtMain.Columns[1].ColumnName = "Internal_Dep_No";
                dtMain.Columns[2].ColumnName = "Deposit_Slip_No";
                dtMain.Columns[3].ColumnName = "Deposit_Remarks";
                dtMain.Columns[4].ColumnName = "Deposit_Date";
                dtMain.Columns[5].ColumnName = "Total_Amount";
                dtMain.Columns[6].ColumnName = "Total_Instruments";
                dtMain.Columns[7].ColumnName = "Pickup_Date";
                dtMain.Columns[8].ColumnName = "Pickup_Location";
                dtMain.Columns[9].ColumnName = "Pickup_Point";
                dtMain.Columns[10].ColumnName = "Arrangement_Code";
                dtMain.Columns[11].ColumnName = "Instrument_No";
                dtMain.Columns[12].ColumnName = "Instrument_Date";
                dtMain.Columns[13].ColumnName = "Instrument_Amount";
                dtMain.Columns[14].ColumnName = "Clearing_Location";
                dtMain.Columns[15].ColumnName = "Drawer_Name";
                dtMain.Columns[16].ColumnName = "Drawee_Bank";
                dtMain.Columns[17].ColumnName = "Drawee_Branch";
                dtMain.Columns[18].ColumnName = "Product_Code";
                dtMain.Columns[19].ColumnName = "IE1";
                dtMain.Columns[20].ColumnName = "IE2";
                dtMain.Columns[21].ColumnName = "IE3";
                dtMain.Columns[22].ColumnName = "IE4";
                dtMain.Rows.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dtRow = dtMain.NewRow();
                    dtRow["Record_Type"] = dr["Record Type"];
                    dtRow["Internal_Dep_No"] = dr["Internal Dep No#"];
                    dtRow["Deposit_Slip_No"] = dr["Deposit Slip No"];
                    dtRow["Deposit_Remarks"] = dr["Deposit Remarks"];
                    //try
                    //{
                    //    if (dr["Deposit_Date"].ToString().Trim() != "")
                    //    {
                    //        double diffdays = ((TimeSpan)(DateTime.Now - DateTime.Parse(dr["Deposit_Date"].ToString()))).TotalDays;

                    //        if (diffdays > 30)
                    //        {
                    //            return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Deposit_Date cannot be backended date more than 30 days" });
                    //        }
                    //        else if (diffdays < 0)
                    //        {
                    //            return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Deposit_Date cannot be future date" });
                    //        }

                    //    }
                    //    else
                    //    {
                    //        return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Deposit_Date is blank" });
                    //    }
                    //}
                    //catch
                    //{
                    //    return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Deposit_Date is not in correct format" });
                    //}

                    dtRow["Deposit_Date"] = dr["Deposit Date"];// Convert.ToDateTime(dr["Deposit_Date"]).ToString("dd-MMM-yyyy");

                    dtRow["Total_Amount"] = dr["Total Amount"];
                    dtRow["Total_Instruments"] = dr["Total Instruments"];
                    //try
                    //{
                    //    if (dr["Pickup_Date"].ToString().Trim() != "")
                    //    {
                    //        double diffdays = ((TimeSpan)(DateTime.Now - DateTime.Parse(dr["Pickup_Date"].ToString()))).TotalDays;

                    //        if (diffdays > 30)
                    //        {
                    //            return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Pickup_Date cannot be backended date more than 30 days" });
                    //        }
                    //        else if (diffdays < 0)
                    //        {
                    //            return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Pickup_Date cannot be future date" });
                    //        }

                    //    }
                    //    else
                    //    {
                    //        return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Pickup_Date is blank" });
                    //    }
                    //}
                    //catch
                    //{
                    //    return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Pickup_Date is not in correct format" });
                    //}

                    dtRow["Pickup_Date"] = dr["Pickup Date"];// Convert.ToDateTime(dr["Pickup_Date"]).ToString("dd-MMM-yyyy");
                    dtRow["Pickup_Location"] = dr["Pickup Location"];
                    dtRow["Pickup_Point"] = dr["Pickup Point"];
                    dtRow["Arrangement_Code"] = dr["Arrangement Code"];
                    dtRow["Instrument_No"] = dr["Instrument No#"];
                    //try
                    //{
                    //    if (dr["Instrument_Date"].ToString().Trim() != "")
                    //    {
                    //        double diffdays = ((TimeSpan)(DateTime.Now - DateTime.Parse(dr["Instrument_Date"].ToString()))).TotalDays;

                    //        if (diffdays > 30)
                    //        {
                    //            return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Instrument_Date cannot be backended date more than 30 days" });
                    //        }
                    //        else if (diffdays < 0)
                    //        {
                    //            return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Instrument_Date cannot be future date" });
                    //        }
                    //    }
                    //    else
                    //    {
                    //        return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Instrument_Date is blank" });
                    //    }
                    //}
                    //catch
                    //{
                    //    return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Instrument_Date is not in correct format" });
                    //}

                    dtRow["Instrument_Date"] = dr["Instrument Date"];// Convert.ToDateTime(dr["Instrument_Date"]).ToString("dd-MMM-yyyy");
                    dtRow["Instrument_Amount"] = dr["Instrument Amount"];
                    dtRow["Clearing_Location"] = dr["Clearing Location"];
                    dtRow["Drawer_Name"] = dr["Drawer Name"];
                    dtRow["Drawee_Bank"] = dr["Drawee Bank"];
                    dtRow["Drawee_Branch"] = dr["Drawee Branch"];
                    dtRow["Product_Code"] = dr["Product Code"];
                    dtRow["IE1"] = dr["IE1"];
                    dtRow["IE2"] = dr["IE2"];
                    dtRow["IE3"] = dr["IE3"];
                    dtRow["IE4"] = dr["IE4"];

                    dtMain.Rows.Add(dtRow);
                }

                dtMain.AcceptChanges();

                objDAL = new ReconciliationProcessDAL();
                using (DataSet ds = objDAL.SaveData_CMS(dtMain, objBO, objSBO))
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string status = ds.Tables[0].Rows[0][0].ToString();
                        string msg = ds.Tables[0].Rows[0][1].ToString();

                        if (status == "1")
                        {
                            res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
                        }
                        else
                        {
                            throw new Exception(msg);
                        }
                    }
                }
            }

            return res;
        }

        //CMS Collection
        private string CMS_Collection_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            string res = "";
            System.Data.DataTable MainData = new System.Data.DataTable();
            string[] arr = new string[28];
            arr[0] = "band_id";
            arr[1] = "entry_id";
            arr[2] = "type_of_entry";
            arr[3] = "debit_credit_flag";
            arr[4] = "entry_amount";
            arr[5] = "value_date";
            arr[6] = "posting_date";
            arr[7] = "product_code";
            arr[8] = "pickup_location";
            arr[9] = "pickup_point";
            arr[10] = "pickup_date";
            arr[11] = "deposit_slip_number";
            arr[12] = "deposit_date";
            arr[13] = "deposit_amount";
            arr[14] = "number_of_instruments";
            arr[15] = "deposit_remarks";
            arr[16] = "d1";
            arr[17] = "instrument_number";
            arr[18] = "drawee_bank";
            arr[19] = "clearing_location";
            arr[20] = "instrument_amount";
            arr[21] = "instrument_date";
            arr[22] = "drawer_name";
            arr[23] = "e1";
            arr[24] = "e2";
            arr[25] = "e3";
            arr[26] = "e4";
            arr[27] = "return_reason_remarks";

            if (dt.Columns.Count == arr.Count())
            {
                for (int i = 0; i < arr.Count(); i++)
                {
                    if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
                    {
                        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
                        break;
                    }
                }
            }
            else
            {
                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
            }

            if (res == "")
            {
                objDAL = new ReconciliationProcessDAL();
                using (DataSet ds = objDAL.SaveData_CMS_Collection(dt, objBO, objSBO))
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string status = ds.Tables[0].Rows[0][0].ToString();
                        string msg = ds.Tables[0].Rows[0][1].ToString();

                        if (status == "1")
                        {
                            res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
                        }
                        else
                        {
                            throw new Exception(msg);
                        }
                    }
                }
            }

            return res;
        }

        //UPI
        private string UPI_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            string res = "";
            System.Data.DataTable MainData = new System.Data.DataTable();
            string[] arr = new string[4];
            arr[0] = "TranRefNo";
            arr[1] = "APPLNO";
            arr[2] = "Transaction_Date";
            arr[3] = "Transaction_Amount";

            if (dt.Columns.Count == arr.Count())
            {
                for (int i = 0; i < arr.Count(); i++)
                {
                    if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
                    {
                        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
                        break;
                    }
                }
            }
            else
            {
                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
            }

            if (res == "")
            {
                objDAL = new ReconciliationProcessDAL();

                System.Data.DataTable dtMain = new System.Data.DataTable();

                dtMain.Columns.Add("TranRefNo");
                dtMain.Columns.Add("APPLNO");
                dtMain.Columns.Add("Transaction_Date");
                dtMain.Columns.Add("Transaction_Amount");

                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dtRow = dtMain.NewRow();
                    dtRow["TranRefNo"] = dr["TranRefNo"];
                    dtRow["APPLNO"] = dr["APPLNO"];
                    dtRow["Transaction_Amount"] = dr["Transaction_Amount"];
                    //try
                    //{
                    //    if (dr["Transaction_Date"].ToString().Trim() != "")
                    //    {
                    //        double diffdays = ((TimeSpan)(DateTime.Now - DateTime.Parse(dr["Transaction_Date"].ToString()))).TotalDays;

                    //        if (diffdays > 30)
                    //        {
                    //            return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Transaction_Date cannot be backended date more than 30 days" });
                    //        }
                    //        else if (diffdays < 0)
                    //        {
                    //            return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Transaction_Date cannot be future date" });
                    //        }
                    //    }
                    //    else
                    //    {
                    //        return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Transaction_Date is blank" });
                    //    }
                    //}
                    //catch
                    //{
                    //    return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Transaction_Date is not in correct format" });
                    //}
                    dtRow["Transaction_Date"] = dr["Transaction_Date"];// Convert.ToDateTime(dr["Transaction_Date"]).ToString("dd-MMM-yyyy");
                    dtMain.Rows.Add(dtRow);
                }
                dtMain.AcceptChanges();

                using (DataSet ds = objDAL.SaveData_UPI(dtMain, objBO, objSBO))
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string status = ds.Tables[0].Rows[0][0].ToString();
                        string msg = ds.Tables[0].Rows[0][1].ToString();

                        if (status == "1")
                        {
                            res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
                        }
                        else
                        {
                            throw new Exception(msg);
                        }
                    }
                }
            }

            return res;
        }

        //RTGS
        private string RTGS_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            string res = "";
            System.Data.DataTable MainData = new System.Data.DataTable();
            string[] arr = new string[4];
            arr[0] = "TranRefNo";
            arr[1] = "APPLNO";
            arr[2] = "Transaction_Date";
            arr[3] = "Transaction_Amount";

            if (dt.Columns.Count == arr.Count())
            {
                for (int i = 0; i < arr.Count(); i++)
                {
                    if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
                    {
                        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
                        break;
                    }
                }
            }
            else
            {
                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
            }

            if (res == "")
            {
                objDAL = new ReconciliationProcessDAL();

                System.Data.DataTable dtMain = new System.Data.DataTable();

                dtMain.Columns.Add("TranRefNo");
                dtMain.Columns.Add("APPLNO");
                dtMain.Columns.Add("Transaction_Date");
                dtMain.Columns.Add("Transaction_Amount");

                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dtRow = dtMain.NewRow();
                    dtRow["TranRefNo"] = dr["TranRefNo"];
                    dtRow["APPLNO"] = dr["APPLNO"];
                    dtRow["Transaction_Amount"] = dr["Transaction_Amount"];
                    //try
                    //{
                    //    if (dr["Transaction_Date"].ToString().Trim() != "")
                    //    {
                    //        double diffdays = ((TimeSpan)(DateTime.Now - DateTime.Parse(dr["Transaction_Date"].ToString()))).TotalDays;

                    //        if (diffdays > 30)
                    //        {
                    //            return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Transaction_Date cannot be backended date more than 30 days" });
                    //        }
                    //        else if (diffdays < 0)
                    //        {
                    //            return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Transaction_Date cannot be future date" });
                    //        }
                    //    }
                    //    else
                    //    {
                    //        return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Transaction_Date is blank" });
                    //    }
                    //}
                    //catch
                    //{
                    //    return res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Transaction_Date is not in correct format" });
                    //}
                    dtRow["Transaction_Date"] = dr["Transaction_Date"];// Convert.ToDateTime(dr["Transaction_Date"]).ToString("dd-MMM-yyyy");
                    dtMain.Rows.Add(dtRow);
                }
                dtMain.AcceptChanges();



                using (DataSet ds = objDAL.SaveData_RTGS(dtMain, objBO, objSBO))
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string status = ds.Tables[0].Rows[0][0].ToString();
                        string msg = ds.Tables[0].Rows[0][1].ToString();

                        if (status == "1")
                        {
                            res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
                        }
                        else
                        {
                            throw new Exception(msg);
                        }
                    }
                }
            }

            return res;
        }


        private string HDFC_Soft_Feed_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            string res = "";
            System.Data.DataTable MainData = new System.Data.DataTable();
            string[] arr = new string[32];
            arr[0] = "DEPOSIT_NMBR";
            arr[1] = "INSTRUMEN";
            arr[2] = "CHQNO";
            arr[3] = "AMOUNT";
            arr[4] = "BANK";
            arr[5] = "CLIENT_CODE";
            arr[6] = "ENTRYDATE";
            arr[7] = "BRANCH";
            arr[8] = "CLEARING_L";
            arr[9] = "CITY";
            arr[10] = "T";
            arr[11] = "RETURN_REASON";
            arr[12] = "LIQUIDATI";
            arr[13] = "CREDIT_DATE";
            arr[14] = "APPLNAME";
            arr[15] = "APPLNO";
            arr[16] = "BANKSRNO";
            arr[17] = "PIN";
            arr[18] = "BRCODE";
            arr[19] = "BROKER_NAME";
            arr[20] = "MOBILE_NO";
            arr[21] = "MICR";
            arr[22] = "PAN";
            arr[23] = "TENURE";
            arr[24] = "SCHEME";
            arr[25] = "RCPT_DT";
            arr[26] = "ADD1";
            arr[27] = "ADD2";
            arr[28] = "ADD3";
            arr[29] = "DOB";
            arr[30] = "INT_FREQ";
            arr[31] = "AADHAR_NO";


            if (dt.Columns.Count == arr.Count())
            {
                for (int i = 0; i < arr.Count(); i++)
                {
                    if (Convert.ToString(arr[i]).ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).ToUpper())
                    {
                        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
                        break;
                    }
                }
            }
            else
            {
                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
            }

            if (res == "")
            {
                objDAL = new ReconciliationProcessDAL();

                System.Data.DataTable dtMain = new System.Data.DataTable();
                dtMain.Columns.Add("DEPOSIT_NMBR");
                dtMain.Columns.Add("INSTRUMEN");
                dtMain.Columns.Add("CHQNO");
                dtMain.Columns.Add("AMOUNT");
                dtMain.Columns.Add("BANK");
                dtMain.Columns.Add("CLIENT_CODE");
                dtMain.Columns.Add("ENTRYDATE");
                dtMain.Columns.Add("BRANCH");
                dtMain.Columns.Add("CLEARING_L");
                dtMain.Columns.Add("CITY");
                dtMain.Columns.Add("T");
                dtMain.Columns.Add("RETURN_REASON");
                dtMain.Columns.Add("LIQUIDATI");
                dtMain.Columns.Add("CREDIT_DATE");
                dtMain.Columns.Add("APPLNAME");
                dtMain.Columns.Add("APPLNO");
                dtMain.Columns.Add("BANKSRNO");
                dtMain.Columns.Add("PIN");
                dtMain.Columns.Add("BRCODE");
                dtMain.Columns.Add("BROKER_NAME");
                dtMain.Columns.Add("MOBILE_NO");
                dtMain.Columns.Add("MICR");
                dtMain.Columns.Add("PAN");
                dtMain.Columns.Add("TENURE");
                dtMain.Columns.Add("SCHEME");
                dtMain.Columns.Add("RCPT_DT");
                dtMain.Columns.Add("ADD1");
                dtMain.Columns.Add("ADD2");
                dtMain.Columns.Add("ADD3");
                dtMain.Columns.Add("DOB");
                dtMain.Columns.Add("INT_FREQ");
                dtMain.Columns.Add("AADHAR_NO");

                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dtRow = dtMain.NewRow();
                    dtRow["DEPOSIT_NMBR"] = dr["DEPOSIT_NMBR"];
                    dtRow["INSTRUMEN"] = dr["INSTRUMEN"];
                    dtRow["CHQNO"] = dr["CHQNO"];
                    dtRow["AMOUNT"] = dr["AMOUNT"];
                    dtRow["BANK"] = dr["BANK"];
                    dtRow["CLIENT_CODE"] = dr["CLIENT_CODE"];
                    dtRow["ENTRYDATE"] = dr["ENTRYDATE"];
                    dtRow["BRANCH"] = dr["BRANCH"];
                    dtRow["CLEARING_L"] = dr["CLEARING_L"];
                    dtRow["CITY"] = dr["CITY"];
                    dtRow["T"] = dr["T"];
                    dtRow["RETURN_REASON"] = dr["RETURN_REASON"];
                    dtRow["LIQUIDATI"] = dr["LIQUIDATI"];
                    dtRow["CREDIT_DATE"] = dr["CREDIT_DATE"];
                    dtRow["APPLNAME"] = dr["APPLNAME"];
                    dtRow["APPLNO"] = dr["APPLNO"];
                    dtRow["BANKSRNO"] = dr["BANKSRNO"];
                    dtRow["PIN"] = dr["PIN"];
                    dtRow["BRCODE"] = dr["BRCODE"];
                    dtRow["BROKER_NAME"] = dr["BROKER_NAME"];
                    dtRow["MOBILE_NO"] = dr["MOBILE_NO"];
                    dtRow["MICR"] = dr["MICR"];
                    dtRow["PAN"] = dr["PAN"];
                    dtRow["TENURE"] = dr["TENURE"];
                    dtRow["SCHEME"] = dr["SCHEME"];
                    dtRow["RCPT_DT"] = dr["RCPT_DT"];
                    dtRow["ADD1"] = dr["ADD1"];
                    dtRow["ADD2"] = dr["ADD2"];
                    dtRow["ADD3"] = dr["ADD3"];
                    dtRow["DOB"] = dr["DOB"];
                    dtRow["INT_FREQ"] = dr["INT_FREQ"];
                    dtRow["AADHAR_NO"] = dr["AADHAR_NO"];
                    dtMain.Rows.Add(dtRow);
                }
                dtMain.AcceptChanges();



                using (DataSet ds = objDAL.SaveData_HDFCSoftFeed(dtMain, objBO, objSBO))
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string status = ds.Tables[0].Rows[0][0].ToString();
                        string msg = ds.Tables[0].Rows[0][1].ToString();

                        if (status == "1")
                        {
                            res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
                        }
                        else
                        {
                            throw new Exception(msg);
                        }
                    }
                }
            }

            return res;
        }

        private string BankRevalidation_DDPaidUnpaid_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            string res = "";
            System.Data.DataTable MainData = new System.Data.DataTable();
            string[] arr = new string[30];
            arr[0] = "PIR_NMBR";
            arr[1] = "PRODUCT_CODE";
            arr[2] = "PIR_REFERENCE_NMBR";
            arr[3] = "INST_NMBR";
            arr[4] = "INST_DATE";
            arr[5] = "BENEF_DESCRIPTION";
            arr[6] = "INSTRUMENT_AMNT";
            arr[7] = "STATUS";
            arr[8] = "PAID DATE";
            arr[9] = "DISP_BANK_CODE";
            arr[10] = "INST_PRINT_BRANCH_CODE";
            arr[11] = "BENEF_ACCOUNT_NMBR";
            arr[12] = "SUBSTR(E#BENEF_PAYMENT_DETAILS,1,35)";
            arr[13] = "SUBSTR(E#BENEF_PAYMENT_DETAILS,36,35)";
            arr[14] = "SUBSTR(E#BENEF_PAYMENT_DETAILS,71,35)";
            arr[15] = "SUBSTR(E#BENEF_PAYMENT_DETAILS,106,35)";
            arr[16] = "SUBSTR(E#BENEF_PAYMENT_DETAILS,141,35)";
            arr[17] = "SUBSTR(E#BENEF_PAYMENT_DETAILS,176,35)";
            arr[18] = "SUBSTR(E#BENEF_PAYMENT_DETAILS,211,35)";
            arr[19] = "BENEF_ADD_1";
            arr[20] = "BENEF_ADD_2";
            arr[21] = "BENEF_ADD_3";
            arr[22] = "BENEF_ADD_4";
            arr[23] = "BENEF_ADD_5";
            arr[24] = "LOC_DESCRIPTION";
            arr[25] = "LIQ_STATUS";
            arr[26] = "R_STATUS";
            arr[27] = "CUSTOMER_REFERENCE_NO";
            arr[28] = "INSTRUCTION_REFERENCE_NO";
            //arr[29] = "CONTRACT_REFERENCE_NO";
            arr[29] = "REMARKS";



            if (dt.Columns.Count == arr.Count())
            {
                for (int i = 0; i < arr.Count(); i++)
                {
                    if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
                    {
                        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
                        break;
                    }
                }
            }
            else
            {
                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
            }

            if (res == "")
            {
                objDAL = new ReconciliationProcessDAL();

                System.Data.DataTable dtMain = new System.Data.DataTable();

                dtMain.Columns.Add("PIR_NMBR");
                dtMain.Columns.Add("PRODUCT_CODE");
                dtMain.Columns.Add("PIR_REFERENCE_NMBR");
                dtMain.Columns.Add("INST_NMBR");
                dtMain.Columns.Add("INST_DATE");
                dtMain.Columns.Add("BENEF_DESCRIPTION");
                dtMain.Columns.Add("INSTRUMENT_AMNT");
                dtMain.Columns.Add("STATUS");
                dtMain.Columns.Add("PAID_DATE");
                dtMain.Columns.Add("DISP_BANK_CODE");
                dtMain.Columns.Add("INST_PRINT_BRANCH_CODE");
                dtMain.Columns.Add("BENEF_ACCOUNT_NMBR");
                dtMain.Columns.Add("REF_1");
                dtMain.Columns.Add("REF_2");
                dtMain.Columns.Add("REF_3");
                dtMain.Columns.Add("REF_4");
                dtMain.Columns.Add("REF_5");
                dtMain.Columns.Add("REF_6");
                dtMain.Columns.Add("REF_7");
                dtMain.Columns.Add("BENEF_ADD_1");
                dtMain.Columns.Add("BENEF_ADD_2");
                dtMain.Columns.Add("BENEF_ADD_3");
                dtMain.Columns.Add("BENEF_ADD_4");
                dtMain.Columns.Add("BENEF_ADD_5");
                dtMain.Columns.Add("LOC_DESCRIPTION");
                dtMain.Columns.Add("LIQ_STATUS");
                dtMain.Columns.Add("R_STATUS");
                dtMain.Columns.Add("CUSTOMER_REFERENCE_NO");
                dtMain.Columns.Add("INSTRUCTION_REFERENCE_NO");
                dtMain.Columns.Add("CONTRACT_REFERENCE_NO");
                dtMain.Columns.Add("REMARKS");

                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dtRow = dtMain.NewRow();
                    dtRow["PIR_NMBR"] = dr["PIR_NMBR"];
                    dtRow["PRODUCT_CODE"] = dr["PRODUCT_CODE"];
                    dtRow["PIR_REFERENCE_NMBR"] = dr["PIR_REFERENCE_NMBR"];
                    dtRow["INST_NMBR"] = dr["INST_NMBR"];
                    dtRow["INST_DATE"] = dr["INST_DATE"];
                    dtRow["BENEF_DESCRIPTION"] = dr["BENEF_DESCRIPTION"];
                    dtRow["INSTRUMENT_AMNT"] = dr["INSTRUMENT_AMNT"];
                    dtRow["STATUS"] = dr["STATUS"];
                    dtRow["PAID_DATE"] = dr["PAID DATE"];
                    dtRow["DISP_BANK_CODE"] = dr["DISP_BANK_CODE"];
                    dtRow["INST_PRINT_BRANCH_CODE"] = dr["INST_PRINT_BRANCH_CODE"];
                    dtRow["BENEF_ACCOUNT_NMBR"] = dr["BENEF_ACCOUNT_NMBR"];
                    dtRow["REF_1"] = dr["SUBSTR(E#BENEF_PAYMENT_DETAILS,1,35)"];
                    dtRow["REF_2"] = dr["SUBSTR(E#BENEF_PAYMENT_DETAILS,36,35)"];
                    dtRow["REF_3"] = dr["SUBSTR(E#BENEF_PAYMENT_DETAILS,71,35)"];
                    dtRow["REF_4"] = dr["SUBSTR(E#BENEF_PAYMENT_DETAILS,106,35)"];
                    dtRow["REF_5"] = dr["SUBSTR(E#BENEF_PAYMENT_DETAILS,141,35)"];
                    dtRow["REF_6"] = dr["SUBSTR(E#BENEF_PAYMENT_DETAILS,176,35)"];
                    dtRow["REF_7"] = dr["SUBSTR(E#BENEF_PAYMENT_DETAILS,211,35)"];
                    dtRow["BENEF_ADD_1"] = dr["BENEF_ADD_1"];
                    dtRow["BENEF_ADD_2"] = dr["BENEF_ADD_2"];
                    dtRow["BENEF_ADD_3"] = dr["BENEF_ADD_3"];
                    dtRow["BENEF_ADD_4"] = dr["BENEF_ADD_4"];
                    dtRow["BENEF_ADD_5"] = dr["BENEF_ADD_5"];
                    dtRow["LOC_DESCRIPTION"] = dr["LOC_DESCRIPTION"];
                    dtRow["LIQ_STATUS"] = dr["LIQ_STATUS"];
                    dtRow["R_STATUS"] = dr["R_STATUS"];
                    dtRow["CUSTOMER_REFERENCE_NO"] = dr["CUSTOMER_REFERENCE_NO"];
                    dtRow["INSTRUCTION_REFERENCE_NO"] = dr["INSTRUCTION_REFERENCE_NO"];
                    dtRow["CONTRACT_REFERENCE_NO"] = "";
                    dtRow["REMARKS"] = dr["REMARKS"];
                    dtMain.Rows.Add(dtRow);
                }
                dtMain.AcceptChanges();



                using (DataSet ds = objDAL.SaveData_BankValidationDDPaidUnpaid(dtMain, objBO, objSBO))
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string status = ds.Tables[0].Rows[0][0].ToString();
                        string msg = ds.Tables[0].Rows[0][1].ToString();

                        if (status == "1")
                        {
                            res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
                        }
                        else
                        {
                            throw new Exception(msg);
                        }
                    }
                }
            }

            return res;
        }

        private string BankRevalidation_WarrantPaidUnpaid_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            string res = "";
            System.Data.DataTable MainData = new System.Data.DataTable();
            string[] arr = new string[8];
            arr[0] = "Cheque No";
            arr[1] = "Warrant No";
            arr[2] = "Warrant Date";
            arr[3] = "Folio No";
            arr[4] = "Amount";
            arr[5] = "Paid On";
            arr[6] = "DEP NO";
            arr[7] = "STATUS";
            //arr[8] = "Beneficiary Name";
            if (dt.Columns.Count == arr.Count())
            {
                for (int i = 0; i < arr.Count(); i++)
                {
                    if (Convert.ToString(arr[i]).ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).ToUpper())
                    {
                        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
                        break;
                    }
                }
            }
            else
            {
                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
            }

            if (res == "")
            {
                objDAL = new ReconciliationProcessDAL();

                System.Data.DataTable dtMain = new System.Data.DataTable();
                dtMain.Columns.Add("Cheque_No");
                dtMain.Columns.Add("Warrant_No");
                dtMain.Columns.Add("Warrant_Date");
                dtMain.Columns.Add("Folio_No");
                dtMain.Columns.Add("Amount");
                dtMain.Columns.Add("Paid_On");
                dtMain.Columns.Add("DEP_NO");
                dtMain.Columns.Add("STATUS");
                //dtMain.Columns.Add("Beneficiary_Name");

                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dtRow = dtMain.NewRow();
                    dtRow["Cheque_No"] = dr["Cheque No"];
                    dtRow["Warrant_No"] = dr["Warrant No"];
                    dtRow["Warrant_Date"] = dr["Warrant Date"];
                    dtRow["Folio_No"] = dr["Folio No"];
                    dtRow["Amount"] = dr["Amount"];
                    dtRow["Paid_On"] = dr["Paid On"];
                    dtRow["DEP_NO"] = dr["DEP NO"];
                    dtRow["STATUS"] = dr["STATUS"];
                    //dtRow["Beneficiary_Name"] = dr["Beneficiary Name"];
                    dtMain.Rows.Add(dtRow);
                }
                dtMain.AcceptChanges();



                using (DataSet ds = objDAL.SaveData_BankValidationWarrantPaidUnpaid(dtMain, objBO, objSBO))
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string status = ds.Tables[0].Rows[0][0].ToString();
                        string msg = ds.Tables[0].Rows[0][1].ToString();

                        if (status == "1")
                        {
                            res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
                        }
                        else
                        {
                            throw new Exception(msg);
                        }
                    }
                }
            }

            return res;
        }
        private string BankRevalidation_NEFTRejection_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            string res = "";
            System.Data.DataTable MainData = new System.Data.DataTable();
            string[] arr = new string[15];
            arr[0] = "Pay Doc No";
            arr[1] = "Payment Type";
            arr[2] = "Cheque No#";
            arr[3] = "Company Code";
            arr[4] = "Tenor(Days)";
            arr[5] = "Client Code";
            arr[6] = "Beneficiary Name";
            arr[7] = "Bene Acct No";
            arr[8] = "Upload Date";
            arr[9] = "Run Date";
            arr[10] = "Value Date";
            arr[11] = "Total Amount";
            arr[12] = "Trans#Ref#No";
            arr[13] = "Status";
            arr[14] = "Rejection Reason";

            if (dt.Columns.Count == arr.Count())
            {
                for (int i = 0; i < arr.Count(); i++)
                {
                    if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
                    {
                        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
                        break;
                    }
                }
            }
            else
            {
                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
            }

            if (res == "")
            {
                objDAL = new ReconciliationProcessDAL();

                System.Data.DataTable dtMain = new System.Data.DataTable();
                dtMain.Columns.Add("Pay_Doc_No");
                dtMain.Columns.Add("Payment_Type");
                dtMain.Columns.Add("Cheque_No");
                dtMain.Columns.Add("Company_Code");
                dtMain.Columns.Add("Tenor");
                dtMain.Columns.Add("Client_Code");
                dtMain.Columns.Add("Beneficiary_Name");
                dtMain.Columns.Add("Bene_Acct_No");
                dtMain.Columns.Add("Upload_Date");
                dtMain.Columns.Add("Run_Date");
                dtMain.Columns.Add("Value_Date");
                dtMain.Columns.Add("Total_Amount");
                dtMain.Columns.Add("Trans_Ref_No");
                dtMain.Columns.Add("Status");
                dtMain.Columns.Add("Rejection_Reason");


                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dtRow = dtMain.NewRow();
                    dtRow["Pay_Doc_No"] = dr["Pay Doc No"];
                    dtRow["Payment_Type"] = dr["Payment Type"];
                    dtRow["Cheque_No"] = dr["Cheque No#"];
                    dtRow["Company_Code"] = dr["Company Code"];
                    dtRow["Tenor"] = dr["Tenor(Days)"];
                    dtRow["Client_Code"] = dr["Client Code"];
                    dtRow["Beneficiary_Name"] = dr["Beneficiary Name"];
                    dtRow["Bene_Acct_No"] = dr["Bene Acct No"];
                    dtRow["Upload_Date"] = dr["Upload Date"];
                    dtRow["Run_Date"] = dr["Run Date"];
                    dtRow["Value_Date"] = dr["Value Date"];
                    dtRow["Total_Amount"] = dr["Total Amount"];
                    dtRow["Trans_Ref_No"] = dr["Trans#Ref#No"];
                    dtRow["Status"] = dr["Status"];
                    dtRow["Rejection_Reason"] = dr["Rejection Reason"];

                    dtMain.Rows.Add(dtRow);
                }
                dtMain.AcceptChanges();



                using (DataSet ds = objDAL.SaveData_BankValidationNEFTRejection(dtMain, objBO, objSBO))
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string status = ds.Tables[0].Rows[0][0].ToString();
                        string msg = ds.Tables[0].Rows[0][1].ToString();

                        if (status == "1")
                        {
                            res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
                        }
                        else
                        {
                            throw new Exception(msg);
                        }
                    }
                }
            }

            return res;
        }
        private string BankRevalidation_NACHSOFTPayment_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            string res = "";
            System.Data.DataTable MainData = new System.Data.DataTable();
            string[] arr = new string[19];
            //arr[0] = "ACHTransactionCode";
            //arr[1] = "Control1";
            //arr[2] = "DestinationAccountType";
            //arr[3] = "LedgerFolioNumber";
            //arr[4] = "Control2";
            //arr[5] = "BeneficiaryAccountHoldersName";
            //arr[6] = "Control3";
            //arr[7] = "Control4";
            //arr[8] = "UserName";
            //arr[9] = "Control5";
            //arr[10] = "Amount";
            //arr[11] = "ACHItemSeqNo";
            //arr[12] = "Checksum";
            //arr[13] = "SuccessReturnFlag";
            //arr[14] = "ReasonCode";
            //arr[15] = "DestinationBankIFSC_MICR_IIN";
            //arr[16] = "BeneficiaryBankAccountnumber";
            //arr[17] = "SponsorBankIFSC_MICR_IIN";
            //arr[18] = "UserNumber";
            //arr[19] = "TransactionReference";
            //arr[20] = "ProductType";
            //arr[21] = "BeneficiaryAadhaarNumber";
            //arr[22] = "UMRN";
            arr[0] = "Warrant Date";
            arr[1] = "ECS Transaction Code";
            arr[2] = "Destination Short Code";
            arr[3] = "Destination Acc Type";
            arr[4] = "Ledger Folio Number";
            arr[5] = "Destination Acc No";
            arr[6] = "Destination Acc Holder's Name";
            arr[7] = "Sponsor Bank Branch Code";
            arr[8] = "User Number";
            arr[9] = "User Name";
            arr[10] = "User Debit Reference(Unique) no duplicate allowed";
            arr[11] = "Amount";
            arr[12] = "Reserved(to be kept blank by user)";
            arr[13] = "Reserved(to be kept blank by user)1";
            arr[14] = "Reserved(to be kept blank by user)2";
            arr[15] = "Filler";
            arr[16] = "System Request Status";
            arr[17] = "Response Code";
            arr[18] = "RBI SEQ Number";


            if (dt.Columns.Count == arr.Count())
            {
                for (int i = 0; i < arr.Count(); i++)
                {
                    if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
                    {
                        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
                        break;
                    }
                }
            }
            else
            {
                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
            }

            if (res == "")
            {
                objDAL = new ReconciliationProcessDAL();

                System.Data.DataTable dtMain = new System.Data.DataTable();
                //dtMain.Columns.Add("ACHTransactionCode");
                //dtMain.Columns.Add("Control1");
                //dtMain.Columns.Add("DestinationAccountType");
                //dtMain.Columns.Add("LedgerFolioNumber");
                //dtMain.Columns.Add("Control2");
                //dtMain.Columns.Add("BeneficiaryAccountHoldersName");
                //dtMain.Columns.Add("Control3");
                //dtMain.Columns.Add("Control4");
                //dtMain.Columns.Add("UserName");
                //dtMain.Columns.Add("Control5");
                //dtMain.Columns.Add("Amount");
                //dtMain.Columns.Add("ACHItemSeqNo");
                //dtMain.Columns.Add("Checksum");
                //dtMain.Columns.Add("SuccessReturnFlag");
                //dtMain.Columns.Add("ReasonCode");
                //dtMain.Columns.Add("DestinationBankIFSC_MICR_IIN");
                //dtMain.Columns.Add("BeneficiaryBankAccountnumber");
                //dtMain.Columns.Add("SponsorBankIFSC_MICR_IIN");
                //dtMain.Columns.Add("UserNumber");
                //dtMain.Columns.Add("TransactionReference");
                //dtMain.Columns.Add("ProductType");
                //dtMain.Columns.Add("BeneficiaryAadhaarNumber");
                //dtMain.Columns.Add("UMRN");
                dtMain.Columns.Add("Warrant_Date");
                dtMain.Columns.Add("ECS_Transaction_Code");
                dtMain.Columns.Add("Destination_Short_Code");
                dtMain.Columns.Add("Destination_Acc_Type");
                dtMain.Columns.Add("Ledger_Folio_Number");
                dtMain.Columns.Add("Destination_Acc_No");
                dtMain.Columns.Add("Destination_Acc_Holder_Name");
                dtMain.Columns.Add("Sponsor_Bank_Branch_Code");
                dtMain.Columns.Add("User_Number");
                dtMain.Columns.Add("User_Name");
                dtMain.Columns.Add("Warrant_NO");
                dtMain.Columns.Add("Amount");
                dtMain.Columns.Add("Reserved1");
                dtMain.Columns.Add("Reserved2");
                dtMain.Columns.Add("Reserved3");
                dtMain.Columns.Add("Filler");
                dtMain.Columns.Add("System_Request_Status");
                dtMain.Columns.Add("Response_Code");
                dtMain.Columns.Add("RBI_SEQ_Number");

                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dtRow = dtMain.NewRow();
                    //dtRow["ACHTransactionCode"] = dr["ACHTransactionCode"];
                    //dtRow["Control1"] = dr["Control1"];
                    //dtRow["DestinationAccountType"] = dr["DestinationAccountType"];
                    //dtRow["LedgerFolioNumber"] = dr["LedgerFolioNumber"];
                    //dtRow["Control2"] = dr["Control2"];
                    //dtRow["BeneficiaryAccountHoldersName"] = dr["BeneficiaryAccountHoldersName"];
                    //dtRow["Control3"] = dr["Control3"];
                    //dtRow["Control4"] = dr["Control4"];
                    //dtRow["UserName"] = dr["UserName"];
                    //dtRow["Control5"] = dr["Control5"];
                    //dtRow["Amount"] = dr["Amount"];
                    //dtRow["ACHItemSeqNo"] = dr["ACHItemSeqNo"];
                    //dtRow["Checksum"] = dr["Checksum"];
                    //dtRow["SuccessReturnFlag"] = dr["SuccessReturnFlag"];
                    //dtRow["ReasonCode"] = dr["ReasonCode"];
                    //dtRow["DestinationBankIFSC_MICR_IIN"] = dr["DestinationBankIFSC_MICR_IIN"];
                    //dtRow["BeneficiaryBankAccountnumber"] = dr["BeneficiaryBankAccountnumber"];
                    //dtRow["SponsorBankIFSC_MICR_IIN"] = dr["SponsorBankIFSC_MICR_IIN"];
                    //dtRow["UserNumber"] = dr["UserNumber"];
                    //dtRow["TransactionReference"] = dr["TransactionReference"];
                    //dtRow["ProductType"] = dr["ProductType"];
                    //dtRow["BeneficiaryAadhaarNumber"] = dr["BeneficiaryAadhaarNumber"];
                    //dtRow["UMRN"] = dr["UMRN"];
                    dtRow["Warrant_Date"] = dr["Warrant Date"];
                    dtRow["ECS_Transaction_Code"] = dr["ECS Transaction Code"];
                    dtRow["Destination_Short_Code"] = dr["Destination Short Code"];
                    dtRow["Destination_Acc_Type"] = dr["Destination Acc Type"];
                    dtRow["Ledger_Folio_Number"] = dr["Ledger Folio Number"];
                    dtRow["Destination_Acc_No"] = dr["Destination Acc No"];
                    dtRow["Destination_Acc_Holder_Name"] = dr["Destination Acc Holder's Name"];
                    dtRow["Sponsor_Bank_Branch_Code"] = dr["Sponsor Bank Branch Code"];
                    dtRow["User_Number"] = dr["User Number"];
                    dtRow["User_Name"] = dr["User Name"];
                    dtRow["Warrant_NO"] = dr["User Debit Reference(Unique) no duplicate allowed"];
                    dtRow["Amount"] = dr["Amount"];
                    dtRow["Reserved1"] = dr["Reserved(to be kept blank by user)"];
                    dtRow["Reserved2"] = dr["Reserved(to be kept blank by user)1"];
                    dtRow["Reserved3"] = dr["Reserved(to be kept blank by user)2"];
                    dtRow["Filler"] = dr["Filler"];
                    dtRow["System_Request_Status"] = dr["System Request Status"];
                    dtRow["Response_Code"] = dr["Response Code"];
                    dtRow["RBI_SEQ_Number"] = dr["RBI SEQ Number"];



                    dtMain.Rows.Add(dtRow);
                }
                dtMain.AcceptChanges();

                using (DataSet ds = objDAL.SaveData_BankValidationNACHSOFTPayment(dtMain, objBO, objSBO))
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string status = ds.Tables[0].Rows[0][0].ToString();
                        string msg = ds.Tables[0].Rows[0][1].ToString();

                        if (status == "1")
                        {
                            res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
                        }
                        else
                        {
                            throw new Exception(msg);
                        }
                    }
                }
            }

            return res;
        }

        private string BankRevalidation_DD_ACH_REJECT_RecoProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            string res = "";
            System.Data.DataTable MainData = new System.Data.DataTable();
            string[] arr = new string[31];
            arr[0] = "ACCNAME";
            arr[1] = "ACCNO";
            arr[2] = "DD Date";
            arr[3] = "WARRANTDATE";
            arr[4] = "REFNO";
            arr[5] = "BENENAME";
            arr[6] = "BENEBANKNAME";
            arr[7] = "BENEACCNO";
            arr[8] = "DRAFTAMT";
            arr[9] = "FOLIONO";
            arr[10] = "PYMNTDETLINE5";
            arr[11] = "PYMNTDETLINE6";
            arr[12] = "PYMNTDETLINE7";
            arr[13] = "BENEADDLINE1";
            arr[14] = "BENEADDLINE2";
            arr[15] = "BENEADDLINE3";
            arr[16] = "BENEADDLINE4";
            arr[17] = "BENEADDLINE5";
            arr[18] = "DDNO";
            arr[19] = "INSTDATE";
            arr[20] = "ORIGINALINSTTYPE";
            arr[21] = "ENRICHMENTFIELD1";
            arr[22] = "ENRICHMENTFIELD2";
            arr[23] = "ENRICHMENTFIELD3";
            arr[24] = "ENRICHMENTFIELD4";
            arr[25] = "ENRICHMENTFIELD5";
            arr[26] = "ENRICHMENTFIELD6";
            arr[27] = "ENRICHMENTFIELD7";
            arr[28] = "ENRICHMENTFIELD8";
            arr[29] = "ENRICHMENTFIELD9";
            arr[30] = "ENRICHMENTFIELD10";

            if (dt.Columns.Count == arr.Count())
            {
                for (int i = 0; i < arr.Count(); i++)
                {
                    if (Convert.ToString(arr[i]).Trim().ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).Trim().ToUpper())
                    {
                        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
                        break;
                    }
                }
            }
            else
            {
                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
            }

            if (res == "")
            {
                objDAL = new ReconciliationProcessDAL();

                System.Data.DataTable dtMain = new System.Data.DataTable();

                dtMain.Columns.Add("ACCNAME");
                dtMain.Columns.Add("ACCNO");
                dtMain.Columns.Add("DD_Date");
                dtMain.Columns.Add("WARRANTDATE");
                dtMain.Columns.Add("REFNO");
                dtMain.Columns.Add("BENENAME");
                dtMain.Columns.Add("BENEBANKNAME");
                dtMain.Columns.Add("BENEACCNO");
                dtMain.Columns.Add("DRAFTAMT");
                dtMain.Columns.Add("FOLIONO");
                dtMain.Columns.Add("PYMNTDETLINE5");
                dtMain.Columns.Add("PYMNTDETLINE6");
                dtMain.Columns.Add("PYMNTDETLINE7");
                dtMain.Columns.Add("BENEADDLINE1");
                dtMain.Columns.Add("BENEADDLINE2");
                dtMain.Columns.Add("BENEADDLINE3");
                dtMain.Columns.Add("BENEADDLINE4");
                dtMain.Columns.Add("BENEADDLINE5");
                dtMain.Columns.Add("DDNO");
                dtMain.Columns.Add("INSTDATE");
                dtMain.Columns.Add("ORIGINALINSTTYPE");
                dtMain.Columns.Add("ENRICHMENTFIELD1");
                dtMain.Columns.Add("ENRICHMENTFIELD2");
                dtMain.Columns.Add("ENRICHMENTFIELD3");
                dtMain.Columns.Add("ENRICHMENTFIELD4");
                dtMain.Columns.Add("ENRICHMENTFIELD5");
                dtMain.Columns.Add("ENRICHMENTFIELD6");
                dtMain.Columns.Add("ENRICHMENTFIELD7");
                dtMain.Columns.Add("ENRICHMENTFIELD8");
                dtMain.Columns.Add("ENRICHMENTFIELD9");
                dtMain.Columns.Add("ENRICHMENTFIELD10");

                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dtRow = dtMain.NewRow();

                    dtRow["ACCNAME"] = dr["ACCNAME"];
                    dtRow["ACCNO"] = dr["ACCNO"];
                    dtRow["DD_Date"] = dr["DD Date"];
                    dtRow["WARRANTDATE"] = dr["WARRANTDATE"];
                    dtRow["REFNO"] = dr["REFNO"];
                    dtRow["BENENAME"] = dr["BENENAME"];
                    dtRow["BENEBANKNAME"] = dr["BENEBANKNAME"];
                    dtRow["BENEACCNO"] = dr["BENEACCNO"];
                    dtRow["DRAFTAMT"] = dr["DRAFTAMT"];
                    dtRow["FOLIONO"] = dr["FOLIONO"];
                    dtRow["PYMNTDETLINE5"] = dr["PYMNTDETLINE5"];
                    dtRow["PYMNTDETLINE6"] = dr["PYMNTDETLINE6"];
                    dtRow["PYMNTDETLINE7"] = dr["PYMNTDETLINE7"];
                    dtRow["BENEADDLINE1"] = dr["BENEADDLINE1"];
                    dtRow["BENEADDLINE2"] = dr["BENEADDLINE2"];
                    dtRow["BENEADDLINE3"] = dr["BENEADDLINE3"];
                    dtRow["BENEADDLINE4"] = dr["BENEADDLINE4"];
                    dtRow["BENEADDLINE5"] = dr["BENEADDLINE5"];
                    dtRow["DDNO"] = dr["DDNO"];
                    dtRow["INSTDATE"] = dr["INSTDATE"];
                    dtRow["ORIGINALINSTTYPE"] = dr["ORIGINALINSTTYPE"];
                    dtRow["ENRICHMENTFIELD1"] = dr["ENRICHMENTFIELD1"];
                    dtRow["ENRICHMENTFIELD2"] = dr["ENRICHMENTFIELD2"];
                    dtRow["ENRICHMENTFIELD3"] = dr["ENRICHMENTFIELD3"];
                    dtRow["ENRICHMENTFIELD4"] = dr["ENRICHMENTFIELD4"];
                    dtRow["ENRICHMENTFIELD5"] = dr["ENRICHMENTFIELD5"];
                    dtRow["ENRICHMENTFIELD6"] = dr["ENRICHMENTFIELD6"];
                    dtRow["ENRICHMENTFIELD7"] = dr["ENRICHMENTFIELD7"];
                    dtRow["ENRICHMENTFIELD8"] = dr["ENRICHMENTFIELD8"];
                    dtRow["ENRICHMENTFIELD9"] = dr["ENRICHMENTFIELD9"];
                    dtRow["ENRICHMENTFIELD10"] = dr["ENRICHMENTFIELD10"];
                    dtMain.Rows.Add(dtRow);
                }
                dtMain.AcceptChanges();



                using (DataSet ds = objDAL.SaveData_BankValidationDD_ACH_REJECT(dtMain, objBO, objSBO))
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string status = ds.Tables[0].Rows[0][0].ToString();
                        string msg = ds.Tables[0].Rows[0][1].ToString();

                        if (status == "1")
                        {
                            res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
                        }
                        else
                        {
                            throw new Exception(msg);
                        }
                    }
                }
            }

            return res;
        }

        private string BankRevalWrappCommProcess(System.Data.DataTable dt, ReconciliationProcessBO objBO, SessionBO objSBO)
        {
            string res = "";
            System.Data.DataTable MainData = new System.Data.DataTable();
            string[] arr = new string[2];
            arr[0] = "DEP_NO";
            arr[1] = "FOLIO_NO";


            if (dt.Columns.Count == arr.Count())
            {
                for (int i = 0; i < arr.Count(); i++)
                {
                    if (Convert.ToString(arr[i]).ToUpper() != Convert.ToString(dt.Columns[i].ColumnName).ToUpper())
                    {
                        res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "Column Name did Not Match " + dt.Columns[i].ColumnName + " .Please use given template." });
                        break;
                    }
                }
            }
            else
            {
                res = JsonConvert.SerializeObject(new { Status = "0", Message = "Error", Data = "No. of Columns to upload file should be " + arr.Count().ToString() + " and be in given format.Please use given template." });
            }

            if (res == "")
            {
                objDAL = new ReconciliationProcessDAL();

                System.Data.DataTable dtMain = new System.Data.DataTable();
                dtMain.Columns.Add("DEP_NO");
                dtMain.Columns.Add("FOLIO_NO");

                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dtRow = dtMain.NewRow();
                    dtRow["DEP_NO"] = dr["DEP_NO"];
                    dtRow["FOLIO_NO"] = dr["FOLIO_NO"];
                    dtMain.Rows.Add(dtRow);
                }
                dtMain.AcceptChanges();



                using (DataSet ds = objDAL.SaveData_BankRevalWrappCommUpload(dtMain, objBO, objSBO))
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string status = ds.Tables[0].Rows[0][0].ToString();
                        string msg = ds.Tables[0].Rows[0][1].ToString();

                        if (status == "1")
                        {
                            res = JsonConvert.SerializeObject(new { Status = "1", Message = "Success", Data = msg });
                        }
                        else
                        {
                            throw new Exception(msg);
                        }
                    }
                }
            }

            return res;
        }
    }
}
