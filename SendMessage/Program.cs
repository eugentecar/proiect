using ConsoleApp1;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMessage
{
    class Program
    {
        //de pe net
        //async System.Threading.Tasks.Task
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc => 
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                   {
                       h.Username("guest");
                       h.Password("guest");
                   }
                );

            });

            bus.Start();

            bus.Publish(new Model("sall",1));

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            bus.Stop();
        }
    }
}
