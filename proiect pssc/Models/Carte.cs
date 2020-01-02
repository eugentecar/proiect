using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace proiect_pssc.Models
{
    public class Carte
    {
        //public Model(string teststring, int testint)
        //{
        //    TestString = teststring;
        //    TestInt = testint;
        //}

        //public string TestString { get; set; }
        //public int TestInt { get; set; }

            public Carte()
        {

        }

        public Carte(int carteid, string titlu,string autor, string editura,int anaparitie)
        {

            CarteId = carteid;
            Titlu = titlu;
            Autor = autor;
            Editura = editura;
            AnAparitie = anaparitie;
        }



        public int CarteId { get; set; }
        public string Titlu { get; set; }
        public string Autor { get; set; }
        public string Editura { get; set; }
        [Range(1900,2016),Display(Name="Anul aparitiei")]
        public int AnAparitie { get; set; }
    }
}