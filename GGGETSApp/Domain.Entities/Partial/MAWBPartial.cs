﻿//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        总运单总重量和总体积计算
// 作成者				ZhiWei.Shen
// 改版日				2011.02.18
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using ETS.GGGETSApp.Domain.Core.Entities;

namespace ETS.GGGETSApp.Domain.Application.Entities
{
    #region 枚举
    public enum MAWBStatus
    {
        未起飞,
        起飞 = 1,
        抵达 = 2
    }
    #endregion
    public partial class MAWB
    {
        public MAWB()
        {
            ChangeTracker.ChangeTrackingEnabled = true;
            this.Packages.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(MAWB_CollectionChanged);
        }

        /// <summary>
        /// 当添加或者删除总运单中包裹时，都要重新计算总运单重量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MAWB_CollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            CalculateMAWBTotalWeight();
        }

        /// <summary>
        /// 计算总运单总重量，总运单总重量=该订单中所有包裹重量之和。 
        /// </summary>
        public void CalculateMAWBTotalWeight()
        {
            this.TotalWeight = 0;
            foreach (Package package in this.Packages)
            {
                this.TotalWeight += package.TotalWeight;
            }
            this.TotalWeight = Math.Ceiling(TotalWeight);
            this.TotalVolume = Math.Round(TotalWeight / 166, 2);//总体积赋值
        }

        /// <summary>
        /// 判断包裹是否可以重复添加到同一个总运单中
        /// </summary>
        /// <param name="package">包裹信息</param>
        /// <returns></returns>
        public bool JudgePackage(Package package)
        {
            bool judge = true;//判断
            //获取这个包裹的包号
            if(package!=null)
            {
                string pid = package.PID.ToString();
                if(this.Packages.Count!=0)//如果包裹集合有，那么这里就需要判断重复问题
                {
                    foreach(Package packageTemp in Packages)
                    {
                        if(packageTemp.PID.ToString().Equals(pid))
                        {
                            judge = false;
                        }
                    }
                }
            }
            return judge;
        }
    }
}
