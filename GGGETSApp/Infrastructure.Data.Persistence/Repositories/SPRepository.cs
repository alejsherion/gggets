//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        存储过程底层调用
// 作成者				ZhiWei.Shen
// 改版日				2011.04.22
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Data;
using System.Data.EntityClient;
using System.Data.SqlClient;
using ETS.GGGETSApp.Infrastructure.Data.Core;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork;
using ETS.GGGETSApp.Infrastructure.CrossCutting.Logging;
using ETS.GGGETSApp.Domain.Application.Entities;
using Domain.GGGETS;

namespace ETS.GGGETSApp.Infrastructure.Data.Persistence.Repositories
{
    public class SPRepository : Repository<HAWBItem>, ISPRepository
    {
        public SPRepository(IGGGETSAppUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager) { }
        /// <summary>
        /// 获取数据库连接信息
        /// </summary>
        /// <returns></returns>
        private EntityConnectionStringBuilder GetSQLBuilder()
        {
            // Specify the provider name, server and database.
            string providerName = "System.Data.SqlClient";
            string serverName = ".";
            string databaseName = "MYGGGETS";

            // Initialize the connection string builder for the
            // underlying provider.
            SqlConnectionStringBuilder sqlBuilder =
                new SqlConnectionStringBuilder();

            // Set the properties for the data source.
            sqlBuilder.DataSource = serverName;
            sqlBuilder.InitialCatalog = databaseName;
            sqlBuilder.IntegratedSecurity = true;

            // Build the SqlConnection connection string.
            string providerString = sqlBuilder.ToString();

            // Initialize the EntityConnectionStringBuilder.
            EntityConnectionStringBuilder entityBuilder =
                new EntityConnectionStringBuilder();

            //Set the provider name.
            entityBuilder.Provider = providerName;

            // Set the provider-specific connection string.
            entityBuilder.ProviderConnectionString = providerString;

            // Set the Metadata location.
            entityBuilder.Metadata = @"res://*/Model.GGGETSAppDataModel.csdl|
                            res://*/Model.GGGETSAppDataModel.ssdl|
                            res://*/Model.GGGETSAppDataModel.msl";

            return entityBuilder;
        }

        /// <summary>
        /// 批量更新报关文件导入后的运单报关状态
        /// 有效解决返回值问题
        /// </summary>
        /// <param name="xmlStr">XML解析字符串</param>
        /// <param name="mawbCode">总运单编号</param>
        /// <returns></returns>
        public int UseBatchUpdateCustomsClearanceState(string xmlStr, string mawbCode)
        {
            EntityConnection connection = new EntityConnection(GetSQLBuilder().ToString());
            GGGETSUnitOfWork EFModel = new GGGETSUnitOfWork(connection);
            //EFModel.BatchUpdateCustomsClearanceState(xmlStr, mawbCode);//另外一种简便方法，但是获取不到返回值

            EFModel.Connection.Open();
            EntityCommand cmd = ((EntityConnection)EFModel.Connection).CreateCommand();

            cmd.CommandText = EFModel.DefaultContainerName + ".BatchUpdateCustomsClearanceState";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("xmldata", xmlStr);
            cmd.Parameters.AddWithValue("mawbCode", mawbCode);

            //EntityParameter para2 = new EntityParameter("out_sErrMsg", DbType.String);
            //para2.Direction = ParameterDirection.Output;
            //para2.Value = string.Empty;
            //cmd.Parameters.Add(para2);

            EntityParameter ret = new EntityParameter("ReturnValue", DbType.Int32);
            ret.Direction = ParameterDirection.ReturnValue;
            ret.Value = 0;
            cmd.Parameters.Add(ret);

            cmd.ExecuteNonQuery();

            //errMsg = cmd.Parameters["out_sErrMsg"].Value.ToString();
            int returnValue = (int)cmd.Parameters["ReturnValue"].Value;
            return returnValue;
        }

        /// <summary>
        /// 批量更新运单拆包后的状态
        /// </summary>
        /// <param name="xmlStr">XML字符串</param>
        public int UseBatchUpdateHAWBPackageState(string xmlStr)
        {
            EntityConnection connection = new EntityConnection(GetSQLBuilder().ToString());
            GGGETSUnitOfWork EFModel = new GGGETSUnitOfWork(connection);

            EFModel.Connection.Open();
            EntityCommand cmd = ((EntityConnection)EFModel.Connection).CreateCommand();

            cmd.CommandText = EFModel.DefaultContainerName + ".BatchUpdateHAWBPackageState";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("xmlStr", xmlStr);

            EntityParameter ret = new EntityParameter("ReturnValue", DbType.Int32);
            ret.Direction = ParameterDirection.ReturnValue;
            ret.Value = 0;
            cmd.Parameters.Add(ret);

            cmd.ExecuteNonQuery();

            int returnValue = (int)cmd.Parameters["ReturnValue"].Value;
            return returnValue;
        }

        /// <summary>
        /// 批量更新运单路单信息
        /// </summary>
        /// <param name="xmlStr">XML字符串</param>
        /// <param name="waybill">路单编号</param>
        /// <returns></returns>
        public int UseBatchUpdateWayBillCode(string xmlStr, string waybill)
        {
            EntityConnection connection = new EntityConnection(GetSQLBuilder().ToString());
            GGGETSUnitOfWork EFModel = new GGGETSUnitOfWork(connection);

            EFModel.Connection.Open();
            EntityCommand cmd = ((EntityConnection)EFModel.Connection).CreateCommand();

            cmd.CommandText = EFModel.DefaultContainerName + ".BatchUpdateWayBillCode";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("xmldata", xmlStr);
            cmd.Parameters.AddWithValue("waybillcode", waybill);

            EntityParameter ret = new EntityParameter("ReturnValue", DbType.Int32);
            ret.Direction = ParameterDirection.ReturnValue;
            ret.Value = 0;
            cmd.Parameters.Add(ret);

            cmd.ExecuteNonQuery();

            int returnValue = (int)cmd.Parameters["ReturnValue"].Value;
            return returnValue;
        }
    }
}
