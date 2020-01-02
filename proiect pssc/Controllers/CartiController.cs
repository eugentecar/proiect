using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using proiect_pssc.Models;
using proiect_pssc.Services;
using System;
using MassTransit;

namespace proiect_pssc.Controllers
{
    public class CartiController : Controller
    {
        private BibliotecaContext db = new BibliotecaContext();
        private MessageBroker _messageBroker;
        private static object aa;
        public Receive variabila=new Services.Receive();
        

        public CartiController()
        {
          //  Receive();

           //voiam sa apelez o functie de initializare din interiorul constructorului astuia
           // fCartiController(messageBroker);
        }

        //private void fCartiController(MessageBroker messageBroker)
        //{
        //    _messageBroker = messageBroker;
        //}



        // GET: Carti
        public ActionResult Index()
        {
            return View(db.Carti.ToList());
        }



        // GET: Carti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
            Carte carte = db.Carti.Find(id);
            if (carte == null)
            {
                return HttpNotFound();
            }
            return View(carte);
        }

        // GET: Carti/Create
        public ActionResult Create()
        {
            return View();
        }


        void Receive()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                sbc.ReceiveEndpoint(host, "queue", ep =>
                {
                    ep.Handler<Carte>(
                        context =>
                        {
                            return Console.Out.WriteLineAsync($"Received: {context.Message.Editura}");

                        });

                });


            });

            bus.Start();

            

            bus.Stop();
        }


        void SendMessagee(Carte carte) {
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
           // bus.Publish("salot");
           // bus.Publish(new Carte(carte.CarteId, carte.Titlu, carte.Autor, carte.Editura, carte.AnAparitie));
            // bus.Publish(new Carte(1,"sal","eugenn","editura",2004));

            System.Diagnostics.Debug.WriteLine("Press any key to exit");

            bus.Publish("Ciaooo");


            bus.Stop();

        }


        // POST: Carti/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarteId,Titlu,Autor,Editura,AnAparitie")] Carte carte)
        {

            Receive();


            if (ModelState.IsValid)
            {
                db.Carti.Add(carte);
                db.SaveChanges();

                //operatia de publish
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
                // bus.Publish("salot");
                 bus.Publish(new Carte(carte.CarteId, carte.Titlu, carte.Autor, carte.Editura, carte.AnAparitie));
                // bus.Publish(new Carte(1,"sal","eugenn","editura",2004));

                System.Diagnostics.Debug.WriteLine("Press any key to exit");


                bus.Stop();



                return RedirectToAction("Index");
            }

            return View(carte);
        }

        // GET: Carti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carte carte = db.Carti.Find(id);
            if (carte == null)
            {
                return HttpNotFound();
            }
            return View(carte);
        }

        // POST: Carti/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarteId,Titlu,Autor,Editura,AnAparitie")] Carte carte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carte).State = EntityState.Modified;
                db.SaveChanges();

                Receive();

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
                // bus.Publish("salot");
                bus.Publish(new Carte(carte.CarteId, carte.Titlu, carte.Autor, carte.Editura, carte.AnAparitie));
                // bus.Publish(new Carte(1,"sal","eugenn","editura",2004));

                System.Diagnostics.Debug.WriteLine("Press any key to exit");


                bus.Stop();



                return RedirectToAction("Index");
            }
            return View(carte);
        }

        // GET: Carti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carte carte = db.Carti.Find(id);
            if (carte == null)
            {
                return HttpNotFound();
            }
            return View(carte);
        }

        // POST: Carti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carte carte = db.Carti.Find(id);
            db.Carti.Remove(carte);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
