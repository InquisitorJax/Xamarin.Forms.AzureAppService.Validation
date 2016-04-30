using Autofac;
using FluentValidation;
using System;
using System.ComponentModel;
using System.Linq;
using Validation.Shared;
using Xamarin.Forms;

namespace Validation.Client.XForms.Custom
{
    /*
      *	<Entry.Behaviors>
             <behaviour:EntryValidationBehaviour ValidationPropertyName="GivenName" ValidationTypeName="Identity"/>
         </Entry.Behaviors>
      */

    public class EntryValidationBehaviour : Behavior<Entry>
    {
        //see: https://blog.xamarin.com/behaviors-in-xamarin-forms/

        #region These First

        private static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EntryValidationBehaviour), true);

        private static readonly BindablePropertyKey ValidationMessagePropertyKey = BindableProperty.CreateReadOnly("ValidationMessage", typeof(string), typeof(EntryValidationBehaviour), null);

        #endregion These First

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;
        public static readonly BindableProperty ValidationMessageProperty = ValidationMessagePropertyKey.BindableProperty;
        public static readonly BindableProperty ValidationPropertyNameProperty = BindableProperty.Create("ValidationPropertyName", typeof(string), typeof(EntryValidationBehaviour), null);
        public static readonly BindableProperty ValidationTypeNameProperty = BindableProperty.Create("ValidationTypeName", typeof(string), typeof(EntryValidationBehaviour), null, BindingMode.OneWay, null, OnValidationTypeNameChanged);

        //TODO: Complete if xamarin exposes Binding information on UI Elements

        private Entry _entry;
        private INotifyPropertyChanged _entryContext;
        private IValidator _validator;

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }

        public string ValidationMessage
        {
            get { return (string)base.GetValue(ValidationMessageProperty); }
            private set { base.SetValue(ValidationMessagePropertyKey, value); }
        }

        public string ValidationPropertyName
        {
            get { return (string)GetValue(ValidationPropertyNameProperty); }
            set { SetValue(ValidationPropertyNameProperty, value); }
        }

        public string ValidationTypeName
        {
            get { return (string)GetValue(ValidationTypeNameProperty); }
            set { SetValue(ValidationTypeNameProperty, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            _entry = bindable;
            SubscribeToBindingContextPropertyChange();
            if (_entry != null)
            {
                _entry.BindingContextChanged += Entry_BindingContextChanged;
                // _entry.TextChanged += Entry_TextChanged;
            }
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            if (_entry != null)
            {
                _entry.BindingContextChanged -= Entry_BindingContextChanged;
                UnsubscribeFromBindingContextPropertyChange();
                //_entry.TextChanged -= Entry_TextChanged;
            }
        }

        private static void OnValidationTypeNameChanged(BindableObject instance, object oldValue, object newValue)
        {
            var behavior = (EntryValidationBehaviour)instance;
            string typeName = (string)newValue;
            if (!string.IsNullOrEmpty(typeName))
            {
                object validationInstance;
                Wibci.IoC.TryResolveNamed(typeName, typeof(IValidator), out validationInstance);
                behavior._validator = (IValidator)validationInstance;
            }
        }

        private void Entry_BindingContextChanged(object sender, EventArgs e)
        {
            SubscribeToBindingContextPropertyChange();
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Validate();
        }

        private void NotifyPropertyChange_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ValidationPropertyName)
            {
                Validate();
            }
        }

        private void SubscribeToBindingContextPropertyChange()
        {
            UnsubscribeFromBindingContextPropertyChange();

            _entryContext = _entry.BindingContext as INotifyPropertyChanged;
            if (_entryContext != null)
            {
                _entryContext.PropertyChanged += NotifyPropertyChange_PropertyChanged;
            }
        }

        private void UnsubscribeFromBindingContextPropertyChange()
        {
            if (_entryContext != null)
            {
                _entryContext.PropertyChanged -= NotifyPropertyChange_PropertyChanged;
            }
        }

        private void Validate()
        {
            if (_validator != null && _entry.BindingContext != null)
            {
                var validationResult = _validator.Validate(_entry.BindingContext);

                bool isValid = true;
                string validationMessage = "";
                if (!validationResult.IsValid)
                {
                    var propertyError = validationResult.Errors.FirstOrDefault(x => x.PropertyName == ValidationPropertyName);
                    isValid = propertyError == null;
                    if (!isValid)
                    {
                        validationMessage = propertyError.ErrorMessage;
                    }
                }

                IsValid = isValid;
                ValidationMessage = IsValid ? null : validationMessage;
            }
        }
    }
}