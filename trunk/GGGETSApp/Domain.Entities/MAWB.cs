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

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using ETS.GGGETSApp.Domain.Core.Entities;

namespace ETS.GGGETSApp.Domain.Application.Entities
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Flight))]
    [KnownType(typeof(Package))]
    [Serializable]
    [System.CodeDom.Compiler.GeneratedCode("STE-EF",".NET 4.0")]
    #if !SILVERLIGHT
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage()]
    #endif
    public partial class MAWB: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
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
        public TrackableCollection<Flight> Flight
        {
            get
            {
                if (_flight == null)
                {
                    _flight = new TrackableCollection<Flight>();
                    _flight.CollectionChanged += FixupFlight;
                }
                return _flight;
            }
            set
            {
                if (!ReferenceEquals(_flight, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_flight != null)
                    {
                        _flight.CollectionChanged -= FixupFlight;
                    }
                    _flight = value;
                    if (_flight != null)
                    {
                        _flight.CollectionChanged += FixupFlight;
                    }
                    OnNavigationPropertyChanged("Flight");
                }
            }
        }
        private TrackableCollection<Flight> _flight;
    
        [DataMember]
        public TrackableCollection<Package> Package
        {
            get
            {
                if (_package == null)
                {
                    _package = new TrackableCollection<Package>();
                    _package.CollectionChanged += FixupPackage;
                }
                return _package;
            }
            set
            {
                if (!ReferenceEquals(_package, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_package != null)
                    {
                        _package.CollectionChanged -= FixupPackage;
                    }
                    _package = value;
                    if (_package != null)
                    {
                        _package.CollectionChanged += FixupPackage;
                    }
                    OnNavigationPropertyChanged("Package");
                }
            }
        }
        private TrackableCollection<Package> _package;

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
            Flight.Clear();
            Package.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupFlight(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (Flight item in e.NewItems)
                {
                    item.MAWB = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("Flight", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Flight item in e.OldItems)
                {
                    if (ReferenceEquals(item.MAWB, this))
                    {
                        item.MAWB = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("Flight", item);
                    }
                }
            }
        }
    
        private void FixupPackage(object sender, NotifyCollectionChangedEventArgs e)
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
                        ChangeTracker.RecordAdditionToCollectionProperties("Package", item);
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
                        ChangeTracker.RecordRemovalFromCollectionProperties("Package", item);
                    }
                }
            }
        }

        #endregion
    }
}
