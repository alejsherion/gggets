﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data;
using System.Data.Objects.DataClasses;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.IO;
using EFCachingProvider;
using EFCachingProvider.Caching;
using EFProviderWrapperToolkit;
using EFTracingProvider;
using ETS.GGGETSApp.Domain.Core.Entities;
using ETS.GGGETSApp.Domain.Application.Entities;



using ETS.GGGETSApp.Domain.Core.Entities;
using ETS.GGGETSApp.Domain.Core;
using ETS.GGGETSApp.Domain.Application.Entities;
using System.Reflection;



using ETS.GGGETSApp.Domain.Core.Entities;
using ETS.GGGETSApp.Domain.Core;
using ETS.GGGETSApp.Domain.Application.Entities;
using System.Reflection;

namespace ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork
{
    [System.Diagnostics.DebuggerNonUserCode()]
    public partial class GGGETSUnitOfWork : ObjectContext,IGGGETSAppUnitOfWork
    {
        public const string ConnectionString = "name=GGGETSUnitOfWork";
        public const string ContainerName = "GGGETSUnitOfWork";
    	private TextWriter logOutput;
    
        #region Constructors
    	
        public GGGETSUnitOfWork()
            : base(EntityConnectionWrapperUtils.CreateEntityConnectionWithWrappers(
                        ConnectionString,
                        "EFTracingProvider",
                        "EFCachingProvider"
                ), ContainerName)
        {
            Initialize();
        }
    
        public GGGETSUnitOfWork(string connectionString)
            : base(EntityConnectionWrapperUtils.CreateEntityConnectionWithWrappers(
                        connectionString,
                        "EFTracingProvider",
                        "EFCachingProvider"
                ), ContainerName)
        {
            Initialize();
        }
    
        public GGGETSUnitOfWork(EntityConnection connection)
            : base(connection, ContainerName)
        {
            Initialize();
        }
    
        private void Initialize()
        {
            // Creating proxies requires the use of the ProxyDataContractResolver and
            // may allow lazy loading which can expand the loaded graph during serialization.
            ContextOptions.ProxyCreationEnabled = false;
    		ContextOptions.LazyLoadingEnabled = false;
            ObjectMaterialized += new ObjectMaterializedEventHandler(HandleObjectMaterialized);
        }
    
        private void HandleObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            var entity = e.Entity as IObjectWithChangeTracker;
            if (entity != null)
            {
                bool changeTrackingEnabled = entity.ChangeTracker.ChangeTrackingEnabled;
                try
                {
                    entity.MarkAsUnchanged();
                }
                finally
                {
                    entity.ChangeTracker.ChangeTrackingEnabled = changeTrackingEnabled;
                }
                this.StoreReferenceKeyValues(entity);
            }
        }
    
        #endregion
    #region Tracing Extensions
    
        private EFTracingConnection TracingConnection
        {
            get { return this.UnwrapConnection<EFTracingConnection>(); }
        }
    
        public event EventHandler<CommandExecutionEventArgs> CommandExecuting
        {
            add { this.TracingConnection.CommandExecuting += value; }
            remove { this.TracingConnection.CommandExecuting -= value; }
        }
    
        public event EventHandler<CommandExecutionEventArgs> CommandFinished
        {
            add { this.TracingConnection.CommandFinished += value; }
            remove { this.TracingConnection.CommandFinished -= value; }
        }
    
        public event EventHandler<CommandExecutionEventArgs> CommandFailed
        {
            add { this.TracingConnection.CommandFailed += value; }
            remove { this.TracingConnection.CommandFailed -= value; }
        }
    
        private void AppendToLog(object sender, CommandExecutionEventArgs e)
        {
            if (this.logOutput != null)
            {
                this.logOutput.WriteLine(e.ToTraceString().TrimEnd());
                this.logOutput.WriteLine();
            }
        }
    
        public TextWriter Log
        {
            get { return this.logOutput; }
            set
            {
                if ((this.logOutput != null) != (value != null))
                {
                    if (value == null)
                    {
                        CommandExecuting -= AppendToLog;
                    }
                    else
                    {
                        CommandExecuting += AppendToLog;
                    }
                }
    
                this.logOutput = value;
            }
        }
    
    
        #endregion
    
