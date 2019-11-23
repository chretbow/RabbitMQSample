using Autofac;
using RabbitMQSample2.Model;
using System.Reflection;

namespace RabbitMQSample2.Applibs
{
    internal static class AutofacConfig
    {
        private static IContainer container;

        public static IContainer Container
        {
            get
            {
                if (container == null)
                {
                    Register();
                }

                return container;
            }
        }

        public static void Register()
        {
            var builder = new ContainerBuilder();

            var asm = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(asm)
                .Where(t => t.IsAssignableTo<IRabbitMQEventStreamHandler>())
                .Named<IPubSubHandler<RabbitMQEventStream>>(t => t.Name.Replace("Handler", string.Empty))
                .SingleInstance();

            container = builder.Build();
        }
    }
}
