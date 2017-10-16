using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture
{
    interface IOrder
    {

    }
    public class Decorator
    {
        public static void Execute()
        {
            ///////////////////////////////////////////////////////
            var coffee = new Drink() { Price = 4 };

            var cream = new Cream(coffee);

            var cream1 = new Cream(cream);

            Console.WriteLine(cream1.Price);

            /////////////////////////////////////////////
            var log = new Logger();
            var logWrapOne = new LogWrapper(log);
            var logWrapTwo = new LogWrapper(logWrapOne);

            logWrapTwo.Log("Just a test");
            ///////////////////////////////////////////////
            var inputStream = new BufferReader(new FileReader("xanadu.txt"));
            
        }

        abstract class Bevarage
        {
            public string Name { get; }
            public virtual double Price { get; set; }
        }

        class Drink : Bevarage
        {

        }


        class Topping : Bevarage
        {
            public override double Price {
                get => base.Price + _bevarage.Price;
                set => base.Price = value; }

            private readonly Bevarage _bevarage;

            public Topping(Bevarage bevarage)
            {
                _bevarage = bevarage;
            }
        }

        class Cream : Topping
        {
            public Cream(Bevarage bevarage) : base(bevarage)
            {
                this.Price = 2;
            }
        }

        interface ILogger
        {
            void Log(string log);
        }

        class LogWrapper: ILogger
        {
            private readonly ILogger _logger;

            public LogWrapper(ILogger logger)
            {
                _logger = logger;
            }

            public void Log(string log)
            {
                Console.WriteLine("Starting");
                _logger.Log($"{nameof(Logger)} { log}");
                Console.WriteLine("Ending");
            }
        }

        class Logger: ILogger
        {
            public void Log(string log)
            {
                Console.WriteLine($"{nameof(Logger)} { log}");
            }
        }


        interface IStream
        {
            void Stream();
        }

        class InputStream : IStream
        {
            public void Stream()
            {
                throw new NotImplementedException();
            }
        }

        class OutputStream : IStream { 
            public void Stream()
            {
                throw new NotImplementedException();
            }
        }

        interface IReader
        {
            void Read();
        }

        class FileReader: IReader
        {
            private readonly string _location;

            public FileReader(string location)
            {
                _location = location;
            }

            public void Read()
            {
                Console.WriteLine($"Reading location: {_location}");
            }
        }

        class BufferReader: IReader
        {
            private readonly IReader _reader;

            public BufferReader(IReader reader)
            {
                _reader = reader;
                Read();
                _reader.Read();
                
            }

            public void Read()
            {
                Console.WriteLine("Start reading");
                //Make buffer space
                //Load reader
                //pass a part of the loaded buffer
            }
        }
           
    }
}
