using Newtonsoft.Json;
using System.Collections.Generic;
using Validation.Shared;

namespace Validation.Client.XForms.Custom
{
    public class TodoItem : ModelBase, ITodoItem
    {
        private bool _complete;

        private string _email;

        private string _message;
        private string _text;

        public TodoItem()
        {
            Complete = true;
        }

        [JsonProperty(PropertyName = "complete")]
        public bool Complete
        {
            get { return _complete; }
            set { SetProperty(ref _complete, value); }
        }

        [JsonProperty(PropertyName = "email")]
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        [JsonIgnore]
        public string Message
        {
            //Client specific property
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        [JsonProperty(PropertyName = "text")]
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        public void RequestValidation(IList<string> propertyNames)
        {
            foreach (var property in propertyNames)
            {
                OnPropertyChanged(property);
            }
        }
    }
}