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
    [KnownType(typeof(AddressBook))]
    [KnownType(typeof(Company))]
    [KnownType(typeof(HAWB))]
    [KnownType(typeof(User))]
    [Serializable]
    [System.CodeDom.Compiler.GeneratedCode("STE-EF",".NET 4.0")]
    #if !SILVERLIGHT
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage()]
    #endif
    public partial class Department: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public System.Guid DID
        {
            get { return _dID; }
            set
            {
                if (_dID != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'DID' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _dID = value;
                    OnPropertyChanged("DID");
                }
            }
        }
        private System.Guid _dID;
    
        [DataMember]
        public Nullable<System.Guid> CID
        {
            get { return _cID; }
            set
            {
                if (_cID != value)
                {
                    ChangeTracker.RecordOriginalValue("CID", _cID);
                    if (!IsDeserializing)
                    {
                        if (Company != null && Company.CID != value)
                        {
                            Company = null;
                        }
                    }
                    _cID = value;
                    OnPropertyChanged("CID");
                }
            }
        }
        private Nullable<System.Guid> _cID;
    
        [DataMember]
        public string CompanyCode
        {
            get { return _companyCode; }
            set
            {
                if (_companyCode != value)
                {
                    _companyCode = value;
                    OnPropertyChanged("CompanyCode");
                }
            }
        }
        private string _companyCode;
    
        [DataMember]
        public string DepCode
        {
            get { return _depCode; }
            set
            {
                if (_depCode != value)
                {
                    _depCode = value;
                    OnPropertyChanged("DepCode");
                }
            }
        }
        private string _depCode;
    
        [DataMember]
        public string DepName
        {
            get { return _depName; }
            set
            {
                if (_depName != value)
                {
                    _depName = value;
                    OnPropertyChanged("DepName");
                }
            }
        }
        private string _depName;
    
        [DataMember]
        public int FeeDiscountType
        {
            get { return _feeDiscountType; }
            set
            {
                if (_feeDiscountType != value)
                {
                    _feeDiscountType = value;
                    OnPropertyChanged("FeeDiscountType");
                }
            }
        }
        private int _feeDiscountType;
    
        [DataMember]
        public decimal FeeDiscountRate
        {
            get { return _feeDiscountRate; }
            set
            {
                if (_feeDiscountRate != value)
                {
                    _feeDiscountRate = value;
                    OnPropertyChanged("FeeDiscountRate");
                }
            }
        }
        private decimal _feeDiscountRate;
    
        [DataMember]
        public int WeightDiscountType
        {
            get { return _weightDiscountType; }
            set
            {
                if (_weightDiscountType != value)
                {
                    _weightDiscountType = value;
                    OnPropertyChanged("WeightDiscountType");
                }
            }
        }
        private int _weightDiscountType;
    
        [DataMember]
        public decimal WeightDiscountRate
        {
            get { return _weightDiscountRate; }
            set
            {
                if (_weightDiscountRate != value)
                {
                    _weightDiscountRate = value;
                    OnPropertyChanged("WeightDiscountRate");
                }
            }
        }
        private decimal _weightDiscountRate;
    
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
        public int WeightCalType
        {
            get { return _weightCalType; }
            set
            {
                if (_weightCalType != value)
                {
                    _weightCalType = value;
                    OnPropertyChanged("WeightCalType");
                }
            }
        }
        private int _weightCalType;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<AddressBook> AddressBooks
        {
            get
            {
                if (_addressBooks == null)
                {
                    _addressBooks = new TrackableCollection<AddressBook>();
                    _addressBooks.CollectionChanged += FixupAddressBooks;
                }
                return _addressBooks;
            }
            set
            {
                if (!ReferenceEquals(_addressBooks, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_addressBooks != null)
                    {
                        _addressBooks.CollectionChanged -= FixupAddressBooks;
                    }
                    _addressBooks = value;
                    if (_addressBooks != null)
                    {
                        _addressBooks.CollectionChanged += FixupAddressBooks;
                    }
                    OnNavigationPropertyChanged("AddressBooks");
                }
            }
        }
        private TrackableCollection<AddressBook> _addressBooks;
    
        [DataMember]
        public Company Company
        {
            get { return _company; }
            set
            {
                if (!ReferenceEquals(_company, value))
                {
                    var previousValue = _company;
                    _company = value;
                    FixupCompany(previousValue);
                    OnNavigationPropertyChanged("Company");
                }
            }
        }
        private Company _company;
    
        [DataMember]
        public TrackableCollection<HAWB> HAWBs
        {
            get
            {
                if (_hAWBs == null)
                {
                    _hAWBs = new TrackableCollection<HAWB>();
                    _hAWBs.CollectionChanged += FixupHAWBs;
                }
                return _hAWBs;
            }
            set
            {
                if (!ReferenceEquals(_hAWBs, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_hAWBs != null)
                    {
                        _hAWBs.CollectionChanged -= FixupHAWBs;
                    }
                    _hAWBs = value;
                    if (_hAWBs != null)
                    {
                        _hAWBs.CollectionChanged += FixupHAWBs;
                    }
                    OnNavigationPropertyChanged("HAWBs");
                }
            }
        }
        private TrackableCollection<HAWB> _hAWBs;
    
        [DataMember]
        public TrackableCollection<User> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new TrackableCollection<User>();
                    _users.CollectionChanged += FixupUsers;
                }
                return _users;
            }
            set
            {
                if (!ReferenceEquals(_users, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_users != null)
                    {
                        _users.CollectionChanged -= FixupUsers;
                    }
                    _users = value;
                    if (_users != null)
                    {
                        _users.CollectionChanged += FixupUsers;
                    }
                    OnNavigationPropertyChanged("Users");
                }
            }
        }
        private TrackableCollection<User> _users;

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
            AddressBooks.Clear();
            Company = null;
            HAWBs.Clear();
            Users.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupCompany(Company previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Departments.Contains(this))
            {
                previousValue.Departments.Remove(this);
            }
    
            if (Company != null)
            {
                if (!Company.Departments.Contains(this))
                {
                    Company.Departments.Add(this);
                }
    
                CID = Company.CID;
            }
            else if (!skipKeys)
            {
                CID = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Company")
                    && (ChangeTracker.OriginalValues["Company"] == Company))
                {
                    ChangeTracker.OriginalValues.Remove("Company");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Company", previousValue);
                }
                if (Company != null && !Company.ChangeTracker.ChangeTrackingEnabled)
                {
                    Company.StartTracking();
                }
            }
        }
    
        private void FixupAddressBooks(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (AddressBook item in e.NewItems)
                {
                    item.Department = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("AddressBooks", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (AddressBook item in e.OldItems)
                {
                    if (ReferenceEquals(item.Department, this))
                    {
                        item.Department = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("AddressBooks", item);
                    }
                }
            }
        }
    
        private void FixupHAWBs(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (HAWB item in e.NewItems)
                {
                    item.Department = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("HAWBs", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (HAWB item in e.OldItems)
                {
                    if (ReferenceEquals(item.Department, this))
                    {
                        item.Department = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("HAWBs", item);
                    }
                }
            }
        }
    
        private void FixupUsers(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (User item in e.NewItems)
                {
                    item.Department = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("Users", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (User item in e.OldItems)
                {
                    if (ReferenceEquals(item.Department, this))
                    {
                        item.Department = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("Users", item);
                    }
                }
            }
        }

        #endregion
    }
}
