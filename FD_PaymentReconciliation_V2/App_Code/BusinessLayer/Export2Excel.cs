using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.AspNetCore.Http;

/// <summary>
/// Summary description for Export2Excel
/// </summary>
public class Export2Excel
{
    public Export2Excel()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //public static void DataTableToExcel(DataTable tbl, string filename, HttpResponse response)
    //{

    //    using (ExcelPackage pck = new ExcelPackage())
    //    {
    //        //Create the worksheet
    //        ExcelWorksheet ws = pck.Workbook.Worksheets.Add(filename);

    //        //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
    //        ws.Cells["A1"].LoadFromDataTable(tbl, true);

    //        //Format the header for column 1-3
    //        using (ExcelRange rng = ws.Cells["A1:FZ1"])
    //        {
    //            rng.Style.Font.Bold = true;
    //            // rng.Style.Fill.PatternType = ExcelFillStyle.Solid;

    //            // //Set Pattern for the background to Solid
    //            //// rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));

    //            // //Set color to dark blue
    //            // rng.Style.Font.Color.SetColor(Color.White);
    //        }

    //        //Example how to Format Column 1 as numeric
    //        using (ExcelRange col = ws.Cells[2, 1, 2 + tbl.Rows.Count, 1])
    //        {
    //            col.Style.Numberformat.Format = "#";
    //            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
    //        }


    //        //Write it back to the client
    //        response.Clear();
    //        response.AddHeader("content-disposition", "attachment; filename=" + filename + ".xlsx");
    //        response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //        response.BinaryWrite(pck.GetAsByteArray());
    //        response.End();
    //    }
    //}

    public static string  DataTableToBytesArray(DataTable tbl, string filename,string FilePath)
    {

        using (ExcelPackage pck = new ExcelPackage())
        {
           
            //Create the worksheet
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add(filename);

            //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells["A1"].LoadFromDataTable(tbl, true);

            //Format the header for column 1-3
            using (ExcelRange rng = ws.Cells["A1:FZ1"])
            {
                rng.Style.Font.Bold = true;
                
            }

            //Example how to Format Column 1 as numeric
            using (ExcelRange col = ws.Cells[2, 1, 2 + tbl.Rows.Count, 1])
            {
                col.Style.Numberformat.Format = "#";
                col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }
            File.WriteAllBytes(FilePath, pck.GetAsByteArray());
            
            return FilePath;
        }
    }

    /// <summary>
    /// This Method is used for passing Sheet Name also.
    /// </summary>
    /// <param name="tbl"></param>
    /// <param name="filename"></param>
    /// <param name="response"></param>
    //public static void DataTableToExcel(DataTable tbl, string filename, string sheetname, HttpResponse response)
    //{
    //    using (ExcelPackage pck = new ExcelPackage())
    //    {
    //        //Create the worksheet
    //        ExcelWorksheet ws = pck.Workbook.Worksheets.Add(sheetname);

    //        //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
    //        ws.Cells["A1"].LoadFromDataTable(tbl, true);

    //        //Format the header for column 1-3
    //        using (ExcelRange rng = ws.Cells["A1:FZ1"])
    //        {
    //            rng.Style.Font.Bold = true;
    //            // rng.Style.Fill.PatternType = ExcelFillStyle.Solid;

    //            // //Set Pattern for the background to Solid
    //            //// rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));

    //            // //Set color to dark blue
    //            // rng.Style.Font.Color.SetColor(Color.White);
    //        }

    //        //Example how to Format Column 1 as numeric
    //        using (ExcelRange col = ws.Cells[2, 1, 2 + tbl.Rows.Count, 1])
    //        {
    //            col.Style.Numberformat.Format = "#";
    //            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
    //        }


    //        //Write it back to the client
    //        response.Clear();
    //        response.AddHeader("content-disposition", "attachment; filename=" + filename + ".xlsx");
    //        response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //        response.BinaryWrite(pck.GetAsByteArray());
    //        response.End();
    //    }

    //}

    //public static void DataTableToExcel(DataTable tbl, string filename, HttpResponse response, string contentType)
    //{
    //    if (contentType == "xlsx" || contentType == "xls")
    //    {
    //        using (ExcelPackage pck = new ExcelPackage())
    //        {
    //            //Create the worksheet
    //            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");

    //            //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
    //            ws.Cells["A1"].LoadFromDataTable(tbl, true);

    //            //Format the header for column 1-3
    //            using (ExcelRange rng = ws.Cells["A1:ZZ1"])
    //            {
    //                rng.Style.Font.Bold = true;
    //                // rng.Style.Fill.PatternType = ExcelFillStyle.Solid;

    //                // //Set Pattern for the background to Solid
    //                //// rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));

    //                // //Set color to dark blue
    //                // rng.Style.Font.Color.SetColor(Color.White);
    //            }

    //            //Example how to Format Column 1 as numeric
    //            using (ExcelRange col = ws.Cells[2, 1, 2 + tbl.Rows.Count, 1])
    //            {
    //                col.Style.Numberformat.Format = "#";
    //                col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
    //            }

    //            //Write it back to the client
    //            response.Clear();
    //            response.AddHeader("content-disposition", "attachment; filename=" + filename + "." + contentType);
    //            response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    //            response.BinaryWrite(pck.GetAsByteArray());
    //            response.End();
    //        }

