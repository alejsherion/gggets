//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        航班BLL
// 作成者				ZhiWei.Shen
// 改版日				2011.02.21
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Core;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Domain.Core.Specification;

namespace Application.GGETS
{
    public class FlightManagementService:IFlightManagementService
    {
        /// <summary>
        /// IOC Injecting into
        /// </summary>
        IFlightRepository _flightRepository;

        private IHAWBRepository _hawbRepository;

        public FlightManagementService(IFlightRepository flightRepository, IHAWBRepository hawbRepository)
        {
            _flightRepository = flightRepository;
            _hawbRepository = hawbRepository;
        }
        /// <summary>
        /// 航班多条件查询
        /// </summary>
        /// <param name="flightNo">航班号</param>
        /// <param name="from">出发地三字码</param>
        /// <param name="to">目的地三字码</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public IList<Flight> FindFlightByCondition(string flightNo, string from, string to, DateTime? beginDate, DateTime? endDate)
        {
            return _flightRepository.FindFlightByCondition(flightNo, from, to, beginDate, endDate);
        }

        /// <summary>
        /// 根据航班号查询航班
        /// </summary>
        /// <param name="flightNo">航班号</param>
        /// <returns></returns>
        public Flight FindFlightByFlightNo(string flightNo)
        {
            return _hawbRepository.FindFlightByFlightNo(flightNo);
        }

        /// <summary>
        /// 修改航班属性
        /// </summary>
        /// <param name="flight">航班</param>
        public void ModifyFlight(Flight flight)
        {
            if (flight == null)
                throw new ArgumentNullException("Flight is null");
            IUnitOfWork unitOfWork = _flightRepository.UnitOfWork;
            _flightRepository.Modify(flight);
            //complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
        }

        /// <summary>
        /// 添加航班
        /// </summary>
        /// <param name="flight">航班</param>
        public void AddFlight(Flight flight)
        {
            if (flight == null)
                throw new ArgumentNullException("Flight is null");
            IUnitOfWork unitOfWork = _flightRepository.UnitOfWork;
            _flightRepository.Add(flight);
            //complete changes in this unit of work
            unitOfWork.Commit();
        }

        /// <summary>
        /// 获取所有的航班信息
        /// </summary>
        /// <returns></returns>
        public IList<Flight> FindAllFlights()
        {
            return _hawbRepository.FindAllFlights();
        }
    }
}
