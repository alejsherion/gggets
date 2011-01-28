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
    [KnownType(typeof(CompanyUser))]
    [KnownType(typeof(IndividualUser))]
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
                        if (CompanyUser != null && CompanyUser.UID != value)
                        {
                            CompanyUser = null;
                        }
                        if (IndividualUser != null && IndividualUser.UID != value)
                        {
                            IndividualUser = null;
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
    
        [DataMember]
        public System.DateTime LastUsedTime
        {
            get { return _lastUsedTime; }
            set
            {
                if (_lastUsedTime != value)
                {
                    _lastUsedTime = value;
                    OnPropertyChanged("LastUsedTime");
                }
            }
        }
        private System.DateTime _lastUsedTime;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public CompanyUser CompanyUser
        {
            get { return _companyUser; }
            set
            {
                if (!ReferenceEquals(_companyUser, value))
                {
                    var previousValue = _companyUser;
                    _companyUser = value;
                    FixupCompanyUser(previousValue);
                    OnNavigationPropertyChanged("CompanyUser");
                }
            }
        }
        private CompanyUser _companyUser;
    
        [DataMember]
        public IndividualUser IndividualUser
        {
            get { return _individualUser; }
            set
            {
                if (!ReferenceEquals(_individualUser, value))
                {
                    var previousValue = _individualUser;
                    _individualUser = value;
                    FixupIndividualUser(previousValue);
                    OnNavigationPropertyChanged("IndividualUser");
                }
            }
        }
        private IndividualUser _individualUser;

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
            CompanyUser = null;
            IndividualUser = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupCompanyUser(CompanyUser previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.AddressBooks.Contains(this))
            {
                previousValue.AddressBooks.Remove(this);
            }
    
            if (CompanyUser != null)
            {
                if (!CompanyUser.AddressBooks.Contains(this))
                {
                    CompanyUser.AddressBooks.Add(this);
                }
    
                UID = CompanyUser.UID;
            }
            else if (!skipKeys)
            {
                UID = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("CompanyUser")
                    && (ChangeTracker.OriginalValues["CompanyUser"] == CompanyUser))
                {
                    ChangeTracker.OriginalValues.Remove("CompanyUser");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("CompanyUser", previousValue);
                }
                if (CompanyUser != null && !CompanyUser.ChangeTracker.ChangeTrackingEnabled)
                {
                    CompanyUser.StartTracking();
                }
            }
        }
    
        private void FixupIndividualUser(IndividualUser previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.AddressBooks.Contains(this))
            {
                previousValue.AddressBooks.Remove(this);
            }
    
            if (IndividualUser != null)
            {
                if (!IndividualUser.AddressBooks.Contains(this))
                {
                    IndividualUser.AddressBooks.Add(this);
                }
    
                UID = IndividualUser.UID;
            }
            else if (!skipKeys)
            {
                UID = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("IndividualUser")
                    && (ChangeTracker.OriginalValues["IndividualUser"] == IndividualUser))
                {
                    ChangeTracker.OriginalValues.Remove("IndividualUser");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("IndividualUser", previousValue);
                }
                if (IndividualUser != null && !IndividualUser.ChangeTracker.ChangeTrackingEnabled)
                {
                    IndividualUser.StartTracking();
                }
            }
        }

        #endregion
    }
}