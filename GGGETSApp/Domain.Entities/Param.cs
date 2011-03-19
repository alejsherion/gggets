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
    [KnownType(typeof(Template))]
    [Serializable]
    [System.CodeDom.Compiler.GeneratedCode("STE-EF",".NET 4.0")]
    #if !SILVERLIGHT
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage()]
    #endif
    public partial class Param: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public System.Guid PID
        {
            get { return _pID; }
            set
            {
                if (_pID != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'PID' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _pID = value;
                    OnPropertyChanged("PID");
                }
            }
        }
        private System.Guid _pID;
    
        [DataMember]
        public Nullable<System.Guid> TID
        {
            get { return _tID; }
            set
            {
                if (_tID != value)
                {
                    ChangeTracker.RecordOriginalValue("TID", _tID);
                    if (!IsDeserializing)
                    {
                        if (Template != null && Template.TID != value)
                        {
                            Template = null;
                        }
                    }
                    _tID = value;
                    OnPropertyChanged("TID");
                }
            }
        }
        private Nullable<System.Guid> _tID;
    
        [DataMember]
        public int Tag
        {
            get { return _tag; }
            set
            {
                if (_tag != value)
                {
                    _tag = value;
                    OnPropertyChanged("Tag");
                }
            }
        }
        private int _tag;
    
        [DataMember]
        public string Key
        {
            get { return _key; }
            set
            {
                if (_key != value)
                {
                    _key = value;
                    OnPropertyChanged("Key");
                }
            }
        }
        private string _key;
    
        [DataMember]
        public string Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged("Value");
                }
            }
        }
        private string _value;
    
        [DataMember]
        public int Top
        {
            get { return _top; }
            set
            {
                if (_top != value)
                {
                    _top = value;
                    OnPropertyChanged("Top");
                }
            }
        }
        private int _top;
    
        [DataMember]
        public int Left
        {
            get { return _left; }
            set
            {
                if (_left != value)
                {
                    _left = value;
                    OnPropertyChanged("Left");
                }
            }
        }
        private int _left;
    
        [DataMember]
        public int Height
        {
            get { return _height; }
            set
            {
                if (_height != value)
                {
                    _height = value;
                    OnPropertyChanged("Height");
                }
            }
        }
        private int _height;
    
        [DataMember]
        public int Width
        {
            get { return _width; }
            set
            {
                if (_width != value)
                {
                    _width = value;
                    OnPropertyChanged("Width");
                }
            }
        }
        private int _width;
    
        [DataMember]
        public string FontName
        {
            get { return _fontName; }
            set
            {
                if (_fontName != value)
                {
                    _fontName = value;
                    OnPropertyChanged("FontName");
                }
            }
        }
        private string _fontName;
    
        [DataMember]
        public Nullable<int> FontSize
        {
            get { return _fontSize; }
            set
            {
                if (_fontSize != value)
                {
                    _fontSize = value;
                    OnPropertyChanged("FontSize");
                }
            }
        }
        private Nullable<int> _fontSize;
    
        [DataMember]
        public Nullable<int> Alignment
        {
            get { return _alignment; }
            set
            {
                if (_alignment != value)
                {
                    _alignment = value;
                    OnPropertyChanged("Alignment");
                }
            }
        }
        private Nullable<int> _alignment;
    
        [DataMember]
        public Nullable<int> Bold
        {
            get { return _bold; }
            set
            {
                if (_bold != value)
                {
                    _bold = value;
                    OnPropertyChanged("Bold");
                }
            }
        }
        private Nullable<int> _bold;
    
        [DataMember]
        public Nullable<int> Italic
        {
            get { return _italic; }
            set
            {
                if (_italic != value)
                {
                    _italic = value;
                    OnPropertyChanged("Italic");
                }
            }
        }
        private Nullable<int> _italic;
    
        [DataMember]
        public Nullable<int> Underline
        {
            get { return _underline; }
            set
            {
                if (_underline != value)
                {
                    _underline = value;
                    OnPropertyChanged("Underline");
                }
            }
        }
        private Nullable<int> _underline;
    
        [DataMember]
        public string ParamType
        {
            get { return _paramType; }
            set
            {
                if (_paramType != value)
                {
                    _paramType = value;
                    OnPropertyChanged("ParamType");
                }
            }
        }
        private string _paramType;
    
        [DataMember]
        public string DefaultValue
        {
            get { return _defaultValue; }
            set
            {
                if (_defaultValue != value)
                {
                    _defaultValue = value;
                    OnPropertyChanged("DefaultValue");
                }
            }
        }
        private string _defaultValue;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public Template Template
        {
            get { return _template; }
            set
            {
                if (!ReferenceEquals(_template, value))
                {
                    var previousValue = _template;
                    _template = value;
                    FixupTemplate(previousValue);
                    OnNavigationPropertyChanged("Template");
                }
            }
        }
        private Template _template;

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
            Template = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupTemplate(Template previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Params.Contains(this))
            {
                previousValue.Params.Remove(this);
            }
    
            if (Template != null)
            {
                if (!Template.Params.Contains(this))
                {
                    Template.Params.Add(this);
                }
    
                TID = Template.TID;
            }
            else if (!skipKeys)
            {
                TID = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Template")
                    && (ChangeTracker.OriginalValues["Template"] == Template))
                {
                    ChangeTracker.OriginalValues.Remove("Template");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Template", previousValue);
                }
                if (Template != null && !Template.ChangeTracker.ChangeTrackingEnabled)
                {
                    Template.StartTracking();
                }
            }
        }

        #endregion
    }
}
