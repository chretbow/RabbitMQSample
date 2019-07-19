namespace RabbitMQSample.Model
{
    using RabbitMQSample.Applibs;
    public interface IRabbitMQEventStreamHandler : IPubSubHandler<RabbitMQEventStream>
    {
    }
}
