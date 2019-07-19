
namespace RabbitMQSample.Applibs
{
    using Autofac;
    using RabbitMQSample.Model;
    using System;

    public class PubSubDispatcher<TEventStream> : IPubSubDispatcher<TEventStream> where TEventStream : EventStream
    {
        private IContainer container;

        public PubSubDispatcher(IContainer container)
        {
            this.container = container;
        }

        public bool DispatchMessage(TEventStream stream)
        {
            try
            {
                using (var scope = container.BeginLifetimeScope())
                {
                    var handler = scope.ResolveNamed<IPubSubHandler<TEventStream>>(stream.Type);
                    handler?.Handle(stream);
                    return true;
                }
            }
            catch (Autofac.Core.Registration.ComponentNotRegisteredException ex)
            {
                // Save Error Log
                Console.WriteLine($"DispatchMessage ComponentNotRegisteredException Exception:{ex.Message}");
            }
            catch (Exception ex)
            {
                // Save Error Log
                Console.WriteLine($"DispatchMessage Exception:{ex.Message}");
            }

            return false;
        }
    }
}
