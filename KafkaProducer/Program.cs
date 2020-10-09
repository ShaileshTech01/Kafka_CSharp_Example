using System;
using System.Threading.Tasks;
using Confluent.Kafka;


namespace KafkaProducer
{
    class Program
    {
        public static  async Task Main(string[] args)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            //Action<DeliveryReport<Null, string>> handler = r => 
            //Console.WriteLine(!r.Error.IsError
            //    ? $"Delivered message to {r.TopicPartitionOffset}"
            //    : $"Delivery Error: {r.Error.Reason}");
            //using (var p = new ProducerBuilder<Null, string>(conf).Build())
            //{
            //    for (int i = 0; i < 100; ++i)
            //    {
            //        p.Produce("my-topic", new Message<Null, string> { Value = i.ToString() }, handler);
            //    }

            //    // wait for up to 10 seconds for any inflight messages to be delivered.
            //    p.Flush(TimeSpan.FromSeconds(10));
            //}

            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var dr = await p.ProduceAsync("TestTopic", new Message<Null, string> { Value = "Test Message from service1" });
                    Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
                }
                catch(ProduceException<Null, string> e) {
                    Console.WriteLine($"Delivered failed:'{ e.Error.Reason }'");
                }
            }
            Console.WriteLine("Hello World!");

        }
    }
}
