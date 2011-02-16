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
using System.Linq;
using System.Text;

namespace ETS.GGGETSApp.Domain.Application.Entities
{
    public partial class Package
    {
        public Package()
        {
            ChangeTracker.ChangeTrackingEnabled = true;
            this.HAWBs.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(HAWBs_CollectionChanged);
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
        /// 计算包裹总重量，包裹总重量=内部所有运单重量之和。 
        /// </summary>
        public void CalculateHAWBSTotalWeight()
        {
            this.TotalWeight = 0;
            foreach (HAWB hawb in this.HAWBs)
            {
                this.TotalWeight += hawb.TotalWeight;
            }
        }

        /// <summary>
        /// 计算包裹总件数，包裹总件数=内部所有运单之和。 
        /// </summary>
        public void CalculateHAWBSTotalPiece()
        {
            this.Piece = 0;
            this.Piece += this.HAWBs.Count;
        }
    }
}
