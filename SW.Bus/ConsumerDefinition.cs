﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace SW.Bus
{
    public class ConsumerDefinition
    {
        private readonly string queueNamePrefix;
        private readonly BusOptions busOptions;
        private readonly QueueOptions queueOptions;

        public ConsumerDefinition(string queueNamePrefix, BusOptions busOptions, string nakedQueueName)
        {
            this.queueNamePrefix = queueNamePrefix;
            this.busOptions = busOptions;
            NakedQueueName = nakedQueueName;
            busOptions.Options.TryGetValue(NakedQueueName, out queueOptions);
            
        }

        public Type ServiceType { get; set; }
        public Type MessageType { get; set; }
        public string MessageTypeName { get; set; }
        public MethodInfo Method { get; set; }
        public int RetryCount => queueOptions?.RetryCount ?? busOptions.DefaultRetryCount;
        public uint RetryAfter => queueOptions?.RetryAfterSeconds ?? busOptions.DefaultRetryAfter;
        public ushort QueuePrefetch => queueOptions?.Prefetch ?? busOptions.DefaultQueuePrefetch;
        public string NakedQueueName { get; private set; }
        public string QueueName => $"{queueNamePrefix}.{NakedQueueName}".ToLower();
        public string RoutingKey => MessageTypeName.ToLower();
        public string RetryRoutingKey => $"{NakedQueueName}.retry".ToLower();
        public string RetryQueueName => $"{queueNamePrefix}.{NakedQueueName}.retry".ToLower();
        public string BadRoutingKey => $"{NakedQueueName}.bad".ToLower();
        public string BadQueueName => $"{queueNamePrefix}.{NakedQueueName}.bad".ToLower();
        public IDictionary<string, object> RetryArgs => RetryCount == 0 ? null : new Dictionary<string, object>
        {
            { "x-dead-letter-exchange", busOptions.ProcessExchange},
            { "x-dead-letter-routing-key", RetryRoutingKey},
            { "x-message-ttl", RetryAfter == 0 ? 100 : RetryAfter * 1000 }
        };

        public IDictionary<string, object> ProcessArgs => new Dictionary<string, object>
        {
            { "x-dead-letter-exchange", busOptions.DeadLetterExchange },
            { "x-dead-letter-routing-key", RetryRoutingKey },
            
        };

        public static IDictionary<string, object> BadArgs => new Dictionary<string, object>
        {
            { "x-message-ttl", (uint)TimeSpan.FromDays(7).TotalMilliseconds }
        };
    }

}

