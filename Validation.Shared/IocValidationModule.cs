using Autofac;
using FluentValidation;

namespace Validation.Shared
{
    public class IocValidationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<TodoItemValidator>().Named<IValidator>(nameof(ITodoItem));
        }
    }
}