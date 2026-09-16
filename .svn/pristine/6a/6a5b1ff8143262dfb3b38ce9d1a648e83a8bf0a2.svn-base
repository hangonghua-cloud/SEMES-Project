using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Drawing;
using NPOI.HSSF.Util;
using System;
using System.Collections.Generic;
using NPOI.XSSF.UserModel;
using ALP.Util.WebControl;
//using ALP.Application.UtilExtend.Json;
//using ALP.Util;
using ALP.Application.UtilExtend;
using ALP.Application.UtilExtend.Offices;//.Util.WebControl;
using NPOI.SS.Util;

namespace ALP.Application.UtilExtend.Offices
{
    public class ExcelHelper : IDisposable
    {
        private IWorkbook workbook = new HSSFWorkbook();
        private bool disposed;

        public ExcelHelper()
        {
            disposed = false;
        }

        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <param name="data">导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <returns>文件流</returns>
        public MemoryStream DataTableToExcel(string sheetName, DataTable data, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return null;
                }

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    //定义字体
                    IFont cellFont = workbook.CreateFont();
                    cellFont.Boldweight = (short)FontBoldWeight.Bold;
                    //注意事项：样式定义要在循环之外，不然生成的EXCEL表第21行以后样式丢失 刘万军 2019-09-18
                    //定义样式
                    ICellStyle cellStyle = workbook.CreateCellStyle();
                    //背景色
                    //cellStyle.FillForegroundColor = HSSFColor.PaleBlue.Index; 
                    //标题 背景色
                    cellStyle.FillForegroundColor = HSSFColor.Grey25Percent.Index;
                    cellStyle.FillPattern = FillPattern.SolidForeground;
                    cellStyle.Alignment = HorizontalAlignment.Center;
                    //cellStyle.Alignment = HorizontalAlignment.Left;
                    cellStyle.VerticalAlignment = VerticalAlignment.Center;
                    cellStyle.BorderTop = BorderStyle.Thin;
                    cellStyle.BorderBottom = BorderStyle.Thin;
                    cellStyle.BorderLeft = BorderStyle.Thin;
                    cellStyle.BorderRight = BorderStyle.Thin;
                    cellStyle.SetFont(cellFont);

                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        ICell c = row.CreateCell(j);
                        c.SetCellValue(data.Columns[j].ColumnName);
                        //设置单元格样式
                        c.CellStyle = cellStyle;

                        int n = UTF8Encoding.Default.GetBytes(data.Columns[j].ColumnName).Length;
                        //是否汉字
                        if (IsHZ(data.Columns[j].ColumnName))
                        {
                            sheet.SetColumnWidth(j, n * 400 + (n - data.Columns[j].ColumnName.Length) * 200 + 1800);
                        }
                        else
                        {
                            sheet.SetColumnWidth(j, n * 400 + (n - data.Columns[j].ColumnName.Length) * 200 + 1000);
                        }
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }

                //注意事项：样式定义要在循环之外，不然生成的EXCEL表第21行以后样式丢失 刘万军 2019-09-18
                //定义样式
                ICellStyle cellStyle_Columns = workbook.CreateCellStyle();
                cellStyle_Columns.Alignment = HorizontalAlignment.Center;
                cellStyle_Columns.VerticalAlignment = VerticalAlignment.Center;
                cellStyle_Columns.BorderTop = BorderStyle.Thin;
                cellStyle_Columns.BorderBottom = BorderStyle.Thin;
                cellStyle_Columns.BorderLeft = BorderStyle.Thin;
                cellStyle_Columns.BorderRight = BorderStyle.Thin;

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    //设置单元格的高度
                    row.Height = 18 * 20;
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        ICell c = row.CreateCell(j);
                        c.SetCellValue(data.Rows[i][j].ToString());
                        //设置单元格样式
                        c.CellStyle = cellStyle_Columns;

