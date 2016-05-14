using Autofac;
using Microsoft.WindowsAzure.MobileServices;

namespace Validation.Client.XForms.Droid
{
    public class IocPlatformModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register<IMobileServiceClient>(ctx =>
            {
                string localAddress = "http://10.0.0.10/Validation.Server";
                var mobileService = new MobileServiceClient(localAddress);
                return mobileService;
            });
        }
    }
}