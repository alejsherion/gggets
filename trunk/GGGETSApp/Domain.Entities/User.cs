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
    [KnownType(typeof(Department))]
    [KnownType(typeof(HAWB))]
    [Serializable]
    [System.CodeDom.Compiler.GeneratedCode("STE-EF",".NET 4.0")]
    #if !SILVERLIGHT
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage()]
    #endif
    public partial class User: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public System.Guid UID
        {
            get { return _uID; }
            set
            {
                if (_uID != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'UID' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _uID = value;
                    OnPropertyChanged("UID");
                }
            }
        }
        private System.Guid _uID;
    
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
                        if (Department1 != null && Department1.DID != value)
                        {
                            Department1 = null;
                        }
                    }
                    _dID = value;
                    OnPropertyChanged("DID");
                }
            }
        }
        private Nullable<System.Guid> _dID;
    
        [DataMember]
        public string LoginName
        {
            get { return _loginName; }
            set
            {
                if (_loginName != value)
                {
                    _loginName = value;
                    OnPropertyChanged("LoginName");
                }
            }
        }
        private string _loginName;
    
        [DataMember]
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }
        private string _password;
    
        [DataMember]
        public string RealName
        {
            get { return _realName; }
            set
            {
                if (_realName != value)
                {
                    _realName = value;
                    OnPropertyChanged("RealName");
                }
            }
        }
        private string _realName;
    
        [DataMember]
        public string Department
        {
            get { return _department; }
            set
            {
                if (_department != value)
                {
                    _department = value;
                    OnPropertyChanged("Department");
                }
            }
        }
        private string _department;
    
        [DataMember]
        public string PostCode
        {
            get { return _postCode; }
            set
            {
                if (_postCode != value)
                {
                    _postCode = value;
                    OnPropertyChanged("PostCode");
                }
            }
        }
        private string _postCode;
    
        [DataMember]
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        private string _email;
    
        [DataMember]
        public string Fax
        {
            get { return _fax; }
            set
            {
                if (_fax != value)
                {
                    _fax = value;
                    OnPropertyChanged("Fax");
                }
            }
        }
        private string _fax;
    
        [DataMember]
        public string Phone
        {
            get { return _phone; }
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged("Phone");
                }
            }
        }
        private string _phone;
    
        [DataMember]
        public string CountryCode
        {
            get { return _countryCode; }
            set
            {
                if (_countryCode != value)
                {
                    _countryCode = value;
                    OnPropertyChanged("CountryCode");
                }
            }
        }
        private string _countryCode;
    
        [DataMember]
        public string RegionCode
        {
            get { return _regionCode; }
            set
            {
                if (_regionCode != value)
                {
                    _regionCode = value;
                    OnPropertyChanged("RegionCode");
                }
            }
        }
        private string _regionCode;
    
        [DataMember]
        public string Address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged("Address");
                }
            }
        }
        private string _address;
    
        [DataMember]
        public System.DateTime UpdateTime
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
        private System.DateTime _updateTime;
    
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
        public TrackableCollection<AddressBook> AddressBook
        {
            get
            {
                if (_addressBook == null)
                {
                    _addressBook = new TrackableCollection<AddressBook>();
                    _addressBook.CollectionChanged += FixupAddressBook;
                }
                return _addressBook;
            }
            set
            {
                if (!ReferenceEquals(_addressBook, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_addressBook != null)
                    {
                        _addressBook.CollectionChanged -= FixupAddressBook;
                    }
                    _addressBook = value;
                    if (_addressBook != null)
                    {
                        _addressBook.CollectionChanged += FixupAddressBook;
                    }
                    OnNavigationPropertyChanged("AddressBook");
                }
            }
        }
        private TrackableCollection<AddressBook> _addressBook;
    
        [DataMember]
        public Department Department1
        {
            get { return _department1; }
            set
            {
                if (!ReferenceEquals(_department1, value))
                {
                    var previousValue = _department1;
                    _department1 = value;
                    FixupDepartment1(previousValue);
                    OnNavigationPropertyChanged("Department1");
                }
            }
        }
        private Department _department1;
    
        [DataMember]
        public TrackableCollection<HAWB> HAWB
        {
            get
            {
                if (_hAWB == null)
                {
                    _hAWB = new TrackableCollection<HAWB>();
                    _hAWB.CollectionChanged += FixupHAWB;
                }
                return _hAWB;
            }
            set
            {
                if (!ReferenceEquals(_hAWB, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_hAWB != null)
                    {
                        _hAWB.CollectionChanged -= FixupHAWB;
                    }
                    _hAWB = value;
                    if (_hAWB != null)
                    {
                        _hAWB.CollectionChanged += FixupHAWB;
                    }
                    OnNavigationPropertyChanged("HAWB");
                }
            }
        }
        private TrackableCollection<HAWB> _hAWB;

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
            AddressBook.Clear();
            Department1 = null;
            HAWB.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupDepartment1(Department previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.User.Contains(this))
            {
                previousValue.User.Remove(this);
            }
    
            if (Department1 != null)
            {
                if (!Department1.User.Contains(this))
                {
                    Department1.User.Add(this);
                }
    
                DID = Department1.DID;
            }
            else if (!skipKeys)
            {
                DID = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Department1")
                    && (ChangeTracker.OriginalValues["Department1"] == Department1))
                {
                    ChangeTracker.OriginalValues.Remove("Department1");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Department1", previousValue);
                }
                if (Department1 != null && !Department1.ChangeTracker.ChangeTrackingEnabled)
                {
                    Department1.StartTracking();
                }
            }
        }
    
        private void FixupAddressBook(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (AddressBook item in e.NewItems)
                {
                    item.User = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("AddressBook", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (AddressBook item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("AddressBook", item);
                    }
                }
            }
        }
    
        private void FixupHAWB(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (HAWB item in e.NewItems)
                {
                    item.User = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("HAWB", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (HAWB item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("HAWB", item);
                    }
                }
            }
        }

        #endregion
    }
}
