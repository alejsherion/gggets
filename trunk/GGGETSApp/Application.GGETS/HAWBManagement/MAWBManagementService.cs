//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        总运单BLL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.15
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Domain.Core;

namespace Application.GGETS
{
    public class MAWBManagementService:IMAWBManagementService
    {
        /// <summary>
        /// IOC Injecting into
        /// </summary>
        private IMAWBRepository _mawbRepository;

        private IHAWBRepository _hawbRepository;

        public MAWBManagementService(IMAWBRepository mawbRepository, IHAWBRepository hawbRepository)
        {
            _mawbRepository = mawbRepository;
            _hawbRepository = hawbRepository;
        }

        /// <summary>
        /// 新增总运单
        /// </summary>
        /// <param name="mawb">总运单</param>
        public void AddMAWB(MAWB mawb)
        {
            if (mawb == null)
                throw new ArgumentNullException("MAWB is null");
            IUnitOfWork unitOfWork = _mawbRepository.UnitOfWork;
            _mawbRepository.Add(mawb);
            //complete changes in this unit of work
            unitOfWork.Commit();
        }

        /// <summary>
        /// 通过条形码获取总订单
        /// </summary>
        /// <param name="barcode">条形码</param>
        /// <returns></returns>
        public MAWB FindMAWBByBarcode(string barcode)
        {
            return _hawbRepository.FindMAWBByBarcode(barcode);
        }

        /// <summary>
        /// 修改总运单
        /// </summary>
        /// <param name="mawb">总运单</param>
        public void ModifyMAWB(MAWB mawb)
        {
            if (mawb == null)
                throw new ArgumentNullException("MAWB is null");
            IUnitOfWork unitOfWork = _mawbRepository.UnitOfWork;
            _mawbRepository.Modify(mawb);
            //complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
        }

        /// <summary>
        /// 多条件查询总运单
        /// </summary>
        /// <param name="barCode">总运单编号</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public IList<MAWB> FindMAWBByCondition(string barCode, DateTime? beginDate, DateTime? endDate)
        {
            return _mawbRepository.FindMAWBByCondition(barCode, beginDate, endDate);
        }

        /// <summary>
        /// 通过航班号获取总运单
        /// </summary>
        /// <param name="flightNo">航班号</param>
        /// <returns></returns>
        public IList<MAWB> FindAllMAWBsByFlightNo(string flightNo)
        {
            return _mawbRepository.FindAllMAWBsByFlightNo(flightNo);
        }

        /// <summary>
        /// 通过MID获取总运单
        /// </summary>
        /// <param name="MID">总运单编号</param>
        /// <returns></returns>
        public MAWB FindMAWBByMID(string MID)
        {
            return _mawbRepository.FindMAWBByMID(MID);
        }

        /// <summary>
        /// 通过航班信息查询下面所有的总运单信息
        /// </summary>
        /// <param name="flightNo">航班编号</param>
        /// <param name="from">起始地字码</param>
        /// <param name="to">目的地字码</param>
        /// <returns></returns>
        public IList<MAWB> FindMAWBByFlightCondition(string flightNo, string from, string to)
        {
            return _mawbRepository.FindMAWBByFlightCondition(flightNo, from, to);
        }

        /// <summary>
        /// 判断MID是否为空，是空的才可以添加改包裹
        /// </summary>
        /// <param name="barcode">包裹编号</param>
        /// <returns></returns>
        public bool JudgeMIDIsNull(string barcode)
        {
            bool judge = false;
            Package package = _hawbRepository.FindPackageByBarcode(barcode);
            if (package != null)
            {
                if (string.IsNullOrEmpty(Convert.ToString(package.MID))) judge = true;
            }
            return judge;
        }
    }
}
