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
    [KnownType(typeof(Trans))]
    [System.CodeDom.Compiler.GeneratedCode("STE-EF",".NET 4.0")]
    #if !SILVERLIGHT
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage()]
    #endif
    public partial class TransLog: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public System.Guid Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'Id' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        private System.Guid _id;
    
        [DataMember]
        public Nullable<System.Guid> TransId
        {
            get { return _transId; }
            set
            {
                if (_transId != value)
                {
                    ChangeTracker.RecordOriginalValue("TransId", _transId);
                    if (!IsDeserializing)
                    {
                        if (Trans != null && Trans.TransId != value)
                        {
                            Trans = null;
                        }
                    }
                    _transId = value;
                    OnPropertyChanged("TransId");
                }
            }
        }
        private Nullable<System.Guid> _transId;
    
        [DataMember]
        public string Log
        {
            get { return _log; }
            set
            {
                if (_log != value)
                {
                    _log = value;
                    OnPropertyChanged("Log");
                }
            }
        }
        private string _log;
    
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

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public Trans Trans
        {
            get { return _trans; }
            set
            {
                if (!ReferenceEquals(_trans, value))
                {
                    var previousValue = _trans;
                    _trans = value;
                    FixupTrans(previousValue);
                    OnNavigationPropertyChanged("Trans");
                }
            }
        }
        private Trans _trans;

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
            Trans = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupTrans(Trans previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TransLog.Contains(this))
            {
                previousValue.TransLog.Remove(this);
            }
    
            if (Trans != null)
            {
                if (!Trans.TransLog.Contains(this))
                {
                    Trans.TransLog.Add(this);
                }
    
                TransId = Trans.TransId;
            }
            else if (!skipKeys)
            {
                TransId = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Trans")
                    && (ChangeTracker.OriginalValues["Trans"] == Trans))
                {
                    ChangeTracker.OriginalValues.Remove("Trans");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Trans", previousValue);
                }
                if (Trans != null && !Trans.ChangeTracker.ChangeTrackingEnabled)
                {
                    Trans.StartTracking();
                }
            }
        }

        #endregion
    }
}
