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
    [KnownType(typeof(User))]
    [Serializable]
    [System.CodeDom.Compiler.GeneratedCode("STE-EF",".NET 4.0")]
    #if !SILVERLIGHT
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage()]
    #endif
    public partial class AddressBook: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public System.Guid AID
        {
            get { return _aID; }
            set
            {
                if (_aID != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'AID' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _aID = value;
                    OnPropertyChanged("AID");
                }
            }
        }
        private System.Guid _aID;
    
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
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        private string _name;
    
        [DataMember]
        public string ContactorName
        {
            get { return _contactorName; }
            set
            {
                if (_contactorName != value)
                {
                    _contactorName = value;
                    OnPropertyChanged("ContactorName");
                }
            }
        }
        private string _contactorName;
    
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
        public int AddressType
        {
            get { return _addressType; }
            set
            {
                if (_addressType != value)
                {
                    _addressType = value;
                    OnPropertyChanged("AddressType");
                }
            }
        }
        private int _addressType;
    
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

        #endregion
        #region Navigation Properties
    
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
    
        // This entity type is the dependent end in at least one association that performs cascade deletes.
        // This event handler will process notifications that occur when the principal end is deleted.
        internal void HandleCascadeDelete(object sender, ObjectStateChangingEventArgs e)
        {
            if (e.NewState == ObjectState.Deleted)
            {
                this.MarkAsDeleted();
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
            User = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupUser(User previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.AddressBook.Contains(this))
            {
                previousValue.AddressBook.Remove(this);
            }
    
            if (User != null)
            {
                if (!User.AddressBook.Contains(this))
                {
                    User.AddressBook.Add(this);
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

        #endregion
    }
}