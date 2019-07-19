
namespace RabbitMQSample.Applibs
{
    using Newtonsoft.Json;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using RabbitMQSample.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class RabbitMQConsumer
    {
        private IPubSubDispatcher<RabbitMQEventStream> dispatcher;

        private IEnumerable<string> queueNames;

        private string queueId;

        public RabbitMQConsumer(IEnumerable<string> queueNames, IPubSubDispatcher<RabbitMQEventStream> dispatcher, string queueId)
        {
            this.queueNames = queueNames;
            this.dispatcher = dispatcher;
            this.queueId = queueId;
        }

        public void Start()
        {
            this.queueNames.ToList().ForEach(topicName =>
            {
                var channel = RabbitMQService.GetChannel(topicName);
                //// generate queue
                var queueName = channel.QueueDeclare($"{this.queueId}-{topicName}", false, false, false, null).QueueName;

                //// bind queue to exchange
                channel.QueueBind(queueName, $"Exchange-{ExchangeType.Direct}-{topicName}", string.Empty, null);
                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var @event = JsonConvert.DeserializeObject<RabbitMQEventStream>(Encoding.UTF8.GetString(ea.Body));
                    if (this.dispatcher.DispatchMessage(@event))
                    {
                        channel.BasicAck(ea.DeliveryTag, true);
                    }
                };

                var consumerTag = channel.BasicConsume(queueName, false, $"{Environment.MachineName}", false, false, null, consumer);
            });
        }
    }
}
