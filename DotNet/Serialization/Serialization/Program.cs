using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            var persons = new List<Person>();
            

            for (int i = 0; i < 100; i++)
            {
                var firstName = WordGenerator(1, 6)[0];
                var lastName = WordGenerator(1, 12)[0];

                persons.Add(new Person(i, firstName, lastName));
            }

            Stream stream = File.Open("test.xml", FileMode.Create);
            var formatter = new BinaryFormatter();

            formatter.Serialize(stream, persons);
            stream.Close();

            //Empties obj.
            stream.Close();

            //Empties obj.
            persons = null;

            //Opens file "test.xml" and deserializes the object from it.
            stream = File.Open("test.xml", FileMode.Open);
            formatter = new BinaryFormatter();

            //formatter = new BinaryFormatter();

            persons = (List<Person>)formatter.Deserialize(stream);
            stream.Close();

            foreach (var person in persons)
            {
                person.Print();
            }

            Console.ReadLine();
        }

        static List<string> WordGenerator(int num_words, int num_letters)
        {
            var result = new List<string>();
            // Make an array of the letters we will use.
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            // Make a random number generator.
            Random rand = new Random();

            // Make the words.
            for (int i = 1; i <= num_words; i++)
            {
                // Make a word.
                string word = "";
                for (int j = 1; j <= num_letters; j++)
                {
                    // Pick a random number between 0 and 25
                    // to select a letter from the letters array.
                    int letter_num = rand.Next(0, letters.Length - 1);

                    // Append the letter.
                    word += letters[letter_num];
                }

                // Add the word to the list.
                result.Add(word);

            }
            return result;
        }
    }
}
