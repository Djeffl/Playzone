using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture
{
    public class Mediator
    {
        public static void Execute()
        {
            var plane1 = new PlaneObject() { Name = "Plane One" };
            var plane2 = new PlaneObject() { Name = "Plane Two" };

            var controlerRoom = new ControlRoom();

            controlerRoom.Register(plane1);
            controlerRoom.Register(plane2);

            plane1.Mediator = controlerRoom;

            plane1.SendMessage("Hellow Plane2");
        }

        

        
    }
    abstract class Mediate
    {
        public virtual List<PlaneObject> Planes { get; protected set; }
        public abstract void SendMessage(string msg, PlaneObject fromPlane, PlaneObject toPlane);
        public abstract void Register(PlaneObject plane);
    }

    class ControlRoom : Mediate
    {
        public override List<PlaneObject> Planes { get;protected set; } = new List<PlaneObject>();

        public override void Register(PlaneObject plane)
        {
            Planes.Add(plane);
        }

        public override void SendMessage(string msg, PlaneObject fromPlane, PlaneObject toPlane)
        {
            if (Planes.Contains(toPlane))
            {
                Console.WriteLine($"Sending message from {fromPlane}");
                toPlane.Notify(msg);

            }
            else
            {
                Console.WriteLine("This plane does not exist");
            }
        }

    }

    class PlaneObject
    {
        public Mediate Mediator { get; set; }
        public string Name { get; set; }
        public void SendMessage(string message)
        {
            Mediator.SendMessage(message, this, Mediator.Planes.FirstOrDefault());
        }

        public void Notify(string message)
        {
            Console.WriteLine($"Plane '{Name}' gets message: {message}");
        }

    }
}
