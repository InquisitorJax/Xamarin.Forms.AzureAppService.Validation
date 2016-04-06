using Autofac;

namespace Validation.Shared
{
    public static class Wibci
    {
        private static IComponentContext _container;

        static Wibci()
        {
        }

        public static IComponentContext IoC
        {
            get { return _container; }
        }

        public static void InitializeIoc(Module[] modules)
        {
            var builder = new ContainerBuilder();
            foreach (var module in modules)
            {
                builder.RegisterModule(module);
            }
            _container = builder.Build();
        }
    }
}