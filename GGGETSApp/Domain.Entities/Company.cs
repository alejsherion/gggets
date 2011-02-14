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
    [Serializable]
    [System.CodeDom.Compiler.GeneratedCode("STE-EF",".NET 4.0")]
    #if !SILVERLIGHT
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage()]
    #endif
    public partial class Company: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public System.Guid CID
        {
            get { return _cID; }
            set
            {
                if (_cID != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'CID' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _cID = value;
                    OnPropertyChanged("CID");
                }
            }
        }
        private System.Guid _cID;
    
        [DataMember]
        public string FullName
        {
            get { return _fullName; }
            set
            {
                if (_fullName != value)
                {
                    _fullName = value;
                    OnPropertyChanged("FullName");
                }
            }
        }
        private string _fullName;
    
        [DataMember]
        public string ShortName
        {
            get { return _shortName; }
            set
            {
                if (_shortName != value)
                {
                    _shortName = value;
                    OnPropertyChanged("ShortName");
                }
            }
        }
        private string _shortName;
    
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
        public string Contactor
        {
            get { return _contactor; }
            set
            {
                if (_contactor != value)
                {
                    _contactor = value;
                    OnPropertyChanged("Contactor");
                }
            }
        }
        private string _contactor;
    
        [DataMember]
        public string ContactorPhone
        {
            get { return _contactorPhone; }
            set
            {
                if (_contactorPhone != value)
                {
                    _contactorPhone = value;
                    OnPropertyChanged("ContactorPhone");
                }
            }
        }
        private string _contactorPhone;
    
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
        public decimal ResidualAmount
        {
            get { return _residualAmount; }
            set
            {
                if (_residualAmount != value)
                {
                    _residualAmount = value;
                    OnPropertyChanged("ResidualAmount");
                }
            }
        }
        private decimal _residualAmount;
    
        [DataMember]
        public string OrganizationCode
        {
            get { return _organizationCode; }
            set
            {
                if (_organizationCode != value)
                {
                    _organizationCode = value;
                    OnPropertyChanged("OrganizationCode");
                }
            }
        }
        private string _organizationCode;
    
        [DataMember]
        public decimal LimitAmount
        {
            get { return _limitAmount; }
            set
            {
                if (_limitAmount != value)
                {
                    _limitAmount = value;
                    OnPropertyChanged("LimitAmount");
                }
            }
        }
        private decimal _limitAmount;
    
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

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<Department> Department
        {
            get
            {
                if (_department == null)
                {
                    _department = new TrackableCollection<Department>();
                    _department.CollectionChanged += FixupDepartment;
                }
                return _department;
            }
            set
            {
                if (!ReferenceEquals(_department, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_department != null)
                    {
                        _department.CollectionChanged -= FixupDepartment;
                    }
                    _department = value;
                    if (_department != null)
                    {
                        _department.CollectionChanged += FixupDepartment;
                    }
                    OnNavigationPropertyChanged("Department");
                }
            }
        }
        private TrackableCollection<Department> _department;

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
            Department.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupDepartment(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (Department item in e.NewItems)
                {
                    item.Company = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("Department", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Department item in e.OldItems)
                {
                    if (ReferenceEquals(item.Company, this))
                    {
                        item.Company = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("Department", item);
                    }
                }
            }
        }

        #endregion
    }
}