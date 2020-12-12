using Common.Kafka;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class AppLogger:IAppLogger
    {
        ConfluentProducer producer = new ConfluentProducer();

        public async Task Debug(string message)
        {
            LogModel log = new LogModel { };
            log.Level = 0;
            log.Message = message;
            log.Timestamp = DateTime.UtcNow;

            await producer.PublishAsync("test-topic", JsonConvert.SerializeObject(log));
        }
    }
}
