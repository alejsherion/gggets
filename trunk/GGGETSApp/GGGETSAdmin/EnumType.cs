using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace GGGETSAdmin
{
    public class EnumType
    {
        /// <summary>
        /// 结算方式
        /// </summary>
        private enum SettleType
        {
            预付月结 = 0,
            预付现结 = 1,
            到付月结 = 2,
            到付现结 = 3
        };
        /// <summary>
        /// 运单状态
        /// </summary>
        private enum StatusType
        {
            待审核 = 0,
            取货 = 1,
            核单 = 2,
            派送 = 3,
            in包 = 4
        };
        /// <summary>
        /// 运单类型
        /// </summary>
        private enum BoxType
        {
            文件 = 0,
            小包裹 = 1,
            普货 = 2,
        };
        /// <summary>
        /// 折扣率
        /// </summary>
        private enum DiscountType
        {
            灵活折扣 = 0,
            固定折扣 = 1
        };
        /// <summary>
        /// 计费方式
        /// </summary>
        private enum WeightCalType
        {
            按照零点五千克标准=0,
            按照分段标准=1
        };
        /// <summary>
        /// 绑定DropDownList的text值
        /// </summary>
        public string Text
        {
            get;
            set;
        }
        /// <summary>
        /// 绑定DropDownList的Value值
        /// </summary>
        public int Value
        {
            get;
            set;
        }
        /// <summary>
        /// 返回枚举方法
        /// </summary>
        /// <returns></returns>
        public List<EnumType> GetName(string name)
        {
            var item = new List<EnumType>();
            if (name == "SettleType")
            {
                foreach (int value in Enum.GetValues(typeof(SettleType)))
                {
                    item.Add(new EnumType { Text = Enum.GetName(typeof(SettleType), value), Value = value });
                }
            }
            else if (name == "StatusType")
            {
                foreach (int value in Enum.GetValues(typeof(StatusType)))
                {
                    item.Add(new EnumType { Text = Enum.GetName(typeof(StatusType), value), Value = value });
                }
            }
            else if (name == "BoxType")
            {
                foreach (int value in Enum.GetValues(typeof(BoxType)))
                {
                    item.Add(new EnumType { Text = Enum.GetName(typeof(BoxType), value), Value = value });
                }
            }
            else if (name == "DiscountType")
            {
                foreach (int value in Enum.GetValues(typeof(DiscountType)))
                {
                    item.Add(new EnumType { Text = Enum.GetName(typeof(DiscountType), value), Value = value });
                }
            }
            else if (name == "WeightCalType")
            {
                foreach (int value in Enum.GetValues(typeof(WeightCalType)))
                {
                    item.Add(new EnumType { Text = Enum.GetName(typeof(WeightCalType), value), Value = value });
                }
            }
            return item;
        }
    }
}