    //    }
    //    else if (contentType == "txt" || contentType == "TSV")
    //    {
    //        var text = GetTextFromDataTable(tbl);  //Export in TSV          
    //        response.Clear();
    //        response.AddHeader("content-disposition", "attachment;filename=" + filename + "." + contentType);
    //        response.Charset = "";
    //        response.Cache.SetCacheability(HttpCacheability.NoCache);
    //        response.ContentType = "application/vnd.text";
    //        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
    //        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
    //        response.Write(text.ToString());
    //        response.End();
    //    }
    //    else if (contentType == "CSV")
    //    {
    //        var text = GetCSVfromDatatable(tbl);  //Export in CSV          
    //        response.Clear();
    //        response.AddHeader("content-disposition", "attachment;filename=" + filename + "." + contentType);
    //        response.Charset = "";
    //        response.Cache.SetCacheability(HttpCacheability.NoCache);
    //        response.ContentType = "application/vnd.text";
    //        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
    //        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
    //        response.Write(text.ToString());
    //        response.End();
    //    }
    //}

    /// <summary>
    /// Data Export to CSV format
    /// </summary>
    /// <param name="dt">pass datatable parameter</param>
    /// <returns></returns>
    private static string GetCSVfromDatatable(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();

        IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
        sb.AppendLine(string.Join(",", columnNames));

        foreach (DataRow row in dt.Rows)
        {
            IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
            sb.AppendLine(string.Join(",", fields));
        }
        return sb.ToString();
    }

    /// <summary>
    /// Data Export to TSV or Text Format
    /// </summary>
    /// <param name="dataTable">pass datatable parameter</param>
    /// <returns></returns>
    private static string GetTextFromDataTable(DataTable dataTable)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(string.Join("\t", dataTable.Columns.Cast<DataColumn>().Select(arg => arg.ColumnName)));
        foreach (DataRow dataRow in dataTable.Rows)
            stringBuilder.AppendLine(string.Join("\t", dataRow.ItemArray.Select(arg => arg.ToString())));
        return stringBuilder.ToString();
    }


    public static string SaveDataTableToExcel(DataTable tbl, string filename, string FilePath)
    {

        using (ExcelPackage pck = new ExcelPackage())
        {

            //Create the worksheet
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add(filename);

            //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells["A1"].LoadFromDataTable(tbl, true);

            //Format the header for column 1-3
            using (ExcelRange rng = ws.Cells["A1:FZ1"])
            {
                rng.Style.Font.Bold = true;

            }

            //Example how to Format Column 1 as numeric
            using (ExcelRange col = ws.Cells[2, 1, 2 + tbl.Rows.Count, 1])
            {
                col.Style.Numberformat.Format = "#";
                col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }
            File.WriteAllBytes(FilePath, pck.GetAsByteArray());

            return FilePath;
        }
    }

    /// <summary>
    /// This Method is used for passing Sheet Name also.
    /// </summary>
    /// <param name="tbl"></param>
    /// <param name="filename"></param>
    /// <param name="response"></param>

    public static bool SaveDataTableToExcel(string strFilePath, DataTable dt)
    {
        try
        {
            string path = strFilePath;

            using (ExcelPackage p = new ExcelPackage())
            {
                ExcelWorksheet ws = CreateSheet(p, "Sheet1");
                int rowIndex = 1;
                CreateHeader(ws, ref rowIndex, dt);
                CreateData(ws, ref rowIndex, dt);
                Byte[] bin = p.GetAsByteArray();
                string file = path;
                File.WriteAllBytes(file, bin);
                p.Dispose();
            }
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private static ExcelWorksheet CreateSheet(ExcelPackage p, string sheetName)
    {
        p.Workbook.Worksheets.Add(sheetName);
        ExcelWorksheet ws = p.Workbook.Worksheets[1];
        ws.Name = sheetName; //Setting Sheet's name
        return ws;
    }
    private static void CreateData(ExcelWorksheet ws, ref int rowIndex, DataTable dt)
    {
        int colIndex = 0;
        foreach (DataRow dr in dt.Rows) // Adding Data into rows
        {
            colIndex = 1;
            rowIndex++;
            foreach (DataColumn dc in dt.Columns)
            {
                var cell = ws.Cells[rowIndex, colIndex];

                //Setting Value in cell
                cell.Value = dr[dc.ColumnName];

                //Setting borders of cell
                var border = cell.Style.Border;
                border.Left.Style = border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                colIndex++;
            }
        }
    }

    private static void CreateHeader(ExcelWorksheet ws, ref int rowIndex, DataTable dt)
    {
        int colIndex = 1;
        foreach (DataColumn dc in dt.Columns) //Creating Headings
        {
            var cell = ws.Cells[rowIndex, colIndex];

            //Setting the background color of header cells to Gray
            //var fill = cell.Style.Fill;
            //fill.PatternType = ExcelFillStyle.Solid;
            //fill.BackgroundColor.SetColor(Color.Gray);

            //Setting Top/left,right/bottom borders.
            var border = cell.Style.Border;
            //border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

            //Setting Value in cell
            cell.Value = dc.ColumnName;

            colIndex++;
        }
    }
}
