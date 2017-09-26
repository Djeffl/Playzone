using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEquatable
{
    class Program
    {
        private const int size = 100;
        static void Main(string[] args)
        {

            var list = new List<Person>();
            var smallerList = new List<Person>();

            //fill list 
            for (int i = 0; i < size; i++)
            {
                list.Add(new Person(i, "", ""));

                if( i % 2 == 0)
                {
                    smallerList.Add(new Person(i, "", ""));

                }
            }

            // To let the Except method work on your list with a custom object
            // You have to override equal properly or implement it by inherriting IEquatable
            var result = list.Except(smallerList);

            foreach (var person in result)
            {
                Console.WriteLine(person);
            }

            Console.ReadLine();
        }
    }
}
