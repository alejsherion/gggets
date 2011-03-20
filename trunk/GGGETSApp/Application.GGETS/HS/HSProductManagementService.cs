//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        HS海关商品编码BLL
// 作成者				ZhiWei.Shen
// 改版日				2011.03.12
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
    public class HSProductManagementService : IHSProductManagementService
    {
        /// <summary>
        /// IOC Injecting into
        /// </summary>
        IHSProductRepository _HSProductRepository;
        public HSProductManagementService(IHSProductRepository HSProductRepository)
        {
            _HSProductRepository = HSProductRepository;
        }

        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public IList<HSProduct> GetAll()
        {
            return _HSProductRepository.GetAll().ToList();
        }

        /// <summary>
        /// 获取分页后的商品编码信息
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageCount">一页显示个数</param>
        /// <returns></returns>
        public IList<HSProduct> GetPagedAll(int pageIndex, int pageCount)
        {
            return _HSProductRepository.GetPagedAll(pageIndex, pageCount);
        }

        /// <summary>
        /// 通过HS编码获取产品
        /// </summary>
        /// <param name="HSCode">HS编码</param>
        /// <returns></returns>
        public HSProduct FindHSProductByHSCode(string HSCode)
        {
            return _HSProductRepository.FindHSProductByHSCode(HSCode);
        }

        public HSProduct LoadHSProductByHSCode(string HSCode)
        {
            return _HSProductRepository.LoadHSProductByHSCode(HSCode);
        }

        /// <summary>
        /// HS海关商品编码多条件查询
        /// </summary>
        /// <param name="HSCode">编码</param>
        /// <param name="HSName">商品名称</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageCount">一页显示个数</param>
        /// <returns></returns>
        public IList<HSProduct> FindHSProductByCondition(string HSCode, string HSName,int pageIndex,int pageCount, ref int totalCount)
        {
            IList<HSProduct> hsproducts = FindHSProductByCondition(HSCode, HSName);
            if (hsproducts != null || hsproducts.Count!=0) totalCount = hsproducts.Count();
            else totalCount = 0;

            return _HSProductRepository.FindHSProductByCondition(HSCode, HSName, pageIndex, pageCount);
        }

        public IList<HSProduct> FindHSProductByCondition(string HSCode, string HSName)
        {
            return _HSProductRepository.FindHSProductByCondition(HSCode, HSName);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="product">海关商品</param>
        public void ModifyHSProduct(HSProduct product)
        {
            if (product == null)
                throw new ArgumentNullException("HSProduct is null");
            IUnitOfWork unitOfWork = _HSProductRepository.UnitOfWork;
            _HSProductRepository.Modify(product);
            //complete changes in this unit of work
            unitOfWork.CommitAndRefreshChanges();
        }

        /// <summary>
        /// 新增海关商品
        /// </summary>
        /// <param name="product">商品</param>
        public void AddHSProduct(HSProduct product)
        {
            if (product == null)
                throw new ArgumentNullException("HSProduct is null");
            IUnitOfWork unitOfWork = _HSProductRepository.UnitOfWork;
            _HSProductRepository.Add(product);
            //complete changes in this unit of work
            unitOfWork.Commit();
        }
    }
}
