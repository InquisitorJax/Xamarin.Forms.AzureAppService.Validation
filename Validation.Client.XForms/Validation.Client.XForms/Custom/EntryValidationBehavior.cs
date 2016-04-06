using Autofac;
using FluentValidation;
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

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;
        public static readonly BindableProperty ValidationMessageProperty = ValidationMessagePropertyKey.BindableProperty;
        public static readonly BindableProperty ValidationPropertyNameProperty = BindableProperty.Create("ValidationPropertyName", typeof(string), typeof(EntryValidationBehaviour), null);
        public static readonly BindableProperty ValidationTypeNameProperty = BindableProperty.Create("ValidationTypeName", typeof(string), typeof(EntryValidationBehaviour), null, BindingMode.OneWay, null, OnValidationTypeNameChanged);

        //TODO: Complete if xamarin exposes Binding information on UI Elements
        private static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EntryValidationBehaviour), true);

        private static readonly BindablePropertyKey ValidationMessagePropertyKey = BindableProperty.CreateReadOnly("ValidationMessage", typeof(string), typeof(EntryValidationBehaviour), null);
        private Entry _entry;
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

        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            _entry = bindable;
            if (_entry != null)
            {
                _entry.TextChanged += Entry_TextChanged;
            }
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            if (_entry != null)
            {
                _entry.TextChanged -= Entry_TextChanged;
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
                behavior._validator = (IValidator)instance;
            }
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_validator != null)
            {
                var validationResult = _validator.Validate(_entry.BindingContext);

                bool isValid = true;
                string validationMessage = "";
                if (!validationResult.IsValid)
                {
                    var propertyError = validationResult.Errors.FirstOrDefault(x => x.PropertyName == ValidationPropertyName);
                    isValid = propertyError == null;
                    validationMessage = propertyError.ErrorMessage;
                }

                IsValid = isValid;
                ValidationMessage = IsValid ? null : validationMessage;
            }
        }
    }
}