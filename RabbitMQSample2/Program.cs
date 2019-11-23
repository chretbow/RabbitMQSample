namespace RabbitMQSample2
{
    using RabbitMQSample2.Applibs;
    using RabbitMQSample2.Model;
    using System;
    class Program
    {
        static void Main(string[] args)
        {
            RabbitMQService.Start(ConfigHelper.RabbitMQURI);

            var consumer = new RabbitMQConsumer(
                ConfigHelper.SubQueueNames,
                new PubSubDispatcher<RabbitMQEventStream>(AutofacConfig.Container),
                ConfigHelper.QueueId);
            consumer.Start();

            Console.Read();

            RabbitMQService.Stop();
        }
    }
}
