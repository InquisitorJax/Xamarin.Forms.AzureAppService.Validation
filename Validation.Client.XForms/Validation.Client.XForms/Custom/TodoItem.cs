using Prism.Mvvm;
using Validation.Shared;

namespace Validation.Client.XForms.Custom
{
    public class TodoItem : BindableBase, ITodoItem
    {
        private bool _complete;

        private string _email;

        private string _text;

        public bool Complete
        {
            get { return _complete; }
            set { SetProperty(ref _complete, value); }
        }

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }
    }
}