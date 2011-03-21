//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Data.Objects.DataClasses;

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using ETS.GGGETSApp.Domain.Core.Entities;

namespace ETS.GGGETSApp.Domain.Application.Entities
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Package))]
    [Serializable]
    [System.CodeDom.Compiler.GeneratedCode("STE-EF",".NET 4.0")]
    #if !SILVERLIGHT
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage()]
    #endif
    public partial class MAWB: EntityObject,IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
    			[EdmScalarProperty(EntityKeyProperty=true,IsNullable=false)]
    			
        public System.Guid MID
        {
            get { return _mID; }
            set
            {
                if (_mID != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'MID' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _mID = value;
                    OnPropertyChanged("MID");
                }
            }
        }
        private System.Guid _mID;
    
        [DataMember]
    			[EdmScalarProperty(EntityKeyProperty=false,IsNullable=false)]
    			
        public string BarCode
        {
            get { return _barCode; }
            set
            {
                if (_barCode != value)
                {
                    _barCode = value;
                    OnPropertyChanged("BarCode");
                }
            }
        }
        private string _barCode;
    
        [DataMember]
    			[EdmScalarProperty(EntityKeyProperty=false,IsNullable=false)]
    			
        public string From
        {
            get { return _from; }
            set
            {
                if (_from != value)
                {
                    _from = value;
                    OnPropertyChanged("From");
                }
            }
        }
        private string _from;
    
        [DataMember]
    			[EdmScalarProperty(EntityKeyProperty=false,IsNullable=false)]
    			
        public string To
        {
            get { return _to; }
            set
            {
                if (_to != value)
                {
                    _to = value;
                    OnPropertyChanged("To");
                }
            }
        }
        private string _to;
    
        [DataMember]
    			[EdmScalarProperty(EntityKeyProperty=false,IsNullable=false)]
    			
        public string FlightNo
        {
            get { return _flightNo; }
            set
            {
                if (_flightNo != value)
                {
                    _flightNo = value;
                    OnPropertyChanged("FlightNo");
                }
            }
        }
        private string _flightNo;
    
        [DataMember]
    			[EdmScalarProperty(EntityKeyProperty=false,IsNullable=false)]
    			
        public System.DateTime CreateTime
        {
            get { return _createTime; }
            set
            {
                if (_createTime != value)
                {
                    _createTime = value;
                    OnPropertyChanged("CreateTime");
                }
            }
        }
        private System.DateTime _createTime;
    
        [DataMember]
    			[EdmScalarProperty(EntityKeyProperty=false,IsNullable=true)]	
        public Nullable<System.DateTime> LockedTime
        {
            get { return _lockedTime; }
            set
            {
                if (_lockedTime != value)
                {
                    _lockedTime = value;
                    OnPropertyChanged("LockedTime");
                }
            }
        }
        private Nullable<System.DateTime> _lockedTime;
    
        [DataMember]
    			[EdmScalarProperty(EntityKeyProperty=false,IsNullable=false)]
    			
        public string Operator
        {
            get { return _operator; }
            set
            {
                if (_operator != value)
                {
                    _operator = value;
                    OnPropertyChanged("Operator");
                }
            }
        }
        private string _operator;
    
        [DataMember]
    			[EdmScalarProperty(EntityKeyProperty=false,IsNullable=false)]
    			
        public decimal TotalWeight
        {
            get { return _totalWeight; }
            set
            {
                if (_totalWeight != value)
                {
                    _totalWeight = value;
                    OnPropertyChanged("TotalWeight");
                }
            }
        }
        private decimal _totalWeight;
    
        [DataMember]
    			[EdmScalarProperty(EntityKeyProperty=false,IsNullable=false)]
    			
        public decimal TotalVolume
        {
            get { return _totalVolume; }
            set
            {
                if (_totalVolume != value)
                {
                    _totalVolume = value;
                    OnPropertyChanged("TotalVolume");
                }
            }
        }
        private decimal _totalVolume;
    
        [DataMember]
    			[EdmScalarProperty(EntityKeyProperty=false,IsNullable=false)]
    			
        public int Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
        }
        private int _status;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<Package> Packages
        {
            get
            {
                if (_packages == null)
                {
                    _packages = new TrackableCollection<Package>();
                    _packages.CollectionChanged += FixupPackages;
                }
                return _packages;
            }
            set
            {
                if (!ReferenceEquals(_packages, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_packages != null)
                    {
                        _packages.CollectionChanged -= FixupPackages;
                    }
                    _packages = value;
                    if (_packages != null)
                    {
                        _packages.CollectionChanged += FixupPackages;
                    }
                    OnNavigationPropertyChanged("Packages");
                }
            }
        }
        private TrackableCollection<Package> _packages;

        #endregion
        #region ChangeTracking
    
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (ChangeTracker.State != ObjectState.Added && ChangeTracker.State != ObjectState.Deleted)
            {
                ChangeTracker.State = ObjectState.Modified;
            }
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        protected virtual void OnNavigationPropertyChanged(String propertyName)
        {
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged{ add { _propertyChanged += value; } remove { _propertyChanged -= value; } }
        private event PropertyChangedEventHandler _propertyChanged;
        private ObjectChangeTracker _changeTracker;
    
        [DataMember]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if (_changeTracker == null)
                {
                    _changeTracker = new ObjectChangeTracker();
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
                return _changeTracker;
            }
            set
            {
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging -= HandleObjectStateChanging;
                }
                _changeTracker = value;
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
            }
        }
    
        private void HandleObjectStateChanging(object sender, ObjectStateChangingEventArgs e)
        {
            if (e.NewState == ObjectState.Deleted)
            {
                ClearNavigationProperties();
            }
        }
    
        protected bool IsDeserializing { get; private set; }
    
        [OnDeserializing]
        public void OnDeserializingMethod(StreamingContext context)
        {
            IsDeserializing = true;
        }
    
        [OnDeserialized]
        public void OnDeserializedMethod(StreamingContext context)
        {
            IsDeserializing = false;
            ChangeTracker.ChangeTrackingEnabled = true;
        }
    
        protected virtual void ClearNavigationProperties()
        {
            Packages.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupPackages(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (Package item in e.NewItems)
                {
                    item.MAWB = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("Packages", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Package item in e.OldItems)
                {
                    if (ReferenceEquals(item.MAWB, this))
                    {
                        item.MAWB = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("Packages", item);
                    }
                }
            }
        }

        #endregion
    }
}
