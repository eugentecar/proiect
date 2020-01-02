using MassTransit;
using proiect_pssc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proiect_pssc.Services
{
    public class Receive
    {

        public void ReceiveMessage()
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
                    ep.Handler<Carte>(
                        context =>
                        {
                            return Console.Out.WriteLineAsync($"Received: {context.Message.Editura}");

                        });

                });


            });

            bus.Start();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            bus.Stop();


        }
    }
}