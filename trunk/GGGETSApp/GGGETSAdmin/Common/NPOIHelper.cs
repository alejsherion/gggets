using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ETS.GGGETSApp.Domain.Application.Entities;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;

namespace GGGETSAdmin.Common
{
    /// <summary>
    /// 导出类型
    /// </summary>
    public enum ExportType
    {
        运单号=1,
        承运单号=2
    }

    public class NpoiHelper
    {
        #region 构造函数以及字段
        /// <summary>
        /// 数据源
        /// </summary>
        private readonly HAWB _hawbDataSource;
        private readonly MAWB _mawbDataSource;
        private readonly string _hawbId;
        private readonly IList<HAWB> _hawbDataSources;
        private readonly ExportType _hawbtype;
        static HSSFWorkbook _hssfworkbook;
        /// <summary>
        /// 导出发票
        /// </summary>
        /// <param name="dataSource">数据源</param>
        /// <param name="hawbId">运单号或承运单号</param>
        public NpoiHelper(HAWB dataSource,string hawbId)
        {
            if (dataSource == null||string.IsNullOrEmpty(hawbId)) throw new ArgumentNullException("dataSource", @"运单或运单号不能为空");
            _hawbDataSource = dataSource;
            _hawbId=hawbId;
        }

        /// <summary>
        /// 导出总运单号
        /// </summary>
        /// <param name="dataSource">总运单</param>
        /// <param name="dataSource2">子运单数据</param>
        /// <param name="hawbtype">导出类型</param>
        public NpoiHelper(MAWB dataSource,IList<HAWB> dataSource2,ExportType hawbtype)
        {
            if (dataSource == null) throw new ArgumentNullException("dataSource", @"总运单不能为空");
            _mawbDataSource = dataSource;
            _hawbDataSources = dataSource2;
            _hawbtype=hawbtype;
        }

