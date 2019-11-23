namespace RabbitMQSample2.Applibs
{
    using RabbitMQSample2.Model;

    public interface IPubSubHandler<TEventStream> where TEventStream : EventStream
    {
        void Handle(TEventStream stream);
    }
}
