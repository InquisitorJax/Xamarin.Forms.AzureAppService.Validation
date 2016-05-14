using Prism.Mvvm;
using Validation.Shared;

namespace Validation.Client.XForms.Custom
{
    public abstract class ModelBase : BindableBase, IModel
    {
        private string _id;

        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
    }
}