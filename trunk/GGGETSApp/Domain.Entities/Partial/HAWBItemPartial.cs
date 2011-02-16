//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        运单货物
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
    public partial class HAWBItem
    {
        public HAWBItem()
        {
            ChangeTracker.ChangeTrackingEnabled = true;
            this._propertyChanged += new System.ComponentModel.PropertyChangedEventHandler(HAWBItem_PropertyChanged);
        }

        /// <summary>
        /// 处理内部业务规则
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAWBItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            HAWBItem hawbItem = sender as HAWBItem;
            //当运单货物的件数或者单价发生变化，改变其总价格
            if ((e.PropertyName == "UnitAmount") || (e.PropertyName == "Piece"))
            {
                this.TotalAmount = this.UnitAmount*this.Piece;
            }
        }
    }
}
