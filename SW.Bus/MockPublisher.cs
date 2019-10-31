﻿using Microsoft.Extensions.Logging;
using SW.PrimitiveTypes;
using System.Threading.Tasks;

namespace SW.Bus
{
    public class MockPublisher : IPublish
    {
        private readonly ILogger<MockPublisher> logger;

        public MockPublisher(ILogger<MockPublisher> logger)
        {
            this.logger = logger;
        }

        public Task Publish(string messageTypeName, string message)
        {
            logger.LogInformation("mock published...");
            return Task.CompletedTask;  
        }

        public Task Publish(string messageTypeName, byte[] message)
        {
            logger.LogInformation("mock published...");
            return Task.CompletedTask;
        }

        public Task Publish<TMessage>(TMessage message)
        {
            logger.LogInformation("mock published...");
            return Task.CompletedTask;
        }
    }
}
