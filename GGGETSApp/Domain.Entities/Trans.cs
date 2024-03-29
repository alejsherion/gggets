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
    [KnownType(typeof(TransItem))]
    [KnownType(typeof(TransLog))]
    [System.CodeDom.Compiler.GeneratedCode("STE-EF",".NET 4.0")]
    #if !SILVERLIGHT
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage()]
    #endif
    public partial class Trans: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public System.Guid TransId
        {
            get { return _transId; }
            set
            {
                if (_transId != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'TransId' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _transId = value;
                    OnPropertyChanged("TransId");
                }
            }
        }
        private System.Guid _transId;
    
        [DataMember]
        public string Sender
        {
            get { return _sender; }
            set
            {
                if (_sender != value)
                {
                    _sender = value;
                    OnPropertyChanged("Sender");
                }
            }
        }
        private string _sender;
    
        [DataMember]
        public string SenderAddress
        {
            get { return _senderAddress; }
            set
            {
                if (_senderAddress != value)
                {
                    _senderAddress = value;
                    OnPropertyChanged("SenderAddress");
                }
            }
        }
        private string _senderAddress;
    
        [DataMember]
        public Nullable<int> SenderAreaCode
        {
            get { return _senderAreaCode; }
            set
            {
                if (_senderAreaCode != value)
                {
                    _senderAreaCode = value;
                    OnPropertyChanged("SenderAreaCode");
                }
            }
        }
        private Nullable<int> _senderAreaCode;
    
        [DataMember]
        public string SenderPostCode
        {
            get { return _senderPostCode; }
            set
            {
                if (_senderPostCode != value)
                {
                    _senderPostCode = value;
                    OnPropertyChanged("SenderPostCode");
                }
            }
        }
        private string _senderPostCode;
    
        [DataMember]
        public string SenderPhone
        {
            get { return _senderPhone; }
            set
            {
                if (_senderPhone != value)
                {
                    _senderPhone = value;
                    OnPropertyChanged("SenderPhone");
                }
            }
        }
        private string _senderPhone;
    
        [DataMember]
        public string Receiver
        {
            get { return _receiver; }
            set
            {
                if (_receiver != value)
                {
                    _receiver = value;
                    OnPropertyChanged("Receiver");
                }
            }
        }
        private string _receiver;
    
        [DataMember]
        public string ReceiverAddress
        {
            get { return _receiverAddress; }
            set
            {
                if (_receiverAddress != value)
                {
                    _receiverAddress = value;
                    OnPropertyChanged("ReceiverAddress");
                }
            }
        }
        private string _receiverAddress;
    
        [DataMember]
        public Nullable<int> ReceiverAreaCode
        {
            get { return _receiverAreaCode; }
            set
            {
                if (_receiverAreaCode != value)
                {
                    _receiverAreaCode = value;
                    OnPropertyChanged("ReceiverAreaCode");
                }
            }
        }
        private Nullable<int> _receiverAreaCode;
    
        [DataMember]
        public string ReceiverPhone
        {
            get { return _receiverPhone; }
            set
            {
                if (_receiverPhone != value)
                {
                    _receiverPhone = value;
                    OnPropertyChanged("ReceiverPhone");
                }
            }
        }
        private string _receiverPhone;
    
        [DataMember]
        public Nullable<int> ItemsType
        {
            get { return _itemsType; }
            set
            {
                if (_itemsType != value)
                {
                    _itemsType = value;
                    OnPropertyChanged("ItemsType");
                }
            }
        }
        private Nullable<int> _itemsType;
    
        [DataMember]
        public Nullable<int> ItemsWeight
        {
            get { return _itemsWeight; }
            set
            {
                if (_itemsWeight != value)
                {
                    _itemsWeight = value;
                    OnPropertyChanged("ItemsWeight");
                }
            }
        }
        private Nullable<int> _itemsWeight;
    
        [DataMember]
        public Nullable<int> ItemsVolumn
        {
            get { return _itemsVolumn; }
            set
            {
                if (_itemsVolumn != value)
                {
                    _itemsVolumn = value;
                    OnPropertyChanged("ItemsVolumn");
                }
            }
        }
        private Nullable<int> _itemsVolumn;
    
        [DataMember]
        public Nullable<int> IsCustom
        {
            get { return _isCustom; }
            set
            {
                if (_isCustom != value)
                {
                    _isCustom = value;
                    OnPropertyChanged("IsCustom");
                }
            }
        }
        private Nullable<int> _isCustom;
    
        [DataMember]
        public Nullable<int> CustomFee
        {
            get { return _customFee; }
            set
            {
                if (_customFee != value)
                {
                    _customFee = value;
                    OnPropertyChanged("CustomFee");
                }
            }
        }
        private Nullable<int> _customFee;
    
        [DataMember]
        public Nullable<int> Fee
        {
            get { return _fee; }
            set
            {
                if (_fee != value)
                {
                    _fee = value;
                    OnPropertyChanged("Fee");
                }
            }
        }
        private Nullable<int> _fee;
    
        [DataMember]
        public Nullable<int> Status
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
        private Nullable<int> _status;
    
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
        public TrackableCollection<TransItem> TransItem
        {
            get
            {
                if (_transItem == null)
                {
                    _transItem = new TrackableCollection<TransItem>();
                    _transItem.CollectionChanged += FixupTransItem;
                }
                return _transItem;
            }
            set
            {
                if (!ReferenceEquals(_transItem, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_transItem != null)
                    {
                        _transItem.CollectionChanged -= FixupTransItem;
                    }
                    _transItem = value;
                    if (_transItem != null)
                    {
                        _transItem.CollectionChanged += FixupTransItem;
                    }
                    OnNavigationPropertyChanged("TransItem");
                }
            }
        }
        private TrackableCollection<TransItem> _transItem;
    
        [DataMember]
        public TrackableCollection<TransLog> TransLog
        {
            get
            {
                if (_transLog == null)
                {
                    _transLog = new TrackableCollection<TransLog>();
                    _transLog.CollectionChanged += FixupTransLog;
                }
                return _transLog;
            }
            set
            {
                if (!ReferenceEquals(_transLog, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_transLog != null)
                    {
                        _transLog.CollectionChanged -= FixupTransLog;
                    }
                    _transLog = value;
                    if (_transLog != null)
                    {
                        _transLog.CollectionChanged += FixupTransLog;
                    }
                    OnNavigationPropertyChanged("TransLog");
                }
            }
        }
        private TrackableCollection<TransLog> _transLog;

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
            TransItem.Clear();
            TransLog.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupTransItem(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TransItem item in e.NewItems)
                {
                    item.Trans = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TransItem", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TransItem item in e.OldItems)
                {
                    if (ReferenceEquals(item.Trans, this))
                    {
                        item.Trans = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TransItem", item);
                    }
                }
            }
        }
    
        private void FixupTransLog(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TransLog item in e.NewItems)
                {
                    item.Trans = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TransLog", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TransLog item in e.OldItems)
                {
                    if (ReferenceEquals(item.Trans, this))
                    {
                        item.Trans = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TransLog", item);
                    }
                }
            }
        }

        #endregion
    }
}
