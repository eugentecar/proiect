using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Model
    {
        public Model(string teststring, int testint)
        {
            TestString = teststring;
            TestInt = testint;
        }

        public string TestString { get; set; }
        public int TestInt { get; set; }
    }
}