                        //设置单元格宽度
                        int n = UTF8Encoding.Default.GetBytes(data.Rows[i][j].ToString()).Length;
                        //是否汉字
                        if (IsHZ(data.Columns[j].ColumnName))
                        {
                            if (sheet.GetColumnWidth(j) < n * 400 + (n - data.Rows[i][j].ToString().Length) * 200 + 3000)
                            {
                                if (n * 400 + (n - data.Rows[i][j].ToString().Length) * 200 + 3000 > 65000)
                                {
                                    sheet.SetColumnWidth(j, 65000);
                                }
                                else
                                {
                                    sheet.SetColumnWidth(j, n * 400 + (n - data.Rows[i][j].ToString().Length) * 200 + 3000);
                                }
                            }
                        }
                        else
                        {
                            if (sheet.GetColumnWidth(j) < n * 400 + (n - data.Rows[i][j].ToString().Length) * 200 + 1000)
                            {
                                if (n * 400 + (n - data.Rows[i][j].ToString().Length) * 200 + 1000 > 65000)
                                {
                                    sheet.SetColumnWidth(j, 65000);
                                }
                                else
                                {
                                    sheet.SetColumnWidth(j, n * 400 + (n - data.Rows[i][j].ToString().Length) * 200 + 1000);
                                }
                            }
                        }


                    }
                    ++count;
                }

                MemoryStream ms = new MemoryStream();
                workbook.Write(ms);
                return ms;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 检测内容是否汉字
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public bool IsHZ(string content)
        {
            try
            {
                char[] c = content.ToCharArray();
                if (c[0] >= 0x4e00 && c[0] <= 0x9fbb)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return true;
            }
        }
        /// <summary>
        /// 导出补料单
        /// jpf
        /// </summary>
        /// <param name="data1"></param>
        /// <param name="isColumnWritten"></param>
        /// <returns></returns>
        public MemoryStream DataTableToExcelWorkOrder(DataTable data1, bool isColumnWritten)
        {
            ISheet sheet1 = null;
            int count = 0;
            int i = 0;
            int j = 0;
            try
            {
                if (workbook != null)
                {
                    sheet1 = workbook.CreateSheet("补料单明细");
                    sheet1.PrintSetup.Landscape = true;
                    sheet1.PrintSetup.PaperSize = 9;
                    if (isColumnWritten == true) //写入DataTable的列名
                    {
                        //定义字体
                        IFont cellFont = workbook.CreateFont();
                        cellFont.Boldweight = (short)FontBoldWeight.Bold;
                        cellFont.FontHeightInPoints = 15;
                        //注意事项：样式定义要在循环之外，不然生成的EXCEL表第21行以后样式丢失 刘万军 2019-09-18
                        //定义样式
                        ICellStyle cellStyle = workbook.CreateCellStyle();
                        cellStyle.FillForegroundColor = HSSFColor.PaleBlue.Index; //背景色
                        cellStyle.FillPattern = FillPattern.SolidForeground;
                        cellStyle.Alignment = HorizontalAlignment.Center;
                        cellStyle.SetFont(cellFont);

                        ICellStyle cellStyleBT = workbook.CreateCellStyle();//声明样式
                        cellStyleBT.Alignment = HorizontalAlignment.Center;//水平居中
                        cellStyleBT.VerticalAlignment = VerticalAlignment.Center;//垂直居中
                        IFont font = workbook.CreateFont();//声明字体
                        font.Boldweight = (Int16)FontBoldWeight.Bold;//加粗
                        font.FontHeightInPoints = 28;//字体大小
                        cellStyleBT.SetFont(font);//加入单元格

                        IRow row0 = sheet1.CreateRow(0);//创建行
                        row0.HeightInPoints = 35;//行高
                        ICell cell0 = row0.CreateCell(0);//创建单元格
                        cell0.SetCellValue("泰州华丽塑料有限公司成品补料单");//赋值
                        cell0.CellStyle = cellStyleBT;//设置样式
                        sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, data1.Columns.Count - 1));//合并单元格（第几行，到第几行，第几列，到第几列）

                        IRow row = sheet1.CreateRow(1);
                        for (j = 0; j < data1.Columns.Count; ++j)
                        {
                            ICell c = row.CreateCell(j);
                            c.SetCellValue(data1.Columns[j].ColumnName);
                            c.CellStyle = cellStyle; //设置单元格样式

                            int n = UTF8Encoding.Default.GetBytes(data1.Columns[j].ColumnName).Length;

                            n = n + 4;
                            sheet1.SetColumnWidth(j, n * 300 + (n - data1.Columns[j].ColumnName.Length) * 250);
                        }
                        count = 2;
                    }
                    else
                    {
                        count = 0;
                    }
                    //注意事项：样式定义要在循环之外，不然生成的EXCEL表第21行以后样式丢失 刘万军 2019-09-18
                    //定义样式
                    ICellStyle cellStyle_Columns = workbook.CreateCellStyle();
                    cellStyle_Columns.Alignment = HorizontalAlignment.Center;
                    cellStyle_Columns.VerticalAlignment = VerticalAlignment.Center;
                    IFont cellFont3 = workbook.CreateFont();
                    cellFont3.FontHeightInPoints = 15;
                    cellStyle_Columns.SetFont(cellFont3);


                    for (i = 0; i < data1.Rows.Count; ++i)
                    {
                        IRow row = sheet1.CreateRow(count);
                        for (j = 0; j < data1.Columns.Count; ++j)
                        {
                            ICell c = row.CreateCell(j);
                            c.SetCellValue(data1.Rows[i][j].ToString());
                            c.CellStyle = cellStyle_Columns; //设置单元格样式

                            //设置单元格宽度
                            int n = UTF8Encoding.Default.GetBytes(data1.Rows[i][j].ToString()).Length;

                            n = n + 4;
                            if (sheet1.GetColumnWidth(j) < n * 300 + (n - data1.Rows[i][j].ToString().Length) * 150)
                            {
                                sheet1.SetColumnWidth(j, n * 300 + (n - data1.Rows[i][j].ToString().Length) * 150);
                            }
                        }
                        ++count;
                    }
                    ICellStyle cellStyle_Columnnew = workbook.CreateCellStyle();
                    cellStyle_Columnnew.Alignment = HorizontalAlignment.Center;
                    cellStyle_Columnnew.VerticalAlignment = VerticalAlignment.Center;
                    IFont fontnew = workbook.CreateFont();//声明字体
                    fontnew.Boldweight = (Int16)FontBoldWeight.Bold;//加粗
                    fontnew.FontHeightInPoints = 15;
                    cellStyle_Columnnew.SetFont(fontnew);//加入单元格
                    IRow rows = sheet1.CreateRow(count + 3);
                    ICell cs = rows.CreateCell(0);
                    cs.CellStyle = cellStyle_Columnnew;
                    cs.SetCellValue("开单人:");


                    ICell cf = rows.CreateCell(2);
                    cf.CellStyle = cellStyle_Columnnew;
                    cf.SetCellValue("计划人员:");

                    ICell csc = rows.CreateCell(4);
                    csc.CellStyle = cellStyle_Columnnew;
                    csc.SetCellValue("生产部经理:");
                    ICell csh = rows.CreateCell(6);
                    csh.CellStyle = cellStyle_Columnnew;
                    csh.SetCellValue("日期:");


                    MemoryStream ms = new MemoryStream();
                    workbook.Write(ms);
                    return ms;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <param name="data">导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <returns>文件流</returns>
        public MemoryStream DataTableToExcelProductionOrder(string sheetName, DataTable data, DataTable data1, DataTable data2, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;
            ISheet sheet1 = null;
            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                    sheet.PrintSetup.Landscape = true;
                    sheet.PrintSetup.PaperSize = 9;
                    sheet1 = workbook.CreateSheet("销售订单明细");
                    sheet1.PrintSetup.Landscape = true;
                    sheet.PrintSetup.PaperSize = 9;
                }
                else
                {
                    return null;
                }

                //定义字体
                IFont cellFont1 = workbook.CreateFont();
                cellFont1.Boldweight = (short)FontBoldWeight.Bold;



                //定义样式
                ICellStyle cellStylePR = workbook.CreateCellStyle();
                cellStylePR.VerticalAlignment = VerticalAlignment.Center;
                cellStylePR.Alignment = HorizontalAlignment.Center;
                cellStylePR.FillForegroundColor = HSSFColor.PaleBlue.Index; //背景色
                cellStylePR.FillPattern = FillPattern.SolidForeground;
                cellStylePR.SetFont(cellFont1);

                //工艺要求内容样式
                ICellStyle cellStylePRgy = workbook.CreateCellStyle();
                cellStylePRgy.VerticalAlignment = VerticalAlignment.Top;
                cellStylePRgy.Alignment = HorizontalAlignment.Left;
                cellStylePRgy.WrapText = true;
                IFont fontgy = workbook.CreateFont();//声明字体
                fontgy.FontHeightInPoints = 17;//字体大小
                cellStylePRgy.SetFont(fontgy);//加入单元格
                int n1 = UTF8Encoding.Default.GetBytes(data.Rows[0][8].ToString()).Length;

                sheet.AddMergedRegion(new CellRangeAddress(4, 50, 1, 4));
                sheet.AddMergedRegion(new CellRangeAddress(4, 50, 0, 0));


                IRow row1 = sheet.CreateRow(1);
                ICell c0 = row1.CreateCell(0);
                c0.SetCellValue("订单号");
                c0.CellStyle = cellStylePR;
                ICell c1 = row1.CreateCell(1);
                c1.SetCellValue(data.Rows[0][0].ToString());
                ICell c2 = row1.CreateCell(3);
                c2.SetCellValue("下单日期");
                c2.CellStyle = cellStylePR;
                ICell c3 = row1.CreateCell(4);
                c3.SetCellValue(DateTime.Parse(data.Rows[0][4].ToString()).ToString("yyyy-MM-dd hh:mm:ss"));

                IRow row2 = sheet.CreateRow(2);
                ICell c4 = row2.CreateCell(0);
                c4.SetCellValue("生产计划号");
                c4.CellStyle = cellStylePR;
                ICell c5 = row2.CreateCell(1);
                c5.SetCellValue(data.Rows[0][3].ToString());
                ICell c6 = row2.CreateCell(3);
                c6.SetCellValue("交货日期");
                c6.CellStyle = cellStylePR;
                ICell c7 = row2.CreateCell(4);
                c7.SetCellValue(DateTime.Parse(data.Rows[0][5].ToString()).ToString("yyyy-MM-dd hh:mm:ss"));

                IRow row3 = sheet.CreateRow(3);
                ICell c8 = row3.CreateCell(0);
                c8.SetCellValue("纸盒日期");
                c8.CellStyle = cellStylePR;
                ICell c9 = row3.CreateCell(1);
                c9.SetCellValue(DateTime.Parse(data.Rows[0][6].ToString()).ToString("yyyy-MM-dd hh:mm:ss"));

                IRow row4 = sheet.CreateRow(4);
                ICell c10 = row4.CreateCell(0);
                c10.SetCellValue("工艺要求");
                c10.CellStyle = cellStylePR;
                ICell c11 = row4.CreateCell(1);
                c11.SetCellValue(data.Rows[0][8].ToString());
                c11.CellStyle = cellStylePRgy;


                sheet.SetColumnWidth(1, 50 * 256);
                sheet.SetColumnWidth(4, 50 * 256);
                if (isColumnWritten == true) //写入DataTable的列名
                {
                    //定义字体
                    IFont cellFont = workbook.CreateFont();
                    cellFont.Boldweight = (short)FontBoldWeight.Bold;
                    cellFont.FontHeightInPoints = 15;
                    //注意事项：样式定义要在循环之外，不然生成的EXCEL表第21行以后样式丢失 刘万军 2019-09-18
                    //定义样式
                    ICellStyle cellStyle = workbook.CreateCellStyle();
                    cellStyle.FillForegroundColor = HSSFColor.PaleBlue.Index; //背景色
                    cellStyle.FillPattern = FillPattern.SolidForeground;
                    cellStyle.Alignment = HorizontalAlignment.Center;
                    cellStyle.SetFont(cellFont);

                    ICellStyle cellStyleBT = workbook.CreateCellStyle();//声明样式
                    cellStyleBT.Alignment = HorizontalAlignment.Center;//水平居中
                    cellStyleBT.VerticalAlignment = VerticalAlignment.Center;//垂直居中
                    IFont font = workbook.CreateFont();//声明字体
                    font.Boldweight = (Int16)FontBoldWeight.Bold;//加粗
                    font.FontHeightInPoints = 28;//字体大小
                    cellStyleBT.SetFont(font);//加入单元格

                    IRow row0 = sheet1.CreateRow(0);//创建行
                    row0.HeightInPoints = 35;//行高
                    ICell cell0 = row0.CreateCell(0);//创建单元格
                    cell0.SetCellValue("泰州华丽塑料有限公司生产计划单");//赋值
                    cell0.CellStyle = cellStyleBT;//设置样式
                    sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, data1.Columns.Count - 1));//合并单元格（第几行，到第几行，第几列，到第几列）

                    IRow row = sheet1.CreateRow(1);
                    for (j = 0; j < data1.Columns.Count; ++j)
                    {
                        ICell c = row.CreateCell(j);
                        c.SetCellValue(data1.Columns[j].ColumnName);
                        c.CellStyle = cellStyle; //设置单元格样式

                        int n = UTF8Encoding.Default.GetBytes(data1.Columns[j].ColumnName).Length;

                        n = n + 4;
                        sheet1.SetColumnWidth(j, n * 300 + (n - data1.Columns[j].ColumnName.Length) * 250);
                    }
                    count = 2;
                }
                else
                {
                    count = 0;
                }
                //注意事项：样式定义要在循环之外，不然生成的EXCEL表第21行以后样式丢失 刘万军 2019-09-18
                //定义样式
                ICellStyle cellStyle_Columns = workbook.CreateCellStyle();
                cellStyle_Columns.Alignment = HorizontalAlignment.Center;
                cellStyle_Columns.VerticalAlignment = VerticalAlignment.Center;
                IFont cellFont3 = workbook.CreateFont();
                cellFont3.FontHeightInPoints = 15;
                cellStyle_Columns.SetFont(cellFont3);
                var createName = data2.Rows[0]["制单人"].ToString();
                var auditName = data2.Rows[0]["审核人"].ToString();

                for (i = 0; i < data1.Rows.Count; ++i)
                {
                    IRow row = sheet1.CreateRow(count);
                    for (j = 0; j < data1.Columns.Count; ++j)
                    {
                        ICell c = row.CreateCell(j);
                        c.SetCellValue(data1.Rows[i][j].ToString());
                        c.CellStyle = cellStyle_Columns; //设置单元格样式

                        //设置单元格宽度
                        int n = UTF8Encoding.Default.GetBytes(data1.Rows[i][j].ToString()).Length;

                        n = n + 4;
                        if (sheet1.GetColumnWidth(j) < n * 300 + (n - data1.Rows[i][j].ToString().Length) * 150)
                        {
                            sheet1.SetColumnWidth(j, n * 300 + (n - data1.Rows[i][j].ToString().Length) * 150);
                        }
                    }
                    ++count;
                }
                ICellStyle cellStyle_Columnnew = workbook.CreateCellStyle();
                cellStyle_Columnnew.Alignment = HorizontalAlignment.Center;
                cellStyle_Columnnew.VerticalAlignment = VerticalAlignment.Center;
                IFont fontnew = workbook.CreateFont();//声明字体
                fontnew.Boldweight = (Int16)FontBoldWeight.Bold;//加粗
                fontnew.FontHeightInPoints = 15;
                cellStyle_Columnnew.SetFont(fontnew);//加入单元格
                IRow rows = sheet1.CreateRow(count + 3);
                ICell cs = rows.CreateCell(0);
                cs.CellStyle = cellStyle_Columnnew;
                cs.SetCellValue("制单人:");

                ICell cz = rows.CreateCell(1);
                cz.CellStyle = cellStyle_Columns;
                cz.SetCellValue(createName);
                ICell cf = rows.CreateCell(2);
                cf.CellStyle = cellStyle_Columnnew;
                cf.SetCellValue("复核:");
                ICell cshv = rows.CreateCell(3);
                cshv.CellStyle = cellStyle_Columns;
                cshv.SetCellValue(auditName);
                ICell csc = rows.CreateCell(5);
                csc.CellStyle = cellStyle_Columnnew;
                csc.SetCellValue("生产确认:");
                ICell csh = rows.CreateCell(8);
                csh.CellStyle = cellStyle_Columnnew;
                csh.SetCellValue("审批:");


                MemoryStream ms = new MemoryStream();
                workbook.Write(ms);
                return ms;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 将DataTable数据导入到excel中 生产下载导出模板
        /// </summary>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <param name="data">导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <returns>文件流</returns>
        public MemoryStream DataTableToExcelProductionOrder2(string sheetName, DataTable data, DataTable data1, DataTable data2, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;
            ISheet sheet1 = null;
            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                    sheet.PrintSetup.Landscape = true;
                    sheet.PrintSetup.PaperSize = 9;
                    sheet1 = workbook.CreateSheet("销售订单明细");
                    sheet1.PrintSetup.Landscape = true;
                    sheet.PrintSetup.PaperSize = 9;
                }
                else
                {
                    return null;
                }

                //定义字体
                IFont cellFont1 = workbook.CreateFont();
                cellFont1.Boldweight = (short)FontBoldWeight.Bold;



                //定义样式
                ICellStyle cellStylePR = workbook.CreateCellStyle();
                cellStylePR.VerticalAlignment = VerticalAlignment.Center;
                cellStylePR.Alignment = HorizontalAlignment.Center;
                cellStylePR.FillForegroundColor = HSSFColor.PaleBlue.Index; //背景色
                cellStylePR.FillPattern = FillPattern.SolidForeground;
                cellStylePR.SetFont(cellFont1);

                //工艺要求内容样式
                ICellStyle cellStylePRgy = workbook.CreateCellStyle();
                cellStylePRgy.VerticalAlignment = VerticalAlignment.Top;
                cellStylePRgy.Alignment = HorizontalAlignment.Left;
                cellStylePRgy.WrapText = true;
                IFont fontgy = workbook.CreateFont();//声明字体
                fontgy.FontHeightInPoints = 17;//字体大小
                cellStylePRgy.SetFont(fontgy);//加入单元格

                int n1 = UTF8Encoding.Default.GetBytes(data.Rows[0][8].ToString()).Length;

                sheet.AddMergedRegion(new CellRangeAddress(4, 50, 1, 4));
                sheet.AddMergedRegion(new CellRangeAddress(4, 50, 0, 0));


                IRow row1 = sheet.CreateRow(1);
                ICell c0 = row1.CreateCell(0);
                c0.SetCellValue("订单号");
                c0.CellStyle = cellStylePR;
                ICell c1 = row1.CreateCell(1);
                c1.SetCellValue(data.Rows[0][0].ToString());
                ICell c2 = row1.CreateCell(3);
                c2.SetCellValue("下单日期");
                c2.CellStyle = cellStylePR;
                ICell c3 = row1.CreateCell(4);
                c3.SetCellValue(DateTime.Parse(data.Rows[0][4].ToString()).ToString("yyyy-MM-dd hh:mm:ss"));

                IRow row2 = sheet.CreateRow(2);
                ICell c4 = row2.CreateCell(0);
                c4.SetCellValue("生产计划号");
                c4.CellStyle = cellStylePR;
                ICell c5 = row2.CreateCell(1);
                c5.SetCellValue(data.Rows[0][3].ToString());
                ICell c6 = row2.CreateCell(3);
                c6.SetCellValue("交货日期");
                c6.CellStyle = cellStylePR;
                ICell c7 = row2.CreateCell(4);
                c7.SetCellValue(DateTime.Parse(data.Rows[0][5].ToString()).ToString("yyyy-MM-dd hh:mm:ss"));

                IRow row3 = sheet.CreateRow(3);
                ICell c8 = row3.CreateCell(0);
                c8.SetCellValue("纸盒日期");
                c8.CellStyle = cellStylePR;
                ICell c9 = row3.CreateCell(1);
                c9.SetCellValue(DateTime.Parse(data.Rows[0][6].ToString()).ToString("yyyy-MM-dd hh:mm:ss"));

                IRow row4 = sheet.CreateRow(4);
                ICell c10 = row4.CreateCell(0);
                c10.SetCellValue("工艺要求");
                c10.CellStyle = cellStylePR;
                ICell c11 = row4.CreateCell(1);
                c11.SetCellValue(data.Rows[0][8].ToString());
                c11.CellStyle = cellStylePRgy;

                sheet.SetColumnWidth(1, 50 * 256);
                sheet.SetColumnWidth(4, 50 * 256);
                if (isColumnWritten == true) //写入DataTable的列名
                {
                    //定义字体
                    IFont cellFont = workbook.CreateFont();
                    cellFont.Boldweight = (short)FontBoldWeight.Bold;
                    cellFont.FontHeightInPoints = 15;
                    //注意事项：样式定义要在循环之外，不然生成的EXCEL表第21行以后样式丢失 刘万军 2019-09-18
                    //定义样式
                    ICellStyle cellStyle = workbook.CreateCellStyle();
                    cellStyle.FillForegroundColor = HSSFColor.PaleBlue.Index; //背景色
                    cellStyle.FillPattern = FillPattern.SolidForeground;
                    cellStyle.Alignment = HorizontalAlignment.Center;
                    cellStyle.SetFont(cellFont);

                    ICellStyle cellStyleBT = workbook.CreateCellStyle();//声明样式
                    cellStyleBT.Alignment = HorizontalAlignment.Center;//水平居中
                    cellStyleBT.VerticalAlignment = VerticalAlignment.Center;//垂直居中
                    IFont font = workbook.CreateFont();//声明字体
                    font.Boldweight = (Int16)FontBoldWeight.Bold;//加粗
                    font.FontHeightInPoints = 28;//字体大小
                    cellStyleBT.SetFont(font);//加入单元格

                    IRow row0 = sheet1.CreateRow(0);//创建行
                    row0.HeightInPoints = 35;//行高
                    ICell cell0 = row0.CreateCell(0);//创建单元格
                    cell0.SetCellValue("泰州华丽塑料有限公司生产计划单（" + data.Rows[0][1].ToString() + "）");//赋值
                    cell0.CellStyle = cellStyleBT;//设置样式
                    sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, data1.Columns.Count - 1));//合并单元格（第几行，到第几行，第几列，到第几列）

                    IRow row = sheet1.CreateRow(1);
                    for (j = 0; j < data1.Columns.Count; ++j)
                    {
                        ICell c = row.CreateCell(j);
                        c.SetCellValue(data1.Columns[j].ColumnName);
                        c.CellStyle = cellStyle; //设置单元格样式

                        int n = UTF8Encoding.Default.GetBytes(data1.Columns[j].ColumnName).Length;

                        n = n + 4;
                        sheet1.SetColumnWidth(j, n * 300 + (n - data1.Columns[j].ColumnName.Length) * 250);
                    }
                    count = 2;
                }
                else
                {
                    count = 0;
                }
                //注意事项：样式定义要在循环之外，不然生成的EXCEL表第21行以后样式丢失 刘万军 2019-09-18
                //定义样式
                ICellStyle cellStyle_Columns = workbook.CreateCellStyle();
                cellStyle_Columns.Alignment = HorizontalAlignment.Center;
                cellStyle_Columns.VerticalAlignment = VerticalAlignment.Center;
                IFont cellFont3 = workbook.CreateFont();
                cellFont3.FontHeightInPoints = 15;
                cellStyle_Columns.SetFont(cellFont3);
                var createName = data2.Rows[0]["制单人"].ToString();
                var auditName = data2.Rows[0]["审核人"].ToString();

                for (i = 0; i < data1.Rows.Count; ++i)
                {
                    IRow row = sheet1.CreateRow(count);
                    for (j = 0; j < data1.Columns.Count; ++j)
                    {
                        ICell c = row.CreateCell(j);
                        c.SetCellValue(data1.Rows[i][j].ToString());
                        c.CellStyle = cellStyle_Columns; //设置单元格样式

                        //设置单元格宽度
                        int n = UTF8Encoding.Default.GetBytes(data1.Rows[i][j].ToString()).Length;

                        n = n + 4;
                        if (sheet1.GetColumnWidth(j) < n * 300 + (n - data1.Rows[i][j].ToString().Length) * 150)
                        {
                            sheet1.SetColumnWidth(j, n * 300 + (n - data1.Rows[i][j].ToString().Length) * 150);
                        }
                    }
                    ++count;
                }
                ICellStyle cellStyle_Columnnew = workbook.CreateCellStyle();
                cellStyle_Columnnew.Alignment = HorizontalAlignment.Center;
                cellStyle_Columnnew.VerticalAlignment = VerticalAlignment.Center;
                IFont fontnew = workbook.CreateFont();//声明字体
                fontnew.Boldweight = (Int16)FontBoldWeight.Bold;//加粗
                fontnew.FontHeightInPoints = 15;
                cellStyle_Columnnew.SetFont(fontnew);//加入单元格
                IRow rows = sheet1.CreateRow(count + 3);
                ICell cs = rows.CreateCell(0);
                cs.CellStyle = cellStyle_Columnnew;
                cs.SetCellValue("制单人:");

                ICell cz = rows.CreateCell(1);
                cz.CellStyle = cellStyle_Columns;
                cz.SetCellValue(createName);
                ICell cf = rows.CreateCell(2);
                cf.CellStyle = cellStyle_Columnnew;
                cf.SetCellValue("复核:");
                ICell cshv = rows.CreateCell(3);
                cshv.CellStyle = cellStyle_Columns;
                cshv.SetCellValue(auditName);
                ICell csc = rows.CreateCell(5);
                csc.CellStyle = cellStyle_Columnnew;
                csc.SetCellValue("生产确认:");
                ICell csh = rows.CreateCell(8);
                csh.CellStyle = cellStyle_Columnnew;
                csh.SetCellValue("审批:");


                MemoryStream ms = new MemoryStream();
                workbook.Write(ms);
                return ms;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 将DataTable数据导入到excel中
        /// 生成表说明  刘万军 2019-04-28
        /// </summary>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <param name="data">导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <returns>文件流</returns>
        /*
        public MemoryStream DataTableZNToExcel(string sheetName, DataTable data, bool isColumnWritten, string zuozhe = "")
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return null;
                }
                //写入DataTable的列名
                if (isColumnWritten == true)
                {
                    //定义字体
                    IFont cellFont = workbook.CreateFont();
                    cellFont.Boldweight = (short)FontBoldWeight.Bold;

                    //定义样式
                    ICellStyle cellStyle = workbook.CreateCellStyle();
                    //标题 背景色
                    cellStyle.FillForegroundColor = HSSFColor.Grey25Percent.Index;
                    cellStyle.FillPattern = FillPattern.SolidForeground;
                    cellStyle.Alignment = HorizontalAlignment.Left;
                    cellStyle.VerticalAlignment = VerticalAlignment.Center;
                    cellStyle.BorderTop = BorderStyle.Thin;
                    cellStyle.BorderBottom = BorderStyle.Thin;
                    cellStyle.BorderLeft = BorderStyle.Thin;
                    cellStyle.BorderRight = BorderStyle.Thin;
                    //cellStyle.SetFont(cellFont);

                    //定义样式
                    ICellStyle cellStyle_ms = workbook.CreateCellStyle();
                    //标题 背景色
                    cellStyle_ms.FillForegroundColor = HSSFColor.White.Index;
                    cellStyle_ms.FillPattern = FillPattern.SolidForeground;
                    cellStyle_ms.Alignment = HorizontalAlignment.Center;
                    cellStyle_ms.VerticalAlignment = VerticalAlignment.Center;
                    cellStyle_ms.BorderTop = BorderStyle.Thin;
                    cellStyle_ms.BorderBottom = BorderStyle.Thin;
                    cellStyle_ms.BorderLeft = BorderStyle.Thin;
                    cellStyle_ms.BorderRight = BorderStyle.Thin;
                    //cellStyle_ms.SetFont(cellFont);

                    //第一行 表名 与描述
                    IRow row_ms = sheet.CreateRow(0);
                    //设置单元格的高度
                    row_ms.Height = 18 * 20;
                    //第0列 表名
                    ICell c_xh_ms = row_ms.CreateCell(0);
                    c_xh_ms.SetCellValue("表名");
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle;
                    c_xh_ms = row_ms.CreateCell(1);
                    c_xh_ms.SetCellValue("表功能描述");
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    c_xh_ms = row_ms.CreateCell(2);
                    c_xh_ms.SetCellValue("");
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    //设置一个合并单元格区域，使用上下左右定义CellRangeAddress区域
                    //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
                    sheet.AddMergedRegion(new CellRangeAddress(0, 0, 1, 2));
                    c_xh_ms = row_ms.CreateCell(3);
                    //表名
                    c_xh_ms.SetCellValue(sheetName);
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    c_xh_ms = row_ms.CreateCell(4);
                    //表名
                    c_xh_ms.SetCellValue("");
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    c_xh_ms = row_ms.CreateCell(5);
                    //表名
                    c_xh_ms.SetCellValue("");
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    c_xh_ms = row_ms.CreateCell(6);
                    //表名
                    c_xh_ms.SetCellValue("");
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    sheet.AddMergedRegion(new CellRangeAddress(0, 0, 3, 6));

                    //第二行 创建者  更新者
                    row_ms = sheet.CreateRow(1);
                    //设置单元格的高度
                    row_ms.Height = 18 * 20;
                    //第0列 表名
                    c_xh_ms = row_ms.CreateCell(0);
                    c_xh_ms.SetCellValue("创建者");
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle;
                    c_xh_ms = row_ms.CreateCell(1);
                    c_xh_ms.SetCellValue(zuozhe);
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    c_xh_ms = row_ms.CreateCell(2);
                    c_xh_ms.SetCellValue("");
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    //设置一个合并单元格区域，使用上下左右定义CellRangeAddress区域
                    //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
                    sheet.AddMergedRegion(new CellRangeAddress(1, 1, 1, 2));
                    c_xh_ms = row_ms.CreateCell(3);
                    //表名
                    c_xh_ms.SetCellValue("更新者");
                    // 设置单元格样式
                    c_xh_ms.CellStyle = cellStyle;
                    c_xh_ms = row_ms.CreateCell(4);
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle;
                    //表名
                    c_xh_ms.SetCellValue("");
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    c_xh_ms = row_ms.CreateCell(5);
                    //表名
                    c_xh_ms.SetCellValue("");
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    c_xh_ms = row_ms.CreateCell(6);
                    //表名
                    c_xh_ms.SetCellValue("");
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    sheet.AddMergedRegion(new CellRangeAddress(1, 1, 4, 6));

                    //第三行 创建时间  更新时间
                    row_ms = sheet.CreateRow(2);
                    //设置单元格的高度
                    row_ms.Height = 18 * 20;
                    //第0列 表名
                    c_xh_ms = row_ms.CreateCell(0);
                    c_xh_ms.SetCellValue("创建时间");
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle;
                    c_xh_ms = row_ms.CreateCell(1);
                    c_xh_ms.SetCellValue(DateTime.Now.ToString("yyyy/MM/dd"));
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    c_xh_ms = row_ms.CreateCell(2);
                    c_xh_ms.SetCellValue("");
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    //设置一个合并单元格区域，使用上下左右定义CellRangeAddress区域
                    //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
                    sheet.AddMergedRegion(new CellRangeAddress(2, 2, 1, 2));
                    c_xh_ms = row_ms.CreateCell(3);
                    //表名
                    c_xh_ms.SetCellValue("更新时间");
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle;
                    //宽度
                    sheet.SetColumnWidth(3, 4 * 30 * 256);
                    c_xh_ms = row_ms.CreateCell(4);
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    //表名
                    c_xh_ms.SetCellValue("");
                    c_xh_ms = row_ms.CreateCell(5);
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    //表名
                    c_xh_ms.SetCellValue("");
                    c_xh_ms = row_ms.CreateCell(6);
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    //表名
                    c_xh_ms.SetCellValue("");
                    sheet.AddMergedRegion(new CellRangeAddress(2, 2, 4, 6));

                    //第四行 说明
                    row_ms = sheet.CreateRow(3);
                    //设置单元格的高度
                    row_ms.Height = 18 * 20;
                    //第0列 表名
                    c_xh_ms = row_ms.CreateCell(0);
                    c_xh_ms.SetCellValue("说明");
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle;
                    c_xh_ms = row_ms.CreateCell(1);
                    c_xh_ms.SetCellValue("");
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    c_xh_ms = row_ms.CreateCell(2);
                    c_xh_ms.SetCellValue("");
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    c_xh_ms = row_ms.CreateCell(3);
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    //表名
                    c_xh_ms.SetCellValue("");
                    c_xh_ms = row_ms.CreateCell(4);
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    //表名
                    c_xh_ms.SetCellValue("");
                    c_xh_ms = row_ms.CreateCell(5);
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    //表名
                    c_xh_ms.SetCellValue("");
                    c_xh_ms = row_ms.CreateCell(6);
                    //设置单元格样式
                    c_xh_ms.CellStyle = cellStyle_ms;
                    //表名
                    c_xh_ms.SetCellValue("");
                    //设置一个合并单元格区域，使用上下左右定义CellRangeAddress区域
                    //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
                    sheet.AddMergedRegion(new CellRangeAddress(3, 3, 1, 6));

                    IRow row = sheet.CreateRow(4);
                    //设置单元格的高度
                    row.Height = 18 * 20;
                    //第0列 序号
                    ICell c_xh = row.CreateCell(0);
                    c_xh.SetCellValue("序号");
                    //设置单元格样式
                    c_xh.CellStyle = cellStyle;

                    int n_xh = UTF8Encoding.Default.GetBytes("序号").Length;
                    sheet.SetColumnWidth(0, n_xh * 400 + (n_xh - "序号".Length) * 200);

                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        ICell c = row.CreateCell(j + 1);
                        string FieldName = data.Columns[j].ColumnName;
                        switch (FieldName)
                        {
                            case "field":
                                FieldName = "字段名称";
                                break;
                            case "datatype":
                                FieldName = "字段类型";
                                break;
                            case "flength":
                                FieldName = "长度";
                                break;
                            case "fnull":
                                FieldName = "是否为空";
                                break;
                            case "shuoming":
                                FieldName = "描述";
                                break;
                            case "zengzhang":
                                FieldName = "是否自增";
                                break;
                            case "morenValue":
                                FieldName = "默认值";
                                break;
                            case "fpk":
                                FieldName = "主键";
                                break;
                        }
                        c.SetCellValue(FieldName);
                        c.CellStyle = cellStyle; //设置单元格样式

                        int n = UTF8Encoding.Default.GetBytes(FieldName).Length;
                        sheet.SetColumnWidth(j + 1, n * 400 + (n - FieldName.Length) * 200);
                    }
                    count = 5;
                }
                else
                {
                    count = 4;
                }

                //注意事项：样式定义要在循环之外，不然生成的EXCEL表第21行以后样式丢失 刘万军 2019-09-18
                //定义样式  
                ICellStyle cellStyle_Columns = workbook.CreateCellStyle();
                cellStyle_Columns.Alignment = HorizontalAlignment.Left;
                cellStyle_Columns.VerticalAlignment = VerticalAlignment.Center;
                cellStyle_Columns.BorderTop = BorderStyle.Thin;
                cellStyle_Columns.BorderBottom = BorderStyle.Thin;
                cellStyle_Columns.BorderLeft = BorderStyle.Thin;
                cellStyle_Columns.BorderRight = BorderStyle.Thin;

                //定义样式
                ICellStyle cellStyle_Center = workbook.CreateCellStyle();
                cellStyle_Center.Alignment = HorizontalAlignment.Center;
                cellStyle_Center.VerticalAlignment = VerticalAlignment.Center;
                cellStyle_Center.BorderTop = BorderStyle.Thin;
                cellStyle_Center.BorderBottom = BorderStyle.Thin;
                cellStyle_Center.BorderLeft = BorderStyle.Thin;
                cellStyle_Center.BorderRight = BorderStyle.Thin;

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    if (i >= 20)
                    {
                        int k = 1;
                    }


                    IRow row = sheet.CreateRow(count);
                    //设置单元格的高度
                    row.Height = 18 * 20;
                    //添加序号值
                    //第0列 序号
                    ICell c_xh = row.CreateCell(0);
                    c_xh.SetCellValue(i + 1);
                    //设置单元格样式
                    c_xh.CellStyle = cellStyle_Center;

                    int n_xh = UTF8Encoding.Default.GetBytes("序号").Length;
                    sheet.SetColumnWidth(0, n_xh * 400 + (n_xh - "序号".Length) * 200);

                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        ICell c = row.CreateCell(j + 1);
                        c.SetCellValue(data.Rows[i][j].ToString());
                        c.CellStyle = cellStyle_Columns; //设置单元格样式

                        //设置单元格宽度
                        int n = UTF8Encoding.Default.GetBytes(data.Rows[i][j].ToString()).Length;
                        if (sheet.GetColumnWidth(j + 1) < n * 400 + (n - data.Rows[i][j].ToString().Length) * 200)
                        {
                            sheet.SetColumnWidth(j + 1, n * 400 + (n - data.Rows[i][j].ToString().Length) * 200);
                        }
                    }
                    ++count;
                }

                MemoryStream ms = new MemoryStream();
                workbook.Write(ms);
                return ms;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }
        */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="data"></param>
        /// <param name="isColumnWritten"></param>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public MemoryStream DataTableToOldExcel(string sheetName, DataTable data, bool isColumnWritten, string filepath)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;

            try
            {
                DirectoryInfo di = new DirectoryInfo(filepath);
                workbook = new HSSFWorkbook(new FileStream(di.FullName, FileMode.Open));
                if (workbook != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                }
                else
                {
                    return null;
                }

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    //定义字体
                    IFont cellFont = workbook.CreateFont();
                    cellFont.Boldweight = (short)FontBoldWeight.Bold;

                    //定义样式
                    ICellStyle cellStyle = workbook.CreateCellStyle();
                    cellStyle.FillForegroundColor = HSSFColor.PaleBlue.Index; //背景色
                    cellStyle.FillPattern = FillPattern.SolidForeground;
                    cellStyle.Alignment = HorizontalAlignment.Center;
                    cellStyle.SetFont(cellFont);
                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        ICell c = row.CreateCell(j);
                        c.SetCellValue(data.Columns[j].ColumnName);
                        c.CellStyle = cellStyle; //设置单元格样式

                        int n = UTF8Encoding.Default.GetBytes(data.Columns[j].ColumnName).Length;
                        sheet.SetColumnWidth(j, n * 300 + (n - data.Columns[j].ColumnName.Length) * 150);
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }

                //注意事项：样式定义要在循环之外，不然生成的EXCEL表第21行以后样式丢失 刘万军 2019-09-18
                //定义样式
                ICellStyle cellStyle_Columns = workbook.CreateCellStyle();
                cellStyle_Columns.Alignment = HorizontalAlignment.Center;
                cellStyle_Columns.VerticalAlignment = VerticalAlignment.Center;

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        ICell c = row.CreateCell(j, CellType.Numeric);
                        string dataval = data.Rows[i][j].ToString();
                        if (j == 0)
                        {
                            c.SetCellValue(dataval);
                        }
                        else
                        {
                            try
                            {
                                c.SetCellValue(double.Parse(dataval));
                            }
                            catch
                            {
                                c.SetCellValue(dataval);
                            }
                        }
                        c.CellStyle = cellStyle_Columns; //设置单元格样式


                        //设置单元格宽度
                        int n = UTF8Encoding.Default.GetBytes(data.Rows[i][j].ToString()).Length;
                        if (sheet.GetColumnWidth(j) < n * 300 + (n - data.Rows[i][j].ToString().Length) * 150)
                        {
                            sheet.SetColumnWidth(j, n * 300 + (n - data.Rows[i][j].ToString().Length) * 150);
                        }
                    }
                    ++count;
                }

                MemoryStream ms = new MemoryStream();
                workbook.Write(ms);
                return ms;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <param name="bytes">图片流</param>
        /// <param name="data">导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <returns>文件流</returns>
        public MemoryStream DataTableToExcel(string sheetName, byte[] bytes, DataTable data, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 19;
            ISheet sheet = null;

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return null;
                }

                //先插入图片
                int pictureIdx = workbook.AddPicture(bytes, NPOI.SS.UserModel.PictureType.JPEG);
                HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
                HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 1023, 0, 0, 0, 12, 18);
                HSSFPicture pict = (HSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);
                //pict.Resize(); //图片原大小

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    //定义字体
                    IFont cellFont = workbook.CreateFont();
                    cellFont.Boldweight = (short)FontBoldWeight.Bold;

                    //定义样式
                    ICellStyle cellStyle = workbook.CreateCellStyle();
                    cellStyle.FillForegroundColor = HSSFColor.PaleBlue.Index; //背景色
                    cellStyle.FillPattern = FillPattern.SolidForeground;
                    cellStyle.Alignment = HorizontalAlignment.Center;
                    cellStyle.SetFont(cellFont);

                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        ICell c = row.CreateCell(j);
                        c.SetCellValue(data.Columns[j].ColumnName);
                        c.CellStyle = cellStyle; //设置单元格样式

                        int n = UTF8Encoding.Default.GetBytes(data.Columns[j].ColumnName).Length;
                        sheet.SetColumnWidth(j, n * 300 + (n - data.Columns[j].ColumnName.Length) * 150);
                    }
                    count++;
                }
                //注意事项：样式定义要在循环之外，不然生成的EXCEL表第21行以后样式丢失 刘万军 2019-09-18
                //定义样式
                ICellStyle cellStyle_Columns = workbook.CreateCellStyle();
                cellStyle_Columns.Alignment = HorizontalAlignment.Center;
                cellStyle_Columns.VerticalAlignment = VerticalAlignment.Center;

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        ICell c = row.CreateCell(j);
                        c.SetCellValue(data.Rows[i][j].ToString());
                        c.CellStyle = cellStyle_Columns; //设置单元格样式

                        //设置单元格宽度
                        int n = UTF8Encoding.Default.GetBytes(data.Rows[i][j].ToString()).Length;
                        if (sheet.GetColumnWidth(j) < n * 300 + (n - data.Rows[i][j].ToString().Length) * 150)
                        {
                            sheet.SetColumnWidth(j, n * 300 + (n - data.Rows[i][j].ToString().Length) * 150);
                        }
                    }
                    ++count;
                }

                MemoryStream ms = new MemoryStream();
                workbook.Write(ms);
                return ms;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <param name="bytes">图片流</param>
        /// <param name="data">导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <returns>文件流</returns>
        /*
        public MemoryStream DataTableToExcel1(string sheetName, byte[] bytes, DataTable data, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 19;
            ISheet sheet = null;

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return null;
                }

                //先插入图片
                int pictureIdx = workbook.AddPicture(bytes, NPOI.SS.UserModel.PictureType.JPEG);
                HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
                HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 1023, 0, 0, 0, 12, 18);
                HSSFPicture pict = (HSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);
                //pict.Resize(); //图片原大小

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    //定义字体
                    IFont cellFont = workbook.CreateFont();
                    cellFont.Boldweight = (short)FontBoldWeight.Bold;

                    //定义样式
                    ICellStyle cellStyle = workbook.CreateCellStyle();
                    cellStyle.FillForegroundColor = HSSFColor.PaleBlue.Index;
                    cellStyle.FillPattern = FillPattern.SolidForeground;
                    cellStyle.Alignment = HorizontalAlignment.Center;
                    cellStyle.SetFont(cellFont);

                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        ICell c = row.CreateCell(j);
                        c.SetCellValue(data.Columns[j].ColumnName);
                        c.CellStyle = cellStyle; //设置单元格样式

                        int n = UTF8Encoding.Default.GetBytes(data.Columns[j].ColumnName).Length;
                        sheet.SetColumnWidth(j, n * 300 + (n - data.Columns[j].ColumnName.Length) * 150);
                    }
                    count++;
                }

                string temp = "";
                int rowspan = 1;
                //注意事项：样式定义要在循环之外，不然生成的EXCEL表第21行以后样式丢失 刘万军 2019-09-18
                //定义样式
                ICellStyle cellStyle_Columns = workbook.CreateCellStyle();
                cellStyle_Columns.Alignment = HorizontalAlignment.Center;
                cellStyle_Columns.VerticalAlignment = VerticalAlignment.Center;

                for (i = 0; i < data.Rows.Count; ++i)
                {

                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        if (j == 1) //合并单元格
                        {
                            if (temp == data.Rows[i][j].ToString())
                            {
                                rowspan++;
                            }
                            else
                            {
                                if (rowspan > 1)
                                {
                                    CellRangeAddress cellRangeAddress = new CellRangeAddress(count - rowspan, count - 1, 1, 1);
                                    sheet.AddMergedRegion(cellRangeAddress);

                                    cellRangeAddress = new CellRangeAddress(count - rowspan, count - 1, 4, 4);
                                    sheet.AddMergedRegion(cellRangeAddress);

                                    cellRangeAddress = new CellRangeAddress(count - rowspan, count - 1, 5, 5);
                                    sheet.AddMergedRegion(cellRangeAddress);
                                    rowspan = 1;
                                }
                            }
                            temp = data.Rows[i][j].ToString();
                        }

                        ICell c = row.CreateCell(j);
                        c.SetCellValue(data.Rows[i][j].ToString());
                        c.CellStyle = cellStyle_Columns; //设置单元格样式

                        //设置单元格宽度
                        int n = UTF8Encoding.Default.GetBytes(data.Rows[i][j].ToString()).Length;
                        if (sheet.GetColumnWidth(j) < n * 300 + (n - data.Rows[i][j].ToString().Length) * 150)
                        {
                            sheet.SetColumnWidth(j, n * 300 + (n - data.Rows[i][j].ToString().Length) * 150);
                        }
                    }
                    ++count;
                }

                if (rowspan > 1)
                {
                    CellRangeAddress cellRangeAddress = new CellRangeAddress(count - rowspan, count - 1, 1, 1);
                    sheet.AddMergedRegion(cellRangeAddress);

                    cellRangeAddress = new CellRangeAddress(count - rowspan, count - 1, 4, 4);
                    sheet.AddMergedRegion(cellRangeAddress);

                    cellRangeAddress = new CellRangeAddress(count - rowspan, count - 1, 5, 5);
                    sheet.AddMergedRegion(cellRangeAddress);
                }

                CellRangeAddress aa = new CellRangeAddress(count - data.Rows.Count, count - 1, 0, 0);
                sheet.AddMergedRegion(aa);

                MemoryStream ms = new MemoryStream();
                workbook.Write(ms);
                return ms;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }
        */
        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public DataTable ExcelToDataTable(string fileName, string sheetName, bool isFirstRowColumn)
        {
            FileStream fs = null;
            DataTable data = new DataTable();
            ISheet sheet = null;
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                {
                    try
                    {
                        workbook = new XSSFWorkbook(fs);
                    }
                    catch
                    {
                        workbook = new HSSFWorkbook(fs);  //非正常2007版本
                    }
                }
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                {
                    try
                    {
                        workbook = new HSSFWorkbook(fs);
                    }
                    catch
                    {
                        workbook = new XSSFWorkbook(fs); //非正常2003版本
                    }
                }

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            DataColumn column = new DataColumn(firstRow.GetCell(i).StringCellValue);
                            data.Columns.Add(column);
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        //没有数据的行默认是null,第一列为空也默认当前行为空
                        if (row == null || string.IsNullOrEmpty(row.GetCell(row.FirstCellNum).ToString().Trim())) continue;

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                if (row.GetCell(j).CellType == CellType.Formula) //如果单元格中设置的是公式
                                {
                                    try
                                    {
                                        dataRow[j] = row.GetCell(j).RichStringCellValue.ToString(); //公式对应值是字符串类型
                                    }
                                    catch
                                    {
                                        dataRow[j] = row.GetCell(j).NumericCellValue.ToString(); //公式对应值是数值类型
                                    }
                                }
                                else
                                {
                                    dataRow[j] = row.GetCell(j).ToString(); //没有公式
                                }
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <param name="data">导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <returns>文件流</returns>
        public MemoryStream DataTableToExcel1img(string sheetName, DataTable data, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return null;
                }

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    //定义字体
                    IFont cellFont = workbook.CreateFont();
                    cellFont.Boldweight = (short)FontBoldWeight.Bold;

                    //定义样式
                    ICellStyle cellStyle = workbook.CreateCellStyle();
                    cellStyle.FillForegroundColor = HSSFColor.PaleBlue.Index;
                    cellStyle.FillPattern = FillPattern.SolidForeground;
                    cellStyle.Alignment = HorizontalAlignment.Center;
                    cellStyle.SetFont(cellFont);

                    IRow row = sheet.CreateRow(count);

                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        ICell c = row.CreateCell(j);
                        c.SetCellValue(data.Columns[j].ColumnName);
                        c.CellStyle = cellStyle; //设置单元格样式  
                        int n = UTF8Encoding.Default.GetBytes(data.Columns[j].ColumnName).Length;
                        sheet.SetColumnWidth(j, n * 300 + (n - data.Columns[j].ColumnName.Length) * 150);
                    }
                    count++;
                }
                //注意事项：样式定义要在循环之外，不然生成的EXCEL表第21行以后样式丢失 刘万军 2019-09-18
                //定义样式
                ICellStyle cellStyle_Columns = workbook.CreateCellStyle();
                cellStyle_Columns.Alignment = HorizontalAlignment.Center;
                cellStyle_Columns.VerticalAlignment = VerticalAlignment.Center;

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    byte[] image = new byte[5];
                    if (data.Rows[i]["图片"].ToString() != "")
                    {
                        image = (byte[])data.Rows[i]["图片"];
                    }

                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        if (j == 10 && image.Length > 5) //合并单元格
                        {
                            int pictureIdx = workbook.AddPicture(image, NPOI.SS.UserModel.PictureType.JPEG);
                            HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
                            HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, 10, i + 1, 11, i + 2);
                            HSSFPicture pict = (HSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);
                        }
                        ICell c = row.CreateCell(j);
                        c.SetCellValue(data.Rows[i][j].ToString());
                        c.CellStyle = cellStyle_Columns; //设置单元格样式

                        //设置单元格宽度
                        int n = UTF8Encoding.Default.GetBytes(data.Rows[i][j].ToString()).Length;
                        if (j == 10)
                        {
                            sheet.AutoSizeColumn(10, true);
                        }
                        else
                        {
                            if (sheet.GetColumnWidth(j) < n * 300 + (n - data.Rows[i][j].ToString().Length) * 150)
                            {
                                sheet.SetColumnWidth(j, n * 300 + (n - data.Rows[i][j].ToString().Length) * 150);
                            }
                        }
                    }
                    ++count;
                }

                MemoryStream ms = new MemoryStream();
                workbook.Write(ms);
                return ms;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <param name="data">导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <returns>文件流</returns>
        public MemoryStream DataTableToExcelleiqu(string sheetName, DataTable data, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return null;
                }

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    //定义字体
                    IFont cellFont = workbook.CreateFont();
                    cellFont.Boldweight = (short)FontBoldWeight.Bold;

                    //定义样式
                    ICellStyle cellStyle = workbook.CreateCellStyle();
                    cellStyle.FillForegroundColor = HSSFColor.PaleBlue.Index;
                    cellStyle.FillPattern = FillPattern.SolidForeground;
                    cellStyle.Alignment = HorizontalAlignment.Center;
                    cellStyle.SetFont(cellFont);

                    IRow row = sheet.CreateRow(count);

                    //data.Columns.Add("photo", typeof(byte[]));
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        ICell c = row.CreateCell(j);
                        c.SetCellValue(data.Columns[j].ColumnName);
                        c.CellStyle = cellStyle; //设置单元格样式

                        int n = UTF8Encoding.Default.GetBytes(data.Columns[j].ColumnName).Length;
                        sheet.SetColumnWidth(j, n * 300 + (n - data.Columns[j].ColumnName.Length) * 150);
                    }
                    count++;
                }
                //注意事项：样式定义要在循环之外，不然生成的EXCEL表第21行以后样式丢失 刘万军 2019-09-18
                //定义样式
                ICellStyle cellStyle_Columns = workbook.CreateCellStyle();
                cellStyle_Columns.Alignment = HorizontalAlignment.Center;
                cellStyle_Columns.VerticalAlignment = VerticalAlignment.Center;
                for (i = 0; i < data.Rows.Count; ++i)
                {
                    byte[] image = new byte[5];
                    if (data.Rows[i]["图片"].ToString() != "")
                    {
                        image = (byte[])data.Rows[i]["图片"];
                    }

                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        if (j == 6 && image.Length > 5) //合并单元格
                        {
                            int pictureIdx = workbook.AddPicture(image, NPOI.SS.UserModel.PictureType.JPEG);
                            HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
                            HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, 6, i + 1, 7, i + 2);
                            HSSFPicture pict = (HSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);

                        }
                        ICell c = row.CreateCell(j);
                        c.SetCellValue(data.Rows[i][j].ToString());
                        c.CellStyle = cellStyle_Columns; //设置单元格样式

                        //设置单元格宽度
                        int n = UTF8Encoding.Default.GetBytes(data.Rows[i][j].ToString()).Length;
                        if (sheet.GetColumnWidth(j) < n * 300 + (n - data.Rows[i][j].ToString().Length) * 150)
                        {
                            sheet.SetColumnWidth(j, n * 300 + (n - data.Rows[i][j].ToString().Length) * 150);
                        }
                    }
                    ++count;
                }

                MemoryStream ms = new MemoryStream();
                workbook.Write(ms);
                return ms;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }

                disposed = true;
            }
        }

        public void saveTofle(MemoryStream file, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] buffer = file.ToArray();//转化为byte格式存储
                fs.Write(buffer, 0, buffer.Length);
                fs.Flush();
                buffer = null;
            }//使用using可以最后不用关闭fs 比较方便
        }
    }
}
