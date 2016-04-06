using Autofac;
using System.Collections.Generic;
using Validation.Shared;
using Xamarin.Forms;

namespace Validation.Client.XForms
{
    public class App : Application
    {
        public App()
        {
            Initialize();

            // The root page of your application
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        private void Initialize()
        {
            //IOC
            List<Module> modules = new List<Module>
            {
                new IocValidationModule()
            };

            Wibci.InitializeIoc(modules.ToArray());
        }
    }
}