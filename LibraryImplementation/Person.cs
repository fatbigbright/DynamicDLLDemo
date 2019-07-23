using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILibrary;

namespace LibraryImplementation
{
    [Serializable]
    public class Person : IPersonControl
    {
        public void Print()
        {
            Console.WriteLine("Implementation of Person - new version.");
        }
    }
}
