using Autofac;
using Microsoft.Owin;
using Owin;
using System.Collections.Generic;
using Validation.Server.Custom;
using Validation.Shared;

[assembly: OwinStartup(typeof(Validation.Server.Startup))]

namespace Validation.Server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
            CustomConfiguration();
        }

        private void CustomConfiguration()
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