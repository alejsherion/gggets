using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETS.GGGETSApp.Domain.Application.Entities
{
    public partial class HAWBBox
    {
        public HAWBBox()
        {
            ChangeTracker.ChangeTrackingEnabled = true;
            this._propertyChanged += new System.ComponentModel.PropertyChangedEventHandler(HAWBBox_PropertyChanged);
        }
        
        /// <summary>
        /// 处理内部业务规则
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAWBBox_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            HAWBBox hawbBox = sender as HAWBBox;
            //当重量发生变化时更新HAWB的总重量，注意此时的HAWBBox实例必须已经绑定在一个运单中
            if ((e.PropertyName == "Weight")&&(hawbBox.HAWB!=null))
            {
                hawbBox.HAWB.CalculateTotalWeight();               
            }
        }
        
    }
}
