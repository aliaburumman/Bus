﻿using System;
using System.Reflection;

namespace SW.Bus
{
    public class ConsumerDefiniton
    {
        public Type ServiceType { get; set; }
        public Type MessageType { get; set; }
        public string MessageTypeName { get; set; }
        public MethodInfo Method { get; set; }
        public string QueueName { get; set; }
        public string RetryQueueName { get; set; }
        public string NakedQueueName { get; set; }
        public string RetryRoutingKey { get; set; }
        public int RetryCount { get; set; }
        public int RetryAfter { get; set; }
    }

}

