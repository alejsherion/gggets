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
using System.Data.Objects.DataClasses;
using System.Data.Objects;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;



using ETS.GGGETSApp.Infrastructure.Data.Core;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork
{
    
    ///<sumary>
    ///Base contract for context in Main Module 
    ///</sumary>
    public interface IGGGETSAppUnitOfWork :IQueryableUnitOfWork
    {
       
        #region ObjectSet Properties
    
        IObjectSet<AddressBook> AddressBook{get;}
        
    
        IObjectSet<AppModule> AppModule{get;}
        
    
        IObjectSet<Company> Company{get;}
        
    
        IObjectSet<CountryCode> CountryCode{get;}
        
    
        IObjectSet<Department> Department{get;}
        
    
        IObjectSet<HAWB> HAWB{get;}
        
    
        IObjectSet<HAWBBox> HAWBBox{get;}
        
    
        IObjectSet<HAWBItem> HAWBItem{get;}
        
    
        IObjectSet<MAWB> MAWB{get;}
        
    
        IObjectSet<Package> Package{get;}
        
    
        IObjectSet<Param> Param{get;}
        
    
        IObjectSet<Privilege> Privilege{get;}
        
    
        IObjectSet<RegionCode> RegionCode{get;}
        
    
        IObjectSet<Template> Template{get;}
        
    
        IObjectSet<User> User{get;}
        
    
        IObjectSet<HSProduct> HSProduct{get;}
        
    
        IObjectSet<HSProperty> HSProperty{get;}
        
    
        IObjectSet<HSRelation> HSRelation{get;}
        

        #endregion
    
    }
}
	
