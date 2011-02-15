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
        }

        /// <summary>
        /// 当添加或者删除运单包裹时，都要重新计算运单重量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAWBBoxes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {          
            CalculateTotalWeight();
        }

        /// <summary>
        /// 计算运单总重量，运单总重量=个运单包裹重量之和。 
        /// </summary>
        public void  CalculateTotalWeight()
        {
            this.TotalWeight = 0;
            foreach (HAWBBox box in this.HAWBBoxes)
            {
                this.TotalWeight += box.Weight;
            }
        }

    }

}
