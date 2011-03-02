using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ETS.GGGETSApp.Domain.Application.Entities;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Core;

namespace Application.GGETS
{
    public class NpoiHelper
    {
        #region 构造函数以及字段
        /// <summary>
        /// 数据源
        /// </summary>
        private readonly HAWB _hawbDataSource;
        private readonly MAWB _mawbDataSource;
        private readonly IList<HAWB> _hawbDataSources;
        static HSSFWorkbook _hssfworkbook;

        public NpoiHelper(HAWB dataSource)
        {
            if (dataSource == null) throw new ArgumentNullException("dataSource", @"运单不能为空");
            _hawbDataSource = dataSource;
        }

        public NpoiHelper(MAWB dataSource,IList<HAWB> dataSource2)
        {
            if (dataSource == null) throw new ArgumentNullException("dataSource", @"总运单不能为空");
            _mawbDataSource = dataSource;
            _hawbDataSources = dataSource2;
        }
        #endregion

        #region 公有方法
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public void ExportInvoice()
        {
            InitializeWorkbook("~/Template/Invoice.xls");
            var sheet = _hssfworkbook.GetSheet("Sheet1");
            var rowIndex=CreateSonTable(sheet, 9);
            var hamwitemArry = _hawbDataSource.HAWBItems;
            if (hamwitemArry == null || hamwitemArry.Count == 0)
            {
                sheet.GetRow(rowIndex +2).GetCell(9).SetCellValue(0);
            }
            else
            {
                var totalValue = Convert.ToString(hamwitemArry.Sum(p => p.UnitAmount));
                sheet.GetRow(rowIndex +2).GetCell(9).SetCellValue(totalValue);
            }
            sheet.GetRow(2).GetCell(2).SetCellValue(_hawbDataSource.BarCode);
            sheet.GetRow(4).GetCell(0).SetCellValue(_hawbDataSource.ShipperName);
            var desc = _hawbDataSource.ConsigneeName + _hawbDataSource.ConsigneeAddress;
            sheet.GetRow(4).GetCell(4).SetCellValue(desc);
            WriteToFile();
        }

        public void ExportMAWB()
        {
            InitializeWorkbook("~/Template/MAWB.xls");
            var sheet = _hssfworkbook.GetSheet("Sheet1");

            HSSFCellStyle style = _hssfworkbook.CreateCellStyle();
            style.BorderBottom = CellBorderType.MEDIUM;
            style.BorderLeft = CellBorderType.MEDIUM;
            style.BorderRight = CellBorderType.MEDIUM;
            style.BorderTop = CellBorderType.MEDIUM;

            //赋值MAWB
            sheet.GetRow(2).GetCell(1).SetCellValue(_mawbDataSource.BarCode);
            sheet.GetRow(2).GetCell(2).SetCellValue(_mawbDataSource.FlightNo);
            sheet.GetRow(2).GetCell(4).SetCellValue(Convert.ToString(_mawbDataSource.TotalWeight));
            //赋值HAWBs
            if(_mawbDataSource.Packages==null || _mawbDataSource.Packages.Count==0) throw new ArgumentException("该总运单下没有包裹，无法进行导出！");
            int count = 0;
            int rowCount = 5;//起始行
            foreach (HAWB hawb in _hawbDataSources)
            {
                //foreach(HAWB hawb in package.HAWBs)
                //{
                sheet.CreateRow(rowCount).CreateCell(0).CellStyle = style;
                sheet.CreateRow(rowCount).CreateCell(1).CellStyle = style;
                sheet.CreateRow(rowCount).CreateCell(2).CellStyle = style;
                sheet.CreateRow(rowCount).CreateCell(3).CellStyle = style;
                sheet.CreateRow(rowCount).CreateCell(4).CellStyle = style;
                sheet.CreateRow(rowCount).CreateCell(5).CellStyle = style;
                sheet.CreateRow(rowCount).CreateCell(6).CellStyle = style;
                sheet.CreateRow(rowCount).CreateCell(7).CellStyle = style;
                sheet.CreateRow(rowCount).CreateCell(8).CellStyle = style;
                sheet.CreateRow(rowCount).CreateCell(9).CellStyle = style;

                sheet.CreateRow(rowCount).CreateCell(0).SetCellValue(count + 1);//序号
                sheet.CreateRow(rowCount).CreateCell(1).SetCellValue(hawb.BarCode);//运单编号
                sheet.CreateRow(rowCount).CreateCell(2).SetCellValue((hawb.HAWBItems.First()).Name);//运单编号
                sheet.CreateRow(rowCount).CreateCell(3).SetCellValue((hawb.Piece.ToString()));//件数
                sheet.CreateRow(rowCount).CreateCell(4).SetCellValue((hawb.TotalWeight.ToString()));//总重量
                sheet.CreateRow(rowCount).CreateCell(5).SetCellValue("9999999");//申报价值
                sheet.CreateRow(rowCount).CreateCell(6).SetCellValue("USD");//USD
                sheet.CreateRow(rowCount).CreateCell(7).SetCellValue(hawb.ConsigneeName);//收件人
                //todo 调用方法
                sheet.CreateRow(rowCount).CreateCell(8).SetCellValue(hawb.ConsigneeRegion);//地区
                sheet.CreateRow(rowCount).CreateCell(9).SetCellValue(hawb.ShipperAddress);//发件人地址

                //计算运单数量
                count++;
                rowCount++;
                //}
            }
            sheet.GetRow(2).GetCell(6).SetCellValue(count);
            WriteToFile();
        }