        #region Caching Extensions
    
        private EFCachingConnection CachingConnection
        {
            get { return this.UnwrapConnection<EFCachingConnection>(); }
        }
    
        public ICache Cache
        {
            get { return CachingConnection.Cache; }
            set { CachingConnection.Cache = value; }
        }
    
        public CachingPolicy CachingPolicy
        {
            get { return CachingConnection.CachingPolicy; }
            set { CachingConnection.CachingPolicy = value; }
        }
    
        #endregion
        #region IMainModuleUnitOfWork
    	
    	public  IObjectSet<TEntity> CreateSet<TEntity>() 
    		where TEntity : class,IObjectWithChangeTracker
    	{
    		return base.CreateObjectSet<TEntity>() as IObjectSet<TEntity>;
    	}
    	public void RegisterChanges<TEntity>(TEntity item)
    		where TEntity : class, IObjectWithChangeTracker
    	{
    		this.CreateObjectSet<TEntity>().ApplyChanges(item);
    	}
    	public void CommitAndRefreshChanges()
    	{
    		try
    		{
    			//Default option is DetectChangesBeforeSave
    			base.SaveChanges();
    			
    			//accept all changes in STE entities attached in context
                IEnumerable<IObjectWithChangeTracker> steEntities = (from entry in 
    																	this.ObjectStateManager.GetObjectStateEntries(~EntityState.Detached)
                                                                     where 
    																 	entry.Entity != null 
    																 && 
    																 	(entry.Entity as IObjectWithChangeTracker != null)
                                                                     select
    																 	entry.Entity as IObjectWithChangeTracker);
    
                steEntities.ToList().ForEach(ste => ste.MarkAsUnchanged());
    		}
    		catch (OptimisticConcurrencyException ex)
    		{
    			
    			//if client wins refresh data ( queries database and adapt original values
    			//and re-save changes in client
    			base.Refresh(RefreshMode.ClientWins, ex.StateEntries.Select(se => se.Entity));
    			
    			base.SaveChanges(); 
    			
    			//accept all changes in STE entities attached in context
                IEnumerable<IObjectWithChangeTracker> steEntities = (from entry in 
    																	this.ObjectStateManager.GetObjectStateEntries(~EntityState.Detached)
                                                                     where 
    																 	entry.Entity != null 
    																 && 
    																 	(entry.Entity as IObjectWithChangeTracker != null)
                                                                     select
    																 	entry.Entity as IObjectWithChangeTracker);
    
                steEntities.ToList().ForEach(ste => ste.MarkAsUnchanged());
    		}
    	}
    	public  void Commit()
    	{
    		//Default option is DetectChangesBeforeSave
    		base.SaveChanges();
    		
    		//accept all changes in STE entities attached in context
    		IEnumerable<IObjectWithChangeTracker> steEntities = (from entry in 
    																	this.ObjectStateManager.GetObjectStateEntries(~EntityState.Detached)
                                                                     where 
    																 	entry.Entity != null 
    																 && 
    																 	(entry.Entity as IObjectWithChangeTracker != null)
                                                                     select
    																 	entry.Entity as IObjectWithChangeTracker);
    
    		steEntities.ToList().ForEach(ste => ste.MarkAsUnchanged());
    	}
    	public void RollbackChanges()
    	{
    		//Refresh context and override changes
                
    		IEnumerable<object> itemsToRefresh = base.ObjectStateManager.GetObjectStateEntries(EntityState.Modified)
                                                                        .Where(ose=>!ose.IsRelationship && ose.Entity != null)
                                                                        .Select(ose=>ose.Entity);
            base.Refresh(RefreshMode.StoreWins, itemsToRefresh);
    	}
    	public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
    		return base.ExecuteStoreQuery<TEntity>(sqlQuery, parameters);
       	}
    
    	public int ExecuteCommand(string sqlCommand, params object[] parameters)
    	{
    		return base.ExecuteStoreCommand(sqlCommand, parameters);
    	}
    	

        #endregion
        #region ObjectSet Properties
    
        public IObjectSet<AddressBook> AddressBook
        {
            get { return _addressBook  ?? (_addressBook = CreateObjectSet<AddressBook>("AddressBook")); }
        }
        private ObjectSet<AddressBook> _addressBook;
    
        public IObjectSet<AppModule> AppModule
        {
            get { return _appModule  ?? (_appModule = CreateObjectSet<AppModule>("AppModule")); }
        }
        private ObjectSet<AppModule> _appModule;
    
        public IObjectSet<Company> Company
        {
            get { return _company  ?? (_company = CreateObjectSet<Company>("Company")); }
        }
        private ObjectSet<Company> _company;
    
        public IObjectSet<CountryCode> CountryCode
        {
            get { return _countryCode  ?? (_countryCode = CreateObjectSet<CountryCode>("CountryCode")); }
        }
        private ObjectSet<CountryCode> _countryCode;
    
        public IObjectSet<Department> Department
        {
            get { return _department  ?? (_department = CreateObjectSet<Department>("Department")); }
        }
        private ObjectSet<Department> _department;
    
        public IObjectSet<HAWB> HAWB
        {
            get { return _hAWB  ?? (_hAWB = CreateObjectSet<HAWB>("HAWB")); }
        }
        private ObjectSet<HAWB> _hAWB;
    
        public IObjectSet<HAWBBox> HAWBBox
        {
            get { return _hAWBBox  ?? (_hAWBBox = CreateObjectSet<HAWBBox>("HAWBBox")); }
        }
        private ObjectSet<HAWBBox> _hAWBBox;
    
        public IObjectSet<HAWBItem> HAWBItem
        {
            get { return _hAWBItem  ?? (_hAWBItem = CreateObjectSet<HAWBItem>("HAWBItem")); }
        }
        private ObjectSet<HAWBItem> _hAWBItem;
    
        public IObjectSet<MAWB> MAWB
        {
            get { return _mAWB  ?? (_mAWB = CreateObjectSet<MAWB>("MAWB")); }
        }
        private ObjectSet<MAWB> _mAWB;
    
        public IObjectSet<Package> Package
        {
            get { return _package  ?? (_package = CreateObjectSet<Package>("Package")); }
        }
        private ObjectSet<Package> _package;
    
        public IObjectSet<Param> Param
        {
            get { return _param  ?? (_param = CreateObjectSet<Param>("Param")); }
        }
        private ObjectSet<Param> _param;
    
        public IObjectSet<Privilege> Privilege
        {
            get { return _privilege  ?? (_privilege = CreateObjectSet<Privilege>("Privilege")); }
        }
        private ObjectSet<Privilege> _privilege;
    
        public IObjectSet<RegionCode> RegionCode
        {
            get { return _regionCode  ?? (_regionCode = CreateObjectSet<RegionCode>("RegionCode")); }
        }
        private ObjectSet<RegionCode> _regionCode;
    
        public IObjectSet<Template> Template
        {
            get { return _template  ?? (_template = CreateObjectSet<Template>("Template")); }
        }
        private ObjectSet<Template> _template;
    
        public IObjectSet<User> User
        {
            get { return _user  ?? (_user = CreateObjectSet<User>("User")); }
        }
        private ObjectSet<User> _user;
    
        public IObjectSet<HSProduct> HSProduct
        {
            get { return _hSProduct  ?? (_hSProduct = CreateObjectSet<HSProduct>("HSProduct")); }
        }
        private ObjectSet<HSProduct> _hSProduct;
    
        public IObjectSet<HSProperty> HSProperty
        {
            get { return _hSProperty  ?? (_hSProperty = CreateObjectSet<HSProperty>("HSProperty")); }
        }
        private ObjectSet<HSProperty> _hSProperty;
    
        public IObjectSet<FindInfo> FindInfo
        {
            get { return _findInfo  ?? (_findInfo = CreateObjectSet<FindInfo>("FindInfo")); }
        }
        private ObjectSet<FindInfo> _findInfo;

        #endregion
    }
    
}
