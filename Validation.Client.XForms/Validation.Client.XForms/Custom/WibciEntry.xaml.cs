using Xamarin.Forms;

namespace Validation.Client.XForms.Custom
{
    public partial class WibciEntry : ContentView
    {
        public static readonly BindableProperty EntryTextProperty = BindableProperty.Create("EntryText", typeof(string), typeof(WibciEntry), null, BindingMode.TwoWay);

        public static readonly BindableProperty LabelTextProperty = BindableProperty.Create("LabelText", typeof(string), typeof(WibciEntry), null);

        public static readonly BindableProperty ValidationPropertyNameProperty = BindableProperty.Create("ValidationPropertyName", typeof(string), typeof(WibciEntry), null);

        public static readonly BindableProperty ValidationTypeNameProperty = BindableProperty.Create("ValidationTypeName", typeof(string), typeof(WibciEntry), null);

        public WibciEntry()
        {
            InitializeComponent();
        }

        public string EntryText
        {
            get { return (string)GetValue(EntryTextProperty); }
            set { SetValue(EntryTextProperty, value); }
        }

        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
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
    }
}