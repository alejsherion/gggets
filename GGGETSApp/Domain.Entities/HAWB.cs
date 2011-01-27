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
    [KnownType(typeof(Item))]
    [Serializable]
    [System.CodeDom.Compiler.GeneratedCode("STE-EF",".NET 4.0")]
    #if !SILVERLIGHT
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage()]
    #endif
    public partial class HAWB: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public System.Guid HID
        {
            get { return _hID; }
            set
            {
                if (_hID != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'HID' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _hID = value;
                    OnPropertyChanged("HID");
                }
            }
        }
        private System.Guid _hID;
    
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
        public string Carrier
        {
            get { return _carrier; }
            set
            {
                if (_carrier != value)
                {
                    _carrier = value;
                    OnPropertyChanged("Carrier");
                }
            }
        }
        private string _carrier;
    
        [DataMember]
        public Nullable<System.Guid> CarrierHAWBID
        {
            get { return _carrierHAWBID; }
            set
            {
                if (_carrierHAWBID != value)
                {
                    _carrierHAWBID = value;
                    OnPropertyChanged("CarrierHAWBID");
                }
            }
        }
        private Nullable<System.Guid> _carrierHAWBID;
    
        [DataMember]
        public Nullable<System.Guid> Account
        {
            get { return _account; }
            set
            {
                if (_account != value)
                {
                    _account = value;
                    OnPropertyChanged("Account");
                }
            }
        }
        private Nullable<System.Guid> _account;
    
        [DataMember]
        public string SettleType
        {
            get { return _settleType; }
            set
            {
                if (_settleType != value)
                {
                    _settleType = value;
                    OnPropertyChanged("SettleType");
                }
            }
        }
        private string _settleType;
    
        [DataMember]
        public string ServiceType
        {
            get { return _serviceType; }
            set
            {
                if (_serviceType != value)
                {
                    _serviceType = value;
                    OnPropertyChanged("ServiceType");
                }
            }
        }
        private string _serviceType;
    
        [DataMember]
        public Nullable<System.DateTime> CreateTime
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
        private Nullable<System.DateTime> _createTime;
    
        [DataMember]
        public Nullable<System.DateTime> UpdateTime
        {
            get { return _updateTime; }
            set
            {
                if (_updateTime != value)
                {
                    _updateTime = value;
                    OnPropertyChanged("UpdateTime");
                }
            }
        }
        private Nullable<System.DateTime> _updateTime;
    
        [DataMember]
        public Nullable<System.DateTime> DeadlienTime
        {
            get { return _deadlienTime; }
            set
            {
                if (_deadlienTime != value)
                {
                    _deadlienTime = value;
                    OnPropertyChanged("DeadlienTime");
                }
            }
        }
        private Nullable<System.DateTime> _deadlienTime;
    
        [DataMember]
        public Nullable<int> State
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    OnPropertyChanged("State");
                }
            }
        }
        private Nullable<int> _state;
    
        [DataMember]
        public Nullable<System.Guid> Owner
        {
            get { return _owner; }
            set
            {
                if (_owner != value)
                {
                    _owner = value;
                    OnPropertyChanged("Owner");
                }
            }
        }
        private Nullable<System.Guid> _owner;
    
        [DataMember]
        public string Remark
        {
            get { return _remark; }
            set
            {
                if (_remark != value)
                {
                    _remark = value;
                    OnPropertyChanged("Remark");
                }
            }
        }
        private string _remark;
    
        [DataMember]
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
        private string _description;
    
        [DataMember]
        public Nullable<System.Guid> ShipperID
        {
            get { return _shipperID; }
            set
            {
                if (_shipperID != value)
                {
                    _shipperID = value;
                    OnPropertyChanged("ShipperID");
                }
            }
        }
        private Nullable<System.Guid> _shipperID;
    
        [DataMember]
        public string ShipperContactor
        {
            get { return _shipperContactor; }
            set
            {
                if (_shipperContactor != value)
                {
                    _shipperContactor = value;
                    OnPropertyChanged("ShipperContactor");
                }
            }
        }
        private string _shipperContactor;
    
        [DataMember]
        public string ShipperCountry
        {
            get { return _shipperCountry; }
            set
            {
                if (_shipperCountry != value)
                {
                    _shipperCountry = value;
                    OnPropertyChanged("ShipperCountry");
                }
            }
        }
        private string _shipperCountry;
    
        [DataMember]
        public string ShipperRegion
        {
            get { return _shipperRegion; }
            set
            {
                if (_shipperRegion != value)
                {
                    _shipperRegion = value;
                    OnPropertyChanged("ShipperRegion");
                }
            }
        }
        private string _shipperRegion;
    
        [DataMember]
        public string ShipperAddress
        {
            get { return _shipperAddress; }
            set
            {
                if (_shipperAddress != value)
                {
                    _shipperAddress = value;
                    OnPropertyChanged("ShipperAddress");
                }
            }
        }
        private string _shipperAddress;
    
        [DataMember]
        public string ShipperZipCode
        {
            get { return _shipperZipCode; }
            set
            {
                if (_shipperZipCode != value)
                {
                    _shipperZipCode = value;
                    OnPropertyChanged("ShipperZipCode");
                }
            }
        }
        private string _shipperZipCode;
    
        [DataMember]
        public string ShipperTel
        {
            get { return _shipperTel; }
            set
            {
                if (_shipperTel != value)
                {
                    _shipperTel = value;
                    OnPropertyChanged("ShipperTel");
                }
            }
        }
        private string _shipperTel;
    
        [DataMember]
        public Nullable<System.Guid> ConsigneeID
        {
            get { return _consigneeID; }
            set
            {
                if (_consigneeID != value)
                {
                    _consigneeID = value;
                    OnPropertyChanged("ConsigneeID");
                }
            }
        }
        private Nullable<System.Guid> _consigneeID;
    
        [DataMember]
        public string ConsigneeContactor
        {
            get { return _consigneeContactor; }
            set
            {
                if (_consigneeContactor != value)
                {
                    _consigneeContactor = value;
                    OnPropertyChanged("ConsigneeContactor");
                }
            }
        }
        private string _consigneeContactor;
    
        [DataMember]
        public string ConsigneeCountry
        {
            get { return _consigneeCountry; }
            set
            {
                if (_consigneeCountry != value)
                {
                    _consigneeCountry = value;
                    OnPropertyChanged("ConsigneeCountry");
                }
            }
        }
        private string _consigneeCountry;
    
        [DataMember]
        public string ConsigneeRegion
        {
            get { return _consigneeRegion; }
            set
            {
                if (_consigneeRegion != value)
                {
                    _consigneeRegion = value;
                    OnPropertyChanged("ConsigneeRegion");
                }
            }
        }
        private string _consigneeRegion;
    
        [DataMember]
        public string ConsigneeAddress
        {
            get { return _consigneeAddress; }
            set
            {
                if (_consigneeAddress != value)
                {
                    _consigneeAddress = value;
                    OnPropertyChanged("ConsigneeAddress");
                }
            }
        }
        private string _consigneeAddress;
    
        [DataMember]
        public string ConsigneeZipCode
        {
            get { return _consigneeZipCode; }
            set
            {
                if (_consigneeZipCode != value)
                {
                    _consigneeZipCode = value;
                    OnPropertyChanged("ConsigneeZipCode");
                }
            }
        }
        private string _consigneeZipCode;
    
        [DataMember]
        public string ConsigneeTel
        {
            get { return _consigneeTel; }
            set
            {
                if (_consigneeTel != value)
                {
                    _consigneeTel = value;
                    OnPropertyChanged("ConsigneeTel");
                }
            }
        }
        private string _consigneeTel;
    
        [DataMember]
        public Nullable<byte> WeightType
        {
            get { return _weightType; }
            set
            {
                if (_weightType != value)
                {
                    _weightType = value;
                    OnPropertyChanged("WeightType");
                }
            }
        }
        private Nullable<byte> _weightType;
    
        [DataMember]
        public Nullable<double> WeightValue
        {
            get { return _weightValue; }
            set
            {
                if (_weightValue != value)
                {
                    _weightValue = value;
                    OnPropertyChanged("WeightValue");
                }
            }
        }
        private Nullable<double> _weightValue;
    
        [DataMember]
        public Nullable<int> Piece
        {
            get { return _piece; }
            set
            {
                if (_piece != value)
                {
                    _piece = value;
                    OnPropertyChanged("Piece");
                }
            }
        }
        private Nullable<int> _piece;
    
        [DataMember]
        public Nullable<bool> IsInternational
        {
            get { return _isInternational; }
            set
            {
                if (_isInternational != value)
                {
                    _isInternational = value;
                    OnPropertyChanged("IsInternational");
                }
            }
        }
        private Nullable<bool> _isInternational;
    
        [DataMember]
        public string SpecialInstruction
        {
            get { return _specialInstruction; }
            set
            {
                if (_specialInstruction != value)
                {
                    _specialInstruction = value;
                    OnPropertyChanged("SpecialInstruction");
                }
            }
        }
        private string _specialInstruction;
    
        [DataMember]
        public string Taxes
        {
            get { return _taxes; }
            set
            {
                if (_taxes != value)
                {
                    _taxes = value;
                    OnPropertyChanged("Taxes");
                }
            }
        }
        private string _taxes;
    
        [DataMember]
        public Nullable<System.DateTime> PickupTime
        {
            get { return _pickupTime; }
            set
            {
                if (_pickupTime != value)
                {
                    _pickupTime = value;
                    OnPropertyChanged("PickupTime");
                }
            }
        }
        private Nullable<System.DateTime> _pickupTime;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<Item> Item
        {
            get
            {
                if (_item == null)
                {
                    _item = new TrackableCollection<Item>();
                    _item.CollectionChanged += FixupItem;
                }
                return _item;
            }
            set
            {
                if (!ReferenceEquals(_item, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_item != null)
                    {
                        _item.CollectionChanged -= FixupItem;
                        // This is the principal end in an association that performs cascade deletes.
                        // Remove the cascade delete event handler for any entities in the current collection.
                        foreach (Item item in _item)
                        {
                            ChangeTracker.ObjectStateChanging -= item.HandleCascadeDelete;
                        }
                    }
                    _item = value;
                    if (_item != null)
                    {
                        _item.CollectionChanged += FixupItem;
                        // This is the principal end in an association that performs cascade deletes.
                        // Add the cascade delete event handler for any entities that are already in the new collection.
                        foreach (Item item in _item)
                        {
                            ChangeTracker.ObjectStateChanging += item.HandleCascadeDelete;
                        }
                    }
                    OnNavigationPropertyChanged("Item");
                }
            }
        }
        private TrackableCollection<Item> _item;

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
            Item.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupItem(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (Item item in e.NewItems)
                {
                    item.HAWB = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("Item", item);
                    }
                    // This is the principal end in an association that performs cascade deletes.
                    // Update the event listener to refer to the new dependent.
                    ChangeTracker.ObjectStateChanging += item.HandleCascadeDelete;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Item item in e.OldItems)
                {
                    if (ReferenceEquals(item.HAWB, this))
                    {
                        item.HAWB = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("Item", item);
                    }
                    // This is the principal end in an association that performs cascade deletes.
                    // Remove the previous dependent from the event listener.
                    ChangeTracker.ObjectStateChanging -= item.HandleCascadeDelete;
                }
            }
        }

        #endregion
    }
}
