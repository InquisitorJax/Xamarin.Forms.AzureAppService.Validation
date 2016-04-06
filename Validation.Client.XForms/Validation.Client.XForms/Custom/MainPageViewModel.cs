using Prism.Commands;
using Prism.Mvvm;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Validation.Client.XForms.Custom
{
    public class MainPageViewModel : BindableBase
    {
        private TodoItem _model;

        public MainPageViewModel()
        {
            SaveCommand = DelegateCommand.FromAsyncHandler(SaveAsync);
        }

        public TodoItem Model
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }

        public ICommand SaveCommand { get; private set; }

        private Task SaveAsync()
        {
            return Task.FromResult(default(int));
        }
    }
}