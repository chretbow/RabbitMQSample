namespace RabbitMQSample.Applibs
{
    using Newtonsoft.Json;
    using RabbitMQ.Client;
    using RabbitMQSample.Model;
    using System;
    using System.Text;

    internal static class RabbitMQProducer
    {
        public static void Publish<T>(string topicName, T data)
        {
            var channel = RabbitMQService.GetChannel(topicName);
            var es = new RabbitMQEventStream(
                typeof(T).Name,
                JsonConvert.SerializeObject(data),
                TimeStampHelper.ToUtcTimeStamp(DateTime.Now));

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(es));
            var prop = channel.CreateBasicProperties();
            prop.Expiration = ConfigHelper.RmqExpiration;

            channel.BasicPublish(
                $"Exchange-{ExchangeType.Direct}-{topicName}",
                string.Empty,
                prop,
                body);
        }
    }
}
