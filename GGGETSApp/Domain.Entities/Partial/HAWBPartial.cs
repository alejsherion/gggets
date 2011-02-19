//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        运单总重量，总体积，总件数和体积重量
// 作成者				ZhiWei.Shen
// 改版日				2011.02.16
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETS.GGGETSApp.Domain.Core.Entities;

namespace ETS.GGGETSApp.Domain.Application.Entities
{
    public partial class HAWB
    {
        public HAWB()
        {
            ChangeTracker.ChangeTrackingEnabled = true;
            this.HAWBBoxes.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(HAWBBoxes_CollectionChanged);
            this._propertyChanged += new System.ComponentModel.PropertyChangedEventHandler(HAWB_propertyChanged);
        }

        /// <summary>
        /// 当添加或者删除运单包裹时，都要重新计算运单重量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAWBBoxes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            CalculateTotalWeight();
            CalculateVolumeWeight();
            CalculateTotalVolume();
            CalculateTotalPiece();
        }

        /// <summary>
        /// 当运单属性发生变化时，都要重新计算包裹的总重量，件数可以不用考虑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAWB_propertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            HAWB hawb = sender as HAWB;
            //当重量发生变化时更新包裹的总重量，注意此时的hawb中必须有包裹实例
            if (e.PropertyName == "TotalWeight" && hawb.Package != null)
            {
                hawb.Package.CalculateHAWBSTotalWeight();//包裹会变化
                //hawb.Package.MAWB.CalculateMAWBTotalWeight();//总运单重量也会变化
            }
        }

        /// <summary>
        /// 计算运单总重量，运单总重量=1个运单包裹重量之和。 
        /// </summary>
        public void  CalculateTotalWeight()
        {
            this.TotalWeight = 0;
            foreach (HAWBBox box in this.HAWBBoxes)
            {
                this.TotalWeight += box.Weight;
            }
        }

        /// <summary>
        /// 计算运单总体积重量，运单总体积重量=一个运单包裹重量之和除以166，保留2位小数点
        /// </summary>
        public void CalculateVolumeWeight()
        {
            this.TotalVolume = 0;
            if (this.TotalWeight != 0) this.VolumeWeight = Math.Round(this.TotalWeight/166, 2);
        }

        /// <summary>
        /// 计算运单总件数，运单总件数=运单中盒子的总件数
        /// </summary>
        public void CalculateTotalPiece()
        {
            this.Piece = 0;
            foreach(HAWBBox box in this.HAWBBoxes)
            {
                this.Piece += box.Piece;
            }
        }

        /// <summary>
        /// 计算运单总体积，总体积=该运单内所有单个盒子的体积之和
        /// </summary>
        public void CalculateTotalVolume()
        {
            decimal? volume = 0;
            foreach(HAWBBox box in this.HAWBBoxes)
            {
                volume += box.Length*box.Width*box.Height;
            }
            this.TotalVolume = Convert.ToDecimal(volume);
        }
    }

}