        /// <summary>
        /// 导出电子出口清单
        /// </summary>
        /// <param name="dataSource">总运单对象信息</param>
        /// <param name="dataSource2">子运单集合信息(用户选中)</param>
        public NpoiHelper(MAWB dataSource, IList<HAWB> dataSource2)
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
                var totalValue = Convert.ToString(hamwitemArry.Sum(p => p.TotalAmount));
                sheet.GetRow(rowIndex +2).GetCell(9).SetCellValue(totalValue);
            }
            sheet.GetRow(2).GetCell(2).SetCellValue(_hawbId);
            sheet.GetRow(4).GetCell(0).SetCellValue(_hawbDataSource.ShipperName.ToUpper() + "  " + _hawbDataSource.ShipperAddress.ToUpper());
            var desc = _hawbDataSource.ConsigneeName.ToUpper() + "  " + _hawbDataSource.ConsigneeAddress.ToUpper() + "  JAPAN" + "  TEL:" + _hawbDataSource.ConsigneeTel + "  ZIP:" + _hawbDataSource.ConsigneeZipCode;
            sheet.GetRow(4).GetCell(4).SetCellValue(desc);
            var DateString = sheet.GetRow(rowIndex + 5).GetCell(0).StringCellValue;
            DateString += DateTime.Now.ToString("yyyy-MM-dd");
            sheet.GetRow(rowIndex + 5).GetCell(0).SetCellValue(DateString);
            WriteToFile();
        }

        /// <summary>
        /// 导出总运单
        /// </summary>
        public void ExportMAWB()
        {
            InitializeWorkbook("~/Template/MAWB.xls");
            var sheet = _hssfworkbook.GetSheet("Sheet1");

            //赋值MAWB
            sheet.GetRow(2).GetCell(1).SetCellValue(_mawbDataSource.BarCode.ToUpper());
            if (!string.IsNullOrEmpty(_mawbDataSource.FlightNo))
            {
                sheet.GetRow(2).GetCell(2).SetCellValue(_mawbDataSource.FlightNo.ToUpper());
            }
            else
            {
                sheet.GetRow(2).GetCell(2).SetCellValue(_mawbDataSource.FlightNo);
            }
            sheet.GetRow(2).GetCell(4).SetCellValue((Convert.ToDouble(_mawbDataSource.TotalWeight)));
            sheet.GetRow(2).GetCell(5).SetCellValue(Convert.ToString(_mawbDataSource.Packages.Count));
            //赋值HAWBs
            if(_mawbDataSource.Packages==null || _mawbDataSource.Packages.Count==0) throw new ArgumentException("该总运单下没有包裹，无法进行导出！");
            int count = 0;
            int rowCount = 5;//起始行
            HSSFCellStyle sourceStyle = null;
            foreach (HAWB hawb in _hawbDataSources.OrderBy(it=>it.CreateTime))
            {
                //计算申报价格-》该运单下所有货物的总价
                decimal totalAmount = 0;
                if (hawb.HAWBItems == null || hawb.HAWBItems.Count == 0) totalAmount = 0;
                else
                {
                    foreach(HAWBItem item in hawb.HAWBItems)
                    {
                        totalAmount = totalAmount + item.TotalAmount;
                    }
                }

                if (rowCount==5)
                {
                    sheet.GetRow(rowCount).GetCell(0).SetCellValue(count + 1);//序号
                    if(_hawbtype==ExportType.运单号)
                    {
                        sheet.GetRow(rowCount).GetCell(1).SetCellValue(hawb.BarCode.ToUpper());//运单编号
                    }
                    else
                    {
                         sheet.GetRow(rowCount).GetCell(1).SetCellValue(hawb.CarrierHAWBBarCode.ToUpper());//运单编号
                    }
                    sheet.GetRow(rowCount).GetCell(2).SetCellValue((hawb.HAWBItems.First()).Name.ToUpper());//运单编号
                    sheet.GetRow(rowCount).GetCell(3).SetCellValue(Convert.ToString(hawb.Piece));//件数
                    sheet.GetRow(rowCount).GetCell(4).SetCellValue(Convert.ToDouble(hawb.TotalWeight));//总重量
                    sheet.GetRow(rowCount).GetCell(5).SetCellValue(Convert.ToString(totalAmount));//申报价值
                    sheet.GetRow(rowCount).GetCell(6).SetCellValue("USD");//USD
                    sheet.GetRow(rowCount).GetCell(7).SetCellValue(hawb.ConsigneeName.ToUpper());//收件人
                    sheet.GetRow(rowCount).GetCell(8).SetCellValue(hawb.ConsigneeRegionDesc.ToUpper());//地区
                    sheet.GetRow(rowCount).GetCell(9).SetCellValue(hawb.ShipperName.ToUpper());//发件人地址
                    sourceStyle = sheet.GetRow(rowCount).GetCell(0).CellStyle;
                }
                else
                {
                  
                    var cell1=sheet.CreateRow(rowCount).CreateCell(0);//序号
                    cell1.CellStyle = sourceStyle;
                    cell1.SetCellValue(count + 1);
                    
                    var cell2 = sheet.CreateRow(rowCount).CreateCell(1);//运单编号
                    cell2.CellStyle = sourceStyle;
                    if(_hawbtype==ExportType.运单号)
                    {
                         cell2.SetCellValue(hawb.BarCode.ToUpper());
                    }
                    else
                    {
                         cell2.SetCellValue(hawb.CarrierHAWBBarCode.ToUpper());
                    }
                   
                    
                    var cell3 = sheet.CreateRow(rowCount).CreateCell(2);//运单编号
                    cell3.CellStyle = sourceStyle;
                    cell3.SetCellValue((hawb.HAWBItems.First()).Name.ToUpper());
                    ;
                    var cell4 = sheet.CreateRow(rowCount).CreateCell(3);//件数
                    cell4.CellStyle = sourceStyle;
                    cell4.SetCellValue(Convert.ToString(hawb.Piece));
                    ;
                    var cell5 = sheet.CreateRow(rowCount).CreateCell(4);//总重量
                    cell5.CellStyle = sourceStyle;
                    cell5.SetCellValue(Convert.ToDouble(hawb.TotalWeight));
                    
                    var cell6 = sheet.CreateRow(rowCount).CreateCell(5);//申报价值
                    cell6.CellStyle = sourceStyle;
                    cell6.SetCellValue(Convert.ToString(totalAmount));
                    
                    var cell7 = sheet.CreateRow(rowCount).CreateCell(6);//USD
                    cell7.CellStyle = sourceStyle;
                    cell7.SetCellValue("USD");
                    
                    var cell8 = sheet.CreateRow(rowCount).CreateCell(7);//收件人
                    cell8.CellStyle = sourceStyle;
                    cell8.SetCellValue(hawb.ConsigneeName.ToUpper());
                    
                    var cell9 = sheet.CreateRow(rowCount).CreateCell(8);//地区
                    cell9.CellStyle = sourceStyle;
                    cell9.SetCellValue(hawb.ConsigneeRegionDesc.ToUpper());
                    
                    var cell10 = sheet.CreateRow(rowCount).CreateCell(9);//发件人地址
                    cell10.CellStyle = sourceStyle;
                    cell10.SetCellValue(hawb.ShipperName.ToUpper());
                    

                }
              
                //计算运单数量
                count++;
                rowCount++;

            }
            sheet.GetRow(2).GetCell(6).SetCellValue(count);
            WriteToFile();
        }

        /// <summary>
        /// 导出电子出口清单(报关单)
        /// </summary>
        public void ExportClearance()
        {
            //初始化HSSFWorkBook
            InitializeWorkbook("~/Template/ClearanceImport.xls");
            //获取第一个sheet
            var sheet = _hssfworkbook.GetSheet("Sheet1");

            //格式处理
            HSSFCellStyle cellStyle = _hssfworkbook.CreateCellStyle();
            HSSFDataFormat format = _hssfworkbook.CreateDataFormat();
            cellStyle.DataFormat = format.GetFormat("yyyymmdd");
            sheet.GetRow(3).GetCell(6).CellStyle = cellStyle;

            //开始处理
            #region 总运单信息
            sheet.GetRow(3).GetCell(1).SetCellValue(_mawbDataSource.BarCode.ToUpper());//总运单编号
            sheet.GetRow(3).GetCell(2).SetCellValue(_mawbDataSource.FlightNo.ToUpper());//航班编号
            sheet.GetRow(3).GetCell(4).SetCellValue(_mawbDataSource.TotalWeight.ToString());//总重量
            //sheet.GetRow(3).GetCell(5).SetCellValue(_mawbDataSource.Packages.Count());//总件数，使用区域求和
            sheet.GetRow(3).GetCell(6).SetCellFormula("TODAY()");//今天日期,使用yyyymmdd格式
            sheet.GetRow(3).GetCell(8).SetCellValue("起始地编号");//todo 起始地编号,需要提供
            #endregion

            #region 子运单信息
            int i = 5;
            foreach(HAWB hawb in _hawbDataSources)
            {
                sheet.GetRow(i).GetCell(1).SetCellValue(hawb.BarCode.ToUpper());//子运单编号
                sheet.GetRow(i).GetCell(2).SetCellValue(hawb.ShipperName.ToUpper());//子运单发货人姓名
                sheet.GetRow(i).GetCell(3).SetCellValue(hawb.ConsigneeName.ToUpper());//子运单交付人姓名
                sheet.GetRow(i).GetCell(4).CellStyle = sheet.GetRow(5).GetCell(4).CellStyle;//样式复制
                sheet.GetRow(i).GetCell(4).SetCellValue(hawb.ConsigneeAddress.ToUpper());//子运单交付人地址
                sheet.GetRow(i).GetCell(5).SetCellValue(hawb.Remark.ToUpper());//子运单备注
                sheet.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(hawb.TotalWeight));//子运单重量
                //处理子运单总价格
                //处理子运单总件数
                decimal tempAmount = 0;
                int tempPiece = 0;
                foreach(HAWBItem item in hawb.HAWBItems)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(item.TotalAmount)) && !string.IsNullOrEmpty(Convert.ToString(item.Piece)))
                    {
                        tempAmount = tempAmount + item.TotalAmount;
                        tempPiece = tempPiece + item.Piece;
                    }
                }
                sheet.GetRow(i).GetCell(7).SetCellValue(Convert.ToDouble(tempAmount));//子运单价格
                sheet.GetRow(i).GetCell(8).SetCellValue(hawb.Package.BarCode);//子运单包号
                sheet.GetRow(i).GetCell(9).SetCellValue(tempPiece);//子运单总件数

                i++;
            }
            #endregion

            //区域求和，计算总件数
            HSSFName range = _hssfworkbook.CreateName();
            range.Reference = "Sheet1!$J3:$J60000";
            range.NameName = "range1";
            sheet.GetRow(3).GetCell(5).SetCellFormula("sum(range1)");
            //处理结束
            //创建一个虚拟的XLS文件，用于后续读取写入到stream流中
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
                if (!string.IsNullOrEmpty(hamwitem.Name) && !string.IsNullOrEmpty(hamwitem.Remark))
                {
                    row.GetCell(3).SetCellValue(hamwitem.Name.ToUpper() + hamwitem.Remark.ToUpper());
                }
                else
                {
                    row.GetCell(3).SetCellValue(hamwitem.Name + hamwitem.Remark);
                }
                
                row.GetCell(5).SetCellValue(hamwitem.Piece);
                row.GetCell(6).SetCellValue(Convert.ToString(hamwitem.UnitAmount));
                row.GetCell(7).SetCellValue(Convert.ToString(hamwitem.TotalAmount));
                lastIndexRow = row.RowNum;
                currentRow += 1;

            }
            var tempRow = sheet.GetRow(rowIndex);
            tempRow.GetCell(8).SetCellValue(Convert.ToString(_hawbDataSource.TotalWeight));
            var totalValue = Convert.ToString(hamwitemArry.Sum(p => p.TotalAmount));
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

        ///// <summary>
        ///// 删除行
        ///// </summary>
        ///// <param name="sheet"></param>
        ///// <param name="row"></param>
        //static void DeleteRow(HSSFSheet sheet,HSSFRow row)
        //{
        //    if (row == null) return;
        //    row.RemoveAllCells();
        //    sheet.RemoveRow(row);
        //    sheet.ShiftRows(row.RowNum + 1, sheet.LastRowNum, -1);
        //}
        #endregion
    }
}