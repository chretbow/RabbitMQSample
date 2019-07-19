using RabbitMQ.Client;
using System;
using System.Collections.Generic;

namespace RabbitMQSample.Applibs
{
    internal static class RabbitMQService
    {
        private static ConnectionFactory factory;

        private static IConnection connection;

        private static Dictionary<string, IModel> models = new Dictionary<string, IModel>();

        private static bool TryAddModel(string topicName)
        {
            if (!models.ContainsKey(topicName))
            {
                var chennel = connection.CreateModel();
                chennel.ExchangeDeclare($"Exchange-{ExchangeType.Direct}-{topicName}", ExchangeType.Direct);
                models.Add(topicName, chennel);

                return true;
            }

            return false;
        }

        public static IModel GetChannel(string topicName)
        {
            TryAddModel(topicName);
            return models[topicName];
        }

        public static void Start(string hostUri)
        {
            factory = new ConnectionFactory()
            {
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(5)
            };

            connection = factory.CreateConnection(AmqpTcpEndpoint.ParseMultiple(ConfigHelper.RabbitMQURI));
        }

        public static void Stop()
        {
            foreach (var model in models)
            {
                model.Value.Abort();
                model.Value.Close();
            }

            models = new Dictionary<string, IModel>();
            connection.Abort();
            connection.Close();
            factory = null;
        }
    }
}