        public Stream RenderToExcel()
        {
            var url = HttpContext.Current.Server.MapPath("~/test.xls");
            var file = new FileStream(url, FileMode.Open, FileAccess.Read);
            _hssfworkbook = new HSSFWorkbook(file);
            var ms = new MemoryStream();
            _hssfworkbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            return ms;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        static void InitializeWorkbook(string templatePath)
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            //var url = HttpContext.Current.Server.MapPath("~/template/200902105483.xls");
            if (string.IsNullOrEmpty(templatePath)) return;
            var url = HttpContext.Current.Server.MapPath(templatePath);
            var file = new FileStream(url, FileMode.Open, FileAccess.Read);
            _hssfworkbook = new HSSFWorkbook(file);
        }

        /// <summary>
        /// 生成子表数据Excel
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="rowIndex"></param>
        private int CreateSonTable(HSSFSheet sheet, int rowIndex)
        {
            var hamwitemArry = _hawbDataSource.HAWBItems;
            if (hamwitemArry == null || hamwitemArry.Count == 0) return 0;
            var currentRow = rowIndex;
            var lastIndexRow = 0;
            var sourcerow = sheet.GetRow(rowIndex);
            foreach (var hamwitem in hamwitemArry)
            {
                if (currentRow != rowIndex)
                {
                    MyInsertRow(sheet, currentRow, 1, sourcerow);
                }
                var row = sheet.GetRow(currentRow);
                row.Height = 840;
                row.GetCell(3).SetCellValue(hamwitem.Name);
                row.GetCell(5).SetCellValue(hamwitem.Piece);
                row.GetCell(6).SetCellValue(Convert.ToString(hamwitem.UnitAmount));
                row.GetCell(7).SetCellValue(Convert.ToString(hamwitem.TotalAmount));
                lastIndexRow = row.RowNum;
                currentRow += 1;

            }
            var tempRow = sheet.GetRow(lastIndexRow - 1);
            tempRow.GetCell(8).SetCellValue(Convert.ToString(_hawbDataSource.TotalWeight));
            var totalValue = Convert.ToString(hamwitemArry.Sum(p => p.UnitAmount));
            tempRow.GetCell(9).SetCellValue(totalValue);
            if (hamwitemArry.Count < 6)
            {
                var count = 6 - hamwitemArry.Count;
                for (var i = 0; i < count; i++)
                {
                    MyInsertRow(sheet, currentRow, 1, sourcerow);
                    var row = sheet.GetRow(currentRow);
                    lastIndexRow = row.RowNum;
                    currentRow += 1;
                }
            }
            sheet.AddMergedRegion(new Region(rowIndex, 8, lastIndexRow, 8));
            sheet.AddMergedRegion(new Region(rowIndex, 9, lastIndexRow, 9));
            //var deleteRow = sheet.GetRow(rowIndex);
            //DeleteRow(sheet, deleteRow);
            //lastIndexRow=lastIndexRow - 1;
            return lastIndexRow;
        }

        /// <summary>
        /// 插入Excel行
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="rowIndex"></param>
        /// <param name="count"></param>
        /// <param name="row"></param>
        private static void MyInsertRow(HSSFSheet sheet, int rowIndex, int count, HSSFRow row)
        {
            #region 批量移动行
            sheet.ShiftRows(
                                rowIndex,                                 //--开始行
                                sheet
                                .LastRowNum,                            //--结束行
                                count,                             //--移动大小(行数)--往下移动
                                true,                                   //是否复制行高
                                false,                                  //是否重置行高
                                true                                    //是否移动批注
                            );
            #endregion

            #region 对批量移动后空出的空行插，创建相应的行，并以插入行的上一行为格式源(即：插入行-1的那一行)
            for (int i = rowIndex; i < rowIndex + count - 1; i++)
            {
                HSSFRow targetRow;
                HSSFCell sourceCell;
                HSSFCell targetCell;

                targetRow = sheet.CreateRow(i + 1);
                for (int m = row.FirstCellNum; m < row.LastCellNum; m++)
                {
                    sourceCell = row.GetCell(m);
                    if (sourceCell == null)
                        continue;
                    targetCell = targetRow.CreateCell(m);

                    targetCell.Encoding = sourceCell.Encoding;
                    targetCell.CellStyle = sourceCell.CellStyle;
                    targetCell.SetCellType(sourceCell.CellType);

                }
                //CopyRow(sourceRow, targetRow);

                //Util.CopyRow(sheet, sourceRow, targetRow);
            }

            HSSFRow firstTargetRow = sheet.GetRow(rowIndex);
            HSSFCell firstSourceCell;
            HSSFCell firstTargetCell = null;
            for (int m = row.FirstCellNum; m < row.LastCellNum; m++)
            {
                firstSourceCell = row.GetCell(m);
                if (firstSourceCell == null)
                    continue;
                firstTargetCell = firstTargetRow.CreateCell(m);
                firstTargetCell.Encoding = firstSourceCell.Encoding;
                firstTargetCell.CellStyle = firstSourceCell.CellStyle;
                firstTargetCell.SetCellType(firstSourceCell.CellType);
            }
            sheet.AddMergedRegion(new Region(firstTargetRow.RowNum, 3, firstTargetRow.RowNum, 4));
            #endregion
        }

        /// <summary>
        /// 写Excel文件
        /// </summary>
        static void WriteToFile()
        {
            //Write the stream data of workbook to the root directory
            var url = HttpContext.Current.Server.MapPath("~/test.xls");
            var file = new FileStream(url, FileMode.Create);
            _hssfworkbook.Write(file);
            file.Close();
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="row"></param>
        static void DeleteRow(HSSFSheet sheet,HSSFRow row)
        {
            if (row == null) return;
            row.RemoveAllCells();
            sheet.RemoveRow(row);
            sheet.ShiftRows(row.RowNum + 1, sheet.LastRowNum, -1);
        }
        #endregion
    }
}