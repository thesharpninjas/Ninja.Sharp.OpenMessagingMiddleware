﻿using Confluent.Kafka;
using Ninja.Sharp.OpenMessagingMiddleware.Extensions;
using Ninja.Sharp.OpenMessagingMiddleware.Interfaces;

namespace Ninja.Sharp.OpenMessagingMiddleware.Providers.Kafka
{
    public class KafkaProducer(IProducer<string, string> producer, string name) : MessageProducer
    {
        private readonly IProducer<string, string> producer = producer;

        public override string Name => name;

        public override async Task SendAsync(string topic, string message)
        {
            //TODO aggiungere il caa al message id, come per artemis
            string msgId = Guid.NewGuid().ToString();
            await producer.ProduceAsync(topic, new Message<string, string>
            {
                Key = msgId,
                Value = message
            });
        }
    }
}
