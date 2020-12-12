using Common;
using Common.Kafka;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        public static string topicName = "test-topic";
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            int i = 0;
            ConfluentConsumer consumer = new ConfluentConsumer();
            Task.Run(async () => await consumer.SubscribeAsync(topicName, (x) => Console.WriteLine("Received String " + x)));
            ConfluentProducer producer = new ConfluentProducer();
            while (true)
            {
                LogModel log = new LogModel { };
                log.Message = $"Welcome to Kafka {i++}";
                log.Timestamp = DateTime.UtcNow;

                await producer.PublishAsync(topicName, JsonConvert.SerializeObject(log));
                Thread.Sleep(1000);
            }
        }

    }
}
