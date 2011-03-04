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
    [KnownType(typeof(Department))]
    [KnownType(typeof(Package))]
    [KnownType(typeof(User))]
    [KnownType(typeof(HAWBBox))]
    [KnownType(typeof(HAWBItem))]
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
        public Nullable<System.Guid> PID
        {
            get { return _pID; }
            set
            {
                if (_pID != value)
                {
                    ChangeTracker.RecordOriginalValue("PID", _pID);
                    if (!IsDeserializing)
                    {
                        if (Package != null && Package.PID != value)
                        {
                            Package = null;
                        }
                    }
                    _pID = value;
                    OnPropertyChanged("PID");
                }
            }
        }
        private Nullable<System.Guid> _pID;
    
        [DataMember]
        public Nullable<System.Guid> UID
        {
            get { return _uID; }
            set
            {
                if (_uID != value)
                {
                    ChangeTracker.RecordOriginalValue("UID", _uID);
                    if (!IsDeserializing)
                    {
                        if (User != null && User.UID != value)
                        {
                            User = null;
                        }
                    }
                    _uID = value;
                    OnPropertyChanged("UID");
                }
            }
        }
        private Nullable<System.Guid> _uID;
    
        [DataMember]
        public Nullable<System.Guid> DID
        {
            get { return _dID; }
            set
            {
                if (_dID != value)
                {
                    ChangeTracker.RecordOriginalValue("DID", _dID);
                    if (!IsDeserializing)
                    {
                        if (Department != null && Department.DID != value)
                        {
                            Department = null;
                        }
                    }
                    _dID = value;
                    OnPropertyChanged("DID");
                }
            }
        }
        private Nullable<System.Guid> _dID;
    
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
        public int SettleType
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
        private int _settleType;
    
        [DataMember]
        public Nullable<int> ServiceType
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
        private Nullable<int> _serviceType;
    
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
        public string ShipperName
        {
            get { return _shipperName; }
            set
            {
                if (_shipperName != value)
                {
                    _shipperName = value;
                    OnPropertyChanged("ShipperName");
                }
            }
        }
        private string _shipperName;
    
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
        public string ConsigneeName
        {
            get { return _consigneeName; }
            set
            {
                if (_consigneeName != value)
                {
                    _consigneeName = value;
                    OnPropertyChanged("ConsigneeName");
                }
            }
        }
        private string _consigneeName;
    
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
        private string _consigneeCountryDesc;

        [DataMember]
        public string ConsigneeCountryDesc
        {
            get { return _consigneeCountryDesc; }
            set
            {
                if (_consigneeCountryDesc != value)
                {
                    _consigneeCountryDesc = value;
                    OnPropertyChanged("ConsigneeCountryDesc");
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
        public string ConsigneeRegionDesc
        {
            get { return _consigneeRegionDesc; }
            set
            {
                _consigneeRegionDesc = value;
            }
        }
        private string _consigneeRegionDesc;
    
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
        public string DeliverName
        {
            get { return _deliverName; }
            set
            {
                if (_deliverName != value)
                {
                    _deliverName = value;
                    OnPropertyChanged("DeliverName");
                }
            }
        }
        private string _deliverName;
    
        [DataMember]
        public string DeliverContactor
        {
            get { return _deliverContactor; }
            set
            {
                if (_deliverContactor != value)
                {
                    _deliverContactor = value;
                    OnPropertyChanged("DeliverContactor");
                }
            }
        }
        private string _deliverContactor;
    
        [DataMember]
        public string DeliverCountry
        {
            get { return _deliverCountry; }
            set
            {
                if (_deliverCountry != value)
                {
                    _deliverCountry = value;
                    OnPropertyChanged("DeliverCountry");
                }
            }
        }
        private string _deliverCountry;
    
        [DataMember]
        public string DeliverRegion
        {
            get { return _deliverRegion; }
            set
            {
                if (_deliverRegion != value)
                {
                    _deliverRegion = value;
                    OnPropertyChanged("DeliverRegion");
                }
            }
        }
        private string _deliverRegion;
    
        [DataMember]
        public string DeliverAddress
        {
            get { return _deliverAddress; }
            set
            {
                if (_deliverAddress != value)
                {
                    _deliverAddress = value;
                    OnPropertyChanged("DeliverAddress");
                }
            }
        }
        private string _deliverAddress;
    
        [DataMember]
        public string DeliverZipCode
        {
            get { return _deliverZipCode; }
            set
            {
                if (_deliverZipCode != value)
                {
                    _deliverZipCode = value;
                    OnPropertyChanged("DeliverZipCode");
                }
            }
        }
        private string _deliverZipCode;
    
        [DataMember]
        public string DeliverTel
        {
            get { return _deliverTel; }
            set
            {
                if (_deliverTel != value)
                {
                    _deliverTel = value;
                    OnPropertyChanged("DeliverTel");
                }
            }
        }
        private string _deliverTel;
    
        [DataMember]
        public int WeightType
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
        private int _weightType;
    
        [DataMember]
        public Nullable<decimal> VolumeWeight
        {
            get { return _volumeWeight; }
            set
            {
                if (_volumeWeight != value)
                {
                    _volumeWeight = value;
                    OnPropertyChanged("VolumeWeight");
                }
            }
        }
        private Nullable<decimal> _volumeWeight;
    
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
        public int Piece
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
        private int _piece;
    
        [DataMember]
        public bool IsInternational
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
        private bool _isInternational;
    
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
        public Nullable<int> BillTax
        {
            get { return _billTax; }
            set
            {
                if (_billTax != value)
                {
                    _billTax = value;
                    OnPropertyChanged("BillTax");
                }
            }
        }
        private Nullable<int> _billTax;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public Department Department
        {
            get { return _department; }
            set
            {
                if (!ReferenceEquals(_department, value))
                {
                    var previousValue = _department;
                    _department = value;
                    FixupDepartment(previousValue);
                    OnNavigationPropertyChanged("Department");
                }
            }
        }
        private Department _department;
    
        [DataMember]
        public Package Package
        {
            get { return _package; }
            set
            {
                if (!ReferenceEquals(_package, value))
                {
                    var previousValue = _package;
                    _package = value;
                    FixupPackage(previousValue);
                    OnNavigationPropertyChanged("Package");
                }
            }
        }
        private Package _package;
    
        [DataMember]
        public User User
        {
            get { return _user; }
            set
            {
                if (!ReferenceEquals(_user, value))
                {
                    var previousValue = _user;
                    _user = value;
                    FixupUser(previousValue);
                    OnNavigationPropertyChanged("User");
                }
            }
        }
        private User _user;
    
        [DataMember]
        public TrackableCollection<HAWBBox> HAWBBoxes
        {
            get
            {
                if (_hAWBBoxes == null)
                {
                    _hAWBBoxes = new TrackableCollection<HAWBBox>();
                    _hAWBBoxes.CollectionChanged += FixupHAWBBoxes;
                }
                return _hAWBBoxes;
            }
            set
            {
                if (!ReferenceEquals(_hAWBBoxes, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_hAWBBoxes != null)
                    {
                        _hAWBBoxes.CollectionChanged -= FixupHAWBBoxes;
                        // This is the principal end in an association that performs cascade deletes.
                        // Remove the cascade delete event handler for any entities in the current collection.
                        foreach (HAWBBox item in _hAWBBoxes)
                        {
                            ChangeTracker.ObjectStateChanging -= item.HandleCascadeDelete;
                        }
                    }
                    _hAWBBoxes = value;
                    if (_hAWBBoxes != null)
                    {
                        _hAWBBoxes.CollectionChanged += FixupHAWBBoxes;
                        // This is the principal end in an association that performs cascade deletes.
                        // Add the cascade delete event handler for any entities that are already in the new collection.
                        foreach (HAWBBox item in _hAWBBoxes)
                        {
                            ChangeTracker.ObjectStateChanging += item.HandleCascadeDelete;
                        }
                    }
                    OnNavigationPropertyChanged("HAWBBoxes");
                }
            }
        }
        private TrackableCollection<HAWBBox> _hAWBBoxes;
    
        [DataMember]
        public TrackableCollection<HAWBItem> HAWBItems
        {
            get
            {
                if (_hAWBItems == null)
                {
                    _hAWBItems = new TrackableCollection<HAWBItem>();
                    _hAWBItems.CollectionChanged += FixupHAWBItems;
                }
                return _hAWBItems;
            }
            set
            {
                if (!ReferenceEquals(_hAWBItems, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_hAWBItems != null)
                    {
                        _hAWBItems.CollectionChanged -= FixupHAWBItems;
                    }
                    _hAWBItems = value;
                    if (_hAWBItems != null)
                    {
                        _hAWBItems.CollectionChanged += FixupHAWBItems;
                    }
                    OnNavigationPropertyChanged("HAWBItems");
                }
            }
        }
        private TrackableCollection<HAWBItem> _hAWBItems;

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
            Department = null;
            Package = null;
            User = null;
            HAWBBoxes.Clear();
            HAWBItems.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupDepartment(Department previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.HAWBs.Contains(this))
            {
                previousValue.HAWBs.Remove(this);
            }
    
            if (Department != null)
            {
                if (!Department.HAWBs.Contains(this))
                {
                    Department.HAWBs.Add(this);
                }
    
                DID = Department.DID;
            }
            else if (!skipKeys)
            {
                DID = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Department")
                    && (ChangeTracker.OriginalValues["Department"] == Department))
                {
                    ChangeTracker.OriginalValues.Remove("Department");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Department", previousValue);
                }
                if (Department != null && !Department.ChangeTracker.ChangeTrackingEnabled)
                {
                    Department.StartTracking();
                }
            }
        }
    
        private void FixupPackage(Package previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.HAWBs.Contains(this))
            {
                previousValue.HAWBs.Remove(this);
            }
    
            if (Package != null)
            {
                if (!Package.HAWBs.Contains(this))
                {
                    Package.HAWBs.Add(this);
                }
    
                PID = Package.PID;
            }
            else if (!skipKeys)
            {
                PID = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Package")
                    && (ChangeTracker.OriginalValues["Package"] == Package))
                {
                    ChangeTracker.OriginalValues.Remove("Package");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Package", previousValue);
                }
                if (Package != null && !Package.ChangeTracker.ChangeTrackingEnabled)
                {
                    Package.StartTracking();
                }
            }
        }
    
        private void FixupUser(User previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.HAWBs.Contains(this))
            {
                previousValue.HAWBs.Remove(this);
            }
    
            if (User != null)
            {
                if (!User.HAWBs.Contains(this))
                {
                    User.HAWBs.Add(this);
                }
    
                UID = User.UID;
            }
            else if (!skipKeys)
            {
                UID = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("User")
                    && (ChangeTracker.OriginalValues["User"] == User))
                {
                    ChangeTracker.OriginalValues.Remove("User");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("User", previousValue);
                }
                if (User != null && !User.ChangeTracker.ChangeTrackingEnabled)
                {
                    User.StartTracking();
                }
            }
        }
    
        private void FixupHAWBBoxes(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (HAWBBox item in e.NewItems)
                {
                    item.HAWB = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("HAWBBoxes", item);
                    }
                    // This is the principal end in an association that performs cascade deletes.
                    // Update the event listener to refer to the new dependent.
                    ChangeTracker.ObjectStateChanging += item.HandleCascadeDelete;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (HAWBBox item in e.OldItems)
                {
                    if (ReferenceEquals(item.HAWB, this))
                    {
                        item.HAWB = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("HAWBBoxes", item);
                    }
                    // This is the principal end in an association that performs cascade deletes.
                    // Remove the previous dependent from the event listener.
                    ChangeTracker.ObjectStateChanging -= item.HandleCascadeDelete;
                }
            }
        }
    
        private void FixupHAWBItems(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (HAWBItem item in e.NewItems)
                {
                    item.HAWB = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("HAWBItems", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (HAWBItem item in e.OldItems)
                {
                    if (ReferenceEquals(item.HAWB, this))
                    {
                        item.HAWB = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("HAWBItems", item);
                    }
                }
            }
        }

        #endregion
    }
}
