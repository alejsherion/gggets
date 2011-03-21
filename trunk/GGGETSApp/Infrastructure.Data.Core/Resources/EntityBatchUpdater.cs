//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        批量更新通用类
// 作成者				ZhiWei.Shen
// 改版日				2011.03.21
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ETS.GGGETSApp.Infrastructure.Data.Core
{
    public class EntityBatchUpdater<T>:IDisposable where T :ObjectContext
    {
        private static Assembly _systemDataEntity = null;
	    private static Type _propagatorResultType = null;
	    private static Type _entityAdapterType = null;
	    private static Type _updateTranslatorType = null;
	    private static Type _entityStateType = null;

        static EntityBatchUpdater()
	    {
	        _systemDataEntity = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetName().Name == "System.Data.Entity").FirstOrDefault();
	        Type t = _systemDataEntity.GetType("System.Data.Mapping.Update.Internal.PropagatorResult");
	        Type t1 = typeof(KeyValuePair<,>).MakeGenericType(t, typeof(object));
	        Type t2 = typeof(List<>).MakeGenericType(t1);
	        _entityAdapterType = _systemDataEntity.GetType("System.Data.IEntityAdapter");
	        _updateTranslatorType = _systemDataEntity.GetType("System.Data.Mapping.Update.Internal.UpdateTranslator");
	        _entityStateType = _systemDataEntity.GetType("System.Data.IEntityStateManager");
	        _propagatorResultType = t2;
	    }

        private T _context = null;
        public T ObjectContext
	    {
	        get
	        {
	            return _context;
	        }
	    }

        public EntityBatchUpdater()       
	    {
            _context = (T)typeof(T).GetConstructor(new Type[]{}).Invoke(new object[]{});
	    }

        static object CreatePropagatorResultDictionary()
	    {
	        return Activator.CreateInstance(_propagatorResultType);
	    }

        static object GetEntityAdapter(ObjectContext context)
	    {
	        object providerFactory = typeof(EntityConnection).GetProperty("ProviderFactory",
	            BindingFlags.NonPublic | BindingFlags.Instance).GetValue(context.Connection, null);
	        object result = ((IServiceProvider)providerFactory).GetService(_entityAdapterType);
	        return result;
	    }

        static object CreateUpdateTranslator(object entityStateManager, System.Data.Metadata.Edm.MetadataWorkspace workspace, EntityConnection connection, int? commandTimeout)
	    {
	        ConstructorInfo ci = _updateTranslatorType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null,
	            new Type[] { _entityStateType, typeof(System.Data.Metadata.Edm.MetadataWorkspace), typeof(EntityConnection), typeof(int?) }, null);
	        return ci.Invoke(new object[] { entityStateManager, workspace, connection, commandTimeout });
	    }

        static string GetQueryStatement(ObjectQuery query)
	    {
	        object queryState = typeof(ObjectQuery).GetProperty("QueryState", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(query, null);
	        object queryPlan = queryState.GetType().BaseType.InvokeMember("GetExecutionPlan", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod,
	                                                                        null, queryState, new object[] { null });
	        DbCommandDefinition cmddef = (DbCommandDefinition)queryPlan.GetType().GetField("CommandDefinition", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(queryPlan);
	 
	 
	        IEnumerable<string> cmds = (IEnumerable<string>)cmddef.GetType().GetProperty("MappedCommands", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(cmddef, null);
	        return cmds.FirstOrDefault();
	    }

        public static void Update(ObjectContext context)
	    {
	        object entityAdapter = GetEntityAdapter(context);
	        object updateTranslator = CreateUpdateTranslator(context.ObjectStateManager, ((EntityConnection)context.Connection).GetMetadataWorkspace(), (EntityConnection)context.Connection, context.CommandTimeout);
	        IEnumerable o = (IEnumerable)updateTranslator.GetType().InvokeMember("ProduceCommands",
	            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, updateTranslator, null);
	        Dictionary<int, object> identifierValues = new Dictionary<int, object>();
	        object generateValues = CreatePropagatorResultDictionary();
	        context.Connection.Open();
	        try
	        {
	            foreach (var item in o)
	            {
	                item.GetType().InvokeMember("Execute", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, item,
	                    new object[] { updateTranslator, (EntityConnection)context.Connection, identifierValues, generateValues });
	            }
	        }
	        finally
	        {
	            context.Connection.Close();
	        }
	    }

        private static void MarkModifiedProperty(ObjectContext context, object entity, params string[] propertys)
	    {
	        context.ObjectStateManager.ChangeObjectState(entity, EntityState.Unchanged);
	        ObjectStateEntry objectStateEntry = context.ObjectStateManager.GetObjectStateEntry(entity);
	        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entity.GetType());
	        foreach (FieldMetadata metadata in objectStateEntry.CurrentValues.DataRecordInfo.FieldMetadata)
	        {
	            string name = objectStateEntry.CurrentValues.GetName(metadata.Ordinal);
	            PropertyDescriptor descriptor = properties[name];
	            if (propertys.Contains(descriptor.Name))
	                objectStateEntry.SetModifiedProperty(descriptor.Name);
	        }
	    }

        public static void UpdateDirect(ObjectContext context, string orKeyFields)
	    {
	        object entityAdapter = GetEntityAdapter(context);
	        object updateTranslator = CreateUpdateTranslator(context.ObjectStateManager, ((EntityConnection)context.Connection).GetMetadataWorkspace(),
	            (EntityConnection)context.Connection, context.CommandTimeout);
	        IEnumerable o = (IEnumerable)updateTranslator.GetType().InvokeMember("ProduceCommands",
	            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, updateTranslator, null);
	        Dictionary<int, object> identifierValues = new Dictionary<int, object>();
	        object generateValues = CreatePropagatorResultDictionary();
	        context.Connection.Open();
	        try
	        {
	            foreach (var item in o)
	            {
	                DbCommand cmd = (DbCommand)item.GetType().InvokeMember("CreateCommand", BindingFlags.NonPublic | BindingFlags.Instance |
	                    BindingFlags.InvokeMethod, null, item,
	                    new object[] { updateTranslator, identifierValues });
	                cmd.Connection = ((EntityConnection)context.Connection).StoreConnection;
	                cmd.CommandText = cmd.CommandText + " OR " + orKeyFields;
	                cmd.ExecuteReader(CommandBehavior.CloseConnection);
	            }
	        }
	        finally
	        {
	            context.Connection.Close();
	        }
	    }

        public void UpdateBatch(EntityObject entity, IQueryable query)
	    {
	        if (!(query is ObjectQuery))
	            throw new Exception("only support ObjectQuery.");
	        object entityAdapter = GetEntityAdapter(_context);
	        object updateTranslator = CreateUpdateTranslator(_context.ObjectStateManager, ((EntityConnection)_context.Connection).GetMetadataWorkspace(),
	            (EntityConnection)_context.Connection, _context.CommandTimeout);
	        IEnumerable o = (IEnumerable)updateTranslator.GetType().InvokeMember("ProduceCommands",
	            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, updateTranslator, null);
	        Dictionary<int, object> identifierValues = new Dictionary<int, object>();
	        object generateValues = CreatePropagatorResultDictionary();
	        _context.Connection.Open();
	        try
	        {
	            foreach (var item in o)
	            {
	                DbCommand cmd = (DbCommand)item.GetType().InvokeMember("CreateCommand", BindingFlags.NonPublic | BindingFlags.Instance |
	                    BindingFlags.InvokeMethod, null, item,
	                    new object[] { updateTranslator, identifierValues });
	                cmd.Connection = ((EntityConnection)_context.Connection).StoreConnection;
	                string queryStatement = GetQueryStatement(query as ObjectQuery);
	                if (queryStatement.ToLower().Contains("where"))
	                    queryStatement = queryStatement.Substring(queryStatement.ToLower().IndexOf("where ") + 5);
	                cmd.CommandText = cmd.CommandText.Substring(0, cmd.CommandText.ToLower().IndexOf("where ") - 1) + " Where " +
	                            queryStatement.Replace("[Extent1].", "").Replace("\"Extent1\".", "").Replace("Extent1.", "");
	                RemovePrimaryKeyParameter(cmd, entity);
	                cmd.ExecuteReader(CommandBehavior.CloseConnection);
	            }
	        }
	        finally
	        {
	            _context.Connection.Close();
	        }
	    }

        private static void RemovePrimaryKeyParameter(DbCommand cmd, EntityObject entity)
	    {
	        foreach (var prop in entity.GetType().GetProperties())
	        {
	            EdmScalarPropertyAttribute[] attrs = (EdmScalarPropertyAttribute[])prop.GetCustomAttributes(typeof(EdmScalarPropertyAttribute), true);
	            if (attrs != null && attrs.Length > 0)
	            {
	                if (attrs[0].EntityKeyProperty)
	                    cmd.Parameters.RemoveAt(cmd.Parameters.Count - 1);
	            }
	        }
	    }

        public void TrackEntity(EntityObject entity)
	    {
	        if (entity.EntityKey == null)
	        {
	            EntityKey keys = new EntityKey();
	            List<EntityKeyMember> members = new List<EntityKeyMember>();
	            foreach (var prop in entity.GetType().GetProperties())
	            {
	                EdmScalarPropertyAttribute[] attrs = (EdmScalarPropertyAttribute[])prop.GetCustomAttributes(typeof(EdmScalarPropertyAttribute), true);
	                if (attrs != null && attrs.Length > 0)
                    {
	                    if (attrs[0].EntityKeyProperty)
	                    {
	                        object defaultValue = null;

                            if (prop.PropertyType == typeof(string))
                                defaultValue = "";
                            else if (prop.PropertyType == typeof(int) ||
                                    prop.PropertyType == typeof(double) ||
                                    prop.PropertyType == typeof(float) ||
                                    prop.PropertyType == typeof(Int32) ||
                                    prop.PropertyType == typeof(Int16) ||
                                    prop.PropertyType == typeof(Int64) ||
                                    prop.PropertyType == typeof(long) ||
                                    prop.PropertyType == typeof(short))
                                defaultValue = -1;
                            else if (prop.PropertyType == typeof(DateTime))
                                defaultValue = DateTime.MinValue;
                            else if (prop.PropertyType == typeof(TimeSpan))
                                defaultValue = TimeSpan.MinValue;
                            else if (prop.PropertyType == typeof(Char))
                                defaultValue = 'C';
                            else if (prop.PropertyType == typeof(Guid))
                                defaultValue = Guid.NewGuid();
	                        prop.SetValue(entity, defaultValue, null);
	                        members.Add(new EntityKeyMember(prop.Name, defaultValue));
	                    }
	                }
	            }
	            keys.EntityKeyValues = members.ToArray();
	            EdmEntityTypeAttribute[] attrs1 = (EdmEntityTypeAttribute[])entity.GetType().GetCustomAttributes(typeof(EdmEntityTypeAttribute), true);
	            if (attrs1 != null && attrs1.Length > 0)
	            {
	                keys.EntityContainerName = _context.DefaultContainerName;
	                keys.EntitySetName = attrs1[0].Name;
	            }
	            entity.EntityKey = keys;
	        }
	 
	        _context.Attach(entity);
	 
	        entity.PropertyChanged += (s, args) =>
	        {
	            MarkModifiedProperty(_context, entity, args.PropertyName);
	        };
	    }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
