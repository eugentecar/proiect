using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using MassTransit;
using Newtonsoft.Json;
using proiect_pssc.Models;

namespace ConsoleApp1
{
    class Program
    {
        //asta ii clasa de Receive
        static void Main()
        {
            //de pe net

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
              {
                  var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                     {
                         h.Username("guest");
                         h.Password("guest");
                     });

                  sbc.ReceiveEndpoint(host, "new_queue", ep =>
                    {
                        ep.Handler<Model>(
                            context =>
                            {
                                return Console.Out.WriteLineAsync($"Received: {context.Message.TestString}");

                            });

                    });


              });

            bus.Start();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            bus.Stop();
















            //de la prodaniuc andrei

            //    var factory = new ConnectionFactory() { Uri = new Uri("rabbitmq://localhost"), };
            //    using (var connection = factory.CreateConnection())
            //    using (var channel = connection.CreateModel())

            //    {
            //        channel.QueueDeclare(queue: "students",
            //                             durable: false,
            //                             exclusive: false,
            //                             autoDelete: false,
            //                             arguments: null);

            //        var consumer = new EventingBasicConsumer(channel);

            //        consumer.Received += (model, ea) =>
            //        {
            //            var body = ea.Body;
            //            var message = Encoding.UTF8.GetString(body);
            //            Console.WriteLine(" [x] Received {0}", message);
            //        };

            //        channel.BasicConsume(queue: "students",
            //                             autoAck: true,
            //                             consumer: consumer);

            //        Console.WriteLine(" Press [enter] to exit.");
            //        Console.ReadLine();


            //    }
            //
        }

    }
}
