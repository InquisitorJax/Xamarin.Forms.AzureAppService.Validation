using Validation.Client.XForms.Custom;
using Xamarin.Forms;

namespace Validation.Client.XForms
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            ViewModel = new MainPageViewModel();
            BindingContext = ViewModel;
        }

        private MainPageViewModel ViewModel { get; set; }
    }
}