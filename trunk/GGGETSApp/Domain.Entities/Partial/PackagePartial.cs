//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        包裹总件数，总重量
// 作成者				ZhiWei.Shen
// 改版日				2011.02.16
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ETS.GGGETSApp.Domain.Core.Entities;

namespace ETS.GGGETSApp.Domain.Application.Entities
{
    public partial class Package
    {
        public Package()
        {
            ChangeTracker.ChangeTrackingEnabled = false;
            this.HAWBs.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(HAWBs_CollectionChanged);
            this._propertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Package_propertyChanged);
        }

        /// <summary>
        /// 当添加或者删除运单时，都要重新计算运单件数，总重量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAWBs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            CalculateHAWBSTotalWeight();
            CalculateHAWBSTotalPiece();
        }

        /// <summary>
        /// 当包裹中的总重量属性发生变化时，需要重新计算总运单的总重量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Package_propertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Package package = sender as Package;
            //当重量发生变化时更新总运单的总重量，注意此时的包裹中必须有总重量实例
            if (e.PropertyName == "TotalWeight" && package.MAWB != null)
            package.MAWB.CalculateMAWBTotalWeight();
        }

        /// <summary>
        /// 计算包裹总重量，包裹总重量=内部所有运单重量之和。 
        /// </summary>
        public void CalculateHAWBSTotalWeight()
        {
            this.TotalWeight = 0;
            foreach (HAWB hawb in this.HAWBs)
            {
                this.TotalWeight += hawb.TotalWeight;
            }
            this.TotalWeight = Convert.ToDecimal(TransferWeight(TotalWeight));
        }

        /// <summary>
        /// 转化总重量的倍数问题
        /// </summary>
        /// <param name="weight">包裹总重量</param>
        /// <returns></returns>
        private double TransferWeight(decimal weight)
        {
            var temp = 0.0;
            var transfer = weight - Math.Truncate(weight);
            if (transfer < (decimal)0.5) temp = (double)(Math.Ceiling(weight) - (decimal)0.5);
            if (transfer > (decimal)0.5) temp = (double)Math.Ceiling(weight);
            if (transfer == (decimal)0.5 || transfer == 0) temp = (double)weight;
            return temp;
        }

        /// <summary>
        /// 计算包裹总件数，包裹总件数=内部所有运单之和。 
        /// </summary>
        public void CalculateHAWBSTotalPiece()
        {
            this.Piece = 0;
            this.Piece += this.HAWBs.Count;
        }

        /// <summary>
        /// 为包裹新增运单
        /// true：判断运单重复并且插入
        /// false:未插入
        /// </summary>
        /// <param name="hawb">运单</param>
        /// <returns></returns>
        public bool JudgeHAWB(HAWB hawb)
        {
            bool judge = true;
            if (string.IsNullOrEmpty(Convert.ToString(hawb.PID)))
            {
                if (this.HAWBs.Count != 0)
                {
                    foreach (HAWB hawbObj in this.HAWBs)
                    {
                        if (hawb.BarCode.Equals(hawbObj.BarCode))
                        {
                            judge = false;
                            break;
                        }
                    }
                }
            }
            return judge;
        }
    }
}
