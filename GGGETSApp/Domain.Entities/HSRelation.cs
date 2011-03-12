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
    [KnownType(typeof(HSProduct))]
    [KnownType(typeof(HSProperty))]
    [Serializable]
    [System.CodeDom.Compiler.GeneratedCode("STE-EF",".NET 4.0")]
    #if !SILVERLIGHT
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage()]
    #endif
    public partial class HSRelation: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public System.Guid RID
        {
            get { return _rID; }
            set
            {
                if (_rID != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'RID' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _rID = value;
                    OnPropertyChanged("RID");
                }
            }
        }
        private System.Guid _rID;
    
        [DataMember]
        public Nullable<System.Guid> HSID
        {
            get { return _hSID; }
            set
            {
                if (_hSID != value)
                {
                    ChangeTracker.RecordOriginalValue("HSID", _hSID);
                    if (!IsDeserializing)
                    {
                        if (HSProduct != null && HSProduct.HSID != value)
                        {
                            HSProduct = null;
                        }
                    }
                    _hSID = value;
                    OnPropertyChanged("HSID");
                }
            }
        }
        private Nullable<System.Guid> _hSID;
    
        [DataMember]
        public Nullable<System.Guid> HSPID
        {
            get { return _hSPID; }
            set
            {
                if (_hSPID != value)
                {
                    ChangeTracker.RecordOriginalValue("HSPID", _hSPID);
                    if (!IsDeserializing)
                    {
                        if (HSProperty != null && HSProperty.HSPID != value)
                        {
                            HSProperty = null;
                        }
                    }
                    _hSPID = value;
                    OnPropertyChanged("HSPID");
                }
            }
        }
        private Nullable<System.Guid> _hSPID;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public HSProduct HSProduct
        {
            get { return _hSProduct; }
            set
            {
                if (!ReferenceEquals(_hSProduct, value))
                {
                    var previousValue = _hSProduct;
                    _hSProduct = value;
                    FixupHSProduct(previousValue);
                    OnNavigationPropertyChanged("HSProduct");
                }
            }
        }
        private HSProduct _hSProduct;
    
        [DataMember]
        public HSProperty HSProperty
        {
            get { return _hSProperty; }
            set
            {
                if (!ReferenceEquals(_hSProperty, value))
                {
                    var previousValue = _hSProperty;
                    _hSProperty = value;
                    FixupHSProperty(previousValue);
                    OnNavigationPropertyChanged("HSProperty");
                }
            }
        }
        private HSProperty _hSProperty;

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
            HSProduct = null;
            HSProperty = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupHSProduct(HSProduct previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.HSRelations.Contains(this))
            {
                previousValue.HSRelations.Remove(this);
            }
    
            if (HSProduct != null)
            {
                if (!HSProduct.HSRelations.Contains(this))
                {
                    HSProduct.HSRelations.Add(this);
                }
    
                HSID = HSProduct.HSID;
            }
            else if (!skipKeys)
            {
                HSID = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("HSProduct")
                    && (ChangeTracker.OriginalValues["HSProduct"] == HSProduct))
                {
                    ChangeTracker.OriginalValues.Remove("HSProduct");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("HSProduct", previousValue);
                }
                if (HSProduct != null && !HSProduct.ChangeTracker.ChangeTrackingEnabled)
                {
                    HSProduct.StartTracking();
                }
            }
        }
    
        private void FixupHSProperty(HSProperty previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.HSRelations.Contains(this))
            {
                previousValue.HSRelations.Remove(this);
            }
    
            if (HSProperty != null)
            {
                if (!HSProperty.HSRelations.Contains(this))
                {
                    HSProperty.HSRelations.Add(this);
                }
    
                HSPID = HSProperty.HSPID;
            }
            else if (!skipKeys)
            {
                HSPID = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("HSProperty")
                    && (ChangeTracker.OriginalValues["HSProperty"] == HSProperty))
                {
                    ChangeTracker.OriginalValues.Remove("HSProperty");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("HSProperty", previousValue);
                }
                if (HSProperty != null && !HSProperty.ChangeTracker.ChangeTrackingEnabled)
                {
                    HSProperty.StartTracking();
                }
            }
        }

        #endregion
    }
}
